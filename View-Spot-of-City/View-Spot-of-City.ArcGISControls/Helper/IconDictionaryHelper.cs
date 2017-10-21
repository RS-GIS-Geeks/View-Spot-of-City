using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View_Spot_of_City.ArcGISControls.Helper
{
    public static class IconDictionaryHelper
    {
        public enum Icons : int
        {
            pin = 0,
            pin_blue = 1,
            start = 2,
            end = 3
        }

        public static Dictionary<Icons, Uri> IconDictionary = new Dictionary<Icons, Uri>(5);

        static IconDictionaryHelper()
        {
            IconDictionary.Add(Icons.pin, new Uri("pack://application:,,,/View-Spot-of-City.ArcGISControls;component/Icon/pin.png"));
        }
    }
}
