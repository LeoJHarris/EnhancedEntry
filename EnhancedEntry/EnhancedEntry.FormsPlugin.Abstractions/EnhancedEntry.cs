using System;
using Xamarin.Forms;

namespace LeoJHarris.FormsPlugin.Abstractions
{
    /// <summary>
    /// Specify the keyboard action button text.
    /// </summary>
    public enum ReturnKeyTypes
    {
        Default,
        Go,
        Google,
        Join,
        Next,
        Route,
        Search,
        Send,
        Yahoo,
        Done,
        EmergencyCall,
        Continue
    }

    /// <summary>
    /// Used for iOS only to determine the border style.
    /// </summary>
    public enum TextBorderStyle : long
    {
        None = 0,
        Line = 1,
        Bezel = 2,
        RoundedRect = 3
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Entry" />
    public class EnhancedEntry : Entry
    {
        public const string ReturnKeyPropertyName = "ReturnKeyType";

        new public static readonly BindableProperty BackgroundColorProperty =
                    BindableProperty.Create(nameof(BackgroundColor), typeof(Color),
                        typeof(EnhancedEntry), Color.White);

        public static readonly BindableProperty BorderColorProperty =
                    BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(EnhancedEntry),
                        Color.Transparent, propertyChanged: propertyBorderColorChanged);

        public static readonly BindableProperty FocusBorderColorBindableProperty
                    = BindableProperty.Create(nameof(FocusBorderColor), typeof(Color),
                        typeof(EnhancedEntry), Color.Transparent);

        public static readonly BindableProperty LeftPaddingBindableProperty
                    = BindableProperty.Create(nameof(LeftPadding), typeof(int), typeof(EnhancedEntry), 6);

        public static readonly BindableProperty EntryHeightBindableProperty
                    = BindableProperty.Create(nameof(EntryHeightBindableProperty), typeof(int), typeof(EnhancedEntry), 30);

        public static readonly BindableProperty NextEntryBindableProperty =
                    BindableProperty.Create(nameof(NextEntry), typeof(EnhancedEntry),
                        typeof(EnhancedEntry));

        public static readonly BindableProperty RightPaddingBindableProperty
                    = BindableProperty.Create(nameof(RightPadding), typeof(int), typeof(EnhancedEntry), 6);

        public static readonly BindableProperty TopBottomPaddingBindableProperty
                    = BindableProperty.Create(nameof(TopBottomPadding), typeof(int), typeof(EnhancedEntry), 0);

        private static readonly BindableProperty _borderWidthBindableProperty =
                    BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(EnhancedEntry), 0.5);

