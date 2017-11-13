using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Tasks.NetworkAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Configuration.ConfigurationManager;

namespace View_Spot_of_City.UIControls.Helper
{
    /// <summary>
    /// 基于pdRouting的最短路径服务
    /// </summary>
    public static class RoutingHelper
    {
        /// <summary>
        /// 最短路径服务格式
        /// </summary>
        private static readonly string _pgRoutingUrlFormat = AppSettings["ShortestPathServerApi"] + "";

        /// <summary>
        /// 得到最短路径服务Url
        /// </summary>
        /// <param name="stops"></param>
        /// <returns></returns>
        private static string GetPgRoutingUrl(MapPoint startPoint, MapPoint endPoint)
        {
            return "";
        }

        /// <summary>
        /// 得到最短路径服务Url
        /// </summary>
        /// <param name="stops"></param>
        /// <returns></returns>
        private static string GetPgRoutingUrl(List<MapPoint> stops)
        {
            return "";
        }

        /// <summary>
        /// 获得路径
        /// </summary>
        /// <param name="startPoint">起点</param>
        /// <param name="endPoint">终点</param>
        /// <returns>站点链表</returns>
        public static async Task<List<Stop>> GetRouteStopsAsync(MapPoint startPoint, MapPoint endPoint)
        {
            string pgRoutingServiceUrl = GetPgRoutingUrl(startPoint, endPoint);
            string responseContent = (await WebServiceHelper.GetHttpResponseAsync(pgRoutingServiceUrl, null, RestSharp.Method.GET)).Content;

            return new List<Stop>();
        }

        /// <summary>
        /// 获得路径站点
        /// </summary>
        /// <param name="necessityStops">必须到达的站点</param>
        /// <returns>站点链表</returns>
        public static async Task<List<Stop>> GetRouteStopsAsync(List<MapPoint> necessityStops)
        {
            string pgRoutingServiceUrl = GetPgRoutingUrl(necessityStops);
            string responseContent = (await WebServiceHelper.GetHttpResponseAsync(pgRoutingServiceUrl, null, RestSharp.Method.GET)).Content;

            return new List<Stop>();
        }
    }
}
