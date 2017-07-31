using AdvancedEntry.FormsPlugin.Abstractions;
using System;
using Xamarin.Forms;
using AdvancedEntry.FormsPlugin.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using System.ComponentModel;
using System.Reflection;

[assembly: ExportRenderer(typeof(AdvancedEntry.FormsPlugin.Abstractions.AdvancedEntryControl), typeof(AdvancedEntryRenderer))]
namespace AdvancedEntry.FormsPlugin.iOS
{
    /// <summary>
    /// AdvancedEntry Renderer
    /// </summary>
    public class AdvancedEntryRenderer : EntryRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            AdvancedEntryControl baseEntry = (AdvancedEntryControl)this.Element;
            base.OnElementChanged(e);

            if (!((this.Control != null) & (e.NewElement != null)))
            {
                return;
            }

            // Create a custom border with square corners
            this.Control.BorderStyle = UITextBorderStyle.None;
            this.Control.Layer.CornerRadius = 8;
            this.Control.Layer.BorderWidth = .5f;
            this.Control.Layer.BackgroundColor = baseEntry.BackgroundColor.ToCGColor();
            this.Control.Layer.BorderColor = baseEntry.BorderColor.ToCGColor();

            // Invisible views create padding at the beginning and end
            this.Control.LeftView = new UIView(new CGRect(0, 0, 10, this.Control.Frame.Height));
            this.Control.RightView = new UIView(new CGRect(0, 0, 10, this.Control.Frame.Height));
            this.Control.LeftViewMode = UITextFieldViewMode.Always;
            this.Control.RightViewMode = UITextFieldViewMode.Always;

            // Fixed height creates padding at top and bottom
            this.Element.HeightRequest = 45;

            AdvancedEntryControl customEntry = e.NewElement as AdvancedEntryControl;
            if (customEntry != null)
            {
                this.Control.ReturnKeyType =
                    EnumEx.GetValueFromDescription<UIReturnKeyType>(customEntry.ReturnKeyType.ToString());

                UIImageView viewImage = new UIImageView(new UIImage(customEntry.LeftIcon));

                viewImage.Frame = new CGRect(0.0, 0.0, viewImage.Image.Size.Width + 25.0, viewImage.Image.Size.Height + 18);
                viewImage.ContentMode = UIViewContentMode.Center;

                this.Control.LeftView = viewImage;

                this.Control.ShouldReturn += field =>
                {
                    baseEntry.EntryActionFired();
                    return true;
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName != AdvancedEntryControl.ReturnKeyPropertyName)
            {
                return;
            }

            AdvancedEntryControl customEntry = sender as AdvancedEntryControl;
            if (customEntry != null)
                this.Control.ReturnKeyType =
                    EnumEx.GetValueFromDescription<UIReturnKeyType>(customEntry.ReturnKeyType.ToString());
        }
    }

    public static class EnumEx
    {
        public static T GetValueFromDescription<T>(string description)
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (FieldInfo field in type.GetFields())
            {
                DescriptionAttribute attribute =
                    Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
        }
    }
}
