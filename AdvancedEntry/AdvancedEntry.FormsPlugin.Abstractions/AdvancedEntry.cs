using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace LeoJHarris.AdvancedEntry.Plugin.Abstractions
{
    /// <summary>
    /// AdvancedEntry Interface
    /// </summary>
    public class AdvancedEntry : Entry
    {
        /// <summary>
        /// Left icon (Place images definitions in each of the drawable folders)
        /// </summary>
        public string LeftIcon
        {
            get
            {
                return (string)this.GetValue(LeftIconProperty);
            }

            set
            {
                this.SetValue(LeftIconProperty, value);
            }
        }
        /// <summary>
        /// Padding for the left icon drawable
        /// </summary>
        public int PaddingLeftIcon
        {
            get
            {
                return (int)this.GetValue(PaddingIconTextBindableProperty);
            }

            set
            {
                this.SetValue(PaddingIconTextBindableProperty, value);
            }
        }
        /// <summary>
        /// Border width (only used for iOS)
        /// </summary>
        public double BorderWidth
        {
            get
            {
                return (double)this.GetValue(BorderWidthBindableProperty);
            }

            set
            {
                this.SetValue(BorderWidthBindableProperty, value);
            }
        }
        /// <summary>
        /// Border color (only used for iOS)
        /// </summary>
        public Color BorderColor
        {
            get
            {
                return (Color)this.GetValue(BorderColorProperty);
            }

            set
            {
                this.SetValue(BorderColorProperty, value);
            }
        }
        private event EventHandler EventTriggered;

        public const string ReturnKeyPropertyName = "ReturnKeyType";

        private static readonly BindableProperty LeftIconProperty = BindableProperty.Create(nameof(LeftIcon), typeof(string), typeof(AdvancedEntry), string.Empty);
        

        private static readonly BindableProperty PaddingIconTextBindableProperty = BindableProperty.Create(nameof(PaddingLeftIcon), typeof(int), typeof(AdvancedEntry), 10);
        
        
        private static readonly BindableProperty BorderWidthBindableProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(AdvancedEntry), 0.5);


        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(AdvancedEntry), Color.Gray);
        
        private static readonly BindableProperty CornerRadiusBindableProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(AdvancedEntry), 5);
        
        /// <summary>
        /// Corner radius
        /// </summary>
        public int CornerRadius
        {
            get
            {
                return (int)this.GetValue(CornerRadiusBindableProperty);
            }

            set
            {
                this.SetValue(CornerRadiusBindableProperty, value);
            }
        }

        public static readonly BindableProperty LeftPaddingBindableProperty = BindableProperty.Create(nameof(LeftPadding), typeof(int), typeof(AdvancedEntry), 6);

        public int LeftPadding
        {
            get
            {
                return (int)this.GetValue(LeftPaddingBindableProperty);
            }

            set
            {
                this.SetValue(LeftPaddingBindableProperty, value);
            }
        }
        public static readonly BindableProperty RightPaddingBindableProperty = BindableProperty.Create(nameof(RightPadding), typeof(int), typeof(AdvancedEntry), 6);
        
        public int RightPadding
        {
            get
            {
                return (int)this.GetValue(RightPaddingBindableProperty);
            }

            set
            {
                this.SetValue(RightPaddingBindableProperty, value);
            }
        }

        public static readonly BindableProperty TopBottomPaddingBindableProperty = BindableProperty.Create(nameof(TopBottomPadding), typeof(int), typeof(AdvancedEntry), 0);

        public int TopBottomPadding
        {
            get
            {
                return (int)this.GetValue(TopBottomPaddingBindableProperty);
            }

            set
            {
                this.SetValue(TopBottomPaddingBindableProperty, value);
            }
        }


        public static readonly BindableProperty FocusBorderColorBindableProperty = BindableProperty.Create(nameof(FocusBorderColor), typeof(Color), typeof(AdvancedEntry), Color.Transparent);

        /// <summary>
        /// Background color 
        /// </summary>

        public Color FocusBorderColor
        {
            get
            {
                return (Color)this.GetValue(FocusBorderColorBindableProperty);
            }

            set
            {
                this.SetValue(FocusBorderColorBindableProperty, value);
            }
        }

        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(AdvancedEntry), Color.White);
        
        /// <summary>
        /// Background color 
        /// </summary>

        public Color BackgroundColor
        {
            get
            {
                return (Color)this.GetValue(BackgroundColorProperty);
            }

            set
            {
                this.SetValue(BackgroundColorProperty, value);
            }
        }

        public AdvancedEntry()
        {
            EventTriggered += Goto;
        }
        /// <summary>
        /// The Entry with next highlight
        /// </summary>

        public AdvancedEntry Next { get; set; }
        /// <summary>
        /// The keyboard action command.
        /// </summary>
        public Command KeyBoardAction { get; set; }


        private static void Goto(object sender, EventArgs e)
        {
            ((AdvancedEntry)sender)?.Next?.Focus();

            ((AdvancedEntry)sender)?.KeyBoardAction?.Execute(null);
        }

        private static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnKeyType), typeof(ReturnKeyTypes), typeof(AdvancedEntry), ReturnKeyTypes.Done);
        public ReturnKeyTypes ReturnKeyType
        {
            get { return (ReturnKeyTypes)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        public void EntryActionFired()
        {
            this.EventTriggered?.Invoke(this, null);
        }
        private static string GetStringValue(Enum value)
        {
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetRuntimeField(value.ToString());
            StringValAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(StringValAttribute), false) as StringValAttribute[];

            if (attrs != null && attrs.Any())
            {
                return attrs[0].Value;
            }

            return string.Empty;
        }
        private static readonly BindableProperty UITextBorderStyleBindableProperty = BindableProperty.Create(nameof(UITextBorderStyle), typeof(TextBorderStyle), typeof(AdvancedEntry), TextBorderStyle.None);
        /// <summary>
        /// Used for iOS only to determine the border style.
        /// </summary>
        public TextBorderStyle UITextBorderStyle
        {
            get { return (TextBorderStyle)GetValue(UITextBorderStyleBindableProperty); }
            set { SetValue(UITextBorderStyleBindableProperty, value); }
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
