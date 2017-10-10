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

namespace View_Spot_of_City.UIControls.Form
{
    /// <summary>
    /// LoginDlg.xaml 的交互逻辑
    /// </summary>
    public partial class LoginDlg : Window
    {
        public LoginDlg()
        {
            InitializeComponent();
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
    }
}
