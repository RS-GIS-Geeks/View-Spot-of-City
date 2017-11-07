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
using System.Windows.Shapes;

using View_Spot_of_City.UIControls.OverLayer;

namespace View_Spot_of_City.UIControls.Form
{
    /// <summary>
    /// VisualizationParamsPicker.xaml 的交互逻辑
    /// </summary>
    public partial class VisualizationParamsPicker : Window
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate = new DateTime();

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate = new DateTime();

        /// <summary>
        /// 构造函数
        /// </summary>
        public VisualizationParamsPicker()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            StartDate = StartDatePicker.SelectedDate ?? new DateTime(2015, 1, 1);
            EndDate = EndDatePicker.SelectedDate ?? new DateTime(2017, 12, 30);

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
