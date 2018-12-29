using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using Android.Text.Method;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using LeoJHarris.FormsPlugin.Abstractions;
using LeoJHarris.FormsPlugin.Droid;
using System;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EnhancedEntry), typeof(EnhancedEntryRenderer))]
namespace LeoJHarris.FormsPlugin.Droid
{
    /// <summary>
    /// EnhancedEntryRenderer
    /// </summary>
    public class EnhancedEntryRenderer : EntryRenderer
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly Context _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnhancedEntryRenderer"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EnhancedEntryRenderer(Context context) : base(context)
        {
            AutoPackage = false;
            _context = context;
        }

        /// <summary>
        /// Gets or sets the name of the package.
        /// </summary>
        /// <value>
        /// The name of the package.
        /// </value>
        private static string PackageName
        {
            get;
            set;
        }

        /// <summary>
        /// The gradient drawable
        /// </summary>
        private GradientDrawable _gradientDrawable;

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public static void Init(Context context) { PackageName = context.PackageName; }

        /// <summary>
        /// Raises the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ElementChangedEventArgs{Entry}"/> instance containing the event data.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (!((Control != null) & (e.NewElement != null))) return;

            if (!(e.NewElement is EnhancedEntry entryExt)) return;
            {
                Control.ImeOptions = getValueFromDescription(entryExt.ReturnKeyType);

                Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), Control.ImeOptions);

                _gradientDrawable = new GradientDrawable();
                _gradientDrawable.SetShape(ShapeType.Rectangle);
                _gradientDrawable.SetColor(entryExt.BackgroundColor.ToAndroid());
                _gradientDrawable.SetCornerRadius(entryExt.CornerRadius);
                _gradientDrawable.SetStroke((int)entryExt.BorderWidth, entryExt.BorderColor.ToAndroid());

                Rect padding = new Rect
                {
                    Left = entryExt.LeftPadding,
                    Right = entryExt.RightPadding,
                    Top = entryExt.TopBottomPadding / 2,
                    Bottom = entryExt.TopBottomPadding / 2
                };
                _gradientDrawable.GetPadding(padding);

                e.NewElement.Focused += (sender, evt) =>
                {
                    _gradientDrawable.SetStroke(
                        (int)entryExt.BorderWidth,
                        entryExt.FocusBorderColor.ToAndroid());
                };

                e.NewElement.Unfocused += (sender, evt) =>
                {
                    _gradientDrawable.SetStroke((int)entryExt.BorderWidth, entryExt.BorderColor.ToAndroid());
                };

                if (entryExt.EntryHeight != -1)
                    Control.SetHeight((int)DpToPixels(_context, entryExt.EntryHeight));

                Control.SetBackground(_gradientDrawable);

                if (Control != null && !string.IsNullOrEmpty(PackageName) && !string.IsNullOrEmpty(entryExt.LeftIcon))
                {
                    int identifier = Context.Resources.GetIdentifier(
                        entryExt.LeftIcon,
                        "drawable",
                        PackageName);
                    if (identifier != 0)
                    {
                        Drawable drawable = ContextCompat.GetDrawable(_context, identifier);
                        if (drawable != null)
                        {
                            Control.SetCompoundDrawablesWithIntrinsicBounds(drawable, null, null, null);
                            Control.CompoundDrawablePadding = entryExt.PaddingLeftIcon;

                            entryExt.IconDrawableColorChanged += (sender, args) =>
                            {
                                foreach (Drawable d in Control.GetCompoundDrawables())
                                {
                                    if (args.IsValid)
                                    {
                                        d?.SetColorFilter(new PorterDuffColorFilter(args.Color.ToAndroid(), PorterDuff.Mode.SrcIn));
                                    }
                                    else
                                    {
                                        d?.ClearColorFilter();
                                    }
                                }
                            };
                        }
                    }
                }

                Control.EditorAction += (sender, args) =>
                {
                    if (entryExt.NextEntry == null)
                    {
                        if (_context.GetSystemService(Context.InputMethodService) is InputMethodManager inputMethodManager && _context is Activity)
                        {
                            Activity activity = (Activity)_context;
                            IBinder token = activity.CurrentFocus?.WindowToken;
                            inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                            activity.Window.DecorView.ClearFocus();
                        }
                    }

                    entryExt.EntryActionFired();
                };
            }
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }

        /// <summary>
        /// OnDrawableTouchListener
        /// </summary>
        /// <seealso cref="Java.Lang.Object" />
        /// <seealso cref="Android.Views.View.IOnTouchListener" />
        public class OnDrawableTouchListener : Java.Lang.Object, IOnTouchListener
        {
            public bool OnTouch(Android.Views.View v, MotionEvent e)
            {
                if (v is EditText editText && e.Action == MotionEventActions.Up)
                {
                    if (e.RawX >= (editText.Right - editText.GetCompoundDrawables()[2].Bounds.Width()))
                    {
                        editText.TransformationMethod = editText.TransformationMethod == null ? PasswordTransformationMethod.Instance : null;

                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// The on element property changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName != EnhancedEntry.ReturnKeyPropertyName) return;
            if (!(sender is EnhancedEntry entryExt)) return;
            Control.ImeOptions = getValueFromDescription(entryExt.ReturnKeyType);
            Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), Control.ImeOptions);
        }

        /// <summary>
        /// Gets the value from description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        private static ImeAction getValueFromDescription(ReturnKeyTypes value)
        {
            Type type = typeof(ImeAction);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (FieldInfo field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == value.ToString()) return (ImeAction)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value.ToString()) return (ImeAction)field.GetValue(null);
                }
            }

            throw new NotSupportedException($"Not supported on Android: {value}");
        }
    }
}
