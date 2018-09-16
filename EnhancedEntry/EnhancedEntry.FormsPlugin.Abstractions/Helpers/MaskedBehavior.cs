using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LeoJHarris.FormsPlugin.Abstractions.Helpers
{
    /// <summary>
    /// MaskedBehavior
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Behavior{LeoJHarris.FormsPlugin.Abstractions.EnhancedEntry}" />
    public class MaskedBehavior : Behavior<EnhancedEntry>
    {
        public static readonly BindableProperty IsValidBindableProperty =
           BindableProperty.Create(
               nameof(IsValid),
               typeof(bool),
               typeof(MaskedBehavior),
               default(bool), propertyChanged: isValidPropertyChanged);

        private string _mask = "";
        private IDictionary<int, char> _positions;

        /// <summary>
        /// Gets the entry.
        /// </summary>
        /// <value>
        /// The entry.
        /// </value>
        public EnhancedEntry Entry { get; private set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get => (bool)GetValue(IsValidBindableProperty);

            protected set => SetValue(IsValidBindableProperty, value);
        }

        /// <summary>
        /// Gets or sets the mask.
        /// </summary>
        /// <value>
        /// The mask.
        /// </value>
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                setPositions();
            }
        }

        /// <summary>
        /// Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(T)" /> method on this object.
        /// </summary>
        /// <param name="bindable">The bindable object to which the behavior was attached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAttachedTo(EnhancedEntry bindable)
        {
            bindable.TextChanged += onEntryTextChanged;

            Entry = bindable;

            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(T)" /> method and then detaches from the superclass.
        /// </summary>
        /// <param name="bindable">The bindable object from which the behavior was detached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnDetachingFrom(EnhancedEntry bindable)
        {
            bindable.TextChanged -= onEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// Determines whether [is valid property changed] [the specified bindable].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void isValidPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MaskedBehavior context && newValue is bool isValid)
            {
                context.Entry.SetIconDrawableColor(isValid ? Color.Green : Color.White);
            }
        }

        /// <summary>
        /// Determines whether [is entry valid] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="mask">The mask.</param>
        /// <returns>
        ///   <c>true</c> if [is entry valid] [the specified text]; otherwise, <c>false</c>.
        /// </returns>
        private bool isEntryValid(string text, string mask)
        {
            string numberValues = string.Empty;
            string maskValues = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsDigit(text[i]))
                    numberValues += text[i];
            }

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                    maskValues += mask[i];
            }

            return numberValues.Length == maskValues.Length;
        }

        /// <summary>
        /// Ons the entry text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void onEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            Entry entry = sender as Entry;

            string text = entry.Text;

            if (string.IsNullOrWhiteSpace(text) || _positions == null)
                return;

            if (text.Length > _mask.Length)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            IsValid = isEntryValid(text, _mask);

            foreach (var position in _positions)
            {
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }
            }

            if (entry.Text != text)
                entry.Text = text;
        }

        /// <summary>
        /// Sets the positions.
        /// </summary>
        private void setPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _positions = null;
                return;
            }

            Dictionary<int, char> list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
            {
                if (Mask[i] != 'X')
                    list.Add(i, Mask[i]);
            }

            _positions = list;
        }
    }
}
