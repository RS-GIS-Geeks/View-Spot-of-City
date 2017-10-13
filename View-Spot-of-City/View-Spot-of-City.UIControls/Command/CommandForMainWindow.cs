using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View_Spot_of_City.UIControls.Command
{
    public static class CommandForMainWindow
    {
        /// <summary>
        /// 更改当前用户命令
        /// </summary>
        public static RoutedCommand ChangeCurrentUserCommand { set; get; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static CommandForMainWindow()
        {
            ChangeCurrentUserCommand = new RoutedCommand();
        }
    }
}
