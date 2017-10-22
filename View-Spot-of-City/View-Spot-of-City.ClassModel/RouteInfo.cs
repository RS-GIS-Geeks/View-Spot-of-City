using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Esri.ArcGISRuntime.Geometry;

using View_Spot_of_City.ClassModel.Base;

namespace View_Spot_of_City.ClassModel
{
    public class RouteInfo
    {
        /// <summary>
        /// 用地理坐标定义的矩形
        /// </summary>
        public class MapBox
        {
            MapPoint _LeftTop = null;

            /// <summary>
            /// 左上角的点
            /// </summary>
            public MapPoint LeftTop
            {
                get => _LeftTop;
                internal set { _LeftTop = value; }
            }

            MapPoint _RightBottom = null;

            /// <summary>
            /// 右下角的点
            /// </summary>
            public MapPoint RightBottom
            {
                get => _RightBottom;
                internal set { _RightBottom = value; }
            }

            /// <summary>
            /// 构造一个矩形
            /// </summary>
            /// <param name="leftTop">左上角的点</param>
            /// <param name="rightBottom">右下角的点</param>
            public MapBox(MapPoint leftTop, MapPoint rightBottom)
            {
                LeftTop = leftTop;
                RightBottom = rightBottom;
            }
        }

        MapBox _Envelope = null;

        /// <summary>
        /// 外接矩形
        /// </summary>
        public MapBox Envelop
        {
            get => _Envelope;
            internal set { _Envelope = value; }
        }
        

        List<MapPoint> _RouteStops = new List<MapPoint>();

        /// <summary>
        /// 路径站点
        /// </summary>
        public List<MapPoint> RouteStops
        {
            get => _RouteStops;
            internal set { _RouteStops = value; } 
        }

        /// <summary>
        /// 站点数
        /// </summary>
        public int StopCount
        {
            get => _RouteStops.Count;
        }

        double _Length = double.MinValue;

        /// <summary>
        /// 总长度
        /// </summary>
        public double Length
        {
            get => _Length;
            internal set { _Length = value; }
        }

        double _DescendLength = double.MinValue;

        /// <summary>
        /// 下坡长度
        /// </summary>
        public double DescendLength
        {
            get => _DescendLength;
            internal set { _DescendLength = value; }
        }

        double _AscendLength = double.MinValue;

        /// <summary>
        /// 上坡长度
        /// </summary>
        public double AscendLength
        {
            get => _AscendLength;
            internal set { _AscendLength = value; }
        }

        /// <summary>
        /// 平路长度
        /// </summary>
        public double LevelLength
        {
            get { return Length - DescendLength - AscendLength; }
        }

        /// <summary>
        /// 空间参考
        /// </summary>
        public SpatialReference SpatialReference
        {
            get => SpatialReferences.Wgs84;
        }

        /// <summary>
        /// 创建路径对象
        /// </summary>
        /// <param name="mapPoint">站点</param>
        /// <param name="length">总长度</param>
        /// <param name="ascendLength">上坡长度</param>
        /// <param name="descendLength">下坡长度</param>
        [Obsolete("功能尚未完善", true)]
        public RouteInfo(List<MapPoint> mapPoint, double length, double ascendLength, double descendLength)
        {
            RouteStops = mapPoint;
            Length = length;
            AscendLength = ascendLength;
            DescendLength = descendLength;
            Envelop = GetBoxFromMapPoints(mapPoint);
        }

        /// <summary>
        /// 从点集中返回外接矩形
        /// </summary>
        /// <param name="mapPoint">点集</param>
        /// <returns>外接矩形</returns>
        private MapBox GetBoxFromMapPoints(List<MapPoint> mapPoint)
        {
            throw new NotImplementedException();
        }
    }
}
