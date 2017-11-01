using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using Config = System.Configuration.ConfigurationManager;

using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Geometry;
using View_Spot_of_City.UIControls.UIcontrol;
using Esri.ArcGISRuntime.Symbology;

using View_Spot_of_City.ViewModel;
using View_Spot_of_City.UIControls.Helper;
using View_Spot_of_City.UIControls.Progress;
using View_Spot_of_City.UIControls.OverLayer;
using View_Spot_of_City.ClassModel;
using View_Spot_of_City.Form;
using View_Spot_of_City.UIControls.Form;
using static View_Spot_of_City.Converter.Enum2UIControl;
using static View_Spot_of_City.Language.Language.LanguageDictionaryHelper;
using View_Spot_of_City.UIControls.Command;

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
        //static CircleProgressBox circleProgressBox = null;
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
            closeCircleTimer.Interval = new TimeSpan(0, 0 ,1);
        }

        /// <summary>
        /// 显示圆形进度框
        /// </summary>
        public void ShowCircleProgressBox()
        {
            //circleProgressBox = new CircleProgressBox();
            //circleProgressBox.ShowPregress();
            //circleProgressBox.SetDefaultDescription();
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
            //CreateAppStyleBy(this, ((SolidColorBrush)Application.Current.FindResource("PrimaryHueMidBrush")).Color, true);
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
            { OverlayerIndicator = OverlayerType.SpotQuery };

            SpotSearchOverLayer = new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Find.png",
                "MainNav_SpotQuery",
                new SpotQuery())
            { OverlayerIndicator = OverlayerType.SpotQuery };

            SpotRecommendOverlay = new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Device.png",
                "MainNav_SpotRecommend",
                new OverLayerExample())
            { OverlayerIndicator = OverlayerType.SpotRecommend };

            VisualizationOverlay = new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Horizontal-Align-Left.png",
                "MainNav_Visualization",
                new Visualization())
            { OverlayerIndicator = OverlayerType.Visualization };

            ShareOverlay = new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Talk.png",
                "MainNav_Share",
                new OverLayerExample())
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
        }

        /// <summary>
        /// Logo点击响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Hyperlink link = new Hyperlink();
            {
                link.NavigateUri = new Uri(@"https://github.com/RS-GIS-Geeks/View-Spot-of-City");
            }
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
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
                mainControl = MainControls.GMap;
            else
                mainControl = MainControls.ArcGISMapView;
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
            string mail = CurrentApp.CurrentUser.Mail;
            (Application.Current as App).CurrentUser = UserInfo.NoBody;

            //登录
            bool? loginDlgResult = (new LoginDlg(mail)).ShowDialog();
            if (!loginDlgResult.HasValue || !loginDlgResult.Value)
                //Environment.Exit(0);
                Application.Current.Shutdown();
        }

        private void mainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (MessageboxMaster.DialogResults.Yes == MessageboxMaster.Show(GetString("MainWindowCloseConfirm"), GetString("MessageBox_Tip_Title"), MessageboxMaster.MyMessageBoxButtons.YesNo, MessageboxMaster.MyMessageBoxButton.Yes))
                Application.Current.Shutdown(0);
            else
                e.Cancel = true;
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            //Environment.Exit(0);
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
        
    }
}
