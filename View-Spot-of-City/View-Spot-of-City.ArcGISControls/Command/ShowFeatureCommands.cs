using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View_Spot_of_City.ArcGISControls.Command
{
    public static class ShowFeatureCommands
    {
        /// <summary>
        /// 显示普通要素到地图上
        /// </summary>
        public static RoutedCommand ShowObjectOnMap { set; get; }

        /// <summary>
        /// 显示景点到地图上
        /// </summary>
        public static RoutedCommand ShowViewSpotOnMap { get; set; }
        
        static ShowFeatureCommands()
        {
            ShowObjectOnMap = new RoutedCommand();
            ShowViewSpotOnMap = new RoutedCommand();
        }
    }
}
