using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace r6marketplaceclient.Converters
{
    class NameRarityShadowColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string rarity) return new SolidColorBrush(Colors.White);
            return rarity switch
            {
                "rarity_uncommon" => new SolidColorBrush(Color.FromRgb(0, 255, 0)) // Green
                ,
                "rarity_rare" => new SolidColorBrush(Color.FromRgb(0, 128, 255)) // Blue
                ,
                "rarity_superrare" => new SolidColorBrush(Color.FromRgb(255, 0, 255)) // Purple
                ,
                "rarity_legendary" => new SolidColorBrush(Color.FromRgb(255, 162, 23)) // Orange
                ,
                _ => new SolidColorBrush(Colors.White)
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
