using System.Collections.Generic;
using System.Windows.Controls;

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
