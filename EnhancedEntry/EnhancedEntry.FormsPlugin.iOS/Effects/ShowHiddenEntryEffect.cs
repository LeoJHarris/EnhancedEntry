using System;
using LeoJHarris.FormsPlugin.iOS.Effects;
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
            ConfigureControl();
        }

        protected override void OnDetached()
        {

        }

        private void ConfigureControl()
        {
            if (Control != null)
            {
                UITextField vUpdatedEntry = (UITextField)Control;
                UIButton buttonRect = UIButton.FromType(UIButtonType.Custom);
                buttonRect.SetImage(new UIImage("show_pass"), UIControlState.Normal);
                buttonRect.TouchUpInside += (object sender, EventArgs e1) =>
                {
                    if (vUpdatedEntry.SecureTextEntry)
                    {
                        vUpdatedEntry.SecureTextEntry = false;
                        buttonRect.SetImage(new UIImage("hide_pass"), UIControlState.Normal);
                    }
                    else
                    {
                        vUpdatedEntry.SecureTextEntry = true;
                        buttonRect.SetImage(new UIImage("show_pass"), UIControlState.Normal);
                    }
                };

                vUpdatedEntry.ShouldChangeCharacters += (textField, range, replacementString) =>
                {
                    string text = vUpdatedEntry.Text;
                    string result = text.Substring(0, (int)range.Location) + replacementString + text.Substring((int)range.Location + (int)range.Length);
                    vUpdatedEntry.Text = result;
                    return false;
                };


                buttonRect.Frame = new CoreGraphics.CGRect(10.0f, 0.0f, 15.0f, 15.0f);
                buttonRect.ContentMode = UIViewContentMode.Right;

                UIView paddingViewRight =
                    new UIView(new System.Drawing.RectangleF(5.0f, -5.0f, 30.0f, 18.0f)) { buttonRect };
                paddingViewRight.ContentMode = UIViewContentMode.BottomRight;


                vUpdatedEntry.LeftView = paddingViewRight;
                vUpdatedEntry.LeftViewMode = UITextFieldViewMode.Always;

                Control.Layer.CornerRadius = 4;
                Control.Layer.BorderColor = new CoreGraphics.CGColor(255, 255, 255);
                Control.Layer.MasksToBounds = true;
                vUpdatedEntry.TextAlignment = UITextAlignment.Left;
            }

        }
    }
}