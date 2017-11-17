using System.Windows;

namespace View_Spot_of_City.UIControls.Form
{
    /// <summary>
    /// SpotStatistics.xaml 的交互逻辑
    /// </summary>
    public partial class SpotStatistics : Window
    {
        public SpotStatistics(long viewId)
        {
            InitializeComponent();
            visitorsByYear.Init(viewId);
        }
    }
}
