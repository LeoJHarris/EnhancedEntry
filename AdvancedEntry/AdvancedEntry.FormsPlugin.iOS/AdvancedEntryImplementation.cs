
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using System.ComponentModel;
using System.Reflection;
using LeoJHarris.Control.Abstractions;
using Foundation;

[assembly: ExportRenderer(typeof(AdvancedEntry), typeof(LeoJHarris.Control.iOS.AdvancedEntryRenderer))]
namespace LeoJHarris.Control.iOS
{

    [Preserve(AllMembers = true)]
    /// <summary>
    /// AdvancedEntry Renderer
    /// </summary>
    public class AdvancedEntryRenderer : EntryRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { var temp = DateTime.Now; }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            AdvancedEntry baseEntry = (AdvancedEntry)Element;
            base.OnElementChanged(e);


            if (!((this.Control != null) & (e.NewElement != null)))
            {
                return;
            }
          
            this.Control.LeftView = new UIView(new CGRect(0, 0, 6, this.Control.Frame.Height));
            this.Control.RightView = new UIView(new CGRect(0, 0, 6, this.Control.Frame.Height));
            this.Control.LeftViewMode = UITextFieldViewMode.Always;
            this.Control.RightViewMode = UITextFieldViewMode.Always;

            this.Element.HeightRequest = 30;

            AdvancedEntry customEntry = e.NewElement as AdvancedEntry;

            switch (customEntry.UITextBorderStyle)
            {
                case TextBorderStyle.None:
                    this.Control.BorderStyle = UITextBorderStyle.None;
                    break;
                case TextBorderStyle.Line:
                    this.Control.BorderStyle = UITextBorderStyle.Line;
                    break;
                case TextBorderStyle.Bezel:
                    this.Control.BorderStyle = UITextBorderStyle.Bezel;
                    break;
                case TextBorderStyle.RoundedRect:
                    this.Control.BorderStyle = UITextBorderStyle.RoundedRect;
                    break;
            }

            this.Control.Layer.CornerRadius = customEntry.CornerRadius;
           this.Control.Layer.BorderWidth = customEntry.BorderWidth;
            this.Control.Layer.BackgroundColor = baseEntry.BackgroundColor.ToCGColor();
            this.Control.Layer.BorderColor = baseEntry.BorderColor.ToCGColor();

            if (customEntry != null)
            {
                this.Control.ReturnKeyType =
                   GetValueFromDescription<UIReturnKeyType>(customEntry.ReturnKeyType.ToString());

                if (!string.IsNullOrEmpty(customEntry.LeftIcon))
                {
                    var leftImage = new UIImage(customEntry.LeftIcon);

                    if (leftImage != null)
                    {
                        UIImageView viewImage = new UIImageView(leftImage);

                        viewImage.Frame = new CGRect(0.0, 0.0, viewImage.Image.Size.Width + customEntry.PaddingLeftIcon, viewImage.Image.Size.Height + customEntry.PaddingLeftIcon);
                        viewImage.ContentMode = UIViewContentMode.Center;

                        this.Control.LeftView = viewImage;
                    }
                }

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
            if (e.PropertyName != AdvancedEntry.ReturnKeyPropertyName)
            {
                return;
            }

            AdvancedEntry customEntry = sender as AdvancedEntry;
            if (customEntry != null)
                this.Control.ReturnKeyType = GetValueFromDescription<UIReturnKeyType>(customEntry.ReturnKeyType.ToString());
        }

        private static T GetValueFromDescription<T>(string description)
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
