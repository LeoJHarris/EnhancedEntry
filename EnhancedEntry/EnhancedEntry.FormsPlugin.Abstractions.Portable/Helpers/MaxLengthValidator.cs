using Xamarin.Forms;

namespace LeoJHarris.EnhancedEntry.Plugin.Abstractions.Portable.Helpers
{
    /// <summary>
    /// The max length validator.
    /// </summary>
    public class MaxLengthValidator : Behavior<Entry>
    {
        /// <summary>
        /// The max length property.
        /// </summary>
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create("MaxLength", typeof(int),
                typeof(MaxLengthValidator), 0);

        /// <summary>
        /// Max length
        /// </summary>
        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += BindableTextChanged;
        }

        private void BindableTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 0 && e.NewTextValue.Length > MaxLength)
                ((Entry)sender).Text = e.NewTextValue.Substring(0, MaxLength);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= BindableTextChanged;
        }
    }
}
