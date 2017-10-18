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
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using Config = System.Configuration.ConfigurationManager;

using View_Spot_of_City.ViewModel;
using View_Spot_of_City.UIControls.Progress;
using View_Spot_of_City.UIControls.OverLayer;
using View_Spot_of_City.ClassModel;
using View_Spot_of_City.Form;
using static View_Spot_of_City.UIControls.Theme.MetroThemeMaster;
using static View_Spot_of_City.Converter.Enum2UIControl;
using static View_Spot_of_City.Language.Language.LanguageDictionaryHelper;
using View_Spot_of_City.Language.Language;
using View_Spot_of_City.UIControls.Form;

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
            Thread thread = new Thread(new ThreadStart(circleProgressBox.Begin));
            thread.IsBackground = true;
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

            //mainControl = MainControls.Browser;
            //Browser.Instance.Navigate(@"https://github.com/RS-GIS-Geeks/View-Spot-of-City");
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
            if (MessageboxMaster.DialogResults.Yes != MessageboxMaster.Show(GetString("Logout_Tip"), GetString("MessageBox_Tip_Title"), MessageboxMaster.MyMessageBoxButtons.YesNo))
                return;
            App.CurrentUser = user.NoBody;

            //登录
            bool? loginDlgResult = (new LoginDlg()).ShowDialog();
            if (!loginDlgResult.HasValue || !loginDlgResult.Value)
                //Environment.Exit(0);
                Application.Current.Shutdown();
        }

        private void mainWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            //Environment.Exit(0);
            Application.Current.Shutdown();
        }

        private void LanguageSelecter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /// 切换语言字典
            string requestedCulture = string.Format(@"pack://application:,,,/View-Spot-of-City.Language;component/Language/Language.{0}.xaml", languageDictionary[LanguageSelecter.SelectedIndex]);
            ResourceDictionary resourceDictionary = Application.Current.Resources.MergedDictionaries.FirstOrDefault((x) =>
            {
                return (x.Source == null) ? false : (x.Source.OriginalString.Contains("View-Spot-of-City.Language"));
            });
            if (resourceDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                ResourceDictionary requestDictionary = new ResourceDictionary();
                requestDictionary.Source = new Uri(requestedCulture);
                Application.Current.Resources.MergedDictionaries.Add(requestDictionary);
            }

            //固定字符修改
            Overlayers.ForEach((x) =>
            {
                x.TitleKey = x.TitleKey;
            });
        }
    }
}
