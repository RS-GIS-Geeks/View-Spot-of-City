using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static System.Configuration.ConfigurationManager;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using View_Spot_of_City.UIControls.Helper;
using View_Spot_of_City.ClassModel;
using View_Spot_of_City.Form;
using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.Language.Language;

namespace View_Spot_of_City
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 当前在线用户
        /// </summary>
        UserInfo _currentUser;
        public UserInfo CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentUser"));
            }
        }

        /// <summary>
        /// 景点数据
        /// </summary>
        public ObservableCollection<ViewSpot> ViewSpotList { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            //应用程序关闭时，才 System.Windows.Application.Shutdown 调用
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            //验证License
            if (!RegisterMaster.CanStart())
                this.Shutdown();

            //登录
            bool? loginDlgResult = (new LoginDlg(AppSettings["DEFAULT_USER_MAIL"])).ShowDialog();
            if (!loginDlgResult.HasValue || !loginDlgResult.Value)
                this.Shutdown();

            base.OnStartup(e);
        }

        /// <summary>
        /// 从服务器上获取景点数据
        /// </summary>
        public void GetViewSpotsData()
        {
            //API返回内容
            string jsonString = string.Empty;

            //景点数量
            int viewCount = -1;

            try
            {
                jsonString = (WebServiceHelper.GetHttpResponse(AppSettings["WEB_API_GET_VIEW_COUNT_BY_NAME"] + "?name=%", string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                JToken jtoken = jobject["ViewCount"][0];

                viewCount = (int)jtoken["COUNT(*)"];

            }
            catch
            {
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }

            if (viewCount <= 0)
            {
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("SpotSearch_Null"), LanguageDictionaryHelper.GetString("MessageBox_Tip_Title"));
                return;
            }

            List<ViewSpot> viewSpotList = new List<ViewSpot>(viewCount);

            try
            {
                jsonString = (WebServiceHelper.GetHttpResponse(AppSettings["WEB_API_GET_VIEW_INFO_BY_NAME"] + "?name=%", string.Empty, RestSharp.Method.GET)).Content;
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

            //检查数据
            for (int i = 0; i < viewSpotList.Count; i++)
            {
                viewSpotList[i].CheckData();
            }

            ViewSpotList = new ObservableCollection<ViewSpot>(viewSpotList);
        }
    }
}
