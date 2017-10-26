using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using static System.Configuration.ConfigurationManager;

using View_Spot_of_City.Helper;
using View_Spot_of_City.UIControls.Helper;
using View_Spot_of_City.UIControls.Progress;
using View_Spot_of_City.ClassModel;
using View_Spot_of_City.Form;
using System.Windows.Input;
using View_Spot_of_City.UIControls.Theme;

namespace View_Spot_of_City
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 当前在线用户
        /// </summary>
        UserInfo _currentUser;
        public UserInfo CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentUser"));
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //MetroThemeMaster.CreateAppStyleBy(this, (Current.FindResource("PrimaryHueLightBrush") as SolidColorBrush).Color, true);

            //应用程序关闭时，才 System.Windows.Application.Shutdown 调用
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            //验证License
            if (!RegisterMaster.CanStart())
                //Environment.Exit(0);
                this.Shutdown();

            //登录
            bool? loginDlgResult = (new LoginDlg(AppSettings["DEFAULT_USER_MAIL"])).ShowDialog();
            if (!loginDlgResult.HasValue || !loginDlgResult.Value)
                //Environment.Exit(0);
                this.Shutdown();

            base.OnStartup(e);
        }
    }
}
