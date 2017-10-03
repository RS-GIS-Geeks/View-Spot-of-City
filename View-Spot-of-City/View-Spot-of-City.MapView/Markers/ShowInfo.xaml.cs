using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View_Spot_of_City.MapView.Markers
{
    /// <summary>
    /// ShowInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ShowInfo : UserControl
    {
        public ShowInfo(string title, double fontSize = 14, Color? fontColor = null)
        {
            this.InitializeComponent();

            text.Text = title;

            this.IsHitTestVisible = false;

            text.Foreground = new SolidColorBrush((fontColor == null) ? (Color.FromRgb(85, 85, 85)) : (Color)fontColor);
            text.FontSize = fontSize;

            this.MouseWheel += ShowInfo_MouseWheel;
        }

        private void ShowInfo_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = false;
        }
    }
}