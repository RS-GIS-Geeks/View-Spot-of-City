using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
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

namespace View_Spot_of_City.UIControls.OverLayer
{
    /// <summary>
    /// ShowMap.xaml 的交互逻辑
    /// </summary>
    public partial class ShowMap : UserControl
    {
        public ShowMap()
        {
            InitializeComponent();
            mapView.Map = new Map(new Uri(AppSettings["ARCGIS_BASEMAP"]));
            if (!mapView.LocationDisplay.IsEnabled)
                mapView.LocationDisplay.IsEnabled = true;
            mapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.CompassNavigation;
        }
    }
}
