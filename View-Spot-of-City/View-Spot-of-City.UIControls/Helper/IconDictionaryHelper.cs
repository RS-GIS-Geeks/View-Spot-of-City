using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View_Spot_of_City.UIControls.Helper
{
    public static class IconDictionaryHelper
    {
        public enum Icons : int
        {
            pin = 0,
            pin_blue = 1,
            start = 2,
            end = 3,
            gas_station,
            traffic_station,
            restaurant,
            hotel
        }

        //图标字典
        public static Dictionary<Icons, Uri> IconDictionary = new Dictionary<Icons, Uri>(5);

        static IconDictionaryHelper()
        {
            IconDictionary.Add(Icons.pin, new Uri("pack://application:,,,/View-Spot-of-City.UIControls;component/Icon/pin.png"));
            IconDictionary.Add(Icons.pin_blue, new Uri("pack://application:,,,/View-Spot-of-City.UIControls;component/Icon/pin_bule.png"));
            IconDictionary.Add(Icons.start, new Uri("pack://application:,,,/View-Spot-of-City.UIControls;component/Icon/start.png"));
            IconDictionary.Add(Icons.end, new Uri("pack://application:,,,/View-Spot-of-City.UIControls;component/Icon/end.png"));
            IconDictionary.Add(Icons.gas_station, new Uri("pack://application:,,,/View-Spot-of-City.UIControls;component/Icon/GasStation.png"));
            IconDictionary.Add(Icons.traffic_station, new Uri("pack://application:,,,/View-Spot-of-City.UIControls;component/Icon/station.png"));
            IconDictionary.Add(Icons.restaurant, new Uri("pack://application:,,,/View-Spot-of-City.UIControls;component/Icon/restaurant2.png"));
            IconDictionary.Add(Icons.hotel, new Uri("pack://application:,,,/View-Spot-of-City.UIControls;component/Icon/hotel1.png"));
        }
    }
}