        private static readonly BindableProperty _cornerRadiusBindableProperty =
                    BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(EnhancedEntry), 5);

        private static readonly BindableProperty _leftIconProperty =
                    BindableProperty.Create(nameof(LeftIcon), typeof(string), typeof(EnhancedEntry), string.Empty);

        private static readonly BindableProperty _paddingIconTextBindableProperty =
                    BindableProperty.Create(nameof(PaddingLeftIcon), typeof(int), typeof(EnhancedEntry), 10);

        private static readonly BindableProperty _returnTypeProperty =
                    BindableProperty.Create(nameof(ReturnKeyType), typeof(ReturnKeyTypes),
                        typeof(EnhancedEntry), ReturnKeyTypes.Done);

        private static readonly BindableProperty _uITextBorderStyleBindableProperty =
                    BindableProperty.Create(nameof(UITextBorderStyle), typeof(TextBorderStyle),
                        typeof(EnhancedEntry), TextBorderStyle.None);

        /// <summary>
        /// Enhanced entry
        /// </summary>
        public EnhancedEntry()
        {
            EventTriggered += @goto;
        }

        /// <summary>
        /// Occurs when event triggered.
        /// </summary>
        public event EventHandler EventTriggered;

        /// <summary>
        /// Occurs when [icon drawable color changed].
        /// </summary>
        public event EventHandler<ColorEventArgs> IconDrawableColorChanged;

        /// <summary>
        /// Background color 
        /// </summary>
        new public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);

            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// Border color requires <see cref="BorderWidth"/> to be set
        /// </summary>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);

            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// Border width
        /// </summary>
        public double BorderWidth
        {
            get => (double)GetValue(_borderWidthBindableProperty);

            set => SetValue(_borderWidthBindableProperty, value);
        }

        /// <summary>
        /// Corner radius
        /// </summary>
        public int CornerRadius
        {
            get => (int)GetValue(_cornerRadiusBindableProperty);

            set => SetValue(_cornerRadiusBindableProperty, value);
        }

        /// <summary>
        /// Background color On focus of entry
        /// </summary>
        public Color FocusBorderColor
        {
            get => (Color)GetValue(FocusBorderColorBindableProperty);

            set => SetValue(FocusBorderColorBindableProperty, value);
        }

        /// <summary>
        /// The keyboard action command, please set <see cref="ReturnKeyType"/>
        /// </summary>
        public Command KeyBoardAction { get; set; }

        /// <summary>
        /// Left icon (Place images definitions in each of the drawable folders for android)
        /// </summary>
        public string LeftIcon
        {
            get => (string)GetValue(_leftIconProperty);

            set => SetValue(_leftIconProperty, value);
        }

        /// <summary>
        /// Gets or sets the left padding.
        /// </summary>
        /// <value>
        /// The left padding.
        /// </value>
        public int LeftPadding
        {
            get => (int)GetValue(LeftPaddingBindableProperty);

            set => SetValue(LeftPaddingBindableProperty, value);
        }

        /// <summary>
        /// Gets or sets the height of the entry.
        /// </summary>
        /// <value>
        /// The height of the entry.
        /// </value>
        public int EntryHeight
        {
            get => (int)GetValue(EntryHeightBindableProperty);

            set => SetValue(EntryHeightBindableProperty, value);
        }

        /// <summary>
        /// The Entry with next focus.
        /// </summary>
        public EnhancedEntry NextEntry
        {
            get => (EnhancedEntry)GetValue(NextEntryBindableProperty);

            set => SetValue(NextEntryBindableProperty, value);
        }

        /// <summary>
        /// Padding for the left icon drawable
        /// </summary>
        public int PaddingLeftIcon
        {
            get => (int)GetValue(_paddingIconTextBindableProperty);

            set => SetValue(_paddingIconTextBindableProperty, value);
        }

        /// <summary>
        /// Gets or sets the type of the return key.
        /// </summary>
        /// <value>
        /// The type of the return key.
        /// </value>
        public ReturnKeyTypes ReturnKeyType
        {
            get { return (ReturnKeyTypes)GetValue(_returnTypeProperty); }
            set { SetValue(_returnTypeProperty, value); }
        }

        /// <summary>
        /// Right padding
        /// </summary>
        public int RightPadding
        {
            get => (int)GetValue(RightPaddingBindableProperty);
            set => SetValue(RightPaddingBindableProperty, value);
        }

        /// <summary>
        /// Specified top/bottom padding
        /// </summary>
        public int TopBottomPadding
        {
            get => (int)GetValue(TopBottomPaddingBindableProperty);
            set => SetValue(TopBottomPaddingBindableProperty, value);
        }

        /// <summary>
        /// iOS border style
        /// </summary>
        public TextBorderStyle UITextBorderStyle
        {
            get => (TextBorderStyle)GetValue(_uITextBorderStyleBindableProperty);
            set => SetValue(_uITextBorderStyleBindableProperty, value);
        }

        public void EntryActionFired()
        {
            EventTriggered?.Invoke(this, null);
        }

        /// <summary>
        /// Sets the color of the icon drawable.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="isValid">if set to <c>true</c> [is valid].</param>
        public void SetIconDrawableColor(Color color, bool isValid)
        {
            IconDrawableColorChanged?.Invoke(this, new ColorEventArgs()
            {
                Color = color,
                IsValid = isValid
            });
        }

        /// <summary>
        /// Go to the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private static void @goto(object sender, EventArgs e)
        {
            ((EnhancedEntry)sender)?.KeyBoardAction?.Execute(null);
            ((EnhancedEntry)sender)?.NextEntry?.Focus();
        }

        /// <summary>
        /// Properties the border color changed.
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void propertyBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EnhancedEntry context)
            {
                context.FocusBorderColor = (Color)newValue;
            }
        }

        /// <summary>
        /// ColorEventArgs
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class ColorEventArgs : EventArgs
        {
            public Color Color { get; set; }
            public bool IsValid { get; set; }
        }
    }
}
