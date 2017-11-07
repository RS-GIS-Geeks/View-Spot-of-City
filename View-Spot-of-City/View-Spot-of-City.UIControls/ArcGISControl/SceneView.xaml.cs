using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
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
using View_Spot_of_City.ClassModel;
using static System.Configuration.ConfigurationManager;

namespace View_Spot_of_City.UIControls.ArcGISControl
{
    /// <summary>
    /// SceneView.xaml 的交互逻辑
    /// </summary>
    public partial class SceneView : UserControl
    {
        /// <summary>
        /// 人流量可视化图层
        /// </summary>
        public GraphicsOverlay CylinderOverlayForVisitorData = new GraphicsOverlay();

        /// <summary>
        /// 人流量可视化图形
        /// </summary>
        public List<Graphic> GraphicListForVisitorData = new List<Graphic>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public SceneView()
        {
            InitializeComponent();
            InitializeSceneView();
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        private void InitializeSceneView()
        {
            Scene myScene = new Scene()
            {
                Basemap = new Basemap(new Uri(AppSettings["ARCGIS_BASEMAP"]))
                //Basemap = new Basemap(new Uri("http://www.arcgis.com/home/item.html?id=91b46c2b162c48dba264b2190e1dbcff"))
            };

            var elevationSource = new ArcGISTiledElevationSource(new Uri("http://elevation3d.arcgis.com/arcgis/rest/services/WorldElevation3D/Terrain3D/ImageServer"));
            var sceneSurface = new Surface();
            sceneSurface.ElevationSources.Add(elevationSource);
            myScene.BaseSurface = sceneSurface;

            sceneView.Scene = myScene;
            sceneView.SetViewpointCamera(new Camera(Convert.ToDouble(AppSettings["MAP_CENTER_LAT"]), Convert.ToDouble(AppSettings["MAP_CENTER_LNG"]), 1289, 295, 71, 0));
            sceneView.GraphicsOverlays.Add(CylinderOverlayForVisitorData);
        }

        /// <summary>
        /// 重置地图状态
        /// </summary>
        public void ResetMapViewStatus()
        {
            //清除回调框
            sceneView.DismissCallout();

            //清除图层
            CylinderOverlayForVisitorData.Graphics.Clear();
        }

        /// <summary>
        /// 添加图形到指定图层
        /// </summary>
        /// <param name="overlay"></param>
        /// <param name="graphic"></param>
        public void AddGraphicToOverlay(GraphicsOverlay overlay, Graphic graphic)
        {
            overlay.Graphics.Add(graphic);
        }

        /// <summary>
        /// 添加单个人流量可视化图形到对应图层
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <param name="height"></param>
        public void AddVisitorGraphicToOverlay(double lng, double lat, double height)
        {
            Graphic graphic = new Graphic(new MapPoint(lng, lat, 1000, SpatialReferences.Wgs84), 
                new SimpleMarkerSceneSymbol
                {
                    Style = SimpleMarkerSceneSymbolStyle.Cylinder,
                    Color = Colors.Yellow,
                    Height = height,
                    Width = 100,
                    Depth = 100,
                    AnchorPosition = SceneSymbolAnchorPosition.Center
                });
            GraphicListForVisitorData.Add(graphic);
            AddGraphicToOverlay(CylinderOverlayForVisitorData,graphic);
        }

        /// <summary>
        /// 添加人流量可视化图形到对应图层
        /// </summary>
        /// <param name="height"></param>
        public void AddVisitorGraphicToOverlay(List<VisitorItem> visitorList)
        {
            ResetMapViewStatus();
            foreach (VisitorItem visitor in visitorList)
            {
                AddVisitorGraphicToOverlay(visitor.lng, visitor.lat, visitor.Visitors);
            }
        }

        public void ChangeAttributesOfVisitorGraphics(List<VisitorItem> visitorList)
        {

        }

        /// <summary>
        /// 改变底图
        /// </summary>
        /// <param name="url"></param>
        public void ChangeBaseMap(string url)
        {
            sceneView.Scene.Basemap = new Basemap(new Uri(url));
        }
    }
}
