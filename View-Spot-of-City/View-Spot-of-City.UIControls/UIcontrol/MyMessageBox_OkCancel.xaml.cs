using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using View_Spot_of_City.UIControls.Command;
using static View_Spot_of_City.UIControls.Form.MessageboxMaster;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// MyMessageBox_OkCancel.xaml 的交互逻辑
    /// </summary>
    public partial class MyMessageBox_OkCancel : UserControl, INotifyPropertyChanged
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
                if(_DefaultButton != value)
                {
                    _DefaultButton = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DefaultButton"));
                }
            }
        }

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
