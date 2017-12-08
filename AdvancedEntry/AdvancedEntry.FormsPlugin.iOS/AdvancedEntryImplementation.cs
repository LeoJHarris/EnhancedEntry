using System;
using System.ComponentModel;
using System.Reflection;

using CoreGraphics;

using Foundation;

using LeoJHarris.AdvancedEntry.Plugin.Abstractions;
using LeoJHarris.AdvancedEntry.Plugin.iOS;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AdvancedEntry), typeof(AdvancedEntryRenderer))]
namespace LeoJHarris.AdvancedEntry.Plugin.iOS
{
    /// <summary>
    /// AdvancedEntry Renderer
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AdvancedEntryRenderer : EntryRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { DateTime temp = DateTime.Now; }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            Abstractions.AdvancedEntry baseEntry = (Abstractions.AdvancedEntry)this.Element;
            base.OnElementChanged(e);


            if (!((this.Control != null) & (e.NewElement != null)))
            {
                return;
            }

            this.Control.LeftView = new UIView(new CGRect(0, 0, baseEntry.LeftPadding, this.Control.Frame.Height + baseEntry.TopBottomPadding));
            this.Control.RightView = new UIView(new CGRect(0, 0, baseEntry.RightPadding, this.Control.Frame.Height + baseEntry.TopBottomPadding));
            this.Control.LeftViewMode = UITextFieldViewMode.Always;
            this.Control.RightViewMode = UITextFieldViewMode.Always;

            this.Element.HeightRequest = 30;

            Abstractions.AdvancedEntry customEntry = e.NewElement as Abstractions.AdvancedEntry;

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

            e.NewElement.Focused += (sender, evt) =>
            {
                this.Control.Layer.BorderColor = baseEntry.FocusBorderColor.ToCGColor();
            };

            e.NewElement.Unfocused += (sender, evt) =>
            {
                this.Control.Layer.BorderColor = baseEntry.BorderColor.ToCGColor();
            };


            this.Control.Layer.CornerRadius = new nfloat(customEntry.CornerRadius);
            this.Control.Layer.BorderWidth = new nfloat(customEntry.BorderWidth);
            this.Control.Layer.BackgroundColor = baseEntry.BackgroundColor.ToCGColor();
            this.Control.Layer.BorderColor = baseEntry.BorderColor.ToCGColor();

            if (customEntry != null)
            {
                this.Control.ReturnKeyType =
                   GetValueFromDescription<UIReturnKeyType>(customEntry.ReturnKeyType.ToString());

                if (!string.IsNullOrEmpty(customEntry.LeftIcon))
                {
                    UIImage leftImage = new UIImage(customEntry.LeftIcon);

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
                    if (baseEntry.NextEntry == null)
                    {
                        UIApplication.SharedApplication.KeyWindow.EndEditing(true);
                    }

                    baseEntry.EntryActionFired();
                    return true;
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName != Abstractions.AdvancedEntry.ReturnKeyPropertyName)
            {
                return;
            }

            Abstractions.AdvancedEntry customEntry = sender as Abstractions.AdvancedEntry;
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
