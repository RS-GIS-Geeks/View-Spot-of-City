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
        public List<List<Graphic>> GraphicForVisitorData = new List<List<Graphic>>();

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
            };
            sceneView.Scene = myScene;
            sceneView.SetViewpointCamera(new Camera(Convert.ToDouble(AppSettings["MAP_CENTER_LAT"]), Convert.ToDouble(AppSettings["MAP_CENTER_LNG"]), 1289, 295, 71, 0));
            sceneView.GraphicsOverlays.Add(CylinderOverlayForVisitorData);

            AddVisitorGraphicToOverlay(3000);
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
        /// 添加人流量可视化图形到对应图层
        /// </summary>
        /// <param name="height"></param>
        public void AddVisitorGraphicToOverlay(double height)
        {
            AddGraphicToOverlay(
                CylinderOverlayForVisitorData,
                new Graphic(
                    new MapPoint(Convert.ToDouble(AppSettings["MAP_CENTER_LNG"]),
                    Convert.ToDouble(AppSettings["MAP_CENTER_LAT"]),
                    1000,
                    SpatialReferences.Wgs84),
                new SimpleMarkerSceneSymbol
                {
                    Style = SimpleMarkerSceneSymbolStyle.Cylinder,
                    Color = Colors.Red,
                    Height = height,
                    Width = 30,
                    Depth = 30,
                    AnchorPosition = SceneSymbolAnchorPosition.Center
                }));
        }
    }
}
