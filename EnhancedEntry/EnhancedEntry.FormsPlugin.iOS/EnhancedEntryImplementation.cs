
using LeoJHarris.FormsPlugin.Abstractions;
using LeoJHarris.FormsPlugin.iOS;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(EnhancedEntry), typeof(EnhancedEntryRenderer))]
namespace LeoJHarris.FormsPlugin.iOS
{
    using CoreGraphics;
    using Foundation;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;
    using EnhancedEntry = EnhancedEntry;

    [Preserve(AllMembers = true)]
    public class EnhancedEntryRenderer : EntryRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        new public static void Init() { DateTime temp = DateTime.Now; }

        /// <summary>
        /// Raises the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ElementChangedEventArgs{Entry}"/> instance containing the event data.</param>
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

                e.NewElement.Focused += (sender, evt) => Control.Layer.BorderColor = baseEntry.FocusBorderColor.ToCGColor();

                e.NewElement.Unfocused += (sender, evt) => Control.Layer.BorderColor = baseEntry.BorderColor.ToCGColor();

                Control.Layer.CornerRadius = new nfloat(customEntry.CornerRadius);
                Control.Layer.BorderWidth = new nfloat(customEntry.BorderWidth);
                Control.Layer.BackgroundColor = baseEntry.BackgroundColor.ToCGColor();
                Control.Layer.BorderColor = baseEntry.BorderColor.ToCGColor();

                Control.ReturnKeyType =
                    getValueFromDescription<UIReturnKeyType>(customEntry.ReturnKeyType.ToString());

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

                Control.ShouldReturn += _ =>
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

        /// <summary>
        /// Called when [element property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == EnhancedEntry.ReturnKeyPropertyName)
            {
                if (sender is EnhancedEntry customEntry)
                {
                    Control.ReturnKeyType = getValueFromDescription<UIReturnKeyType>(customEntry.ReturnKeyType.ToString());
                }
            }
        }

        /// <summary>
        /// Gets the value from description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException">Not found. - description</exception>
        private static T getValueFromDescription<T>(string description)
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (FieldInfo field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
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
