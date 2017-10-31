using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Runtime.Serialization.Json;
using System.ComponentModel;
using System.Collections.ObjectModel;
using static System.Configuration.ConfigurationManager;

using Esri.ArcGISRuntime.Geometry;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.UIControls.Command;
using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.UIControls.Helper;
using View_Spot_of_City.Language.Language;

namespace View_Spot_of_City.UIControls.OverLayer
{
    /// <summary>
    /// SpotQuery.xaml 的交互逻辑
    /// </summary>
    public partial class SpotQuery : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Visibility _PanelVisibility = Visibility.Hidden;
        
        /// <summary>
        /// 面板可见性
        /// </summary>
        public Visibility PanelVisibility
        {
            get { return _PanelVisibility; }
            set
            {
                _PanelVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PanelVisibility"));
            }
        }

        /// <summary>
        /// 显示的面板集
        /// </summary>
        public enum CurrentPanel : int
        {
            List,
            Detail,
            Dicuss
        }

        CurrentPanel _CurrentGrid = CurrentPanel.List;

        /// <summary>
        /// 当前显示的面板
        /// </summary>
        public CurrentPanel CurrentGrid
        {
            get { return _CurrentGrid; }
            set
            {
                _CurrentGrid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentGrid"));
            }
        }

        ObservableCollection<ViewSpot> _ViewSpotList = new ObservableCollection<ViewSpot>();

        /// <summary>
        /// 查询到的景点列表
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

        ViewSpot _DetailShowItem = null;

        /// <summary>
        /// 选中的内容
        /// </summary>
        public ViewSpot DetailShowItem
        {
            get { return _DetailShowItem; }
            set
            {
                _DetailShowItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DetailShowItem"));
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SpotQuery()
        {
            InitializeComponent();
            StartPointAddress.Focus();
            CurrentGrid = CurrentPanel.List;
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
                foreach(char siglechar in input_spot_name_spited)
                {
                    sql_regexp += siglechar + "%";
                }
            }

            //API返回内容
            string jsonString = string.Empty;

            //景点数量
            int viewCount = -1;

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_VIEW_COUNT_BY_NAME"] + "?name=" + sql_regexp, string.Empty, RestSharp.Method.GET)).Content;
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

            if(viewCount <= 0)
            {
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("SpotSearch_Null"), LanguageDictionaryHelper.GetString("MessageBox_Tip_Title"));
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

            //检查数据
            for(int i=0;i< viewSpotList.Count;i++)
            {
                viewSpotList[i].CheckData();
            }

            ViewSpotList = new ObservableCollection<ViewSpot>(viewSpotList);
            CurrentGrid = CurrentPanel.List;
            PanelVisibility = Visibility.Visible;

            foreach(ViewSpot viewSpot in viewSpotList)
            {
                MapPoint gcjpoint = new MapPoint(viewSpot.lng, viewSpot.lat);
                MapPoint wgspoint = WGSGCJLatLonHelper.GCJ02ToWGS84(gcjpoint);
                viewSpot.lng = wgspoint.X;
                viewSpot.lat = wgspoint.Y;
                Dictionary<string, object> commandParams = new Dictionary<string, object>(8)
                {
                    { "Lng", viewSpot.lng },
                    { "Lat", viewSpot.lat },
                    { "IconUri", IconDictionaryHelper.IconDictionary[IconDictionaryHelper.Icons.pin_blue ] },
                    { "Width", 16.0 },
                    { "Height", 24.0 },
                    { "OffsetX", 0.0 },
                    { "OffsetY", 9.5 },
                    { "Data", viewSpot }
                };
                
                ArcGISMapCommands.ShowFeatureOnMap.Execute(commandParams, this);
            }
        }

        private void DataItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DetailShowItem == null)
                return;
            ArcGISMapCommands.SetScaleAndLocation.Execute(DetailShowItem, Application.Current.MainWindow);
            CurrentGrid = CurrentPanel.Detail;
        }

        private void DataItemListView_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {

        }

        private void HeadPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RearPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataItemListView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 返回主页按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToMasterButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentGrid = CurrentPanel.List;
            DetailShowItem = null;
            DataItemListView.SelectedIndex = -1;
        }

        /// <summary>
        /// 查看评论按钮点击响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewDiscussButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}