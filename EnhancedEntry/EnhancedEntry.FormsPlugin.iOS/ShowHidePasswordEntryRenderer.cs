using System;
using ShowHidePasswordEntryXF;
using ShowHidePasswordEntryXF.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ShowHidePasswordEntry), typeof(ShowHidePasswordEntryRenderer))]
namespace ShowHidePasswordEntryXF.iOS
{
	public class ShowHidePasswordEntryRenderer: EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				var formsEntry = (ShowHidePasswordEntry)e.NewElement;
				var buttonRect = UIButton.FromType(UIButtonType.Custom);
				buttonRect.SetImage(new UIImage("show_pass"), UIControlState.Normal);
				buttonRect.TouchUpInside += (object sender, EventArgs e1) =>
				{
					if (Control.SecureTextEntry)
					{
						Control.SecureTextEntry = false;
						buttonRect.SetImage(new UIImage("hide_pass"), UIControlState.Normal);
					}
					else {
						Control.SecureTextEntry = true;
						buttonRect.SetImage(new UIImage("show_pass"), UIControlState.Normal);
					}
				};

				Control.ShouldChangeCharacters += (textField, range, replacementString) =>
				{
					string text = Control.Text;
					var result = text.Substring(0, (int)range.Location) + replacementString + text.Substring((int)range.Location + (int)range.Length);
					Control.Text = result;
					(Element as ShowHidePasswordEntry).EntryText = result;
					return false;
				};


				buttonRect.Frame = new CoreGraphics.CGRect(10.0f, 0.0f, 15.0f, 15.0f);
				buttonRect.ContentMode = UIViewContentMode.Right;

				UIView paddingViewRight = new UIView(new System.Drawing.RectangleF(5.0f, -5.0f, 30.0f, 18.0f));
				paddingViewRight.Add(buttonRect);
				paddingViewRight.ContentMode = UIViewContentMode.BottomRight;


				Control.LeftView = paddingViewRight;
				Control.LeftViewMode = UITextFieldViewMode.Always;

				Control.Layer.CornerRadius = 4;
				Control.Layer.BorderColor = new CoreGraphics.CGColor(255, 255, 255);
				Control.Layer.MasksToBounds = true;
				Control.TextAlignment = UITextAlignment.Left;
			}
		}
	}
}
