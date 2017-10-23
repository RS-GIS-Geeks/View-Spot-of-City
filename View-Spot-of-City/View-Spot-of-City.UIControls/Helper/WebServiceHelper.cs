using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Configuration.ConfigurationManager;

namespace View_Spot_of_City.UIControls.Helper
{
    public static class WebServiceHelper
    {
        ///<summary>
        ///采用https协议访问网络
        ///</summary>
        ///<param name="URL">url地址</param>
        ///<param name="strPostdata">发送的数据</param>
        ///<returns>返回的数据</returns>
        public static Task<IRestResponse> GetHttpResponseAsync(string URL, string strPostdata, Method method = Method.POST)
        {
            var client = new RestClient(URL);
            var request = new RestRequest(method)
            {
                Timeout = Convert.ToInt32(AppSettings["NET_WORK_DELAY"])
            };
            request.AddHeader("cache-control", "no-cache");
            if(strPostdata != null || strPostdata != string.Empty)
                request.AddParameter("undefined", strPostdata, ParameterType.RequestBody);
            return client.ExecuteGetTaskAsync(request);
        }
    }
}
