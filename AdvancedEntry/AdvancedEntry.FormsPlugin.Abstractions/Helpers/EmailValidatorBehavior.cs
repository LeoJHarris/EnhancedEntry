using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace LeoJHarris.AdvancedEntry.Plugin.Abstractions.Helpers
{
    public class EmailValidatorBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty EmailRegularExpressionBindableProperty =
            BindableProperty.Create(nameof(EmailRegularExpression), typeof(string),
                typeof(AdvancedEntry), @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
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

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += this.HandleTextChanged;
        }


        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            this.IsValid = Regex.IsMatch(e.NewTextValue,
                this.EmailRegularExpression, RegexOptions.IgnoreCase,
                TimeSpan.FromMilliseconds(250));
            ((Entry)sender).TextColor = this.IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= this.HandleTextChanged;
        }
    }
}
