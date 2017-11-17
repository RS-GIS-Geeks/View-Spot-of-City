using System;
using System.Windows.Controls;
using static System.Configuration.ConfigurationManager;

using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;

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
