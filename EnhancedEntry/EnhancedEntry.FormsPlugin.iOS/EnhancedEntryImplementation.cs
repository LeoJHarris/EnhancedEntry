using LeoJHarris.EnhancedEntry.Plugin.Abstractions;
using LeoJHarris.EnhancedEntry.Plugin.iOS;

using Xamarin.Forms;

[assembly: ExportRenderer(typeof(EnhancedEntry), typeof(EnhancedEntryRenderer))]
namespace LeoJHarris.EnhancedEntry.Plugin.iOS
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    using CoreGraphics;

    using Foundation;

    using UIKit;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    using EnhancedEntry = Abstractions.EnhancedEntry;

    [Preserve(AllMembers = true)]
    public class EnhancedEntryRenderer : EntryRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { DateTime temp = DateTime.Now; }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            EnhancedEntry baseEntry = (EnhancedEntry)Element;
            base.OnElementChanged(e);

            if (!((Control != null) & (e.NewElement != null)))
            {
                return;
            }

            Control.LeftView = new UIView(new CGRect(0, 0, baseEntry.LeftPadding, Control.Frame.Height + baseEntry.TopBottomPadding));
            Control.RightView = new UIView(new CGRect(0, 0, baseEntry.RightPadding, Control.Frame.Height + baseEntry.TopBottomPadding));
            Control.LeftViewMode = UITextFieldViewMode.Always;
            Control.RightViewMode = UITextFieldViewMode.Always;

            Element.HeightRequest = 30;

            if (e.NewElement is EnhancedEntry customEntry)
            {
                switch (customEntry.UITextBorderStyle)
                {
                    case TextBorderStyle.None:
                        Control.BorderStyle = UITextBorderStyle.None;
                        break;
                    case TextBorderStyle.Line:
                        Control.BorderStyle = UITextBorderStyle.Line;
                        break;
                    case TextBorderStyle.Bezel:
                        Control.BorderStyle = UITextBorderStyle.Bezel;
                        break;
                    case TextBorderStyle.RoundedRect:
                        Control.BorderStyle = UITextBorderStyle.RoundedRect;
                        break;
                }

                e.NewElement.Focused += (sender, evt) =>
                    {
                        Control.Layer.BorderColor = baseEntry.FocusBorderColor.ToCGColor();
                    };

                e.NewElement.Unfocused += (sender, evt) =>
                    {
                        Control.Layer.BorderColor = baseEntry.BorderColor.ToCGColor();
                    };


                Control.Layer.CornerRadius = new nfloat(customEntry.CornerRadius);
                Control.Layer.BorderWidth = new nfloat(customEntry.BorderWidth);
                Control.Layer.BackgroundColor = baseEntry.BackgroundColor.ToCGColor();
                Control.Layer.BorderColor = baseEntry.BorderColor.ToCGColor();

                Control.ReturnKeyType =
                    GetValueFromDescription<UIReturnKeyType>(customEntry.ReturnKeyType.ToString());

                if (!string.IsNullOrEmpty(customEntry.LeftIcon))
                {
                    UIImage leftImage = new UIImage(customEntry.LeftIcon);

                    {
                        UIImageView viewImage = new UIImageView(leftImage);

                        viewImage.Frame = new CGRect(
                            0.0,
                            0.0,
                            viewImage.Image.Size.Width + customEntry.PaddingLeftIcon,
                            viewImage.Image.Size.Height + customEntry.PaddingLeftIcon);
                        viewImage.ContentMode = UIViewContentMode.Center;

                        Control.LeftView = viewImage;
                    }
                }

                Control.ShouldReturn += field =>
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
            if (e.PropertyName != EnhancedEntry.ReturnKeyPropertyName)
            {
                return;
            }

            if (sender is EnhancedEntry customEntry)
                Control.ReturnKeyType =
                    GetValueFromDescription<UIReturnKeyType>(customEntry.ReturnKeyType.ToString());
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
