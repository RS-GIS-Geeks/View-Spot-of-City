using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View_Spot_of_City.MapView.MyGmapControl;

using GMap.NET.WindowsPresentation;
using GMap.NET;
using static View_Spot_of_City.MapView.MapView;
using System.Threading;
using View_Spot_of_City.MapView.Helpers;

namespace View_Spot_of_City.MapView.Markers
{
    /// <summary>
    /// PhoneIcon.xaml 的交互逻辑
    /// </summary>
    public partial class PhoneIcon : UserControl
    {
        MyGMap mapControl;

        GMapMarker contentViewer;

        bool isLeftButtonChecked = false;

        bool _isContentVisable = false;

        bool isContentVisiable
        {
            get
            {
                return _isContentVisable;
            }
            set
            {
                _isContentVisable = value;
                if (_isContentVisable)
                {
                    contentViewer.Shape.Visibility = Visibility.Visible;
                }
                else
                {
                    contentViewer.Shape.Visibility = Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="map">地图控件实例</param>
        /// <param name="width">图标宽度</param>
        /// <param name="height">图标高度</param>
        public PhoneIcon(MyGMap map, double width, double height, GMapMarker iconContentViewer)
        {
            this.InitializeComponent();

            mapControl = map;
            
            this.Width = width;
            this.Height = height;
            contentViewer = iconContentViewer;
            iconContentViewer.Shape.Visibility = Visibility.Collapsed;

            this.MouseEnter += PhoneIcon_MouseEnter;
            this.MouseLeave += PhoneIcon_MouseLeave;
            this.MouseWheel += PhoneIcon_MouseWheel;
            this.MouseLeftButtonDown += PhoneIcon_MouseLeftButtonDown;
            this.MouseLeftButtonUp += PhoneIcon_MouseLeftButtonUp;
            
            icon.Source = new BitmapImage(IconDictionaryHelper.iconDictionary[IconDictionaryHelper.Icons.Phone]);
            if (icon.Source.CanFreeze)
            {
                icon.Source.Freeze();
            }
        }

        private void PhoneIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void PhoneIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isLeftButtonChecked)
            {
                isContentVisiable = false;
                isLeftButtonChecked = false;
            }
            else
            {
                isContentVisiable = true;
                isLeftButtonChecked = true;
            }
            e.Handled = true;
        }

        private void PhoneIcon_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mapControl._OnMouseWheel(e);
        }

        private void PhoneIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            if(!isLeftButtonChecked)
            {
                isContentVisiable = true;
            }
        }

        private void PhoneIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!isLeftButtonChecked)
            {
                isContentVisiable = false;
            }
        }
    }
}
