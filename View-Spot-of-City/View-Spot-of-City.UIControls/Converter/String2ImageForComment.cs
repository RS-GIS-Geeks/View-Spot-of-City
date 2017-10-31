using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace View_Spot_of_City.UIControls.Converter
{
    class String2ImageForComment : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value as string) == string.Empty || (value as string) == "-")
                return null;
            string uriStr = value as string;
            return new ImageBrush(new BitmapImage(new Uri(uriStr)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
