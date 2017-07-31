using AdvancedEntry.FormsPlugin.Abstractions;
using System;
using Xamarin.Forms;
using AdvancedEntry.FormsPlugin.Android;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using System.Reflection;
using Android.Views.InputMethods;
using Android.OS;
using Android.Support.V4.Content;

[assembly: ExportRenderer(typeof(AdvancedEntry.FormsPlugin.Abstractions.AdvancedEntryControl), typeof(AdvancedEntryRenderer))]
namespace AdvancedEntry.FormsPlugin.Android
{
    public class AdvancedEntryRenderer : EntryRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            var baseEntry = (AdvancedEntryControl)this.Element;

            base.OnElementChanged(e);

            if (!((this.Control != null) & (e.NewElement != null))) return;
            AdvancedEntryControl entryExt = e.NewElement as AdvancedEntryControl;
            if (entryExt == null) return;

            this.Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();

            this.Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), this.Control.ImeOptions);

            if (this.Control != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    this.Control.Background = ContextCompat.GetDrawable(this.Context, Resource.Drawable.RoundedCornerEntry);
                }
                else
                {
                    this.Control.Background = this.Resources.GetDrawable(Resource.Drawable.RoundedCornerEntry);
                }
            }
           // var resourceId = (int)typeof(Resource.Drawable).GetField(baseEntry.LeftIcon).GetValue(null);
            // Context.Resources.GetDrawable(Context.Resources.GetIdentifier(baseEntry.LeftIcon, "drawable", Context.PackageName));

            //if (resourceId != 0)
            //{
            //    this.Control.SetCompoundDrawablesWithIntrinsicBounds(resourceId, 0, 0, 0);
            //    this.Control.CompoundDrawablePadding = 25;
            //}

            this.Control.EditorAction += (sender, args) =>
            {
                baseEntry.EntryActionFired();
            };
        }

        /// <summary>
        /// The on element property changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName != AdvancedEntryControl.ReturnKeyPropertyName) return;
            AdvancedEntryControl entryExt = sender as AdvancedEntryControl;
            if (entryExt == null) return;
            this.Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();
            this.Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), this.Control.ImeOptions);
        }
    }

    /// <summary>
    /// The enum extensions.
    /// </summary>
    public static class EnumExtensions
    {
        public static ImeAction GetValueFromDescription(this ReturnKeyTypes value)
        {
            Type type = typeof(ImeAction);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (FieldInfo field in type.GetFields())
            {
                DescriptionAttribute attribute = Attribute.GetCustomAttribute(field,
                                                     typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == value.ToString()) return (ImeAction)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value.ToString()) return (ImeAction)field.GetValue(null);
                }
            }

            throw new NotSupportedException($"Not supported on Android: {value}");
        }
    }
}