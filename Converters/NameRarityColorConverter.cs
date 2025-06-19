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
    class NameRarityColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string Rarity)
            {
                switch (Rarity)
                {
                    case "rarity_uncommon":
                        return new SolidColorBrush(Color.FromRgb(0, 153, 0)); // Green
                    case "rarity_rare":
                        return new SolidColorBrush(Color.FromRgb(0, 102, 204)); // Blue
                    case "rarity_superrare":
                        return new SolidColorBrush(Color.FromRgb(153, 0, 153)); // Purple
                    case "rarity_legendary":
                        return new SolidColorBrush(Color.FromRgb(255, 153, 0)); // Orange
                    default:
                        return new SolidColorBrush(Colors.White);
                }
            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
