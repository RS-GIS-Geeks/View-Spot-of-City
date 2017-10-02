using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.Windows.Input;
using GMap.NET.MapProviders;
using System;

namespace GMapView
{
    /// <summary>
    /// GMapView.xaml 的交互逻辑
    /// </summary>
    public partial class GMapView : UserControl
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
        /// 一条折线
        /// </summary>
        private readonly List<PointLatLng> polyLinePoints = new List<PointLatLng>();

        /// <summary>
        /// 所有折线
        /// </summary>
        private readonly List<List<PointLatLng>> polyLinePointsList = new List<List<PointLatLng>>();

        /// <summary>
        /// 一个多边形
        /// </summary>
        private readonly List<PointLatLng> polygonPoints = new List<PointLatLng>();

        /// <summary>
        /// 所有多边形
        /// </summary>
        private readonly List<List<PointLatLng>> polygonPointsList = new List<List<PointLatLng>>();

        /// <summary>
        /// 大头针marker集
        /// </summary>
        private List<GMapMarker> pinMarkerList = new List<GMapMarker>();

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