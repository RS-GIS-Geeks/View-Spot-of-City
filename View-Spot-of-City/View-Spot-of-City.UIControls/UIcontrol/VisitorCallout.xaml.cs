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
using View_Spot_of_City.ClassModel;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// VisitorCallout.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorCallout : UserControl
    {
        public VisitorCallout(List<VisitorItem> visitorItemList)
        {
            InitializeComponent();
            LineChart.Init(visitorItemList);
        }
    }
}
