using Xamarin.Forms;

namespace LeoJHarris.AdvancedEntry.Plugin.Abstractions.Helpers
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
            get => (int)this.GetValue(MaxLengthProperty);
            set => this.SetValue(MaxLengthProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += this.BindableTextChanged;
        }

        private void BindableTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 0 && e.NewTextValue.Length > this.MaxLength)
                ((Entry)sender).Text = e.NewTextValue.Substring(0, this.MaxLength);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= this.BindableTextChanged;
        }
    }
}
