using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace View_Spot_of_City.UIControls.Command
{
    public static class ArcGISMapCommands
    {
        /// <summary>
        /// 在地图上显示要素
        /// </summary>
        public static RoutedCommand ShowFeatureOnMap { get; set; }

        static ArcGISMapCommands()
        {
            ShowFeatureOnMap = new RoutedCommand();
        }
    }
}