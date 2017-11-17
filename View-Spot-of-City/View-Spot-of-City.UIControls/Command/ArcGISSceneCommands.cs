using System.Windows.Input;

namespace View_Spot_of_City.UIControls.Command
{
    public static class ArcGISSceneCommands
    {
        /// <summary>
        /// 添加人流量数据到Scene
        /// </summary>
        public static RoutedCommand AddVisitorsData { get; set; }

        /// <summary>
        /// 改变人流量数据
        /// </summary>
        public static RoutedCommand ChangeVisitorsData { get; set; }

        /// <summary>
        /// 改变Scene的底图
        /// </summary>
        public static RoutedCommand ChangeBaseMap { get; set; }

        static ArcGISSceneCommands()
        {
            AddVisitorsData = new RoutedCommand();
            ChangeBaseMap = new RoutedCommand();
            ChangeVisitorsData = new RoutedCommand();
        }
    }
}