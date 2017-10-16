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
    /// MyMessageBox_YesNo.xaml 的交互逻辑
    /// </summary>
    public partial class MyMessageBox_YesNo : UserControl
    {
        public MyMessageBox_YesNo()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            MyMessageBoxCommands.YesButtonClick.Execute(null, this);
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            MyMessageBoxCommands.NoButtonClick.Execute(null, this);
        }
    }
}
