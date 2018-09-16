using System;
using System.Reflection;

namespace LeoJHarris.FormsPlugin.Abstractions
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    /// <summary>
    /// StringValAttribute
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class StringValAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValAttribute"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public StringValAttribute(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; private set; }

        public static string GetStringValue(Enum value)
        {
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetRuntimeField(value.ToString());

            return fieldInfo.GetCustomAttributes(typeof(StringValAttribute), false) is StringValAttribute[] attrs && attrs.Length > 0
                ? attrs[0].Value
                : string.Empty;
        }
    }
}
