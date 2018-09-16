using Xamarin.Forms;

namespace LeoJHarris.FormsPlugin.Abstractions.Helpers
{
    /// <summary>
    /// The number validator behavior.
    /// </summary>
    public class NumberValidatorBehavior : Behavior<Entry>
    {
        private static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly(nameof(IsValid), typeof(bool), typeof(NumberValidatorBehavior), false);

        /// <summary>
        /// Is valid
        /// </summary>
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        /// <summary>
        /// Gets a value indicating whether is valid.
        /// </summary>
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidPropertyKey, value);
        }

        /// <summary>
        /// The entry to bind to
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += bindableTextChanged;
        }

        /// <summary>
        /// Bindables the text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void bindableTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = double.TryParse(e.NewTextValue, out _);
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        /// <summary>
        /// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(T)" /> method and then detaches from the superclass.
        /// </summary>
        /// <param name="bindable">The bindable object from which the behavior was detached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= bindableTextChanged;
        }
    }
}
