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

using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.Language.Language;
using View_Spot_of_City.UIControls.Helper;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.Serialization.Json;
using View_Spot_of_City.ClassModel;
using Newtonsoft.Json;

namespace View_Spot_of_City.UIControls.OverLayer
{
    /// <summary>
    /// SpotQuery.xaml 的交互逻辑
    /// </summary>
    public partial class SpotQuery : UserControl
    {
        public SpotQuery()
        {
            InitializeComponent();
        }
        
        /// <summary>
         /// 地址文本框获得焦点
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void AddressTextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 文本框失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddressTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 查询按钮响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SpotSearchBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (StartPointAddress.Text == null)
            {
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Input_Empty"), LanguageDictionaryHelper.GetString("MessageBox_Warning_Title"));
                return;
            }
            
            //用户输入的内容
            string input_spot_name = StartPointAddress.Text;

            //逐一分词之后的内容
            char[] input_spot_name_spited = input_spot_name.ToCharArray();

            //模糊查询中的内容
            string sql_regexp = "%";
            {
                foreach(char siglechar in input_spot_name_spited)
                {
                    sql_regexp += siglechar + "%";
                }
            }

            //API返回内容
            string jsonString = string.Empty;

            //景点数量
            int viewCount = -1;

            //创建一个景点实例
            ViewSpot viewSpot = ViewSpot.NullViewSpot;

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_VIEW_COUNT_BY_NAME"] + "?name=" + sql_regexp, string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                JToken jtoken = jobject["ViewCount"][0];

                viewCount = (int)jtoken["COUNT(*)"];

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }

            List<ViewSpot> viewSpotList = new List<ViewSpot>(viewCount);

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_VIEW_INFO_BY_NAME"] + "?name=" + sql_regexp, string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                string content_string = jobject["ViewInfo"].ToString();

                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content_string)))
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(List<ViewSpot>));
                    viewSpotList = (List<ViewSpot>)deseralizer.ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }


        }
    }
}
