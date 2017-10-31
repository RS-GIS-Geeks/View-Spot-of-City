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
using View_Spot_of_City.UIControls.Form;

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

        /// <summary>
        /// 图片集，只能有3张
        /// </summary>
        string[] ImageUrls = new string[3];

        /// <summary>
        /// 当前显示图片的索引
        /// </summary>
        short CurrentImageIndex = 0;

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

        string _LocationDescribe = string.Empty;

        /// <summary>
        /// 景点位置描述
        /// </summary>
        public string LocationDescribe
        {
            get { return _LocationDescribe; }
            set
            {
                _LocationDescribe = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LocationDescribe"));
            }
        }

        /// <summary>
        /// 构造一个回调框
        /// </summary>
        /// <param name="viewSpotInfo"></param>
        public ViewSpotCallout(ViewSpot viewSpotInfo)
        {
            InitializeComponent();

            ViewInfo = viewSpotInfo;
            LocationDescribe = ViewInfo.pname + " " + ViewInfo.cityname + " " + ViewInfo.adminname + " " + ViewInfo.address;
            ImageUrls[0] = ViewInfo.photourl1 ?? string.Empty;
            ImageUrls[1] = ViewInfo.photourl2 ?? string.Empty;
            ImageUrls[2] = ViewInfo.photourl3 ?? string.Empty;
            CurrentImageUrl = ImageUrls[CurrentImageIndex];
        }

        private void GotoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageboxMaster.Show("点击'去这里'按钮", "CS-Tao测试");
        }

        private void PreButton_Click(object sender, RoutedEventArgs e)
        {
            //循环显示图片
            if (CurrentImageIndex == 0)
                CurrentImageIndex = 3;
            CurrentImageIndex--;
            CurrentImageIndex %= 3;
            CurrentImageUrl = ImageUrls[CurrentImageIndex];
            e.Handled = true;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            //循环显示图片
            CurrentImageIndex++;
            CurrentImageIndex %= 3;
            CurrentImageUrl = ImageUrls[CurrentImageIndex];
            e.Handled = true;
        }

        private void mainControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                PreButton_Click(null, null);
            else if (e.Key == Key.Right)
                NextButton_Click(null, null);
            e.Handled = true;
        }
    }
}
