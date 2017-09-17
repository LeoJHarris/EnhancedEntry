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
        public event EventHandler EventTriggered;
        
        public const string ReturnKeyPropertyName = "ReturnKeyType";

        public static readonly BindableProperty LeftIconProperty = BindableProperty.Create(nameof(LeftIconPropertyName), typeof(string), typeof(AdvancedEntry), string.Empty);

        public const string LeftIconPropertyName = "LeftIcon";

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

        public static readonly BindableProperty PaddingIconTextBindableProperty = BindableProperty.Create(nameof(PaddingIconTextPropertyName), typeof(int), typeof(AdvancedEntry), 10);

        public const string PaddingIconTextPropertyName = "PaddingLeftIcon";

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

        public static readonly BindableProperty CustomBackgroundXMLBindableProperty = BindableProperty.Create(nameof(CustomBackgroundXMLPropertyName), typeof(string), typeof(AdvancedEntry), string.Empty);

        public const string CustomBackgroundXMLPropertyName = "CustomBackgroundXML";

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

        public static readonly BindableProperty BorderWidthBindableProperty = BindableProperty.Create(nameof(BorderWidthPropertyName), typeof(int), typeof(AdvancedEntry), 0);

        public const string BorderWidthPropertyName = "BorderWidth";

        /// <summary>
        /// Borfdfs
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


        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColorPropertyName), typeof(Color), typeof(AdvancedEntry), Color.Transparent);

        public const string BorderColorPropertyName = "BorderColor";

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

        
        public static readonly BindableProperty CornerRadiusBindableProperty = BindableProperty.Create(nameof(PaddingIconTextPropertyName), typeof(int), typeof(AdvancedEntry), 5);

        public const string CornerRadiusPropertyName = "PaddingLeftIcon";

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

        public AdvancedEntry Next { get; set; }
        /// <summary>
        /// To specify the keyboard action 
        /// </summary>
        public Command KeyBoardAction { get; set; }


        private static void Goto(object sender, EventArgs e)
        {
            ((AdvancedEntry)sender)?.Next?.Focus();

            ((AdvancedEntry)sender)?.KeyBoardAction?.Execute(null);
        }

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnKeyType), typeof(ReturnKeyTypes), typeof(AdvancedEntry), ReturnKeyTypes.Done);
        public ReturnKeyTypes ReturnKeyType
        {
            get { return (ReturnKeyTypes)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        public void EntryActionFired()
        {
            this.EventTriggered?.Invoke(this, null);
        }
        public static string GetStringValue(Enum value)
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
        public static readonly BindableProperty UITextBorderStyleBindableProperty = BindableProperty.Create(nameof(UITextBorderStyle), typeof(TextBorderStyle), typeof(AdvancedEntry), TextBorderStyle.None);
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
