using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
using LeoJHarris.Control.Abstractions;
using System;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdvancedEntry), typeof(LeoJHarris.Control.Android.AdvancedEntryRenderer))]
namespace LeoJHarris.Control.Android
{
    public class AdvancedEntryRenderer : EntryRenderer
    {
       static string PackageName
        {
            get;
            set;
        }

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init(Context context) { PackageName= context.PackageName; }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var baseEntry = (AdvancedEntry)this.Element;

            if (!((this.Control != null) & (e.NewElement != null))) return;

            AdvancedEntry entryExt = e.NewElement as AdvancedEntry;
            if (baseEntry == null) return;

            this.Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();

            this.Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), this.Control.ImeOptions);

            if (this.Control != null && !string.IsNullOrEmpty(baseEntry.CustomBackgroundXML) && !string
                .IsNullOrEmpty(PackageName))
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    this.Control.Background = Context.Resources.GetDrawable(Context.Resources.GetIdentifier(baseEntry.CustomBackgroundXML, "drawable", PackageName));
                }
                else
                {
                     this.Control.Background = Context.Resources.GetDrawable(Context.Resources.GetIdentifier(baseEntry.CustomBackgroundXML, "drawable", PackageName));
                }
            }

            if (this.Control != null && !string.IsNullOrEmpty(baseEntry.LeftIcon))
            {

                var resourceId = Context.Resources.GetDrawable(Context.Resources.GetIdentifier(baseEntry.LeftIcon, "drawable", PackageName));

                if (resourceId != null)
                {
                    this.Control.SetCompoundDrawablesWithIntrinsicBounds(resourceId, null, null, null);
                    this.Control.CompoundDrawablePadding = baseEntry.PaddingLeftIcon;
                }
            }

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
            if (e.PropertyName != AdvancedEntry.ReturnKeyPropertyName) return;
            AdvancedEntry entryExt = sender as AdvancedEntry;
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