using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.UIControls.UIcontrol;
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
        /// 按月统计的人流量
        /// </summary>
        public List<List<VisitorItem>> VisitorsByMonthAndPlace = new List<List<VisitorItem>>();

        /// <summary>
        /// 图形对应属性
        /// </summary>
        private Dictionary<Graphic, object> _GraphicsAttributes = new Dictionary<Graphic, object>();

        /// <summary>
        /// 图形要素关联的属性
        /// </summary>
        public Dictionary<Graphic, object> GraphicsAttributes
        {
            get { return _GraphicsAttributes; }
            set { _GraphicsAttributes = value; }
        }

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
        /// 地图点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void sceneView_GeoViewTappedAsync(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            //MapPoint mapLocation = e.Location;
            //Esri.ArcGISRuntime.Geometry.Geometry myGeometry = GeometryEngine.Project(mapLocation, SpatialReferences.Wgs84);
            //MapPoint projectedLocation = myGeometry as MapPoint;

            double tolerance = 0;
            int maximumResults = 1;
            bool onlyReturnPopups = false;

            IdentifyGraphicsOverlayResult identifyResults = await sceneView.IdentifyGraphicsOverlayAsync(
                CylinderOverlayForVisitorData,
                e.Position,
                tolerance,
                onlyReturnPopups,
                maximumResults);

            if (identifyResults.Graphics.Count > 0)
            {
                Graphic graphic = identifyResults.Graphics[0];
                MapPoint mapPoint = graphic.Geometry as MapPoint;
                VisitorItem visitorItem = GraphicsAttributes.ContainsKey(graphic) ? (GraphicsAttributes[graphic] as VisitorItem) : null;
                if (visitorItem != null)
                {
                    List<VisitorItem> visitorList = new List<VisitorItem>();
                    for(int month=0; month < VisitorsByMonthAndPlace.Count; month++)
                    {
                        for(int viewIndex=0; viewIndex < VisitorsByMonthAndPlace[month].Count; viewIndex++)
                        {
                            if (VisitorsByMonthAndPlace[month][viewIndex].ViewId == visitorItem.ViewId)
                                visitorList.Add(VisitorsByMonthAndPlace[month][viewIndex]);
                        }
                    }
                    //显示回调框
                    sceneView.ShowCalloutAt(mapPoint, new VisitorCallout(visitorList), new Point(0, 0));
                }
            }
            else
            {
                sceneView.DismissCallout();
            }
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
        public void AddVisitorGraphicToOverlay(double lng, double lat, VisitorItem visitorItem)
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

            Graphic graphic = new Graphic(new MapPoint(lng, lat, 1000, SpatialReferences.Wgs84), 
                new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, Color.FromArgb(100, 37, 117, 229), visitorItem.Visitors / 500.0));
            GraphicListForVisitorData.Add(graphic);
            AddGraphicToOverlay(CylinderOverlayForVisitorData, graphic);
            GraphicsAttributes.Add(graphic, visitorItem);
        }

        /// <summary>
        /// 添加人流量可视化图形到对应图层
        /// </summary>
        /// <param name="height"></param>
        public void AddVisitorGraphicToOverlay(List<List<VisitorItem>> visitorList)
        {
            ResetMapViewStatus();
            foreach (VisitorItem visitor in visitorList[0])
            {
                AddVisitorGraphicToOverlay(visitor.lng, visitor.lat, visitor);
            }
            VisitorsByMonthAndPlace = visitorList;
        }

        /// <summary>
        /// 改变人流量可视化图属性
        /// </summary>
        /// <param name="visitorList"></param>
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
