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
    public partial class SpecifiedInfo : UserControl
    {
        public SpecifiedInfo(string txt1, string txt2, string txt3, string txt4 = null, double fontSize1 = 14, double fontSize2 = 14, double fontSize3 = 14, double fontSize4 = 14)
        {
            this.InitializeComponent();
            this.IsHitTestVisible = false;

            text1.Text = txt1;
            text1.FontSize = fontSize1;

            text2.Text = txt2;
            text2.FontSize = fontSize2;

            text3.Text = txt3;
            text3.FontSize = fontSize3;

            if(txt4 != null)
            {
                text4.Text = txt4;
                text4.FontSize = fontSize4;
            }
            else
            {
                text4.Height = 0;
            }
            
            this.MouseWheel += ShowInfo_MouseWheel;
        }

        private void ShowInfo_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = false;
        }
    }
}