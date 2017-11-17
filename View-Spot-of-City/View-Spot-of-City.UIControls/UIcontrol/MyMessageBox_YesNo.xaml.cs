using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using View_Spot_of_City.UIControls.Command;
using static View_Spot_of_City.UIControls.Form.MessageboxMaster;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// MyMessageBox_YesNo.xaml 的交互逻辑
    /// </summary>
    public partial class MyMessageBox_YesNo : UserControl, INotifyPropertyChanged
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
