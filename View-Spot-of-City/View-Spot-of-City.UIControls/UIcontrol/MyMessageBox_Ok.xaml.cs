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
using System.ComponentModel;

using View_Spot_of_City.UIControls.Command;
using static View_Spot_of_City.UIControls.Form.MessageboxMaster;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// MyMessageBox_Ok.xaml 的交互逻辑
    /// </summary>
    public partial class MyMessageBox_Ok : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 默认响应回车的按钮
        /// </summary>
        MyMessageBoxButton? _DefaultButton = null;
        /// <summary>
        /// 默认响应回车的按钮
        /// </summary>
        public MyMessageBoxButton? DefaultButton
        {
            get { return _DefaultButton; }
            set
            {
                if (_DefaultButton != value)
                {
                    _DefaultButton = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DefaultButton"));
                }
            }
        }

        public MyMessageBox_Ok()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            MyMessageBoxCommands.OKButtonClick.Execute(null, this);
        }
    }
}
