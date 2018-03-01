namespace LeoJHarris.EnhancedEntry.Plugin.Abstractions
{
    using System;
    using System.Linq;
    using System.Reflection;

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
            StringValAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(StringValAttribute), false) as StringValAttribute[];

            if (attrs != null && attrs.Any())
            {
                return attrs[0].Value;
            }

            return string.Empty;
        }
    }
}
