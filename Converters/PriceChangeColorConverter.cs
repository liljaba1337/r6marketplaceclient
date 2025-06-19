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
    class PriceChangeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double priceChange)
            {
                if (priceChange > 0)
                    return new SolidColorBrush(Color.FromRgb(46, 204, 113));
                if (priceChange < 0)
                    return new SolidColorBrush(Color.FromRgb(231, 76, 60));
                return new SolidColorBrush(Color.FromRgb(176, 176, 176));
            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
