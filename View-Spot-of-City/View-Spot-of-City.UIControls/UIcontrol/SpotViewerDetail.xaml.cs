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
using View_Spot_of_City.UIControls.Command;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// SpotViewerDetail.xaml 的交互逻辑
    /// </summary>
    public partial class SpotViewerDetail : UserControl, INotifyPropertyChanged
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

        /// <summary>
        /// 图片集，只能有3张
        /// </summary>
        public string[] ImageUrls = new string[3];

        /// <summary>
        /// 当前显示图片的索引
        /// </summary>
        public short CurrentImageIndex = 0;

        string _CurrentImageUrl = string.Empty;

        /// <summary>
        /// 当前显示的图片URL
        /// </summary>
        public string CurrentImageUrl
        {
            get { return _CurrentImageUrl; }
            set
            {
                _CurrentImageUrl = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentImageUrl"));
            }
        }

        public SpotViewerDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 返回主页按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToMasterButton_Click(object sender, RoutedEventArgs e)
        {
            DetailShowItem = null;
            ViewSpotViewerCommands.BackToMaster.Execute(null, this);
        }

        /// <summary>
        /// 查看评论按钮点击响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewDiscussButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
