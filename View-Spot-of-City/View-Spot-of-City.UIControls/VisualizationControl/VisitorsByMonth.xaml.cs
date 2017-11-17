using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using LiveCharts;
using LiveCharts.Wpf;

using View_Spot_of_City.ClassModel;
using System.ComponentModel;

namespace View_Spot_of_City.UIControls.VisualizationControl
{
    /// <summary>
    /// VisitorsByMonth.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorsByMonth : VisualizationControlBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        SeriesCollection _SeriesCollection = new SeriesCollection();

        public SeriesCollection SeriesCollection
        {
            get { return _SeriesCollection; }
            set
            {
                _SeriesCollection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SeriesCollection"));
            }
        }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }

        public VisitorsByMonth()
        {
            InitializeComponent();
            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        /// <summary>
        /// 外部初始化
        /// </summary>
        public void Init(List<VisitorItem> visitorList)
        {
            this.Visibility = Visibility.Visible;
            
            List<VisitorItem> visitorItemList = new List<VisitorItem>();
            ShowVisitorsInfo(SeriesCollection, visitorList);
        }

        /// <summary>
        /// 显示线状图
        /// </summary>
        /// <param name="SeriesCollection"></param>
        /// <param name="visitorItemList"></param>
        private void ShowVisitorsInfo(SeriesCollection SeriesCollection, List<VisitorItem> visitorItemList)
        {
            if(visitorItemList != null && visitorItemList.Count > 0)
            {
                SeriesCollection.Clear();
                SeriesCollection.Add(new LineSeries()
                {
                    Title = visitorItemList[0].ViewSpotName,
                    Stroke = Brushes.Red
                });

                if(visitorItemList.Count < 18)
                    ContentScroll.Width = 40 * visitorItemList.Count;
                else if(visitorItemList.Count >= 18 && visitorItemList.Count < 24)
                    ContentScroll.Width = 30 * visitorItemList.Count;
                else
                    ContentScroll.Width = 20 * visitorItemList.Count;

                SeriesCollection[0].Values = new ChartValues<int>();
                Labels = new string[visitorItemList.Count];
                int index = 0;
                foreach (VisitorItem visitorItem in visitorItemList)
                {
                    SeriesCollection[0].Values.Add(visitorItem.Visitors);
                    Labels[index++] = visitorItem.Year + " " + visitorItem.Month;
                }
            }
        }
    }
}
