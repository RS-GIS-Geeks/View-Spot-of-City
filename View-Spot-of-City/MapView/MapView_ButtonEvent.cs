using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GMapView.Markers;
using System.Windows.Controls.Primitives;

namespace GMapView
{
    /// <summary>
    /// GMapView.xaml 的交互逻辑
    /// </summary>
    public partial class GMapView : UserControl
    {
        #region ButtonEnvent
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentMapAsImag();
        }

        private void btnMeasureLine_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                InitOperation(false);

                mapControl.lineWidth = 2;
                mapControl.brush = new SolidColorBrush(Color.FromRgb(255, 136, 76));
                mapControl.opacity = 0.8;

                mouseMarker.Shape = new MyIcon(mapControl, new BitmapImage(new Uri("pack://application:,,,/GMapView;component/Icon/结点0.png", UriKind.RelativeOrAbsolute)), 8, 8);
                mouseMarker.Offset = new Point(-4, -4);
                mouseSubMarker.Shape = new MyIcon(mapControl, new BitmapImage(new Uri("pack://application:,,,/GMapView;component/Icon/尺子.png", UriKind.RelativeOrAbsolute)), 25, 25);
                mouseTip.Shape = new SpecifiedInfo("总长：", "0", "米", "单击确定地点，双击结束", 15, 15, 15, 14);

                operationNow = Operations.MeasurePolyLine;
            }
            else
            {
                InitOperation();
            }
        }

        private void btnMeasurePolygon_Click(object sender, RoutedEventArgs e)
        {
            if((sender as ToggleButton).IsChecked == true)
            {
                InitOperation(false);

                mapControl.lineWidth = 3;
                mapControl.brush = new SolidColorBrush(Color.FromRgb(247, 186, 146));
                mapControl.opacity = 0.7;

                mouseMarker.Shape = new MyIcon(mapControl, new BitmapImage(new Uri("pack://application:,,,/GMapView;component/Icon/结点0.png", UriKind.RelativeOrAbsolute)), 8, 8);
                mouseMarker.Offset = new Point(-4, -4);
                mouseTip.Shape = new ShowInfo("单击确定位置，双击结束", 14, Color.FromRgb(167, 167, 167));

                operationNow = Operations.MeasurePolygon;
            }
            else
            {
                InitOperation();
            }
        }

        private void btnZoomToLayer_Click(object sender, RoutedEventArgs e)
        {
            ZoomToAll();
        }
        
        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            mapControl.Zoom++;
            e.Handled = true;
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            mapControl.Zoom--;
            e.Handled = true;
        }
        #endregion
    }
}
