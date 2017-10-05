using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

using View_Spot_of_City;

namespace View_Spot_of_City.Converter
{
    public class Enum2UIControl : IValueConverter
    {

        /// <summary>
        /// 主控件
        /// </summary>
        public enum MainControls : int
        {
            GMap = 0,
            ArcGISMapView = 1,
            ArcGISSceneView = 2
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null) { return null; }
            try { return (int)value == System.Convert.ToInt32((string)parameter) ? Visibility.Visible : Visibility.Collapsed; }
            catch { return null; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
