using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace View_Spot_of_City.UIControls.Converter
{
    public class MyMessageBoxButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return null; }
            try { return (int)value == System.Convert.ToInt32((string)parameter) ? Visibility.Visible : Visibility.Collapsed; }
            catch { return null; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
