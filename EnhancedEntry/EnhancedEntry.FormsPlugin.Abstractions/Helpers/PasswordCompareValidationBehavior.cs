namespace LeoJHarris.EnhancedEntry.Plugin.Abstractions.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Xamarin.Forms;

    /// <summary>
    /// The password validation behavior.
    /// </summary>
    public class PasswordCompareValidationBehavior : Behavior<Entry>
    {
        /// <summary>
        /// The is valid property key.
        /// </summary>
        private static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly(nameof(IsValid), typeof(bool),
                typeof(PasswordCompareValidationBehavior), false);

        /// <summary>
        /// The is valid property.
        /// </summary>
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        /// <summary>
        /// The minimum length.
        /// </summary>
        private static readonly BindableProperty MinimumLengthBindableProperty =
            BindableProperty.Create(nameof(MinimumLength), typeof(int),
                typeof(PasswordCompareValidationBehavior), 6);

        /// <summary>
        /// The minimum length.
        /// </summary>Z
        public int MinimumLength
        {
            get => (int)GetValue(MinimumLengthBindableProperty);
            set => SetValue(MinimumLengthBindableProperty, value);
        }

        private static readonly BindableProperty HasUpperCaseCharactersBindableProperty =
            BindableProperty.Create(nameof(HasUpperCaseCharacters), typeof(bool),
                typeof(PasswordCompareValidationBehavior), true);

        /// <summary>
        /// Has upper case characters
        /// </summary>
        public bool HasUpperCaseCharacters
        {
            get => (bool)GetValue(HasUpperCaseCharactersBindableProperty
                );
            set => SetValue(HasUpperCaseCharactersBindableProperty, value);
        }
        private static readonly BindableProperty HasLowerCasesBindableProperty =
            BindableProperty.Create(nameof(HasLowerCaseCharacters), typeof(bool),
                typeof(PasswordCompareValidationBehavior), true);

        /// <summary>
        /// The minimum length.
        /// </summary>Z
        public bool HasLowerCaseCharacters
        {
            get => (bool)GetValue(HasLowerCasesBindableProperty);
            set => SetValue(HasLowerCasesBindableProperty, value);
        }

        private static readonly BindableProperty HasNumbersBindableProperty =
            BindableProperty.Create(nameof(HasNumbers), typeof(bool),
                typeof(PasswordCompareValidationBehavior), true);

        /// <summary>
        /// Is allowed numbers
        /// </summary>Z
        public bool HasNumbers
        {
            get => (bool)GetValue(HasLowerCasesBindableProperty);
            set => SetValue(HasNumbersBindableProperty, value);
        }


        /// <summary>
        /// The upper case pattern.
        /// </summary>
        public static readonly Regex UpperCasePattern = new Regex("[A-Z]");

        /// <summary>
        /// The lower case pattern.
        /// </summary>
        public static readonly Regex LowerCasePattern = new Regex("[a-z]");

        /// <summary>
        /// The number pattern.
        /// </summary>
        public static readonly Regex NumberPattern = new Regex("[0-9]");

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordCompareValidationBehavior"/> class.
        /// </summary>
        public PasswordCompareValidationBehavior(IEnumerable<Entry> entryCompare)
        {
            EntryComparables = entryCompare;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is valid.
        /// </summary>
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);

            protected set => SetValue(IsValidPropertyKey, value);
        }


        private static readonly BindableProperty ValidColorBindableProperty =
            BindableProperty.Create(nameof(ValidColor), typeof(Color),
                typeof(PasswordCompareValidationBehavior), Color.Transparent);

        /// <summary>
        /// Sets the valid color.
        /// </summary>
        public Color ValidColor
        {
            get => (Color)GetValue(ValidColorBindableProperty);
            set => SetValue(ValidColorBindableProperty, value);
        }

        private static readonly BindableProperty InValidColorBindableProperty =
            BindableProperty.Create(nameof(InValidColor), typeof(Color),
                typeof(PasswordCompareValidationBehavior), Color.Transparent);

        /// <summary>
        /// Sets the valid color.
        /// </summary>
        public Color InValidColor
        {
            get => (Color)GetValue(InValidColorBindableProperty);
            set => SetValue(InValidColorBindableProperty, value);
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

            bindable.TextChanged += HandleTextChange;
        }

        private IEnumerable<Entry> EntryComparables { get; }

        /// <summary>
        /// The on detaching from.
        /// </summary>
        /// <param name="bindable">
        /// The bindable.
        /// </param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.TextChanged -= HandleTextChange;
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

            bool lengthCheck = t.Length >= MinimumLength;
            HasUpperCaseCharacters = UpperCasePattern.IsMatch(t);
            HasLowerCaseCharacters = LowerCasePattern.IsMatch(t);
            HasNumbers = NumberPattern.IsMatch(t);

            IsValid = lengthCheck && HasUpperCaseCharacters && HasLowerCaseCharacters && HasNumbers;

            if (EntryComparables.Any())
            {
                foreach (Entry entry in EntryComparables)
                {
                    if (entry.Text != t)
                    {
                        IsValid = false;
                        break;
                    }
                }
            }

            if (IsValid)
            {
                foreach (Entry entry in EntryComparables)
                {
                    if (entry != null && entry.Behaviors.Any())
                    {
                        foreach (Behavior entryBehavior in entry.Behaviors)
                        {
                            if (entryBehavior is PasswordCompareValidationBehavior behavior)
                                behavior.IsValid = true;
                            entry.TextColor = IsValid ? ValidColor : InValidColor;
                        }
                    }
                }
            }

            ((Entry)sender).TextColor = IsValid ? ValidColor : InValidColor;
        }
    }
}
