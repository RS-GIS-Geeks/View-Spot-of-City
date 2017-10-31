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
using System.Collections.ObjectModel;
using View_Spot_of_City.UIControls.Command;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// SpotViewerMaster.xaml 的交互逻辑
    /// </summary>
    public partial class SpotViewerMaster : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ViewSpot _DetailShowItem = null;

        /// <summary>
        /// 选中的内容
        /// </summary>
        public ViewSpot DetailShowItem
        {
            get { return _DetailShowItem; }
            set
            {
                _DetailShowItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DetailShowItem"));
            }
        }

        ObservableCollection<ViewSpot> _ViewSpotList = new ObservableCollection<ViewSpot>();

        /// <summary>
        /// 查询到的景点列表
        /// </summary>
        public ObservableCollection<ViewSpot> ViewSpotList
        {
            get { return _ViewSpotList; }
            set
            {
                _ViewSpotList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ViewSpotList"));
            }
        }
        
        public SpotViewerMaster()
        {
            InitializeComponent();
        }
        
        private void DataItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DetailShowItem != null)
            {
                ViewSpotViewerCommands.ShowViewSpotDetail.Execute(DetailShowItem, this);
                ArcGISMapCommands.SetScaleAndLocation.Execute(DetailShowItem, Application.Current.MainWindow);
            }
        }
    }
}
