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
                return rarity.ToLower() switch
                {
                    "common" => new SolidColorBrush(Colors.LightGray),
                    "uncommon" => new SolidColorBrush(Colors.LightBlue),
                    "rare" => new SolidColorBrush(Color.FromRgb(129, 212, 250)),
                    "special" => new SolidColorBrush(Color.FromRgb(206, 147, 216)),
                    "epic" => new SolidColorBrush(Color.FromRgb(179, 157, 219)),
                    "extraordinary" => new SolidColorBrush(Color.FromRgb(144, 202, 249)),
                    "mythic" => new SolidColorBrush(Color.FromRgb(255, 213, 79)),
                    "legendary" => new SolidColorBrush(Color.FromRgb(255, 171, 145)),
                    _ => new SolidColorBrush(Colors.LightGray)
                };
            }
            return new SolidColorBrush(Colors.LightGray);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}