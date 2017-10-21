using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.Collections.Generic;

using Config = System.Configuration.ConfigurationManager;
using View_Spot_of_City.UIControls.Form;

namespace View_Spot_of_City.MapView
{
    /// <summary>
    /// MapView.xaml 的交互逻辑
    /// </summary>
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();

            InitMapControl();
        }

        private void InitMapControl()
        {
            // config map
            //mapControl.MapProvider = GMapProviders.OpenStreetMap;
            myGMapProvider = GeoServerProvider.Instance;
            mapControl.MapProvider = myGMapProvider;
            mapControl.Position = new PointLatLng(Convert.ToDouble(Config.AppSettings["MAP_CENTER_LAT"]), Convert.ToDouble(Config.AppSettings["MAP_CENTER_LNG"]));
            mapControl.ShowCenter = false;
            mapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            mapControl.MinZoom = (int)Convert.ToDouble(Config.AppSettings["MAP_MIN_ZOOM"]);
            mapControl.MaxZoom = (int)Convert.ToDouble(Config.AppSettings["MAP_MAX_ZOOM"]);
            mapControl.Zoom = Convert.ToDouble(Config.AppSettings["MAP_ZOOM"]);
            mapControl.ShowCenter = false;
            mapControl.DragButton = MouseButton.Left;
            mapControl.Position = new PointLatLng(Convert.ToDouble(Config.AppSettings["MAP_CENTER_LAT"]), Convert.ToDouble(Config.AppSettings["MAP_CENTER_LNG"]));
            mapControl.Markers.Add(new GMapMarker(mapControl.Position));

            // map events
            mapControl.MouseMove += new MouseEventHandler(mapControl_MouseMove);
            mapControl.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);
            mapControl.MouseEnter += new MouseEventHandler(mapControl_MouseEnter);
            mapControl.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);
            mapControl.MouseRightButtonDown += new MouseButtonEventHandler(mapControl_MouseRightButtonDown);
            mapControl.MouseRightButtonUp += new MouseButtonEventHandler(mapControl_MouseRightButtonUp);
            mapControl.MouseDoubleClick += new MouseButtonEventHandler(mapControl_MouseDoubleClickAsync);

            // set mouse marker
            mouseMarker = new GMapMarker(mapControl.Position);
            mouseMarker.ZIndex = int.MaxValue;
            mouseMarker.Shape = null;
            mouseMarker.Offset = new Point(-4, -4);
            mapControl.Markers.Add(mouseMarker);

            mouseSubMarker = new GMapMarker(mapControl.Position);
            mouseSubMarker.ZIndex = int.MaxValue;
            mouseSubMarker.Shape = null;
            mapControl.Markers.Add(mouseSubMarker);

            mouseTip = new GMapMarker(mapControl.Position);
            mouseTip.ZIndex = int.MaxValue;
            mouseTip.Shape = null;
            mapControl.Markers.Add(mouseTip);
        }

        /// <summary>
        /// Clear sketch,initialize operation and some variable
        /// 清除草图图层、初始化操作、初始化部分变量
        /// </summary>
        private void InitOperation()
        {
            polyLinePoints.Clear();
            polygonPoints.Clear();

            operationNow = (int)Operations.Nothing;

            mouseMarker.Shape = null;
            mouseMarker.Position = new PointLatLng(-1, -1);

            mouseSubMarker.Shape = null;
            mouseSubMarker.Position = new PointLatLng(-1, -1);

            mouseTip.Shape = null;
            mouseTip.Position = new PointLatLng(-1, -1);

            lenthForMeasureLine = 0;

            //Remove sketch layer
            for (int i = 0; i < mapControl.Markers.Count; i++)
            {
                if (mapControl.Markers[i].ZIndex == (int)LayerIndex.Sketch)
                {
                    mapControl.Markers.RemoveAt(i);
                    i--;
                }
            }

            mapControl.brush = new SolidColorBrush(Color.FromRgb(255, 136, 76));
            mapControl.lineWidth = 3;
            mapControl.opacity = 0.6;
        }

        /// <summary>
        /// 通过全局唯一标识删除指定marker集
        /// </summary>
        /// <param name="guid"></param>
        private void DeleteMarkersByGuid(Guid guid)
        {
            int count = this.mapControl.Markers.Count;
            for (int i = 0; i < count; i++)
            {
                if (mapControl.Markers[i].Tag != null && ((Guid)mapControl.Markers[i].Tag) == guid)
                {
                    mapControl.Markers.RemoveAt(i);
                    i--; count--;
                }
            }
        }

        /// <summary>
        /// 用于出现错误或异常时取消当前操作
        /// </summary>
        /// <param name="info">MessageBox的内容</param>
        /// <param name="title">MessageBox的题目</param>
        /// <param name="guid">用于删除当前操作产生的marker级，与marker的tag属性对应</param>
        private void CancelCurrentOperation(string info, string title, Guid guid)
        {
            DeleteMarkersByGuid(guid);
            MessageboxMaster.Show(info, title, MessageboxMaster.MyMessageBoxButtons.Ok, MessageboxMaster.MyMessageBoxButton.Ok);
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            mapControl.Zoom++;
            e.Handled = true;
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            mapControl.Zoom--;
            e.Handled = true;
        }
    }
}