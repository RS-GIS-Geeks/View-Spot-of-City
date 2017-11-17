using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace View_Spot_of_City.UIControls.Converter
{
    public class String2Image : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value as string) == string.Empty)
                return Brushes.DarkBlue;
            string uriStr = value as string;
            return new ImageBrush(new BitmapImage(new Uri(uriStr)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
