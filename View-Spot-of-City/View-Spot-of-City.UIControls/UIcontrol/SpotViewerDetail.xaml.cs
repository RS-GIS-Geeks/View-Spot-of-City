using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using static System.Configuration.ConfigurationManager;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.UIControls.Command;
using View_Spot_of_City.UIControls.Helper;
using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.Language.Language;
using static View_Spot_of_City.UIControls.Command.ArcGISMapCommands;
using Esri.ArcGISRuntime.Geometry;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// SpotViewerDetail.xaml 的交互逻辑
    /// </summary>
    public partial class SpotViewerDetail : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
        /// 图片集，只能有3张
        /// </summary>
        public string[] ImageUrls = new string[3];

        /// <summary>
        /// 当前显示图片的索引
        /// </summary>
        public short CurrentImageIndex = 0;

        string _CurrentImageUrl = string.Empty;

        /// <summary>
        /// 当前显示的图片URL
        /// </summary>
        public string CurrentImageUrl
        {
            get { return _CurrentImageUrl; }
            set
            {
                _CurrentImageUrl = value;
                Player.Pause();
                PauseBtn.IsChecked = false;
                PauseBtn.Visibility = Visibility.Visible;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentImageUrl"));
            }
        }

        public SpotViewerDetail()
        {
            InitializeComponent();
            Player.Pause();
        }

        /// <summary>
        /// 返回主页按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToMasterButton_Click(object sender, RoutedEventArgs e)
        {
            DetailShowItem = null;
            ViewSpotViewerCommands.BackToMaster.Execute(null, this);
        }
        
        /// <summary>
        /// 查看评论按钮点击响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewDiscussButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetailShowItem != null)
                ViewSpotViewerCommands.ShowDiscuss.Execute(DetailShowItem, this);
        }

        /// <summary>
        /// 查看统计按钮点击响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetailShowItem != null)
                ViewSpotViewerCommands.ShowStatistics.Execute(DetailShowItem, this);
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PauseBtn.IsChecked == true)
                Player.Play();
            else
                Player.Pause();
        }

        private void Player_MouseEnter(object sender, MouseEventArgs e)
        {
            PauseBtn.Visibility = Visibility.Visible;
        }

        private void Player_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PauseBtn.IsChecked == true)
                PauseBtn.Visibility = Visibility.Collapsed;
            else
                PauseBtn.Visibility = Visibility.Visible;
        }

        private async void GasStationPackIconModern_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int distance = 5000;

            MapPoint centerMercator = GeometryEngine.Project(new MapPoint(DetailShowItem.GetLng(), DetailShowItem.GetLat(), SpatialReferences.Wgs84),
                SpatialReferences.WebMercator) as MapPoint;

            MapPoint leftTopMercator = new MapPoint(centerMercator.X - distance, centerMercator.Y + distance, centerMercator.SpatialReference);
            MapPoint rightBottomMercator = new MapPoint(centerMercator.X + distance, centerMercator.Y - distance, centerMercator.SpatialReference);

            MapPoint leftTopWgs84 = GeometryEngine.Project(leftTopMercator, SpatialReferences.Wgs84) as MapPoint;
            MapPoint rightBottomWgs84 = GeometryEngine.Project(rightBottomMercator, SpatialReferences.Wgs84) as MapPoint;

            double minLng = leftTopWgs84.X;
            double maxLng = rightBottomWgs84.X;
            double minLat = rightBottomWgs84.Y;
            double maxLat = leftTopWgs84.Y;

            List<MapPoint> points = new List<MapPoint>();

            //API返回内容
            string jsonString = string.Empty;

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_GAS_STATION"] + "?minLng=" + minLng + "&maxLng=" + maxLng + "&minLat=" + minLat + "&maxLat=" + maxLat, string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                JToken content = jobject["StationInfo"];

                foreach (JToken pointToken in content)
                {
                    points.Add(new MapPoint(Convert.ToDouble(pointToken["Lng"].ToString()), Convert.ToDouble(pointToken["Lat"].ToString()), SpatialReferences.Wgs84));
                }

                Dictionary<string, object> param = new Dictionary<string, object>(2)
                {
                    { "type",ViewSpotArounds.GasStation },
                    { "points", points}
                };

                ArcGISMapCommands.AddViewSpotAround.Execute(param, Application.Current.MainWindow);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }
        }

        private async void BusStationPackIconModern_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int distance = 500;

            MapPoint centerMercator = GeometryEngine.Project(new MapPoint(DetailShowItem.GetLng(), DetailShowItem.GetLat(), SpatialReferences.Wgs84),
                SpatialReferences.WebMercator) as MapPoint;

            MapPoint leftTopMercator = new MapPoint(centerMercator.X - distance, centerMercator.Y + distance, centerMercator.SpatialReference);
            MapPoint rightBottomMercator = new MapPoint(centerMercator.X + distance, centerMercator.Y - distance, centerMercator.SpatialReference);

            MapPoint leftTopWgs84 = GeometryEngine.Project(leftTopMercator, SpatialReferences.Wgs84) as MapPoint;
            MapPoint rightBottomWgs84 = GeometryEngine.Project(rightBottomMercator, SpatialReferences.Wgs84) as MapPoint;

            double minLng = leftTopWgs84.X;
            double maxLng = rightBottomWgs84.X;
            double minLat = rightBottomWgs84.Y;
            double maxLat = leftTopWgs84.Y;

            List<MapPoint> points = new List<MapPoint>();

            //API返回内容
            string jsonString = string.Empty;

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_TRAFFIC_STATION"] + "?minLng=" + minLng + "&maxLng=" + maxLng + "&minLat=" + minLat + "&maxLat=" + maxLat, string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                JToken content = jobject["StationInfo"];

                foreach(JToken pointToken in content)
                {
                    points.Add(new MapPoint(Convert.ToDouble(pointToken["Lng"].ToString()), Convert.ToDouble(pointToken["Lat"].ToString()), SpatialReferences.Wgs84));
                }

                Dictionary<string, object> param = new Dictionary<string, object>(2)
                {
                    { "type",ViewSpotArounds.TrafficStation },
                    { "points", points}
                };

                ArcGISMapCommands.AddViewSpotAround.Execute(param, Application.Current.MainWindow);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }
        }

        private async void RestaurantPackIconModern_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int distance = 1000;

            MapPoint centerMercator = GeometryEngine.Project(new MapPoint(DetailShowItem.GetLng(), DetailShowItem.GetLat(), SpatialReferences.Wgs84),
                SpatialReferences.WebMercator) as MapPoint;

            MapPoint leftTopMercator = new MapPoint(centerMercator.X - distance, centerMercator.Y + distance, centerMercator.SpatialReference);
            MapPoint rightBottomMercator = new MapPoint(centerMercator.X + distance, centerMercator.Y - distance, centerMercator.SpatialReference);

            MapPoint leftTopWgs84 = GeometryEngine.Project(leftTopMercator, SpatialReferences.Wgs84) as MapPoint;
            MapPoint rightBottomWgs84 = GeometryEngine.Project(rightBottomMercator, SpatialReferences.Wgs84) as MapPoint;

            double minLng = leftTopWgs84.X;
            double maxLng = rightBottomWgs84.X;
            double minLat = rightBottomWgs84.Y;
            double maxLat = leftTopWgs84.Y;

            List<MapPoint> points = new List<MapPoint>();

            //API返回内容
            string jsonString = string.Empty;

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_RESTAURANT"] + "?minLng=" + minLng + "&maxLng=" + maxLng + "&minLat=" + minLat + "&maxLat=" + maxLat, string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                JToken content = jobject["RestaurantInfo"];

                foreach (JToken pointToken in content)
                {
                    points.Add(WGSGCJLatLonHelper.GCJ02ToWGS84(new MapPoint(Convert.ToDouble(pointToken["Lng"].ToString()), Convert.ToDouble(pointToken["Lat"].ToString()), SpatialReferences.Wgs84)));
                }

                Dictionary<string, object> param = new Dictionary<string, object>(2)
                {
                    { "type",ViewSpotArounds.Restaurant },
                    { "points", points}
                };

                ArcGISMapCommands.AddViewSpotAround.Execute(param, Application.Current.MainWindow);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }
        }

        private async void HotelPackIconModern_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int distance = 1000;

            MapPoint centerMercator = GeometryEngine.Project(new MapPoint(DetailShowItem.GetLng(), DetailShowItem.GetLat(), SpatialReferences.Wgs84),
                SpatialReferences.WebMercator) as MapPoint;

            MapPoint leftTopMercator = new MapPoint(centerMercator.X - distance, centerMercator.Y + distance, centerMercator.SpatialReference);
            MapPoint rightBottomMercator = new MapPoint(centerMercator.X + distance, centerMercator.Y - distance, centerMercator.SpatialReference);

            MapPoint leftTopWgs84 = GeometryEngine.Project(leftTopMercator, SpatialReferences.Wgs84) as MapPoint;
            MapPoint rightBottomWgs84 = GeometryEngine.Project(rightBottomMercator, SpatialReferences.Wgs84) as MapPoint;

            double minLng = leftTopWgs84.X;
            double maxLng = rightBottomWgs84.X;
            double minLat = rightBottomWgs84.Y;
            double maxLat = leftTopWgs84.Y;

            List<MapPoint> points = new List<MapPoint>();

            //API返回内容
            string jsonString = string.Empty;

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_HOTEL"] + "?minLng=" + minLng + "&maxLng=" + maxLng + "&minLat=" + minLat + "&maxLat=" + maxLat, string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                JToken content = jobject["HotelInfo"];

                foreach (JToken pointToken in content)
                {
                    points.Add(WGSGCJLatLonHelper.GCJ02ToWGS84(new MapPoint(Convert.ToDouble(pointToken["Lng"].ToString()), Convert.ToDouble(pointToken["Lat"].ToString()), SpatialReferences.Wgs84)));
                }

                Dictionary<string, object> param = new Dictionary<string, object>(2)
                {
                    { "type",ViewSpotArounds.Hotel },
                    { "points", points}
                };

                ArcGISMapCommands.AddViewSpotAround.Execute(param, Application.Current.MainWindow);
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
