using System;
using System.Linq;
using System.Reflection;

namespace LeoJHarris.Control.Abstractions
{
    public class StringValueAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValueAttribute"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public StringValueAttribute(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; private set; }

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
}
