using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using Config = System.Configuration.ConfigurationManager;

using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Mapping;

using View_Spot_of_City.ViewModel;
using View_Spot_of_City.UIControls.Helper;
using View_Spot_of_City.UIControls.Progress;
using View_Spot_of_City.UIControls.OverLayer;
using View_Spot_of_City.ClassModel;
using View_Spot_of_City.Form;
using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.UIControls.Command;
using static View_Spot_of_City.Converter.Enum2UIControl;
using static View_Spot_of_City.Language.Language.LanguageDictionaryHelper;
using static View_Spot_of_City.UIControls.Command.ArcGISMapCommands;

namespace View_Spot_of_City
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 当前应用程序实例
        /// </summary>
        App _currentApp = null;

        /// <summary>
        /// 当前应用程序实例
        /// </summary>
        public App CurrentApp
        {
            get { return _currentApp; }
            set
            {
                _currentApp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentApp"));
            }
        }
        
        /// <summary>
        /// 获取覆盖面板组
        /// </summary>
        public List<OverlayerItemViewModel> Overlayers { get; internal set; }

        /// <summary>
        /// 浏览地图面板
        /// </summary>
        public OverlayerItemViewModel ViewMapOverLayer { get; internal set; }

        /// <summary>
        /// 查询景点面板
        /// </summary>
        public OverlayerItemViewModel SpotSearchOverLayer { get; internal set; }

        /// <summary>
        /// 景点推荐面板
        /// </summary>
        public OverlayerItemViewModel SpotRecommendOverlay { get; internal set; }

        /// <summary>
        /// 可视化面板
        /// </summary>
        public OverlayerItemViewModel VisualizationOverlay { get; internal set; }

        /// <summary>
        /// 分享面板
        /// </summary>
        public OverlayerItemViewModel ShareOverlay { get; internal set; }

        /// <summary>
        /// 圆形启动界面
        /// </summary>
        CircleProgressAsync circleProgressBox = new CircleProgressAsync();

        /// <summary>
        /// 用于关闭启动进度条的定时器
        /// </summary>
        DispatcherTimer closeCircleTimer = new DispatcherTimer();

        /// <summary>
        /// 当前显示主控件
        /// </summary>
        MainControls? _mainControl = null;

        /// <summary>
        /// 当前显示主控件
        /// </summary>
        public MainControls? mainControl
        {
            get { return _mainControl; }
            set
            {
                if(_mainControl != value)
                {
                    _mainControl = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("mainControl"));
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainWindow()
        {
            ShowCircleProgressBox();

            InitializeComponent();

            InitParams();

            InitWindows();
        }

        /// <summary>
        /// 初始化变量
        /// </summary>
        private void InitParams()
        {
            Application.Current.MainWindow = this;
            this.CurrentApp = Application.Current as App;

            closeCircleTimer.Tick += new EventHandler(CloseCircleTimer_Tick);
            closeCircleTimer.Interval = new TimeSpan(0, 0, Convert.ToInt32(Config.AppSettings["ARCGIS_MAP_NetWork_Delay"]));
        }

        /// <summary>
        /// 显示圆形进度框
        /// </summary>
        public void ShowCircleProgressBox()
        {
            Thread thread = new Thread(new ThreadStart(circleProgressBox.Begin))
            {
                IsBackground = true
            };
            thread.Start();
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        public void InitWindows()
        {
            InitMainNavBar();
        }

        /// <summary>
        /// 初始化导航条
        /// </summary>
        private void InitMainNavBar()
        {
            mainControl = MainControls.ArcGISMapView;

            this.Title = Convert.ToString(Config.AppSettings["SOFTWARE_NAME"]) + " - " + Convert.ToString(Config.AppSettings["CITY_NAME"]);

            MainNavBar.IsEnabled = false;

            // 添加覆盖层
            // 静态绑定
            ViewMapOverLayer = new OverlayerItemViewModel(
                "pack://application:,,,/Icon/3D-Glasses.png",
                "MainNav_MapView",
                null)
            { OverlayerMargin = new Thickness(0, 20, 20, 0), HAlignType = System.Windows.HorizontalAlignment.Right, VAlignType = System.Windows.VerticalAlignment.Top, OverlayerIndicator = OverlayerType.ShowMap };

            SpotSearchOverLayer = new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Find.png",
                "MainNav_SpotQuery",
                new SpotQuery())
            { OverlayerIndicator = OverlayerType.SpotQuery };

            SpotRecommendOverlay = new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Device.png",
                "MainNav_SpotRecommend",
                new SpotRecommend())
            { OverlayerIndicator = OverlayerType.SpotRecommend };

            VisualizationOverlay = new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Horizontal-Align-Left.png",
                "MainNav_Visualization",
                new Visualization())
            { OverlayerMargin = new Thickness(0), HAlignType = System.Windows.HorizontalAlignment.Stretch, OverlayerIndicator = OverlayerType.Visualization };

            ShareOverlay = new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Talk.png",
                "MainNav_Share",
                new Share() { CurrentUser = CurrentApp.CurrentUser })
            { OverlayerIndicator = OverlayerType.Share };

            Overlayers = new List<OverlayerItemViewModel>(5)
            {
                ViewMapOverLayer,
                SpotSearchOverLayer,
                SpotRecommendOverlay,
                VisualizationOverlay,
                ShareOverlay
            };

            MainNavBar.ItemsSource = Overlayers;
            MainNavBar.IsEnabled = true;
        }
        
        /// <summary>
        /// 窗口加载完毕响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //CurrentApp.GetViewSpotsData();

            //Random rand = new Random();
            //List<ViewSpot> viewSpotForShow = new List<ViewSpot>(100);
            //for (int i = 0; i < 100; i++)
            //{
            //    viewSpotForShow.Add(CurrentApp.ViewSpotList[i]);
            //}

            //开始计时，计时完成后即关闭启动进度条
            closeCircleTimer.Start();

            this.Activate();
        }

        /// <summary>
        /// 关闭启动进度条的计时器响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseCircleTimer_Tick(object sender, EventArgs e)
        {
            if (circleProgressBox != null)
                circleProgressBox.CloseProgress();
            closeCircleTimer.Stop();
            ArcGISMapView.SetScaleAndLoction(new MapPoint(Convert.ToDouble(Config.AppSettings["MAP_CENTER_LNG"]), Convert.ToDouble(Config.AppSettings["MAP_CENTER_LAT"]), SpatialReferences.Wgs84), Convert.ToDouble(Config.AppSettings["ARCGIS_MAP_ZOOM"]));
            ArcGISSceneView.SetScaleAndLoction(new Camera(Convert.ToDouble(Config.AppSettings["MAP_CENTER_LAT"]), Convert.ToDouble(Config.AppSettings["MAP_CENTER_LNG"]), Convert.ToDouble(Config.AppSettings["ARCGIS_SENCE_HEADING"]), 0, 0, 0));
        }

        /// <summary>
        /// Logo点击响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (new RepoInfoWindow()).ShowDialog();
            e.Handled = true;
        }

        /// <summary>
        /// 切换菜单响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainNavBar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainNavBar.SelectedIndex != -1 && e.RemovedItems != null && e.RemovedItems.Count > 0)
            {
                var removedItem = e.RemovedItems[0] as OverlayerItemViewModel;
            }
            if (MainNavBar.SelectedIndex == 3)
                mainControl = MainControls.ArcGISSceneView;
            else
                mainControl = MainControls.ArcGISMapView;

            if(MainNavBar.SelectedIndex == 0)
                ArcGISMapView.SetScaleAndLoction(new MapPoint(Convert.ToDouble(Config.AppSettings["MAP_CENTER_LNG"]), Convert.ToDouble(Config.AppSettings["MAP_CENTER_LAT"]), SpatialReferences.Wgs84), Convert.ToDouble(Config.AppSettings["ARCGIS_MAP_ZOOM"]));
        }

        /// <summary>
        /// 点击header事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainNavBar.SelectedIndex = -1;
        }

        /// <summary>
        /// 点击用户信息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInfoButton_Click(object sender, RoutedEventArgs e)
        {
            UserInfoDlg userInfoDlg = new UserInfoDlg();
            userInfoDlg.ShowDialog();
        }

        /// <summary>
        /// 点击退出登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageboxMaster.DialogResults.Yes != MessageboxMaster.Show(GetString("Logout_Tip"), GetString("MessageBox_Tip_Title"), MessageboxMaster.MyMessageBoxButtons.YesNo, MessageboxMaster.MyMessageBoxButton.Yes))
                return;
            LogManager.LogManager.Info("用户[" + CurrentApp.CurrentUser.Mail + "]注销");
            string mail = CurrentApp.CurrentUser.Mail;
            (Application.Current as App).CurrentUser = UserInfo.NoBody;

            //登录
            bool? loginDlgResult = (new LoginDlg(mail)).ShowDialog();
            if (!loginDlgResult.HasValue || !loginDlgResult.Value)
                Application.Current.Shutdown();
        }

        private void mainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (MessageboxMaster.DialogResults.Yes == MessageboxMaster.Show(GetString("MainWindowCloseConfirm"), GetString("MessageBox_Tip_Title"), MessageboxMaster.MyMessageBoxButtons.YesNo, MessageboxMaster.MyMessageBoxButton.Yes))
            {
                ArcGISMapView.ChangeLoctionDisplayEnable(false);
                Application.Current.Shutdown(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            LogManager.LogManager.Info("关闭软件");
            Environment.Exit(0);
            //Application.Current.Shutdown();
        }

        private void LanguageSelecter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //切换语言字典
            string requestedCulture = string.Format(@"pack://application:,,,/View-Spot-of-City.Language;component/Language/Language.{0}.xaml", languageDictionary[LanguageSelecter.SelectedIndex]);
            ResourceDictionary resourceDictionary = Application.Current.Resources.MergedDictionaries.FirstOrDefault((x) =>
            {
                return (x.Source == null) ? false : (x.Source.OriginalString.Contains("View-Spot-of-City.Language"));
            });
            if (resourceDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                ResourceDictionary requestDictionary = new ResourceDictionary()
                {
                    Source = new Uri(requestedCulture)
                };
                Application.Current.Resources.MergedDictionaries.Add(requestDictionary);
            }

            //固定字符修改
            if (Overlayers != null)
            {
                Overlayers.ForEach((x) =>
                {
                    x.TitleKey = x.TitleKey;
                });
            }
        }

        /// <summary>
        /// 在地图上添加要素命令执行函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowFeatureOnMapCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Dictionary<string, object> param = e.Parameter as Dictionary<string, object>;
            GraphicsOverlay graphicsOverlay = ArcGISMapView.PointOverlay;
            MapPoint location = new MapPoint((double)param["Lng"], (double)param["Lat"], SpatialReferences.Wgs84);
            Uri iconUri = param["IconUri"] as Uri;
            double width = (double)param["Width"];
            double height = (double)param["Height"];
            double offsetX = (double)param["OffsetX"];
            double offsetY = (double)param["OffsetY"];
            ViewSpot data = param["Data"] as ViewSpot;
            ArcGISMapView.AddIconToGraphicsOverlay(graphicsOverlay, location, iconUri, width, height, offsetX, offsetY, data);
        }

        /// <summary>
        /// 设置地图显示，并显示地图中心要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetScaleAndLocationCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewSpot data = e.Parameter as ViewSpot;

            foreach (Graphic g in ArcGISMapView.PointOverlay.Graphics)
            {
                MapPoint point = g.Geometry as MapPoint;
                //如果经纬度匹配
                if (point.X == data.GetLng() && point.Y == data.GetLat())
                {
                    //更改图标
                    PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol(IconDictionaryHelper.IconDictionary[IconDictionaryHelper.Icons.pin])
                    {
                        Width = 16,
                        Height = 24,
                        OffsetX = 0,
                        OffsetY = 9.5
                    };
                    g.Symbol = pictureMarkerSymbol;
                }
                else
                {
                    //恢复其他已被改变的图标
                    if((g.Symbol as PictureMarkerSymbol).Uri != IconDictionaryHelper.IconDictionary[IconDictionaryHelper.Icons.pin_blue])
                    {
                        PictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbol(IconDictionaryHelper.IconDictionary[IconDictionaryHelper.Icons.pin_blue])
                        {
                            Width = 16,
                            Height = 24,
                            OffsetX = 0,
                            OffsetY = 9.5
                        };
                        g.Symbol = pictureMarkerSymbol;
                    }
                }
            }
            //改变地图视角
            ArcGISMapView.SetScaleAndLoction(new MapPoint(data.GetLng(), data.GetLat(), SpatialReferences.Wgs84), 10000);
        }
        

        /// <summary>
        /// 清除指定图层的要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearFeaturesCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
                ArcGISMapView.ClearFeatureOnGraphicsOverlay(ArcGISMapView.GraphicsOverlays[(int)e.Parameter]);
        }

        /// <summary>
        /// 添加景点周边要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddViewSpotAroundCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter != null)
            {
                Dictionary<string, object> param = e.Parameter as Dictionary<string, object>;
                ViewSpotArounds type = (ViewSpotArounds)param["type"];
                List<MapPoint> points = param["points"] as List<MapPoint>;
                if (type == ViewSpotArounds.GasStation)
                    ArcGISMapView.AddGasStationToGraphicsOverlay(points);
                else if(type == ViewSpotArounds.TrafficStation)
                    ArcGISMapView.AddTrafficStationToGraphicsOverlay(points);
                else if (type == ViewSpotArounds.Restaurant)
                    ArcGISMapView.AddRestaurantToGraphicsOverlay(points);
                else if (type == ViewSpotArounds.Hotel)
                    ArcGISMapView.AddHotelToGraphicsOverlay(points);
            }
        }

        /// <summary>
        /// 地图导航命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToSomeWhereCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ArcGISMapView.NavigateToSomeWhereAsync(e.Parameter as MapPoint);
        }

        /// <summary>
        /// 命令转发到覆盖面板命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandsFreightStationToOverlayerCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Tuple<object, int> param = e.Parameter as Tuple<object, int>;
            ViewSpot viewspot = param.Item1 as ViewSpot;
            int overlayIndex = param.Item2;
            if (viewspot != null && overlayIndex >= 0 && overlayIndex < 4)
                ViewSpotViewerCommands.ShowViewSpotDetail.Execute(viewspot, Overlayers[overlayIndex].Content as UserControl);
        }

        /// <summary>
        /// 清除回调框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearCalloutCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ArcGISMapView.DismissCallout();
        }
        
        /// <summary>
        /// 添加人流量数据到Sence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddVisitorsDataCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<List<VisitorItem>> visitorList = e.Parameter as List<List<VisitorItem>>;
            ArcGISSceneView.AddVisitorGraphicToOverlay(visitorList);
        }

        /// <summary>
        /// 改变Sence中现有人流量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeVisitorsDataCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<VisitorItem> visitorList = e.Parameter as List<VisitorItem>;
            ArcGISSceneView.ChangeAttributesOfVisitorGraphics(visitorList);
        }
        
        /// <summary>
        /// 改变Sence底图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeBaseMapCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ArcGISSceneView.ChangeBaseMap(e.Parameter as string);
        }
    }
}
