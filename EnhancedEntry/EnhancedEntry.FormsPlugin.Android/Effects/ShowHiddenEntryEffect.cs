using Android.Support.V4.Content;
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
            configureControl();
        }

        protected override void OnDetached()
        {
        }

        private void configureControl()
        {
            EditText editText = ((EditText)Control);

            editText.SetCompoundDrawablesWithIntrinsicBounds(editText.GetCompoundDrawables()[0] ?? null, null, ContextCompat.GetDrawable(Android.App.Application.Context, Resource.Drawable.show_password), null);
            editText.SetOnTouchListener(new OnDrawableTouchListener());
        }
    }

    /// <summary>
    /// OnDrawableTouchListener
    /// </summary>
    /// <seealso cref="Java.Lang.Object" />
    /// <seealso cref="Android.Views.View.IOnTouchListener" />
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
                        editText.SetCompoundDrawablesWithIntrinsicBounds(editText.GetCompoundDrawables()[0] ?? null, null, ContextCompat.GetDrawable(Android.App.Application.Context, Resource.Drawable.show_password), null);
                    }
                    else
                    {
                        editText.TransformationMethod = null;
                        editText.SetCompoundDrawablesWithIntrinsicBounds(editText.GetCompoundDrawables()[0] ?? null, null, ContextCompat.GetDrawable(Android.App.Application.Context, Resource.Drawable.hide_password), null);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
