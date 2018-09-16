using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace LeoJHarris.FormsPlugin.Abstractions.Helpers
{
    /// <summary>
    /// EmailValidatorBehavior
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
    public class EmailValidatorBehavior : Behavior<Entry>
    {
        /// <summary>
        /// The email regular expression bindable property
        /// </summary>
        public static readonly BindableProperty EmailRegularExpressionBindableProperty =
            BindableProperty.Create(nameof(EmailRegularExpression), typeof(string),
                typeof(EnhancedEntry), @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

        /// <summary>
        /// Gets or sets the email regular expression.
        /// </summary>
        /// <value>
        /// The email regular expression.
        /// </value>
        public string EmailRegularExpression
        {
            get => (string)GetValue(EmailRegularExpressionBindableProperty);

            set => SetValue(EmailRegularExpressionBindableProperty, value);
        }

        private static readonly BindablePropertyKey _isValidPropertyKey =
            BindableProperty.CreateReadOnly(nameof(IsValid), typeof(bool), typeof(NumberValidatorBehavior), false);

        /// <summary>
        /// The is valid property
        /// </summary>
        public static readonly BindableProperty IsValidProperty = _isValidPropertyKey.BindableProperty;

        /// <summary>
        /// Is valid
        /// </summary>
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(_isValidPropertyKey, value);
        }

        /// <summary>
        /// The valid color bindable property
        /// </summary>
        public static readonly BindableProperty ValidColorBindableProperty =
            BindableProperty.Create(nameof(ValidColor), typeof(Color),
                typeof(EnhancedEntry), Color.Default);

        /// <summary>
        /// Gets or sets the color of the valid.
        /// </summary>
        /// <value>
        /// The color of the valid.
        /// </value>
        public Color ValidColor
        {
            get => (Color)GetValue(ValidColorBindableProperty);

            set => SetValue(ValidColorBindableProperty, value);
        }

        /// <summary>
        /// The in valid color bindable property
        /// </summary>
        public static readonly BindableProperty InValidColorBindableProperty =
            BindableProperty.Create(nameof(InValidColor), typeof(Color),
                typeof(EnhancedEntry), Color.Default);

        /// <summary>
        /// Gets or sets the color of the in valid.
        /// </summary>
        /// <value>
        /// The color of the in valid.
        /// </value>
        public Color InValidColor
        {
            get => (Color)GetValue(InValidColorBindableProperty);

            set => SetValue(InValidColorBindableProperty, value);
        }

        /// <summary>
        /// Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(T)" /> method on this object.
        /// </summary>
        /// <param name="bindable">The bindable object to which the behavior was attached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += handleTextChanged;
        }

        /// <summary>
        /// Handles the text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void handleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = Regex.IsMatch(e.NewTextValue,
                EmailRegularExpression, RegexOptions.IgnoreCase,
                TimeSpan.FromMilliseconds(250));
            ((Entry)sender).TextColor = IsValid ? ValidColor : InValidColor;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= handleTextChanged;
        }
    }
}
