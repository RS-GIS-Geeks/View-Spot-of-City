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
using System.ComponentModel;
using View_Spot_of_City.UIControls.Form;

namespace View_Spot_of_City.UIControls.OverLayer
{
    /// <summary>
    /// Visualization.xaml 的交互逻辑
    /// </summary>
    public partial class Visualization : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        double _SliderMinValue = 0;

        /// <summary>
        /// Slider最小值
        /// </summary>
        public double SliderMinValue
        {
            get { return _SliderMinValue; }
            set
            {
                _SliderMinValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SliderMinValue"));
            }
        }

        double _SliderMaxValue = 100;

        /// <summary>
        /// Slider最大值
        /// </summary>
        public double SliderMaxValue
        {
            get { return _SliderMaxValue; }
            set
            {
                _SliderMaxValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SliderMaxValue"));
            }
        }

        double _SliderValue = 0;

        /// <summary>
        /// 当前Slider的值
        /// </summary>
        public double SliderValue
        {
            get { return _SliderValue; }
            set
            {
                _SliderValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SliderValue"));
            }
        }

        /// <summary>
        /// 显示模式枚举
        /// </summary>
        public enum ShowMode : int
        {
            ByYear = 0,
            ByMonth = 1,
            ByDay = 2
        }

        /// <summary>
        /// 数据显示模式
        /// </summary>
        public ShowMode DataShowMaode = ShowMode.ByDay;
        
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
        public Visualization()
        {
            InitializeComponent();
        }

        private void Silder_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void SettingBtn_Click(object sender, RoutedEventArgs e)
        {
            VisualizationParamsPicker picker = new VisualizationParamsPicker();
            if(picker.ShowDialog() == true)
            {
                StartDate = picker.StartDate;
                EndDate = picker.EndDate;
                DataShowMaode = picker.ShowMode;
            }
        }
    }
}
