using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static System.Configuration.ConfigurationManager;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.Language.Language;
using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.UIControls.Helper;

namespace View_Spot_of_City.UIControls.OverLayer
{
    /// <summary>
    /// Share.xaml 的交互逻辑
    /// </summary>
    public partial class Share : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 当前在线用户
        /// </summary>
        UserInfo _CurrentUser;
        public UserInfo CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentUser"));
            }
        }

        ObservableCollection<ViewSpot> _ViewSpotList = new ObservableCollection<ViewSpot>();
        
        /// <summary>
        /// 景点链表
        /// </summary>
        public ObservableCollection<ViewSpot> ViewSpotList
        {
            get { return _ViewSpotList; }
            set
            {
                _ViewSpotList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ViewSpotList"));
            }
        }

        public Func<ViewSpot, string> Formatter { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Share()
        {
            InitializeComponent();
            Formatter = value => value.name;
        }

        /// <summary>
        /// 查询按钮响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SpotSearchBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (StartPointAddress.Text == null || StartPointAddress.Text == string.Empty)
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
                foreach (char siglechar in input_spot_name_spited)
                {
                    sql_regexp += siglechar + "%";
                }
            }

            //API返回内容
            string jsonString = string.Empty;

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_VIEW_INFO_BY_NAME"] + "?name=" + sql_regexp, string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                string content_string = jobject["ViewInfo"].ToString();

                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content_string)))
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(ObservableCollection<ViewSpot>));
                    ViewSpotList = (ObservableCollection<ViewSpot>)deseralizer.ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }

            //检查数据
            for (int i = 0; i < ViewSpotList.Count; i++)
            {
                ViewSpotList[i].CheckData();
            }
        }

        private void PublishComment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SpotsCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(SpotsCombox.SelectedIndex != -1 && SpotsCombox.SelectedIndex <= ViewSpotList.Count)
            //{
            //    ArcGISMapCommands.SetScaleAndLocation.Execute(ViewSpotList[SpotsCombox.SelectedIndex], Application.Current.MainWindow);
            //}
        }
    }
}
