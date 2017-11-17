using System;
using System.Globalization;
using System.Windows.Data;

namespace View_Spot_of_City.UIControls.Converter
{
    public class MyMessageBoxDefaultButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return false; }
            try
            {
                bool result = (int)value == System.Convert.ToInt32((string)parameter) ? true : false;
                return result;
            }
            catch { return false; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
