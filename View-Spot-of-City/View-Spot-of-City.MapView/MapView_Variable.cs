using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.Windows.Input;
using GMap.NET.MapProviders;
using System;

namespace View_Spot_of_City.MapView
{
    /// <summary>
    /// MapView.xaml 的交互逻辑
    /// </summary>
    public partial class MapView : UserControl
    {
        #region variable
        /// <summary>
        /// 自定义的provider
        /// </summary>
        GeoServerProvider myGMapProvider;

        /// <summary>
        /// 正在进行的操作类型
        /// </summary>
        private enum Operations : int
        {
            Nothing = 0,
            AddMarker = 1,
            AddRoute = 2,
            MeasurePolyLine = 3,
            MeasurePolygon = 4,
            AddEllipse = 5,

            Test = int.MaxValue
        }

        /// <summary>
        /// 当前正在进行的操作
        /// </summary>
        private Operations operationNow = Operations.Nothing;

        /// <summary>
        /// 地球半径
        /// </summary>
        private const double EARTH_RADIUS = 6378137;

        /// <summary>
        /// 鼠标右键按下时的坐标位置
        /// </summary>
        private Point rBtnDownPoint = new Point();

        /// <summary>
        /// 鼠标左键按下时的坐标位置
        /// </summary>
        private Point lBtnDownPoint = new Point();

        /// <summary>
        /// 鼠标移动时的标记
        /// </summary>
        private GMapMarker mouseMarker;

        /// <summary>
        /// 鼠标移动时的子标记
        /// </summary>
        private GMapMarker mouseSubMarker;

        /// <summary>
        /// 鼠标移动时的提示
        /// </summary>
        private GMapMarker mouseTip;

        /// <summary>
        /// 测量所用结点，一条折线
        /// </summary>
        private readonly List<PointLatLng> polyLinePoints = new List<PointLatLng>();

        /// <summary>
        /// 测量所用结点，所有折线
        /// </summary>
        private readonly List<List<PointLatLng>> polyLinePointsList = new List<List<PointLatLng>>();

        /// <summary>
        /// 多边形结点，所有个多边形
        /// </summary>
        private readonly List<List<PointLatLng>> polygonPointsList = new List<List<PointLatLng>>();

        /// <summary>
        /// 多边形结点，一个多边形
        /// </summary>
        private readonly List<PointLatLng> polygonPoints = new List<PointLatLng>();

        /// <summary>
        /// 测量总长
        /// </summary>
        private double lenthForMeasureLine = 0;

        /// <summary>
        /// 多边形的顶点，最后生成的点
        /// </summary>
        private GMapMarker polygonVertex;

        /// <summary>
        /// 火灾点集（字典）
        /// </summary>
        private Dictionary<string, GMapMarker> firePlacesDic = new Dictionary<string, GMapMarker>();

        /// <summary>
        /// 大头针marker集
        /// </summary>
        private List<GMapMarker> pinMarkerList = new List<GMapMarker>();

        /// <summary>
        /// 大头针编号marker集
        /// </summary>
        private List<GMapMarker> pinTipMarkerList = new List<GMapMarker>();

        /// <summary>
        /// 小圆点marker集
        /// </summary>
        private List<GMapMarker> circleMarkerList = new List<GMapMarker>();

        /// <summary>
        /// 灾害圈链表
        /// </summary>
        private List<GMapMarker> damageCircleList = new List<GMapMarker>();

        /// <summary>
        /// 在灾害圈中显示的要素链表
        /// </summary>
        private List<GMapMarker> objectListInDamageCircle = new List<GMapMarker>();

        /// <summary>
        /// 图层索引，与GMapMarker的ZIndex对应
        /// </summary>
        public enum LayerIndex : int
        {
            /// <summary>
            /// 空
            /// </summary>
            Null = -1,

            /// <summary>
            /// 底图
            /// </summary>
            Map = 0,

            /// <summary>
            /// 消防支队
            /// </summary>
            FireBrigade_Zhi = 1,

            /// <summary>
            /// 消防中队
            /// </summary>
            FireBrigade_Zhong = 2,

            /// <summary>
            /// 车辆
            /// </summary>
            Car = 3,

            /// <summary>
            /// 重点单位
            /// </summary>
            KeyUnit = 4,

            /// <summary>
            /// 消防栓
            /// </summary>
            Fireplug = 5,

            /// <summary>
            /// 天然水源
            /// </summary>
            NaturalWaterSource = 6,

            /// <summary>
            /// 固定视频
            /// </summary>
            FixVideo = 7,

            /// <summary>
            /// 大头针
            /// </summary>
            Pin = 8,

            /// <summary>
            /// 多边形
            /// </summary>
            Polygon = int.MaxValue - 4,

            /// <summary>
            /// 线
            /// </summary>
            PolyLine = int.MaxValue - 3,

            /// <summary>
            /// 点
            /// </summary>
            Point = int.MaxValue - 2,

            /// <summary>
            /// 草图
            /// </summary>
            Sketch = int.MaxValue - 1
        }

        /// <summary>
        /// 消防支队链表
        /// </summary>
        public List<GMapMarker> fireBrigadeZhiList = new List<GMapMarker>();

        /// <summary>
        /// 消防中队链表
        /// </summary>
        public List<GMapMarker> fireBrigadeZhongList = new List<GMapMarker>();

