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
namespace View_Spot_of_City.Form
{
    /// <summary>
    /// EditDlg.xaml 的交互逻辑
    /// </summary>
    public partial class EditDlg : Window, INotifyPropertyChanged
    {
        public EditDlg()
        {
            InitializeComponent();
            this.CurrentApp = Application.Current as App;
        }

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
    }

}
