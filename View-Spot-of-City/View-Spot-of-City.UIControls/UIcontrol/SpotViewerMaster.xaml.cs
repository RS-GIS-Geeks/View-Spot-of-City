using System.Windows;
using System.Windows.Controls;
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
                ArcGISMapCommands.SetScaleAndLocation.Execute(DetailShowItem, Application.Current.MainWindow);
                ViewSpotViewerCommands.ShowViewSpotDetail.Execute(DetailShowItem, this);
            }
        }
    }
}