        /// <summary>
        /// 车辆链表
        /// </summary>
        public List<GMapMarker> carList = new List<GMapMarker>();

        /// <summary>
        /// 重点单位链表
        /// </summary>
        public List<GMapMarker> keyUnitList = new List<GMapMarker>();

        /// <summary>
        /// 消防栓链表
        /// </summary>
        public List<GMapMarker> fireplugList = new List<GMapMarker>();

        /// <summary>
        /// 天然水源链表
        /// </summary>
        public List<GMapMarker> naturalWaterSourceList = new List<GMapMarker>();

        /// <summary>
        /// 固定视频链表
        /// </summary>
        public List<GMapMarker> fixVideoList = new List<GMapMarker>();

        /// <summary>
        /// DrawPoint参数
        /// </summary>
        public struct ParametersForDrawPoint
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="_loction">位置</param>
            /// <param name="_shape">点的显示控件</param>
            /// <param name="_layerindex"></param>
            /// <param name="_offsetX"></param>
            /// <param name="_offsetY"></param>
            /// <param name="_label"></param>
            /// <param name="_labelOffsetX"></param>
            /// <param name="_labelOffsetY"></param>
            /// <param name="_needCloseButton"></param>
            /// <param name="_closeButtonWidth"></param>
            /// <param name="_closeButtonHeight"></param>
            /// <param name="_closebuttonOffsetX"></param>
            /// <param name="_closebuttonOffsetY"></param>
            public ParametersForDrawPoint(
                PointLatLng _loction,
                UIElement _shape,
                LayerIndex _layerindex,
                double _offsetX = 0,
                double _offsetY = 0,
                UIElement _label = null,
                double _labelOffsetX = 0,
                double _labelOffsetY = 0,
                bool _needCloseButton = true,
                double _closeButtonWidth = 12,
                double _closeButtonHeight = 12,
                double _closebuttonOffsetX = 9,
                double _closebuttonOffsetY = -17)
            {
                loction = _loction;
                shape = _shape;
                layerindex = _layerindex;
                offsetX = _offsetX;
                offsetY = _offsetY;
                label = _label;
                labelOffsetX = _labelOffsetX;
                labelOffsetY = _labelOffsetY;
                needCloseButton = _needCloseButton;
                closeButtonWidth = _closeButtonWidth;
                closeButtonHeight = _closeButtonHeight;
                closebuttonOffsetX = _closebuttonOffsetX;
                closebuttonOffsetY = _closebuttonOffsetY;
            }

            /// <summary>
            /// 点的位置
            /// </summary>
            public PointLatLng loction;

            /// <summary>
            /// 点的承载控件
            /// </summary>
            public UIElement shape;

            /// <summary>
            /// 所处图层
            /// </summary>
            public LayerIndex layerindex;

            /// <summary>
            /// 向右偏移
            /// </summary>
            public double offsetX;

            /// <summary>
            /// 向下偏移
            /// </summary>
            public double offsetY;

            /// <summary>
            /// 显示信息的控件，可为空
            /// </summary>
            public UIElement label;

            /// <summary>
            /// 向右偏移
            /// </summary>
            public double labelOffsetX;

            /// <summary>
            /// 向下偏移
            /// </summary>
            public double labelOffsetY;

            /// <summary>
            /// 是否需要关闭按钮
            /// </summary>
            public bool needCloseButton;

            /// <summary>
            /// 宽度
            /// </summary>
            public double closeButtonWidth;

            /// <summary>
            /// 高度
            /// </summary>
            public double closeButtonHeight;

            /// <summary>
            /// 向右偏移
            /// </summary>
            public double closebuttonOffsetX;

            /// <summary>
            /// 向下偏移
            /// </summary>
            public double closebuttonOffsetY;
        };

        /// <summary>
        /// 线的属性
        /// </summary>
        public struct ParametersForDrawLine
        {
            public ParametersForDrawLine(
                double _lineWidth,
                Color _color,
                double _opacity,
                int _larIndex,
                bool _needCloseButton = true,
                double _closeButtonWidth = 12,
                double _closeButtonHeight = 12,
                double _closebuttonOffsetX = 10,
                double _closebuttonOffsetY = 5)
            {
                lineWidth = _lineWidth;
                color = _color;
                opacity = _opacity;
                layerIndex = _larIndex;
                needCloseButton = _needCloseButton;
                closeButtonWidth = _closeButtonWidth;
                closeButtonHeight = _closeButtonHeight;
                closebuttonOffsetX = _closebuttonOffsetX;
                closebuttonOffsetY = _closebuttonOffsetY;
            }

            /// <summary>
            /// 线宽
            /// </summary>
            public double lineWidth;

            /// <summary>
            /// 颜色
            /// </summary>
            public Color color;

            /// <summary>
            /// 透明度
            /// </summary>
            public double opacity;

            /// <summary>
            /// 所在图层
            /// </summary>
            public int layerIndex;

            /// <summary>
            /// 是否需要关闭按钮
            /// </summary>
            public bool needCloseButton;

            /// <summary>
            /// 宽度
            /// </summary>
            public double closeButtonWidth;

            /// <summary>
            /// 高度
            /// </summary>
            public double closeButtonHeight;

            /// <summary>
            /// 向右偏移
            /// </summary>
            public double closebuttonOffsetX;

            /// <summary>
            /// 向下偏移
            /// </summary>
            public double closebuttonOffsetY;
        }


        #endregion
    }
}