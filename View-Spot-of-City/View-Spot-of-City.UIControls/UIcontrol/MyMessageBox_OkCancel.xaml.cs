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
using System.Windows.Navigation;
using System.Windows.Shapes;

using View_Spot_of_City.UIControls.Command;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// MyMessageBox_OkCancel.xaml 的交互逻辑
    /// </summary>
    public partial class MyMessageBox_OkCancel : UserControl
    {
        public MyMessageBox_OkCancel()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            MyMessageBoxCommands.OKButtonClick.Execute(null, this);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MyMessageBoxCommands.CancelButtonClick.Execute(null, this);
        }
    }
}
