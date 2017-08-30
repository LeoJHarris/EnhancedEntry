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

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnKeyType), typeof(ReturnKeyTypes), typeof(AdvancedEntry), ReturnKeyTypes.Done);

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

        public static readonly BindableProperty PaddingIconTextBindableProperty = BindableProperty.Create(nameof(PaddingIconTextPropertyName), typeof(int), typeof(AdvancedEntry), 25);

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

        public static readonly BindableProperty RoundedCornerXMLBindableProperty = BindableProperty.Create(nameof(RoundedCornerXMLPropertyName), typeof(string), typeof(AdvancedEntry), string.Empty);

        public const string RoundedCornerXMLPropertyName = "RoundedCornerXML";

        public string RoundedCornerXML
        {
            get
            {
                return (string)this.GetValue(RoundedCornerXMLBindableProperty);
            }

            set
            {
                this.SetValue(RoundedCornerXMLBindableProperty, value);
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

        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColorPropertyName), typeof(Color), typeof(AdvancedEntry), Color.Transparent);

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

        public Command DoneCommand { get; set; }


        private static void Goto(object sender, EventArgs e)
        {
            ((AdvancedEntry)sender)?.Next?.Focus();

            ((AdvancedEntry)sender)?.DoneCommand?.Execute(null);
        }

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
            StringValueAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            if (attrs != null && attrs.Any())
            {
                return attrs[0].Value;
            }

            return string.Empty;
        }
    }
    /// <summary>
    /// Return key types.
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
}
