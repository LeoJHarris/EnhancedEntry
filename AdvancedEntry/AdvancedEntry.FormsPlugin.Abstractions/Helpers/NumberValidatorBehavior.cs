using Xamarin.Forms;

namespace LeoJHarris.AdvancedEntry.Plugin.Abstractions.Helpers
{
    /// <summary>
    /// The number validator behavior.
    /// </summary>
    public class NumberValidatorBehavior : Behavior<Entry>
    {
        private static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NumberValidatorBehavior), false);

        /// <summary>
        /// Is valid
        /// </summary>
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        /// <summary>
        /// Gets a value indicating whether is valid.
        /// </summary>
        public bool IsValid
        {
            get => (bool)this.GetValue(IsValidProperty);
            private set => this.SetValue(IsValidPropertyKey, value);
        }

        /// <summary>
        /// The entry to bind to
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += this.BindableTextChanged;
        }

        private void BindableTextChanged(object sender, TextChangedEventArgs e)
        {
            this.IsValid = double.TryParse(e.NewTextValue, out _);
            ((Entry)sender).TextColor = this.IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= this.BindableTextChanged;
        }
    }
}
