namespace LeoJHarris.EnhancedEntry.Plugin.Abstractions.Helpers
{
    using System;
    using System.Globalization;

    using Xamarin.Forms;

    public class NotValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Equals(value, false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Equals(value, false);
        }
    }
}
