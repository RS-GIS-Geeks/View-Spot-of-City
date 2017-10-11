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

namespace View_Spot_of_City.VisualControls
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class GeoHeatMap : UserControl
    {
        public Dictionary<string, double> Values { get; set; }
        public Dictionary<string, string> LanguagePack { get; set; }

        public GeoHeatMap()
        {
            InitializeComponent();

            var r = new Random();

            Values = new Dictionary<string, double>();

            Values["1495"] = r.Next(0, 100);
            Values["1512"] = r.Next(0, 100);
            Values["6318"] = r.Next(0, 100);
            Values["1226"] = r.Next(0, 100);
            Values["2283"] = r.Next(0, 100);
            Values["1876"] = r.Next(0, 100);
            Values["2322"] = r.Next(0, 100);
            Values["2039"] = r.Next(0, 100);
            Values["2327"] = r.Next(0, 100);
            Values["1923"] = r.Next(0, 100);
            Values["1896"] = r.Next(0, 100);
            Values["2286"] = r.Next(0, 100);
            Values["2285"] = r.Next(0, 100);
            Values["2329"] = r.Next(0, 100);
            Values["2330"] = r.Next(0, 100);
            Values["3664"] = r.Next(0, 100);

            LanguagePack = new Dictionary<string, string>();
            LanguagePack["1495"] = "重庆";
            LanguagePack["2322"] = "湖北";

            DataContext = this;
        }
    }
}
