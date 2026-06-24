using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace CollectionConsumer.Views.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isDescending)
                return isDescending
                    ? new SolidColorBrush(Color.FromRgb(33, 150, 243))
                    : new SolidColorBrush(Colors.Gray);
            return new SolidColorBrush(Colors.Gray);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}