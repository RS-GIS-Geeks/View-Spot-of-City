using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Collections.ObjectModel;

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.Language.Language;

namespace View_Spot_of_City.UIControls.Converter
{
    public class ViewSpotCostConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double cost = (double)value;
            string precost = LanguageDictionaryHelper.GetString("ShowSpot_PreCost");
            if (precost == "$")
                cost /= 6;
            return precost + cost.ToString("f1");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
