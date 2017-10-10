using MahApps.Metro.Controls;
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
using System.Windows.Shapes;
using static View_Spot_of_City.UIControls.Converter.Enum2LoginUI;

namespace View_Spot_of_City.UIControls.Form
{
    /// <summary>
    /// LoginDlg.xaml 的交互逻辑
    /// </summary>
    public partial class LoginDlg : MetroWindow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private LoginControls? _Page = null;

        public LoginControls? Page
        {
            get { return _Page; }
            set
            {
                if (_Page != value)
                {
                    _Page = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Page"));
                }
            }
        }

        public LoginDlg()
        {
            InitializeComponent();
            Page = LoginControls.Login;
        }

        private void OKAndCloseFormCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("OK");
            this.DialogResult = true;
        }

        private void CancelAndCloseFormCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Cancel");
            this.DialogResult = false;
        }

        private void ChangePageCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null) return;
            Page = e.Parameter as LoginControls?;
            this.Title = (Page == LoginControls.Login) ? (Application.Current.FindResource("LoginTitle") as string) : (Application.Current.FindResource("RegisterTitle") as string);
        }
    }
}
