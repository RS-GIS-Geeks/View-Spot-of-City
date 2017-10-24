using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Diagnostics;

namespace View_Spot_of_City.Form
{
    /// <summary>
    /// UserInfoDlg.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoDlg : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        App _CurrentApp = null;

        /// <summary>
        /// 当前应用程序
        /// </summary>
        public App CurrentApp
        {
            get { return _CurrentApp; }
            set
            {
                _CurrentApp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentApp"));
            }
        }

        string _LocationDescription = string.Empty;

        /// <summary>
        /// 位置描述，省市区等
        /// </summary>
        public string LocationDescription
        {
            get { return _LocationDescription; }
            set
            {
                _LocationDescription = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LocationDescription"));
            }
        }

        string _PersonalInfoDescription = string.Empty;

        /// <summary>
        /// 个人信息描述，如年龄性别星座等
        /// </summary>
        public string PersonalInfoDescription
        {
            get { return _PersonalInfoDescription; }
            set
            {
                _PersonalInfoDescription = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonalInfoDescription"));
            }
        }

        /// <summary>
        /// 创建一个实例
        /// </summary>
        public UserInfoDlg()
        {
            InitializeComponent();
            InitPramas();
        }

        /// <summary>
        /// 初始化变量
        /// </summary>
        private void InitPramas()
        {
            CurrentApp = Application.Current as App;
            CurrentApp.CurrentUser.ReplaceAllEmptyOrNullBy("-");
            LocationDescription = CurrentApp.CurrentUser.Province + " " + CurrentApp.CurrentUser.City + " " + CurrentApp.CurrentUser.Admin;
            PersonalInfoDescription = CurrentApp.CurrentUser.Age + " " + CurrentApp.CurrentUser.Gender + " " + CurrentApp.CurrentUser.Constellation;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            EditDlg editDlg = new EditDlg();
            editDlg.ShowDialog();
        }

        /// <summary>
        /// 头像点击事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Hyperlink link = new Hyperlink();
            {
                link.NavigateUri = new Uri(@"https://en.gravatar.com/");
            }
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
