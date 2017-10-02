using GMapView.MyGmapControl;
using GMap.NET.WindowsPresentation;
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

namespace GMapView.Markers
{
    /// <summary>
    /// TooltipForMap.xaml 的交互逻辑
    /// </summary>
    public partial class TooltipForMap : UserControl
    {
        GMapMarker parentMarker;

        MyGMap mapControl;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">待显示数据</param>
        /// <param name="Marker">用于此控件显示的marker，更具自身宽高改变marker位置</param>
        public TooltipForMap(List<string> data, GMapMarker Marker, MyGMap control)
        {
            InitializeComponent();
            
            this.parentMarker = Marker;

            this.mapControl = control;

            this.MouseWheel += TooltipForMap_MouseWheel;

            if (data == null)
                return;

            foreach (string subData in data)
            {
                ListBoxItem item = new ListBoxItem();
                item.Height = double.NaN;
                item.FontSize = 12;
                item.Padding = new Thickness(0, 4, 0, 4);
                item.Content = subData;
                listBox.Items.Add(item);
            }
        }

        private void TooltipForMap_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mapControl._OnMouseWheel(e);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">待显示数据</param>
        /// <param name="Marker">用于此控件显示的marker，更具自身宽高改变marker位置</param>
        public TooltipForMap(string data, GMapMarker Marker)
        {
            InitializeComponent();

            this.parentMarker = Marker;

            if (data == null)
                return;

            ListBoxItem item = new ListBoxItem();
            item.Height = double.NaN;
            item.FontSize = 12;
            item.Padding = new Thickness(0, 4, 0, 4);
            item.Content = data;
            listBox.Items.Add(item);

        }

        private void ContentOfPhoneIcon_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            parentMarker.Offset = new Point(-ActualWidth / 2, -ActualHeight - 22);
        }

        /// <summary>
        /// 更新显示内容
        /// </summary>
        /// <param name="data">显示数据</param>
        public void UpdateContents(string data)
        {
            listBox.Items.Clear();
            ListBoxItem item = new ListBoxItem();
            item.Height = double.NaN;
            item.FontSize = 12;
            item.Padding = new Thickness(0, 4, 0, 4);
            item.Content = data;
            listBox.Items.Add(item);
        }

        /// <summary>
        /// 更新显示内容
        /// </summary>
        /// <param name="data">显示数据</param>
        public void UpdateContents(List<string> data)
        {
            listBox.Items.Clear();
            foreach (string subData in data)
            {
                ListBoxItem item = new ListBoxItem();
                item.Height = double.NaN;
                item.FontSize = 12;
                item.Padding = new Thickness(0, 4, 0, 4);
                item.Content = subData;
                listBox.Items.Add(item);
            }
        }
    }
}
