using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace CollectionConsumer.Views.Converters
{
    public class RarityToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string rarity)
            {
                var color = rarity.ToLower() switch
                {
                    "common" => Colors.Gray,
                    "uncommon" => Colors.LightBlue,
                    "rare" => Color.FromRgb(129, 212, 250),
                    "special" => Color.FromRgb(206, 147, 216),
                    "epic" => Color.FromRgb(179, 157, 219),
                    "extraordinary" => Color.FromRgb(144, 202, 249),
                    "mythic" => Color.FromRgb(245, 39, 108),
                    "legendary" => Color.FromRgb(245, 73, 39),
                    _ => Colors.Gray
                };

                if (parameter?.ToString() == "Shadow")
                    return color;

                return new SolidColorBrush(color);
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}