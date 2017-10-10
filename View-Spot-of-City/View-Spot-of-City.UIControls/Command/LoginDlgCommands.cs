using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View_Spot_of_City.UIControls.Command
{
    public static class LoginDlgCommands
    {
        /// <summary>
        /// 验证成功并关闭窗口命令
        /// </summary>
        public static RoutedCommand OKAndCloseFormCommand { set; get; }

        /// <summary>
        /// 取消并关闭窗口命令
        /// </summary>
        public static RoutedCommand CancelAndCloseFormCommand { set; get; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static LoginDlgCommands()
        {
            OKAndCloseFormCommand = new RoutedCommand();
            CancelAndCloseFormCommand = new RoutedCommand();
        }
    }
}
