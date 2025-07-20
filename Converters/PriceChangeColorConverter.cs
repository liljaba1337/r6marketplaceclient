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
            if (value is not double priceChange) return new SolidColorBrush(Colors.White);
            return priceChange switch
            {
                > 0 => new SolidColorBrush(Color.FromRgb(46, 204, 113)),
                < 0 => new SolidColorBrush(Color.FromRgb(231, 76, 60)),
                _ => new SolidColorBrush(Color.FromRgb(176, 176, 176))
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
