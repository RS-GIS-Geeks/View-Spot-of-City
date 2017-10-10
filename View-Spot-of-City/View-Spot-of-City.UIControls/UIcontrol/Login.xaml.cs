using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using View_Spot_of_City.UIControls.Command;
using static View_Spot_of_City.UIControls.Converter.Enum2LoginUI;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string _title = null;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }
        public Login()
        {
            InitializeComponent();
            Title = Application.Current.FindResource("LoginTitle") as string;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("点击登录按钮");
            LoginDlgCommands.OKAndCloseFormCommand.Execute(null, this);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoginDlgCommands.CancelAndCloseFormCommand.Execute(null, this);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            LoginDlgCommands.ChangePageCommand.Execute(LoginControls.Register, this);
        }
    }
}
