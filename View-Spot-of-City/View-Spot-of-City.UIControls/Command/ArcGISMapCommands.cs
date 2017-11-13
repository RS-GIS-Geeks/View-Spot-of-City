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

        /// <summary>
        /// 点击地图命令
        /// </summary>
        public static RoutedCommand SetScaleAndLocation { get; set; }

        /// <summary>
        /// 清除要素
        /// </summary>
        public static RoutedCommand ClearFeatures { get; set; }

        /// <summary>
        /// 清除回调框
        /// </summary>
        public static RoutedCommand ClearCallout { get; set; }

        /// <summary>
        /// 添加景点周边要素
        /// </summary>
        public static RoutedCommand AddViewSpotAround { get; set; }

        /// <summary>
        /// 导航到指定地点
        /// </summary>
        public static RoutedCommand NavigateToSomeWhere { get; set; }

        /// <summary>
        /// 景点周边要素
        /// </summary>
        public enum ViewSpotArounds : int
        {
            GasStation,
            TrafficStation,
            Restaurant,
            Hotel
        }

        static ArcGISMapCommands()
        {
            ShowFeatureOnMap = new RoutedCommand();
            SetScaleAndLocation = new RoutedCommand();
            ClearFeatures = new RoutedCommand();
            ClearCallout = new RoutedCommand();
            NavigateToSomeWhere = new RoutedCommand();
            AddViewSpotAround = new RoutedCommand();
        }
    }
}