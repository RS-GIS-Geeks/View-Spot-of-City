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
    public partial class TooltipForMapPin : UserControl
    {
        GMapMarker parentMarker;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">待显示数据</param>
        /// <param name="Marker">用于此控件显示的marker，更具自身宽高改变marker位置</param>
        public TooltipForMapPin(string data, GMapMarker Marker)
        {
            InitializeComponent();

            this.parentMarker = Marker;

            if (data == null)
                return;
            textBlock.Text = data;
        }

        private void ContentOfPhoneIcon_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            parentMarker.Offset = new Point(-ActualWidth / 2, -28);
        }
    }
}
