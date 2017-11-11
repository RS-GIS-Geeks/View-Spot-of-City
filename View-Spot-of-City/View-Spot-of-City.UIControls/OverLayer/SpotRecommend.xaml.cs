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
            Comment,
            Statistics
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
            if (ModeCombox.SelectedIndex == -1)
            {
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Input_Empty"), LanguageDictionaryHelper.GetString("MessageBox_Warning_Title"));
                return;
            }
            
            //API返回内容
            string jsonString = string.Empty;

            List<ViewSpot> viewSpotList = new List<ViewSpot>(100);

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_VIEW_INFO_BY_RATING"] + "?limit=100", string.Empty, RestSharp.Method.GET)).Content;
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

        private void ShowDiscussCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewSpot param = e.Parameter as ViewSpot;
            ViewComment.CommentList.Clear();
            ViewComment.CommentList.Add(new CommentInfo()
            {
                Id = -1,
                UserName = "梦觉知晓",
                Stars = 3.5,
                Year = 2017,
                Month = 8,
                Day = 15,
                Hour = 21,
                Minute = 52,
                Second = 55,
                Goods = 112,
                CommentData = "搭11号线去拍写真😄说好的美丽车厢-没有😔真的很远呀，地铁+公交车都需要2小时左右😔凤凰古村跟版画村很像，但人很少，非常安静，这点很不错👍这是南宋民族英雄文天祥后代的聚集地，一个拥有700多年历史的古老村落，有60多座保存完好的明清时期民居建筑。特别适合拍照[机智]",
                PhotoUrl1 = @"http://qcloud.dpfile.com/pc/XsSvTLEgsXStMMgBoM_lcldddVGgSUqvx8zaPOONmgp8MJdP1Kqdm4-kPdOGmJWT.jpg",
                PhotoUrl2 = @"http://qcloud.dpfile.com/pc/TFiBogtkRNymXHhB2DZ8BSu9E93rjYGE0Fk82BseaqMGKtZVVy10_IHGzJTdp2vy.jpg",
                PhotoUrl3 = @"http://qcloud.dpfile.com/pc/QIEywiBIg9miOs44M6p5ZKy2GEd6XMBArSjSBhgSfZrUOBzXhVSeyTHI_TSgvGWZ.jpg",
                Spot = param,
                TimedForShow = "2017-8-15"
            });
            ViewComment.CommentList.Add(new CommentInfo()
            {
                Id = -1,
                UserName = "梦觉知晓",
                Stars = 3.5,
                Year = 2017,
                Month = 8,
                Day = 15,
                Hour = 21,
                Minute = 52,
                Second = 55,
                Goods = 112,
                CommentData = "搭11号线去拍写真😄说好的美丽车厢-没有😔真的很远呀，地铁+公交车都需要2小时左右😔凤凰古村跟版画村很像，但人很少，非常安静，这点很不错👍这是南宋民族英雄文天祥后代的聚集地，一个拥有700多年历史的古老村落，有60多座保存完好的明清时期民居建筑。特别适合拍照[机智]",
                PhotoUrl1 = @"http://qcloud.dpfile.com/pc/XsSvTLEgsXStMMgBoM_lcldddVGgSUqvx8zaPOONmgp8MJdP1Kqdm4-kPdOGmJWT.jpg",
                PhotoUrl2 = @"http://qcloud.dpfile.com/pc/TFiBogtkRNymXHhB2DZ8BSu9E93rjYGE0Fk82BseaqMGKtZVVy10_IHGzJTdp2vy.jpg",
                PhotoUrl3 = @"http://qcloud.dpfile.com/pc/QIEywiBIg9miOs44M6p5ZKy2GEd6XMBArSjSBhgSfZrUOBzXhVSeyTHI_TSgvGWZ.jpg",
                Spot = param,
                TimedForShow = "2017-8-15"
            });
            ViewComment.CommentList.Add(new CommentInfo()
            {
                Id = -1,
                UserName = "梦觉知晓",
                Year = 2017,
                Stars = 3.5,
                Month = 8,
                Day = 15,
                Hour = 21,
                Minute = 52,
                Second = 55,
                Goods = 112,
                CommentData = "搭11号线去拍写真😄说好的美丽车厢-没有😔真的很远呀，地铁+公交车都需要2小时左右😔凤凰古村跟版画村很像，但人很少，非常安静，这点很不错👍这是南宋民族英雄文天祥后代的聚集地，一个拥有700多年历史的古老村落，有60多座保存完好的明清时期民居建筑。特别适合拍照[机智]",
                PhotoUrl1 = @"http://qcloud.dpfile.com/pc/XsSvTLEgsXStMMgBoM_lcldddVGgSUqvx8zaPOONmgp8MJdP1Kqdm4-kPdOGmJWT.jpg",
                PhotoUrl2 = @"http://qcloud.dpfile.com/pc/TFiBogtkRNymXHhB2DZ8BSu9E93rjYGE0Fk82BseaqMGKtZVVy10_IHGzJTdp2vy.jpg",
                PhotoUrl3 = @"http://qcloud.dpfile.com/pc/QIEywiBIg9miOs44M6p5ZKy2GEd6XMBArSjSBhgSfZrUOBzXhVSeyTHI_TSgvGWZ.jpg",
                Spot = param,
                TimedForShow = "2017-8-15"
            });
            ViewComment.DetailShowItem = param;
            CurrentGrid = CurrentPanel.Comment;
            ViewMaster.DataItemListView.SelectedIndex = -1;
        }

        private void ShowStatisticsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewSpot param = e.Parameter as ViewSpot;
            ViewStatistics.ControlList.Clear();
            ViewStatistics.ControlList.Add(new GeoHeatMap());
            ViewStatistics.ControlList.Add(new GeoHeatMap());
            ViewStatistics.ControlList.Add(new GeoHeatMap());
            ViewStatistics.ControlList.Add(new GeoHeatMap());
            ViewStatistics.ControlList.Add(new GeoHeatMap());
            ViewStatistics.ControlList.Add(new GeoHeatMap());
            ViewStatistics.ControlList.Add(new GeoHeatMap());
            ViewStatistics.DetailShowItem = param;
            CurrentGrid = CurrentPanel.Statistics;
            ViewMaster.DataItemListView.SelectedIndex = -1;
        }

        private void ViewStatistics_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}