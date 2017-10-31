using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views.InputMethods;
using LeoJHarris.AdvancedEntry.Plugin.Abstractions;
using LeoJHarris.AdvancedEntry.Plugin.Droid;
using System;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdvancedEntry), typeof(AdvancedEntryRenderer))]
namespace LeoJHarris.AdvancedEntry.Plugin.Droid
{
    /// <summary>
    /// 
    /// </summary>
    public class AdvancedEntryRenderer : EntryRenderer
    {
        static string PackageName
        {
            get;
            set;
        }

        private GradientDrawable gradietDrawable;

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init(Context context) { PackageName = context.PackageName; }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            Abstractions.AdvancedEntry baseEntry = (Abstractions.AdvancedEntry)this.Element;

            if (!((this.Control != null) & (e.NewElement != null))) return;

            Abstractions.AdvancedEntry entryExt = e.NewElement as Abstractions.AdvancedEntry;
            if (baseEntry == null) return;

            if (entryExt != null)
            {
                this.Control.ImeOptions = GetValueFromDescription(entryExt.ReturnKeyType);
                
                this.Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), this.Control.ImeOptions);
                gradietDrawable = new GradientDrawable();
                gradietDrawable.SetShape(ShapeType.Rectangle);
                gradietDrawable.SetColor(entryExt.BackgroundColor.ToAndroid());
                gradietDrawable.SetCornerRadius(entryExt.CornerRadius);
                gradietDrawable.SetStroke((int)baseEntry.BorderWidth, baseEntry.BorderColor.ToAndroid());

                Rect padding = new Rect
                {
                    Left = entryExt.LeftPadding,
                    Right = entryExt.RightPadding,
                    Top = entryExt.TopBottomPadding / 2,
                    Bottom = entryExt.TopBottomPadding / 2
                };
                gradietDrawable.GetPadding(padding);
            }

            e.NewElement.Focused += (sender, evt) =>
            {
                gradietDrawable.SetStroke((int)baseEntry.BorderWidth, baseEntry.FocusBorderColor.ToAndroid());
            };

            e.NewElement.Unfocused += (sender, evt) =>
            {
                gradietDrawable.SetStroke((int)baseEntry.BorderWidth, baseEntry.BorderColor.ToAndroid());
            };

            Control.SetBackground(gradietDrawable);
            
            if (this.Control != null && !string.IsNullOrEmpty(PackageName))
            {
                if (!string.IsNullOrEmpty(baseEntry.LeftIcon))
                {
                    int identifier = Context.Resources.GetIdentifier(baseEntry.LeftIcon, "drawable", PackageName);
                    if (identifier != 0)
                    {
                        Drawable drawable = Context.Resources.GetDrawable(identifier);
                        if (drawable != null)
                        {
                            this.Control.SetCompoundDrawablesWithIntrinsicBounds(drawable, null, null, null);
                            this.Control.CompoundDrawablePadding = baseEntry.PaddingLeftIcon;
                        }
                    }
                }
            }

            this.Control.EditorAction += (sender, args) =>
            {
                baseEntry.EntryActionFired();
            };

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
            if (e.PropertyName != Abstractions.AdvancedEntry.ReturnKeyPropertyName) return;
            if (!(sender is Abstractions.AdvancedEntry entryExt)) return;
            this.Control.ImeOptions = GetValueFromDescription(entryExt.ReturnKeyType);
            this.Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), this.Control.ImeOptions);
        }

        private static ImeAction GetValueFromDescription(ReturnKeyTypes value)
        {
            Type type = typeof(ImeAction);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (FieldInfo field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
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