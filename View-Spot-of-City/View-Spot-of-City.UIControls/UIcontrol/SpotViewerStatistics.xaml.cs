using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.UIControls.Command;
using View_Spot_of_City.UIControls.VisualizationControl;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// SpotViewerStatistics.xaml 的交互逻辑
    /// </summary>
    public partial class SpotViewerStatistics : UserControl, INotifyPropertyChanged
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

        ObservableCollection<VisualizationControlBase> _ControlList = new ObservableCollection<VisualizationControlBase>();

        /// <summary>
        /// 评论列表
        /// </summary>
        public ObservableCollection<VisualizationControlBase> ControlList
        {
            get { return _ControlList; }
            set
            {
                _ControlList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ControlList"));
            }
        }

        public SpotViewerStatistics()
        {
            InitializeComponent();
        }

        private void BackToDetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetailShowItem == null)
                ViewSpotViewerCommands.BackToMaster.Execute(null, this);
            else
                ViewSpotViewerCommands.ShowViewSpotDetail.Execute(DetailShowItem, this);
        }
    }
}
