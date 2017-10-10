using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.ComponentModel;

using View_Spot_of_City.ViewModel;
using View_Spot_of_City.Converter;
using View_Spot_of_City.UIControls.Progress;
using View_Spot_of_City.UIControls.OverLayer;
using SpeechLib;
using MahApps.Metro.Controls;
using Config = System.Configuration.ConfigurationManager;
using static View_Spot_of_City.Converter.Enum2UIControl;

namespace View_Spot_of_City
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 覆盖面板组
        /// </summary>
        List<OverlayerItemViewModel> _overlayers = new List<OverlayerItemViewModel>();

        /// <summary>
        /// 获取覆盖面板组
        /// </summary>
        List<OverlayerItemViewModel> Overlayers { get { return _overlayers; } }

        /// <summary>
        /// 圆形启动界面
        /// </summary>
        static CircleProgressBox circleProgressBox = null;

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
            InitializeComponent();

            ShowCircleProgressBox();

            InitWindows();
        }

        /// <summary>
        /// 显示圆形进度框
        /// </summary>
        public void ShowCircleProgressBox()
        {
            circleProgressBox = new CircleProgressBox();
            circleProgressBox.ShowPregress();
            circleProgressBox.SetDefaultDescription();
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
            AppTitle.Text = (string)Application.Current.FindResource("MainTitle");

            // 添加覆盖层
            // 静态绑定
            Overlayers.Add(new OverlayerItemViewModel(
                "pack://application:,,,/Icon/3D-Glasses.png",
                "MainNav_MapView",
                null)
            { VAlignType = VerticalAlignment.Top, OverlayerIndicator = OverlayerType.SpotQuery }
            );
            Overlayers.Add(new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Find.png",
                "MainNav_SpotQuery",
                new SpotQuery())
            { VAlignType = VerticalAlignment.Top, OverlayerIndicator = OverlayerType.SpotQuery }
            );
            Overlayers.Add(new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Device.png",
                "MainNav_SpotRecommend",
                new OverLayerExample())
            { OverlayerIndicator = OverlayerType.SpotRecommend }
            );
            Overlayers.Add(new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Horizontal-Align-Left.png",
                "MainNav_Visualization",
                new Visualization())
            { OverlayerIndicator = OverlayerType.Visualization }
            );
            Overlayers.Add(new OverlayerItemViewModel(
                "pack://application:,,,/Icon/Talk.png",
                "MainNav_Share",
                new OverLayerExample())
            { OverlayerIndicator = OverlayerType.Share }
            );
            MainNavBar.ItemsSource = Overlayers;
        }
        
        /// <summary>
        /// 窗口加载完毕响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (circleProgressBox != null)
                circleProgressBox.CloseProgress();
            this.Activate();
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
    }
}
