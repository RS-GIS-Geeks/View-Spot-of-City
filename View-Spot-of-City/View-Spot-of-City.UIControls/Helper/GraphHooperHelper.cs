using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Configuration.ConfigurationManager;

using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Tasks.NetworkAnalysis;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace View_Spot_of_City.UIControls.Helper
{
    public static class GraphHooperHelper
    {
        /// <summary>
        /// 获得路径
        /// </summary>
        /// <param name="startPoint">起点</param>
        /// <param name="endPoint">终点</param>
        /// <returns>站点链表</returns>
        public static async Task<List<MapPoint>> GetRouteStopsAsync(MapPoint startPoint, MapPoint endPoint)
        {
            //https://graphhopper.com/api/1/route?point=49.932707,11.588051&point=50.3404,11.64705&vehicle=car&debug=true&key=d3c705e4-cf27-4dc0-9c9e-5ff599db7ce8&type=json&points_encoded=false
            string grophHooperServiceUrl = AppSettings["GRAPHHOOPER_ROUTE_API_URL"] + "?point=" + startPoint.Y + "," + startPoint.X + "&point=" + endPoint.Y + "," + endPoint.X +
                "&vehicle=" + AppSettings["GRAPHHOOPER_ROUTE_API_VEHICLE"] + "&debug=" + AppSettings["GRAPHHOOPER_ROUTE_API_DEBUG"] + "&key=" + AppSettings["GRAPHHOOPER_ROUTE_API_KEY"] +
                "&type=json&points_encoded=false";
            string responseContent = (await WebServiceHelper.GetHttpResponseAsync(grophHooperServiceUrl, null, RestSharp.Method.GET)).Content;

            JObject jobject = (JObject)JsonConvert.DeserializeObject(responseContent);

            JToken pointsToken = jobject["paths"][0]["points"]["coordinates"];

            List<MapPoint> stopList = new List<MapPoint>();

            foreach (JToken pointToken in pointsToken)
            {
                stopList.Add(new MapPoint(Convert.ToDouble(pointToken[0].ToString()), Convert.ToDouble(pointToken[1].ToString()), SpatialReferences.Wgs84));
            }

            return stopList;
        }

        /// <summary>
        /// 获得路径站点
        /// </summary>
        /// <param name="necessityStops">必须到达的站点</param>
        /// <returns>站点链表</returns>
        public static List<Stop> GetRouteStops(List<MapPoint> necessityStops)
        {
            throw new NotImplementedException();
        }
    }
}
