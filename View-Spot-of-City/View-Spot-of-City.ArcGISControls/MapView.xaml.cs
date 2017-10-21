using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using static System.Configuration.ConfigurationManager;

using View_Spot_of_City.ArcGISControls.Helper;

namespace View_Spot_of_City.ArcGISControls
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class MapView : UserControl
    {
        private GraphicsOverlay _PointOverlay = new GraphicsOverlay();
        /// <summary>
        /// 点图层
        /// </summary>
        public GraphicsOverlay PointOverlay
        {
            get { return _PointOverlay; }
            set
            {
                _PointOverlay = value;
            }
        }

        private GraphicsOverlay _LineOverlay = new GraphicsOverlay();
        /// <summary>
        /// 线图层
        /// </summary>
        public GraphicsOverlay LineOverlay
        {
            get { return _LineOverlay; }
            set
            {
                _LineOverlay = value;
            }
        }

        private GraphicsOverlay _PolygonOverlay = new GraphicsOverlay();
        /// <summary>
        /// 面图层
        /// </summary>
        public GraphicsOverlay PolygonOverlay
        {
            get { return _PolygonOverlay; }
            set
            {
                _PolygonOverlay = value;
            }
        }

        /// <summary>
        /// 路径站点
        /// </summary>
        private List<MapPoint> routeStops = new List<MapPoint>();

        /// <summary>
        /// 多边形顶点
        /// </summary>
        private List<MapPoint> polygonVertexes = new List<MapPoint>();

        public MapView()
        {
            InitializeComponent();
            InitializeMapView();
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        private void InitializeMapView()
        {
            Map map = new Map(Basemap.CreateOpenStreetMap())
            {
                InitialViewpoint = new Viewpoint(new MapPoint(Convert.ToDouble(AppSettings["MAP_CENTER_LNG"]), Convert.ToDouble(AppSettings["MAP_CENTER_LAT"]), SpatialReferences.Wgs84), Convert.ToDouble(AppSettings["ARCGIS_MAP_ZOOM"]))
            };
            mapView.Map = map;
            //mapView.SetViewpointCenterAsync(Convert.ToDouble(AppSettings["MAP_CENTER_LAT"]), Convert.ToDouble(AppSettings["MAP_CENTER_LNG"]), Convert.ToDouble(AppSettings["ARCGIS_MAP_ZOOM"]));
            mapView.GraphicsOverlays.Add(PolygonOverlay);
            mapView.GraphicsOverlays.Add(LineOverlay);
            mapView.GraphicsOverlays.Add(PointOverlay);
        }

        /// <summary>
        /// 重置地图状态
        /// </summary>
        private void ResetMapViewStatus()
        {
            PointOverlay.Graphics.Clear();
            LineOverlay.Graphics.Clear();
            PolygonOverlay.Graphics.Clear();

            routeStops.Clear();
            polygonVertexes.Clear();
        }

        /// <summary>
        /// 地图点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mapView_GeoViewTapped(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            MapPoint mapLocation = e.Location;
            Esri.ArcGISRuntime.Geometry.Geometry myGeometry = GeometryEngine.Project(mapLocation, SpatialReferences.Wgs84);
            MapPoint projectedLocation = myGeometry as MapPoint;

            Uri pinUri = IconDictionaryHelper.IconDictionary[IconDictionaryHelper.Icons.pin];
            AddIconToGraphicsOverlay(PointOverlay, projectedLocation, pinUri, 22, 33, 0, 13);

            routeStops.Add(projectedLocation);
            polygonVertexes.Add(projectedLocation);
            e.Handled = true;
        }

        /// <summary>
        /// 地图双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mapView_GeoViewDoubleTapped(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            if (routeStops == null || routeStops.Count <= 1)
                return;
            //AddRouteToGraphicsOverlay(LineOverlay, routeStops, SimpleLineSymbolStyle.Solid, Colors.Blue, 8);
            //routeStops.Clear();
            AddPolygonToGraphicsOverlay(PolygonOverlay, polygonVertexes, SimpleFillSymbolStyle.DiagonalCross, Colors.LawnGreen, new SimpleLineSymbol(SimpleLineSymbolStyle.Dash,Colors.DarkBlue, 2));
            e.Handled = true;
        }

        /// <summary>
        /// 放大按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 缩小按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 在指定图层上添加图标
        /// </summary>
        /// <param name="overlay">图层实例</param>
        /// <param name="point">位置</param>
        /// <param name="iconUri">图片URI</param>
        /// <param name="width">图片显示宽度</param>
        /// <param name="height">图片显示高度</param>
        /// <param name="offsetX">图片相对point的横向偏移</param>
        /// <param name="offsetY">图片相对point的纵向偏移</param>
        private void AddIconToGraphicsOverlay(GraphicsOverlay overlay, MapPoint point, Uri iconUri, double width, double height, double offsetX, double offsetY)
        {
            PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol(iconUri)
            {
                Width = width,
                Height = height,
                OffsetX = offsetX,
                OffsetY = offsetY
            };
            Graphic graphic = new Graphic(point, pictureMarkerSymbol);
            overlay.Graphics.Add(graphic);
        }

        /// <summary>
        /// 在指定图层上添加点要素
        /// </summary>
        /// <param name="overlay">图层实例</param>
        /// <param name="point">位置</param>
        /// <param name="pointSymbolStyle">点的呈现样式</param>
        /// <param name="pointColor">颜色</param>
        /// <param name="pointSize">大小</param>
        private void AddPointToGraphicsOverlay(GraphicsOverlay overlay, MapPoint point, SimpleMarkerSymbolStyle pointSymbolStyle, Color pointColor, double pointSize)
        {
            Graphic graphic = new Graphic(point, new SimpleMarkerSymbol(pointSymbolStyle, pointColor, pointSize));
            overlay.Graphics.Add(graphic);
        }

        /// <summary>
        /// 在指定图层上添加线要素
        /// </summary>
        /// <param name="overlay">图层实例</param>
        /// <param name="stops">站点</param>
        /// <param name="lineStyle">线的呈现样式</param>
        /// <param name="lineColor">颜色</param>
        /// <param name="lineWidth">线宽</param>
        private void AddRouteToGraphicsOverlay(GraphicsOverlay overlay, List<MapPoint> stops, SimpleLineSymbolStyle lineStyle, Color lineColor, double lineWidth)
        {
            Polyline routePolyline = new Polyline(stops);
            SimpleLineSymbol routeSymbol = new SimpleLineSymbol(lineStyle, lineColor, lineWidth);
            Graphic routeGraphic = new Graphic(routePolyline, routeSymbol);
            overlay.Graphics.Add(routeGraphic);
        }

        /// <summary>
        /// 在指定图层上添加面要素
        /// </summary>
        /// <param name="overlay">图层实例</param>
        /// <param name="vertexes">顶点</param>
        /// <param name="polygonStyle">面的呈现样式</param>
        /// <param name="fillColor">填充颜色</param>
        /// <param name="outline">边缘样式</param>
        private void AddPolygonToGraphicsOverlay(GraphicsOverlay overlay, List<MapPoint> vertexes, SimpleFillSymbolStyle polygonStyle, Color fillColor, SimpleLineSymbol outline)
        {
            Polygon polygon = new Polygon(vertexes);
            SimpleFillSymbol polygonSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.DiagonalCross, fillColor, outline);
            Graphic polygonGraphic = new Graphic(polygon, polygonSymbol);
            overlay.Graphics.Add(polygonGraphic);
        }

        private void mapView_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ResetMapViewStatus();
        }
    }
}
