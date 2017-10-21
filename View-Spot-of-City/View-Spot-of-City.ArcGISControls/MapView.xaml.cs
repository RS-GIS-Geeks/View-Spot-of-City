using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

using static System.Configuration.ConfigurationManager;

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

        public MapView()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            Map map = new Map(Basemap.CreateOpenStreetMap())
            {
                InitialViewpoint = new Viewpoint(new MapPoint(Convert.ToDouble(AppSettings["MAP_CENTER_LNG"]), Convert.ToDouble(AppSettings["MAP_CENTER_LAT"]), SpatialReferences.Wgs84), Convert.ToDouble(AppSettings["ARCGIS_MAP_ZOOM"]))
            };
            mapView.Map = map;
            //mapView.SetViewpointCenterAsync(Convert.ToDouble(AppSettings["MAP_CENTER_LAT"]), Convert.ToDouble(AppSettings["MAP_CENTER_LNG"]), Convert.ToDouble(AppSettings["ARCGIS_MAP_ZOOM"]));
            mapView.GraphicsOverlays.Add(PointOverlay);
        }

        private void mapView_GeoViewTapped(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            MapPoint mapLocation = e.Location;
            Esri.ArcGISRuntime.Geometry.Geometry myGeometry = GeometryEngine.Project(mapLocation, SpatialReferences.Wgs84);
            MapPoint projectedLocation = myGeometry as MapPoint;

            Uri pinUri = new Uri("pack://application:,,,/View-Spot-of-City.ArcGISControls;component/Icon/pin.png");
            AddIconToGraphicsOverlay(PointOverlay, projectedLocation, pinUri, 22, 33, 0, 13);
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddIconToGraphicsOverlay(GraphicsOverlay overlay, MapPoint point, Uri iconUri, double width, double height, double offsetX, double offsetY)
        {
            var symbolUri = new Uri("pack://application:,,,/View-Spot-of-City.ArcGISControls;component/Icon/pin.png");

            PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol(iconUri)
            {
                Width = width,
                Height = height,
                OffsetX = offsetX,
                OffsetY = offsetY
            };
            
            Graphic campsiteGraphic = new Graphic(point, pictureMarkerSymbol);
            
            overlay.Graphics.Add(campsiteGraphic);
        }
    }
}
