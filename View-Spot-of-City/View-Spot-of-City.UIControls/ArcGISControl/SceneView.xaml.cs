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
                Basemap = new Basemap(new Uri(AppSettings["ARCGIS_THEMATIC_BASEMAP"]))
            };

            var elevationSource = new ArcGISTiledElevationSource(new Uri(AppSettings["ARCGIS_ELEVATION3D"]));
            var sceneSurface = new Surface();
            sceneSurface.ElevationSources.Add(elevationSource);
            myScene.BaseSurface = sceneSurface;

            sceneView.Scene = myScene;
            sceneView.SetViewpointCamera(new Camera(Convert.ToDouble(AppSettings["MAP_CENTER_LAT"]), Convert.ToDouble(AppSettings["MAP_CENTER_LNG"]), Convert.ToDouble(AppSettings["ARCGIS_SENCE_HEADING"]), 0, 0, 0));
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
        public void AddVisitorGraphicToOverlay(double lng, double lat, double size)
        {
            //Graphic graphic = new Graphic(new MapPoint(lng, lat, 1000, SpatialReferences.Wgs84), 
            //    new SimpleMarkerSceneSymbol
            //    {
            //        Style = SimpleMarkerSceneSymbolStyle.Cylinder,
            //        Color = Color.FromArgb(100, 37, 117, 229),
            //        Height = height,
            //        Width = 100,
            //        Depth = 100,
            //        AnchorPosition = SceneSymbolAnchorPosition.Center
            //    });

            Graphic graphic = new Graphic(new MapPoint(lng, lat, 1000, SpatialReferences.Wgs84), new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, 
                Color.FromArgb(100, 37, 117, 229), size));
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
                AddVisitorGraphicToOverlay(visitor.lng, visitor.lat, visitor.Visitors / 500.0);
            }
        }

        public void ChangeAttributesOfVisitorGraphics(List<VisitorItem> visitorList)
        {
            if(visitorList.Count == GraphicListForVisitorData.Count)
            {
                for(int i=0;i<visitorList.Count;i++)
                {
                    GraphicListForVisitorData[i].Symbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle,
                Color.FromArgb(100, 37, 117, 229), visitorList[i].Visitors / 500.0);
                }
            }
        }

        /// <summary>
        /// 改变底图
        /// </summary>
        /// <param name="url"></param>
        public void ChangeBaseMap(string url)
        {
            sceneView.Scene.Basemap = new Basemap(new Uri(url));
        }

        /// <summary>
        /// 设置场景视图
        /// </summary>
        /// <param name="camera"></param>
        public void SetScaleAndLoction(Camera camera)
        {
            sceneView.SetViewpointCamera(camera);
        }
    }
}
