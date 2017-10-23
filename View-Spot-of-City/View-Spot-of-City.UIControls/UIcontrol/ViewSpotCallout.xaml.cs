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
using View_Spot_of_City.ClassModel;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// ViewSpotCallout.xaml 的交互逻辑
    /// </summary>
    public partial class ViewSpotCallout : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ViewSpot _ViewInfo = ViewSpot.NullViewSpot;

        /// <summary>
        /// 景点信息
        /// </summary>
        public ViewSpot ViewInfo
        {
            get { return _ViewInfo; }
            set
            {
                _ViewInfo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ViewInfo"));
            }
        }

        public ViewSpotCallout(ViewSpot viewSpotInfo)
        {
            InitializeComponent();

            ViewInfo = viewSpotInfo;
        }
    }
}
