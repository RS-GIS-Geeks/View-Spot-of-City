using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// RepoInfo.xaml 的交互逻辑
    /// </summary>
    public partial class RepoInfo : UserControl
    {
        public RepoInfo()
        {
            InitializeComponent();
        }

        private void DonateButton_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = new Hyperlink();
            {
                link.NavigateUri = new Uri(@"https://github.com/RS-GIS-Geeks/View-Spot-of-City");
            }
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
    }
}
