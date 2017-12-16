using System;
using System.ComponentModel;
using System.Reflection;

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views.InputMethods;

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
        private Context context;

        public EnhancedEntryRenderer(Context context) : base(context)
        {
            this.AutoPackage = false;
            this.context = context;
        }

        private static string PackageName
        {
            get;
            set;
        }

        private GradientDrawable gradietDrawable;


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

            // EnhancedEntry baseEntry = (EnhancedEntry)this.Element;

            if (!((this.Control != null) & (e.NewElement != null))) return;

            EnhancedEntry entryExt = e.NewElement as EnhancedEntry;
            if (entryExt == null) return;

            // LayerDrawable layerDrawable = new LayerDrawable(new Drawable[]
            // {
            // this.gradietDrawable
            // });
            // GradientDrawable strokeDrawable = (GradientDrawable)layerDrawable.findDrawableByLayerId(R.id.item_bottom_stroke);
            // strokeDrawable.setColor(strokeColor[0]);
            // GradientDrawable backgroundColor = (GradientDrawable)layerDrawable.findDrawableByLayerId(R.id.item_navbar_background);
            // backgroundColor.setColors(bgColor);
            if (entryExt != null)
            {
                this.Control.ImeOptions = GetValueFromDescription(entryExt.ReturnKeyType);

                this.Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), this.Control.ImeOptions);

                this.gradietDrawable = new GradientDrawable();
                this.gradietDrawable.SetShape(ShapeType.Rectangle);
                this.gradietDrawable.SetColor(entryExt.BackgroundColor.ToAndroid());
                this.gradietDrawable.SetCornerRadius(entryExt.CornerRadius);
                this.gradietDrawable.SetStroke((int)entryExt.BorderWidth, entryExt.BorderColor.ToAndroid());

                Rect padding = new Rect
                {
                    Left = entryExt.LeftPadding,
                    Right = entryExt.RightPadding,
                    Top = entryExt.TopBottomPadding / 2,
                    Bottom = entryExt.TopBottomPadding / 2
                };
                this.gradietDrawable.GetPadding(padding);


                e.NewElement.Focused += (sender, evt) =>
                    {
                        this.gradietDrawable.SetStroke(
                            (int)entryExt.BorderWidth,
                            entryExt.FocusBorderColor.ToAndroid());
                    };

                e.NewElement.Unfocused += (sender, evt) =>
                    {
                        this.gradietDrawable.SetStroke((int)entryExt.BorderWidth, entryExt.BorderColor.ToAndroid());
                    };

                this.Control.SetBackground(this.gradietDrawable);

                if (this.Control != null && !string.IsNullOrEmpty(PackageName))
                {
                    if (!string.IsNullOrEmpty(entryExt.LeftIcon))
                    {
                        int identifier = this.Context.Resources.GetIdentifier(
                            entryExt.LeftIcon,
                            "drawable",
                            PackageName);
                        if (identifier != 0)
                        {
                            Drawable drawable = this.Context.Resources.GetDrawable(identifier);
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
                            if (this.context.GetSystemService(Context.InputMethodService) is InputMethodManager inputMethodManager && this.context is Activity)
                            {
                                Activity activity = (Activity)this.context;
                                IBinder token = activity.CurrentFocus?.WindowToken;
                                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                                activity.Window.DecorView.ClearFocus();
                            }
                        }

                        entryExt.EntryActionFired();
                    };
            }

            // gradietDrawable.SetColorFilter(Color.Black, PorterDuff.Mode.SrcIn);
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