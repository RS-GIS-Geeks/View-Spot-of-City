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
using View_Spot_of_City.UIControls.OverLayer;
using View_Spot_of_City.ViewModel;
using Config = System.Configuration.ConfigurationManager;

namespace View_Spot_of_City
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 覆盖面板组
        /// </summary>
        List<OverlayerItemViewModel> _overlayers = new List<OverlayerItemViewModel>();

        /// <summary>
        /// 获取覆盖面板组
        /// </summary>
        List<OverlayerItemViewModel> Overlayers { get { return _overlayers; } }

        public MainWindow()
        {
            InitializeComponent();
            InitWindows();
        }

        private void InitWindows()
        {
            this.Title = Convert.ToString(Config.AppSettings["SOFTWARE_NAME"]) + " - " + Convert.ToString(Config.AppSettings["CITY_NAME"]);
            AppTitle.Text = (string)Application.Current.FindResource("MainTitle");

            /// 添加覆盖层
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
                new OverLayerExample())
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

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainNavBar.SelectedIndex = -1;
        }
    }
}
