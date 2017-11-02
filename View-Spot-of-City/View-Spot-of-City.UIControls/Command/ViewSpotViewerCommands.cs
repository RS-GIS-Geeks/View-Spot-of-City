using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View_Spot_of_City.UIControls.Command
{
    /// <summary>
    /// 显示景点的命令集
    /// </summary>
    public static class ViewSpotViewerCommands
    {
        /// <summary>
        /// 显示景点信息命令
        /// </summary>
        public static RoutedCommand ShowViewSpotDetail { get; set; }

        /// <summary>
        /// 返回主页命令
        /// </summary>
        public static RoutedCommand BackToMaster { get; set; }

        /// <summary>
        /// 显示评论命令
        /// </summary>
        public static RoutedCommand ShowDiscuss { get; set; }

        /// <summary>
        /// 显示统计命令
        /// </summary>
        public static RoutedCommand ShowStatistics { get; set; }

        static ViewSpotViewerCommands()
        {
            ShowViewSpotDetail = new RoutedCommand();
            BackToMaster = new RoutedCommand();
            ShowDiscuss = new RoutedCommand();
            ShowStatistics = new RoutedCommand();
        }
    }
}
