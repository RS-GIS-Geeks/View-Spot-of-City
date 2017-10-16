using SpeechLib;
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

namespace View_Spot_of_City.UIControls.OverLayer
{
    /// <summary>
    /// SpotQuery.xaml 的交互逻辑
    /// </summary>
    public partial class SpotQuery : UserControl
    {
        public SpotQuery()
        {
            InitializeComponent();
        }
        
        /// <summary>
         /// 地址文本框获得焦点
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void AddressTextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 文本框失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddressTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        private void PositionSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StartPointAddress.Text != null)
            {
                SpVoice voice = new SpVoice();//SAPI 5.4
                voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                voice.Speak(StartPointAddress.Text);
            }
        }
    }
}
