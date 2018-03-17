
using System;
using LeoJHarris.FormsPlugin.Abstractions.Helpers;
using Xamarin.Forms;

namespace LeoJHarris.FormsPlugin.Abstractions
{
    public class EnhancedEntry : Entry
    {
        /// <summary>
        /// Left icon (Place images definitions in each of the drawable folders for android)
        /// </summary>
        public string LeftIcon
        {
            get => (string)GetValue(LeftIconProperty);

            set => SetValue(LeftIconProperty, value);
        }

        /// <summary>
        /// Padding for the left icon drawable
        /// </summary>
        public int PaddingLeftIcon
        {
            get => (int)GetValue(PaddingIconTextBindableProperty);

            set => SetValue(PaddingIconTextBindableProperty, value);
        }

        /// <summary>
        /// Border width
        /// </summary>
        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthBindableProperty);

            set => SetValue(BorderWidthBindableProperty, value);
        }


        private event EventHandler EventTriggered;

        public const string ReturnKeyPropertyName = "ReturnKeyType";

        private static readonly BindableProperty LeftIconProperty =
            BindableProperty.Create(nameof(LeftIcon), typeof(string), typeof(EnhancedEntry), string.Empty);


        private static readonly BindableProperty PaddingIconTextBindableProperty =
            BindableProperty.Create(nameof(PaddingLeftIcon), typeof(int), typeof(EnhancedEntry), 10);

        private static readonly BindableProperty BorderWidthBindableProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(EnhancedEntry), 0.5);



        private static readonly BindableProperty CornerRadiusBindableProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(EnhancedEntry), 5);

        /// <summary>
        /// Corner radius
        /// </summary>
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusBindableProperty);

            set => SetValue(CornerRadiusBindableProperty, value);
        }

        public static readonly BindableProperty LeftPaddingBindableProperty
            = BindableProperty.Create(nameof(LeftPadding), typeof(int), typeof(EnhancedEntry), 6);

        public int LeftPadding
        {
            get => (int)GetValue(LeftPaddingBindableProperty);

            set => SetValue(LeftPaddingBindableProperty, value);
        }
        public static readonly BindableProperty RightPaddingBindableProperty
            = BindableProperty.Create(nameof(RightPadding), typeof(int), typeof(EnhancedEntry), 6);

        /// <summary>
        /// Right padding
        /// </summary>
        public int RightPadding
        {
            get => (int)GetValue(RightPaddingBindableProperty);

            set => SetValue(RightPaddingBindableProperty, value);
        }

        public static readonly BindableProperty TopBottomPaddingBindableProperty
            = BindableProperty.Create(nameof(TopBottomPadding), typeof(int), typeof(EnhancedEntry), 0);

        /// <summary>
        /// Specified top/bottom padding
        /// </summary>
        public int TopBottomPadding
        {
            get => (int)GetValue(TopBottomPaddingBindableProperty);

            set => SetValue(TopBottomPaddingBindableProperty, value);
        }


        public static readonly BindableProperty FocusBorderColorBindableProperty
            = BindableProperty.Create(nameof(FocusBorderColor), typeof(Color),
                typeof(EnhancedEntry), Color.Transparent);

        /// <summary>
        /// Background color On focus of entry
        /// </summary>
        public Color FocusBorderColor
        {
            get => (Color)GetValue(FocusBorderColorBindableProperty);

            set => SetValue(FocusBorderColorBindableProperty, value);
        }
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(EnhancedEntry),
                Color.Transparent, propertyChanged: PropertyBorderColorChanged);

        private static void PropertyBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EnhancedEntry context)
            {
                context.FocusBorderColor = (Color)newValue;
            }
        }

        /// <summary>
        /// Border color requires <see cref="BorderWidth"/> to be set
        /// </summary>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);

            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color),
                typeof(EnhancedEntry), Color.White);

        /// <summary>
        /// Background color 
        /// </summary>
        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);

            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// GoToNextEntryOnLengthBindableProperty
        /// </summary>
        public static readonly BindableProperty GoToNextEntryOnLengthBindableProperty =
            BindableProperty.Create(nameof(GoToNextEntryOnLengthBehaviour),
                typeof(GoToNextEntryOnLengthBehaviour),
                typeof(EnhancedEntry), propertyChanged: PropertyGoToNextChanged);

        private static void PropertyGoToNextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EnhancedEntry context)
            {
                context.Behaviors.Add((GoToNextEntryOnLengthBehaviour)newValue);
            }
        }

        /// <summary>
        /// Jump next to entry behaviour, requires <see cref="NextEntry"/> to be set.
        /// </summary>
        [Obsolete("Add to Behavior collection instead")]
        public GoToNextEntryOnLengthBehaviour GoToNextEntryOnLengthBehaviour
        {
            get => (GoToNextEntryOnLengthBehaviour)GetValue(GoToNextEntryOnLengthBindableProperty);

            set => SetValue(GoToNextEntryOnLengthBindableProperty, value);
        }

        /// <summary>
        /// GoToNextEntryOnLengthBindableProperty
        /// </summary>
        public static readonly BindableProperty EmailValidatorBehaviorBindableProperty =
            BindableProperty.Create(nameof(EmailValidatorBehavior), typeof(EmailValidatorBehavior),
                typeof(EnhancedEntry), propertyChanged: PropertyEmailValidatorChanged);

        private static void PropertyEmailValidatorChanged(BindableObject bindable,
            object oldValue, object newValue)
        {
            if (bindable is EnhancedEntry context)
            {
                context.Behaviors.Add((EmailValidatorBehavior)newValue);
            }
        }

        /// <summary>
        /// Sets the email to entry.
        /// </summary>
        [Obsolete("Add to Behavior collection instead")]
        public EmailValidatorBehavior EmailValidatorBehavior
        {
            get => (EmailValidatorBehavior)GetValue(EmailValidatorBehaviorBindableProperty);
            set => SetValue(EmailValidatorBehaviorBindableProperty, value);
        }

        public static readonly BindableProperty PasswordCompareValidationBindableProperty =
            BindableProperty.Create(nameof(PasswordCompareValidation),
                typeof(PasswordCompareValidationBehavior),
                typeof(EnhancedEntry), propertyChanged: PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EnhancedEntry context)
            {
                context.Behaviors.Add((PasswordCompareValidationBehavior)newValue);
            }
        }

        public static readonly BindableProperty NextEntryBindableProperty =
            BindableProperty.Create(nameof(NextEntry), typeof(EnhancedEntry),
                typeof(EnhancedEntry));

        /// <summary>
        /// The Entry with next focus.
        /// </summary>
        public EnhancedEntry NextEntry
        {
            get => (EnhancedEntry)GetValue(NextEntryBindableProperty);

            set => SetValue(NextEntryBindableProperty, value);
        }

        /// <summary>
        /// Password compare validation
        /// </summary>
        [Obsolete("Add to Behavior collection instead")]
        public PasswordCompareValidationBehavior PasswordCompareValidation
        {
            get => (PasswordCompareValidationBehavior)
                GetValue(PasswordCompareValidationBindableProperty);
            set => SetValue(PasswordCompareValidationBindableProperty, value);
        }

        /// <summary>
        /// Enhanced entry
        /// </summary>
        public EnhancedEntry()
        {
            EventTriggered += Goto;
        }

        /// <summary>
        /// The keyboard action command, please set <see cref="ReturnKeyType"/>
        /// </summary>
        public Command KeyBoardAction { get; set; }


        private static void Goto(object sender, EventArgs e)
        {
            ((EnhancedEntry)sender)?.KeyBoardAction?.Execute(null);
            ((EnhancedEntry)sender)?.NextEntry?.Focus();
        }

        private static readonly BindableProperty ReturnTypeProperty =
            BindableProperty.Create(nameof(ReturnKeyType), typeof(ReturnKeyTypes),
                typeof(EnhancedEntry), ReturnKeyTypes.Done);

        public ReturnKeyTypes ReturnKeyType
        {
            get { return (ReturnKeyTypes)GetValue(ReturnTypeProperty); }
            set
            {
                SetValue(ReturnTypeProperty, value);
            }
        }

        public void EntryActionFired()
        {
            EventTriggered?.Invoke(this, null);
        }

        private static readonly BindableProperty UITextBorderStyleBindableProperty =
            BindableProperty.Create(nameof(UITextBorderStyle), typeof(TextBorderStyle),
                typeof(EnhancedEntry), TextBorderStyle.None);

        /// <summary>
        /// iOS border style
        /// </summary>
        public TextBorderStyle UITextBorderStyle
        {
            get => (TextBorderStyle)GetValue(UITextBorderStyleBindableProperty);
            set => SetValue(UITextBorderStyleBindableProperty, value);
        }
    }

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
}
