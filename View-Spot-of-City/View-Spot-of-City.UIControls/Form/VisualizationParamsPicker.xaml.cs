using System;
using System.Windows;

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
