namespace LeoJHarris.EnhancedEntry.Plugin.Abstractions.Helpers
{
    using System;
    using System.Globalization;

    using Xamarin.Forms;

    internal class BooleanToObjectConverter<T> : IValueConverter
    {

        public T FalseObject { get; set; }

        public T TrueObject { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TrueObject : FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((T)value).Equals(TrueObject);
        }
    }
}
