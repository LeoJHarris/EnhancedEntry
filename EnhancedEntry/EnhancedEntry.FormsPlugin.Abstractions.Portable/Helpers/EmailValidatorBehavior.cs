namespace LeoJHarris.EnhancedEntry.Plugin.Abstractions.Helpers
{
    using System;
    using System.Text.RegularExpressions;

    using Xamarin.Forms;

    public class EmailValidatorBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty EmailRegularExpressionBindableProperty =
            BindableProperty.Create(nameof(EmailRegularExpression), typeof(string),
                typeof(EnhancedEntry), @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
        
        public string EmailRegularExpression
        {
            get => (string)this.GetValue(EmailRegularExpressionBindableProperty);

            set => this.SetValue(EmailRegularExpressionBindableProperty, value);
        }

        private static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NumberValidatorBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        /// <summary>
        /// Is valid
        /// </summary>
        public bool IsValid
        {
            get => (bool)this.GetValue(IsValidProperty);
            private set => this.SetValue(IsValidPropertyKey, value);
        }

        public static readonly BindableProperty ValidColorBindableProperty =
            BindableProperty.Create(nameof(ValidColor), typeof(Color),
                typeof(EnhancedEntry), Color.Default);

        public Color ValidColor
        {
            get => (Color)this.GetValue(ValidColorBindableProperty);

            set => this.SetValue(ValidColorBindableProperty, value);
        }

        public static readonly BindableProperty InValidColorBindableProperty =
            BindableProperty.Create(nameof(InValidColor), typeof(Color),
                typeof(EnhancedEntry), Color.Default);

        public Color InValidColor
        {
            get => (Color)this.GetValue(InValidColorBindableProperty);

            set => this.SetValue(InValidColorBindableProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += this.HandleTextChanged;
        }


        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            this.IsValid = Regex.IsMatch(e.NewTextValue,
                this.EmailRegularExpression, RegexOptions.IgnoreCase,
                TimeSpan.FromMilliseconds(250));
            ((Entry)sender).TextColor = this.IsValid ? ValidColor : InValidColor;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= this.HandleTextChanged;
        }
    }
}
