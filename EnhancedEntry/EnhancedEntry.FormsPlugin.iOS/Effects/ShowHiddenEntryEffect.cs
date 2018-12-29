using LeoJHarris.FormsPlugin.iOS.Effects;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(ShowHiddenEntryEffect), "ShowHiddenEntryEffect")]
namespace LeoJHarris.FormsPlugin.iOS.Effects
{
    public class ShowHiddenEntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            configureControl();
        }

        protected override void OnDetached()
        {
        }

        /// <summary>
        /// Configures the control.
        /// </summary>
        private void configureControl()
        {
            if (Control != null)
            {
                UITextField enhancedEntry = (UITextField)Control;
                UIButton buttonRect = UIButton.FromType(UIButtonType.Custom);
                buttonRect.SetImage(new UIImage("show_password"), UIControlState.Normal);
                buttonRect.TouchUpInside += (object sender, EventArgs e1) =>
                {
                    if (enhancedEntry.SecureTextEntry)
                    {
                        enhancedEntry.SecureTextEntry = false;
                        buttonRect.SetImage(new UIImage("hide_password"), UIControlState.Normal);
                    }
                    else
                    {
                        enhancedEntry.SecureTextEntry = true;
                        buttonRect.SetImage(new UIImage("show_password"), UIControlState.Normal);
                    }
                };

                enhancedEntry.ShouldChangeCharacters += (textField, range, replacementString) =>
                {
                    string text = enhancedEntry.Text;
                    string result = text.Substring(0, (int)range.Location) + replacementString + text.Substring((int)range.Location + (int)range.Length);
                    enhancedEntry.Text = result;
                    return false;
                };

                buttonRect.Frame = new CoreGraphics.CGRect(10.0f, 0.0f, 15.0f, 15.0f);
                buttonRect.ContentMode = UIViewContentMode.Right;

                UIView paddingViewRight =
                    new UIView(new System.Drawing.RectangleF(5.0f, -5.0f, 30.0f, 18.0f)) { buttonRect };
                paddingViewRight.ContentMode = UIViewContentMode.BottomRight;


                enhancedEntry.RightView = paddingViewRight;
                enhancedEntry.RightViewMode = UITextFieldViewMode.Always;

                Control.Layer.CornerRadius = 4;
                Control.Layer.BorderColor = new CoreGraphics.CGColor(255, 255, 255);
                Control.Layer.MasksToBounds = true;
                enhancedEntry.TextAlignment = UITextAlignment.Left;
            }
        }
    }
}
