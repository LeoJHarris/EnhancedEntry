using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace LeoJHarris.AdvancedEntry.Plugin.Abstractions.Helpers
{
    /// <summary>
    /// The password validation behavior.
    /// </summary>
    public class PasswordCompareValidationBehavior : Behavior<Entry>
    {
        /// <summary>
        /// The is valid property key.
        /// </summary>
        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(nameof(IsValid), typeof(bool), typeof(PasswordCompareValidationBehavior), false);

        /// <summary>
        /// The is valid property.
        /// </summary>
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        /// <summary>
        /// The minimum length.
        /// </summary>
        public static readonly int MinimumLength = 6;

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

        /// <summary>
        /// Gets or sets the valid color.
        /// </summary>
        public Color ValidColor { get; set; }

        /// <summary>
        /// Gets or sets the valid color.
        /// </summary>
        public Color InValidColor { get; set; }

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
            bool hasUpperCase = UpperCasePattern.IsMatch(t);
            bool hasLowerCase = LowerCasePattern.IsMatch(t);
            bool hasNumbers = NumberPattern.IsMatch(t);

           IsValid = lengthCheck && hasUpperCase && hasLowerCase && hasNumbers;

           if(EntryComparables.Any())
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
                        if (entry.Behaviors[0] is PasswordCompareValidationBehavior behavior)
                            behavior.IsValid = true;
                        entry.TextColor = IsValid ? ValidColor : InValidColor;
                    }
                }
            }

            ((Entry)sender).TextColor = IsValid ? ValidColor : InValidColor;
        }
    }
}
