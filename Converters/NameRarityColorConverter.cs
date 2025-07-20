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
    class CompileTimeRarityColorConverter
    {
        public static Color Convert(string rarity)
        {
            return rarity switch
            {
                "rarity_uncommon" => Color.FromRgb(0, 153, 0),
                "rarity_rare" => Color.FromRgb(0, 102, 204),
                "rarity_superrare" => Color.FromRgb(153, 0, 153),
                "rarity_legendary" => Color.FromRgb(255, 153, 0),
                _ => Colors.White
            };
        }
    }
    class NameRarityColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string rarity) return new SolidColorBrush(Colors.White);
            return rarity switch
            {
                "rarity_uncommon" => new SolidColorBrush(Color.FromRgb(0, 153, 0)) // Green
                ,
                "rarity_rare" => new SolidColorBrush(Color.FromRgb(0, 102, 204)) // Blue
                ,
                "rarity_superrare" => new SolidColorBrush(Color.FromRgb(153, 0, 153)) // Purple
                ,
                "rarity_legendary" => new SolidColorBrush(Color.FromRgb(255, 153, 0)) // Orange
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
