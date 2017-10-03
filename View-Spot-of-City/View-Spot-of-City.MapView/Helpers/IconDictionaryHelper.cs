using System;
using System.Collections.Generic;

namespace View_Spot_of_City.MapView.Helpers
{
    public static class IconDictionaryHelper
    {
        public static Dictionary<Icons, Uri> iconDictionary = new Dictionary<Icons, Uri>();

        public enum Icons : int
        {
            /// <summary>
            /// 消防支队
            /// </summary>
            FireBrigade_Zhi = 1,

            /// <summary>
            /// 消防中队
            /// </summary>
            FireBrigade_Zhong = 2,

            /// <summary>
            /// 消防车
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
            /// 红色大头针
            /// </summary>
            Pin = 8,

            /// <summary>
            /// 蓝色大头针
            /// </summary>
            Pin_Blue = 9,

            /// <summary>
            /// 电话
            /// </summary>
            Phone = 10,

            /// <summary>
            /// 关闭按钮
            /// </summary>
            Close = 11,

            /// <summary>
            /// 尺子
            /// </summary>
            Rule = 12,

            /// <summary>
            /// 结点
            /// </summary>
            Node = 13,

            /// <summary>
            /// 节点
            /// </summary>
            Node0 = 14,

            /// <summary>
            /// 路径起点
            /// </summary>
            Route_Start = 15,

            /// <summary>
            /// 路径终点
            /// </summary>
            Route_End = 16,

            /// <summary>
            /// 手工标注
            /// </summary>
            Manual_Mark = 17,

            /// <summary>
            /// 正在处理的火灾位置
            /// </summary>
            Fire_Place_Handling = 18,

            /// <summary>
            /// 尚未处理的火灾位置
            /// </summary>
            Fire_Place_Unhandling = 19,

            /// <summary>
            /// 红色小圆点
            /// </summary>
            Red_Circle = 20,

            /// <summary>
            /// 蓝色小圆点
            /// </summary>
            Blue_Circle = 21
        }
        static IconDictionaryHelper()
        {
            iconDictionary.Add(Icons.FireBrigade_Zhi, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/支队.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.FireBrigade_Zhong, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/中队.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Car, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/消防车.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.KeyUnit, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/重点单位.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Fireplug, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/消火栓.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.NaturalWaterSource, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/天然水源.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.FixVideo, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/固定视频.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Pin, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/大头针.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Pin_Blue, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/大头针蓝.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Phone, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/报警电话位置.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Close, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/叉.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Rule, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/尺子.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Node, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/结点.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Node0, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/结点0.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Route_Start, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/起点.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Route_End, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/终点.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Manual_Mark, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/手工标注.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Fire_Place_Handling, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/火灾位置.png", UriKind.RelativeOrAbsolute)); 
            iconDictionary.Add(Icons.Fire_Place_Unhandling, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/火灾位置0.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Red_Circle, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/red_circle.png", UriKind.RelativeOrAbsolute));
            iconDictionary.Add(Icons.Blue_Circle, new Uri("pack://application:,,,/View_Spot_of_City.MapView;component/Icon/blue_circle.png", UriKind.RelativeOrAbsolute));
        }
    }
}
