using MahApps.Metro.Controls;
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

namespace View_Spot_of_City.Form
{
    /// <summary>
    /// UserInfoDlg.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoDlg : MetroWindow
    {
        public UserInfoDlg()
        {
            InitializeComponent();
            mailTextBox.Text = (Application.Current as App).CurrentUser.id == long.MinValue ? string.Empty : (Application.Current as App).CurrentUser.mail;
        }
    }
}
