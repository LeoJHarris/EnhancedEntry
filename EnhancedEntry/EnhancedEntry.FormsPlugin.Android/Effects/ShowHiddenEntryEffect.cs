using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(LeoJHarris.FormsPlugin.Droid.Effects.ShowHiddenEntryEffect), "ShowHiddenEntryEffect")]
namespace LeoJHarris.FormsPlugin.Droid.Effects
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
            EditText editText = ((EditText)Control);
            editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.ShowPass, 0);
            editText.SetOnTouchListener(new OnDrawableTouchListener());
        }
    }

    public class OnDrawableTouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        public bool OnTouch(View v, MotionEvent e)
        {
            if (v is EditText editText && e.Action == MotionEventActions.Up)
            {
                if (e.RawX >= (editText.Right - editText.GetCompoundDrawables()[2].Bounds.Width()))
                {
                    if (editText.TransformationMethod == null)
                    {
                        editText.TransformationMethod = PasswordTransformationMethod.Instance;
                        editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.ShowPass, 0);
                    }
                    else
                    {
                        editText.TransformationMethod = null;
                        editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.ShowPass, 0);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
