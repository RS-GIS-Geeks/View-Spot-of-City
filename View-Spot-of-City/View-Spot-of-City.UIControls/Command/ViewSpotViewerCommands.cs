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
        /// 显示景点列表命令
        /// </summary>
        public static RoutedCommand ShowViewSpotList { get; set; }

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

        static ViewSpotViewerCommands()
        {
            ShowViewSpotList = new RoutedCommand();
            ShowViewSpotDetail = new RoutedCommand();
            BackToMaster = new RoutedCommand();
            ShowDiscuss = new RoutedCommand();
        }
    }
}
