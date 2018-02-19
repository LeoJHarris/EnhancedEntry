using System;
using System.ComponentModel;
using System.Reflection;

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text.Method;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using LeoJHarris.EnhancedEntry.Plugin.Abstractions;
using LeoJHarris.EnhancedEntry.Plugin.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EnhancedEntry), typeof(EnhancedEntryRenderer))]
namespace LeoJHarris.EnhancedEntry.Plugin.Droid
{
    using Android.App;
    using Android.OS;

    using EnhancedEntry = Abstractions.EnhancedEntry;

    /// <summary>
    /// 
    /// </summary>
    public class EnhancedEntryRenderer : EntryRenderer
    {
        private readonly Context _context;

        public EnhancedEntryRenderer(Context context) : base(context)
        {
            this.AutoPackage = false;
            this._context = context;
        }

        private static string PackageName
        {
            get;
            set;
        }

        private GradientDrawable _gradietDrawable;


        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public static void Init(Context context) { PackageName = context.PackageName; }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (!((this.Control != null) & (e.NewElement != null))) return;

            if (!(e.NewElement is EnhancedEntry entryExt)) return;
            {
                this.Control.ImeOptions = GetValueFromDescription(entryExt.ReturnKeyType);

                this.Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), this.Control.ImeOptions);

                this._gradietDrawable = new GradientDrawable();
                this._gradietDrawable.SetShape(ShapeType.Rectangle);
                this._gradietDrawable.SetColor(entryExt.BackgroundColor.ToAndroid());
                this._gradietDrawable.SetCornerRadius(entryExt.CornerRadius);
                this._gradietDrawable.SetStroke((int)entryExt.BorderWidth, entryExt.BorderColor.ToAndroid());


                Rect padding = new Rect
                {
                    Left = entryExt.LeftPadding,
                    Right = entryExt.RightPadding,
                    Top = entryExt.TopBottomPadding / 2,
                    Bottom = entryExt.TopBottomPadding / 2
                };
                this._gradietDrawable.GetPadding(padding);

                e.NewElement.Focused += (sender, evt) =>
                {
                    this._gradietDrawable.SetStroke(
                        (int)entryExt.BorderWidth,
                        entryExt.FocusBorderColor.ToAndroid());
                };

                e.NewElement.Unfocused += (sender, evt) =>
                {
                    this._gradietDrawable.SetStroke((int)entryExt.BorderWidth, entryExt.BorderColor.ToAndroid());
                };

                this.Control.SetBackground(this._gradietDrawable);

                if (this.Control != null && !string.IsNullOrEmpty(PackageName))
                {
                    //if (entryExt.HasShowAndHidePassword)
                    //{
                    //    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.show_pass, 0);
                    //    Control.SetOnTouchListener(new OnDrawableTouchListener());
                    //}

                    if (!string.IsNullOrEmpty(entryExt.LeftIcon))
                    {
                        int identifier = this.Context.Resources.GetIdentifier(
                            entryExt.LeftIcon,
                            "drawable",
                            PackageName);
                        if (identifier != 0)
                        {
                            Drawable drawable = Resources.GetDrawable(identifier);
                            if (drawable != null)
                            {
                                this.Control.SetCompoundDrawablesWithIntrinsicBounds(drawable, null, null, null);
                                this.Control.CompoundDrawablePadding = entryExt.PaddingLeftIcon;
                            }
                        }
                    }
                }

                this.Control.EditorAction += (sender, args) =>
                {
                    if (entryExt.NextEntry == null)
                    {
                        if (this._context.GetSystemService(Context.InputMethodService) is InputMethodManager inputMethodManager && this._context is Activity)
                        {
                            Activity activity = (Activity)this._context;
                            IBinder token = activity.CurrentFocus?.WindowToken;
                            inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                            activity.Window.DecorView.ClearFocus();
                        }
                    }

                    entryExt.EntryActionFired();
                };
            }
        }

        public class OnDrawableTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
        {
            public bool OnTouch(Android.Views.View v, MotionEvent e)
            {
                if (v is EditText editText && e.Action == MotionEventActions.Up)
                {
                    if (e.RawX >= (editText.Right - editText.GetCompoundDrawables()[2].Bounds.Width()))
                    {
                        if (editText.TransformationMethod == null)
                        {
                            editText.TransformationMethod = PasswordTransformationMethod.Instance;
                            editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.show_pass, 0);
                        }
                        else
                        {
                            editText.TransformationMethod = null;
                            editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.hide_pass, 0);
                        }

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
            this.Control.ImeOptions = GetValueFromDescription(entryExt.ReturnKeyType);
            this.Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), this.Control.ImeOptions);
        }

        private static ImeAction GetValueFromDescription(ReturnKeyTypes value)
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