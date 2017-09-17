using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace LeoJHarris.Control.Abstractions
{
    /// <summary>
    /// AdvancedEntry Interface
    /// </summary>
    public class AdvancedEntry : Entry
    {
        /// <summary>
        /// XML definition for custom background (place file in drawable folder, only used for android)
        /// </summary>
        public string CustomBackgroundXML
        {
            get
            {
                return (string)this.GetValue(CustomBackgroundXMLBindableProperty);
            }

            set
            {
                this.SetValue(CustomBackgroundXMLBindableProperty, value);
            }
        }
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
        public int BorderWidth
        {
            get
            {
                return (int)this.GetValue(BorderWidthBindableProperty);
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

        private static readonly BindableProperty LeftIconProperty = BindableProperty.Create(nameof(LeftIconPropertyName), typeof(string), typeof(AdvancedEntry), string.Empty);

        private const string LeftIconPropertyName = "LeftIcon";

        private static readonly BindableProperty PaddingIconTextBindableProperty = BindableProperty.Create(nameof(PaddingIconTextPropertyName), typeof(int), typeof(AdvancedEntry), 10);

        private const string PaddingIconTextPropertyName = "PaddingLeftIcon";



        private static readonly BindableProperty CustomBackgroundXMLBindableProperty = BindableProperty.Create(nameof(CustomBackgroundXMLPropertyName), typeof(string), typeof(AdvancedEntry), string.Empty);

        private const string CustomBackgroundXMLPropertyName = "CustomBackgroundXML";



        private static readonly BindableProperty BorderWidthBindableProperty = BindableProperty.Create(nameof(BorderWidthPropertyName), typeof(int), typeof(AdvancedEntry), 0);

        private const string BorderWidthPropertyName = "BorderWidth";

        /// <summary>
        /// Borfdfs
        /// </summary>
      


        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColorPropertyName), typeof(Color), typeof(AdvancedEntry), Color.Transparent);

        private const string BorderColorPropertyName = "BorderColor";


        private static readonly BindableProperty CornerRadiusBindableProperty = BindableProperty.Create(nameof(PaddingIconTextPropertyName), typeof(int), typeof(AdvancedEntry), 5);

        private const string CornerRadiusPropertyName = "PaddingLeftIcon";
        /// <summary>
        /// Corner radius (only used for iOS)
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

        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColorPropertyName), typeof(Color), typeof(AdvancedEntry), Color.White);

        public const string BackgroundColorPropertyName = "BackgroundColor";
        /// <summary>
        /// Background color (only used for iOS)
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
