using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Configuration.ConfigurationManager;

using Esri.ArcGISRuntime.Geometry;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.Language.Language;
using View_Spot_of_City.UIControls.Command;
using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.UIControls.Helper;
using View_Spot_of_City.UIControls.VisualizationControl;

namespace View_Spot_of_City.UIControls.OverLayer
{
    /// <summary>
    /// SpotRecommend.xaml 的交互逻辑
    /// </summary>
    public partial class SpotRecommend : UserControl, INotifyPropertyChanged
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
            Comment
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public SpotRecommend()
        {
            InitializeComponent();
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
            int selectedIndex = ModeCombox.SelectedIndex;

            string requestString = null;

            string AscOrDescString = Desc.IsChecked == true ? "desc" : "asc";

            if (selectedIndex == 0)
            {
                requestString = AppSettings["WEB_API_GET_VIEW_INFO_BY_RATING"] + "?limit=100&ascordesc=" + AscOrDescString;
            }
            else if(selectedIndex == 1)
            {
                requestString = AppSettings["WEB_API_GET_VIEW_INFO_BY_COST"] + "?limit=100&ascordesc=" + AscOrDescString;
            }
            else
            {
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Input_Empty"), LanguageDictionaryHelper.GetString("MessageBox_Warning_Title"));
                return;
            }
            
            //API返回内容
            string jsonString = string.Empty;

            List<ViewSpot> viewSpotList = new List<ViewSpot>(100);

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(requestString, string.Empty, RestSharp.Method.GET)).Content;
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

            //设置显示数据
            ViewMaster.ViewSpotList = new ObservableCollection<ViewSpot>(viewSpotList);

            //定义当前显示的面板
            CurrentGrid = CurrentPanel.List;

            //设置面板可见
            PanelVisibility = Visibility.Visible;

            //清除点图层要素
            ArcGISMapCommands.ClearFeatures.Execute(2, this);

            //绘制要素
            foreach (ViewSpot viewSpot in viewSpotList)
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

        private void ShowViewSpotDetailCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewSpot param = e.Parameter as ViewSpot;
            ViewDetail.DetailShowItem = param;
            ViewDetail.ImageUrls[0] = ViewDetail.DetailShowItem.photourl1 ?? string.Empty;
            ViewDetail.ImageUrls[1] = ViewDetail.DetailShowItem.photourl2 ?? string.Empty;
            ViewDetail.ImageUrls[2] = ViewDetail.DetailShowItem.photourl3 ?? string.Empty;
            ViewDetail.CurrentImageUrl = ViewDetail.ImageUrls[ViewDetail.CurrentImageIndex];
            CurrentGrid = CurrentPanel.Detail;

        }

        private void BackToMasterCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentGrid = CurrentPanel.List;
            ViewMaster.DataItemListView.SelectedIndex = -1;
            ArcGISMapCommands.ClearCallout.Execute(null, Application.Current.MainWindow);
        }

        private async void ShowDiscussCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewSpot param = e.Parameter as ViewSpot;
            ViewComment.DetailShowItem = param;
            CurrentGrid = CurrentPanel.Comment;
            ViewMaster.DataItemListView.SelectedIndex = -1;

            if (ViewComment.DetailShowItem == null)
                return;
            
            string requestString = AppSettings["WEB_API_GET_COMMENT_INFO_BY_VIEW"] + "?viewid=" + ViewComment.DetailShowItem.id;

            //API返回内容
            string jsonString = string.Empty;

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(requestString, string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                string content_string = jobject["CommentInfo"].ToString();

                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content_string)))
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(ObservableCollection<CommentInfo>));
                    ViewComment.CommentList = (ObservableCollection<CommentInfo>)deseralizer.ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }

            //检查数据
            for (int i = 0; i < ViewComment.CommentList.Count; i++)
            {
                ViewComment.CommentList[i].Spot = ViewComment.DetailShowItem;
                ViewComment.CommentList[i].CheckData();
            }
        }

        private void ShowStatisticsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewSpot param = e.Parameter as ViewSpot;
            SpotStatistics spotStatistics = new SpotStatistics(param.id);
            spotStatistics.Show();
            ViewMaster.DataItemListView.SelectedIndex = -1;
        }

        private void ModeCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModeCombox.SelectedIndex == 0)
                Desc.IsChecked = true;
            else
                Desc.IsChecked = false;
        }
    }
}