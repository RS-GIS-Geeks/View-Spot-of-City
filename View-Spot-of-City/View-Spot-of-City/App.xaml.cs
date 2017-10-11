using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using View_Spot_of_City.helper;
using View_Spot_of_City.UIControls.Helper;
using View_Spot_of_City.UIControls.Progress;
using View_Spot_of_City.UIControls.Form;

using System.Windows.Media;

namespace View_Spot_of_City
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //应用程序关闭时，才 System.Windows.Application.Shutdown 调用
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            //验证License
            if (!RegisterMaster.CanStart())
            {
                Environment.Exit(0);
            }

            //登录
            LoginDlg logindlg = new LoginDlg();
            logindlg.ShowDialog();

            base.OnStartup(e);
        }
    }
}
