namespace LeoJHarris.AdvancedEntry.Plugin.Abstractions.Helpers
{
    using Xamarin.Forms;

    /// <summary>
    /// The password validation behavior.
    /// </summary>
    public class GoToNextEntryOnLengthBehaviour : Behavior<Entry>
    {
        /// <summary>
        /// The is valid property key.
        /// </summary>
        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(nameof(IsValid), typeof(bool), typeof(PasswordCompareValidationBehavior), false);

        /// <summary>
        /// The is valid property.
        /// </summary>
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        private static readonly BindableProperty CharacterLengthBindableProperty =
            BindableProperty.Create(nameof(CharacterLength), typeof(int), typeof(AdvancedEntry), 6);

        /// <summary>
        /// Character length
        /// </summary>
        public int CharacterLength
        {
            get => (int)this.GetValue(CharacterLengthBindableProperty);

            set => this.SetValue(CharacterLengthBindableProperty, value);
        }

        /// <summary>
        /// Jump to the next <see cref="Entry"/> entry on <see cref="CharacterLength"/> value reached
        /// </summary>
        /// <param name="nextEntry"></param>
        public GoToNextEntryOnLengthBehaviour(Entry nextEntry)
        {
            this.NextEntry = nextEntry;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is valid.
        /// </summary>
        public bool IsValid
        {
            get => (bool)this.GetValue(IsValidProperty);

            protected set => this.SetValue(IsValidPropertyKey, value);
        }

        /// <summary>
        /// The on attached to.
        /// </summary>
        /// <param name="bindable">
        /// The bindable.
        /// </param>
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += this.HandleTextChange;
        }

        private Entry NextEntry { get; set; }

        /// <summary>
        /// The on detaching from.
        /// </summary>
        /// <param name="bindable">
        /// The bindable.
        /// </param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.TextChanged -= this.HandleTextChange;
        }

        /// <summary>
        /// The handle text change.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void HandleTextChange(object sender, TextChangedEventArgs e)
        {
            string t = e.NewTextValue;

            bool lengthCheck = t.Length >= this.CharacterLength;

            this.IsValid = lengthCheck && this.NextEntry != null;
            if (this.IsValid)
            {
                this.NextEntry?.Focus();
            }
        }
    }
}

