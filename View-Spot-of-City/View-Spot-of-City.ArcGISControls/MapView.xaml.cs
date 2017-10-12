using System;
using System.Collections.Generic;
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

using static System.Configuration.ConfigurationManager;

namespace View_Spot_of_City.ArcGISControls
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
            //mapView.SetViewpointCenterAsync(Convert.ToDouble(AppSettings["MAP_CENTER_LAT"]), Convert.ToDouble(AppSettings["MAP_CENTER_LNG"]), Convert.ToDouble(AppSettings["ARCGIS_MAP_ZOOM"]));
        }
    }
}
