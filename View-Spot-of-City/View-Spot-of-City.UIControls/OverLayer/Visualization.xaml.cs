using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Threading;
using static System.Configuration.ConfigurationManager;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.UIControls.Command;
using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.Language.Language;
using View_Spot_of_City.UIControls.Helper;

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
        /// 开始日期
        /// </summary>
        public DateTime StartDate = new DateTime();

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate = new DateTime();

        /// <summary>
        /// 按月统计的人流量
        /// </summary>
        public List<List<VisitorItem>> VisitorsByMonthAndPlace = new List<List<VisitorItem>>();

        bool _CanDragSilder = false;

        /// <summary>
        /// 用于设置滑动条的定时器
        /// </summary>
        DispatcherTimer ChangeSliderTimer = new DispatcherTimer();

        /// <summary>
        /// 指示是否可以拖动Slider
        /// </summary>
        public bool CanDragSlider
        {
            get { return _CanDragSilder; }
            set
            {
                _CanDragSilder = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanDragSlider"));
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Visualization()
        {
            InitializeComponent();

            DateForShow.DisplayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateForShow.Visibility = Visibility.Collapsed;

            ChangeSliderTimer.Tick += new EventHandler(ChangeSliderTimer_Tick);
            ChangeSliderTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private async void SettingBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            VisualizationParamsPicker picker = new VisualizationParamsPicker();
            if (picker.ShowDialog() == true)
            {
                CanDragSlider = false;
                StartDate = picker.StartDate;
                EndDate = picker.EndDate;
                if (picker.ModeCheckBox.IsChecked == true)
                    ArcGISSceneCommands.ChangeBaseMap.Execute(AppSettings["ARCGIS_SATELLITE_BASEMAP"], Application.Current.MainWindow);
                int limit = 100;
                if (EndDate <= StartDate || ((EndDate.Year == StartDate.Year) && (EndDate.Month == StartDate.Month)))
                {
                    MessageboxMaster.Show(LanguageDictionaryHelper.GetString("DataVisualization_DateChooseError"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                    return;
                }

                for (int month1 = StartDate.Month; month1 <= 12; month1++)
                {
                    string jsonString = string.Empty;
                    try
                    {
                        jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_VISITORS_BY_YEAR_MONTH"] + "?year=" + Convert.ToString(StartDate.Year) + "&month=" + Convert.ToString(month1) + "&limit=" + Convert.ToString(limit), string.Empty, RestSharp.Method.GET)).Content;
                        if (jsonString == "")
                            throw new Exception("");

                        JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                        string content_string = jobject["VisitorInfo"].ToString();

                        using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content_string)))
                        {
                            DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(List<VisitorItem>));
                            VisitorsByMonthAndPlace.Add((List<VisitorItem>)deseralizer.ReadObject(ms));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                        return;
                    }
                }

                if (StartDate.Year + 1 < EndDate.Year)
                {
                    for (int year = StartDate.Year; year < EndDate.Year; year++)
                    {
                        for(int i=1;i<=12;i++)
                        {
                            string jsonString = string.Empty;
                            try
                            {
                                jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_VISITORS_BY_YEAR_MONTH"] + "?year=" + Convert.ToString(year) + "&month=" + Convert.ToString(i) + "&limit=" + Convert.ToString(limit), string.Empty, RestSharp.Method.GET)).Content;
                                if (jsonString == "")
                                    throw new Exception("");

                                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                                string content_string = jobject["VisitorInfo"].ToString();

                                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content_string)))
                                {
                                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(List<VisitorItem>));
                                    VisitorsByMonthAndPlace.Add((List<VisitorItem>)deseralizer.ReadObject(ms));
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                                return;
                            }
                        }
                    }
                }

                if(StartDate.Year < EndDate.Year)
                {
                    for (int month2 = 1; month2 <= EndDate.Month; month2++)
                    {
                        string jsonString = string.Empty;
                        try
                        {
                            jsonString = (await WebServiceHelper.GetHttpResponseAsync(AppSettings["WEB_API_GET_VISITORS_BY_YEAR_MONTH"] + "?year=" + Convert.ToString(EndDate.Year) + "&month=" + Convert.ToString(month2) + "&limit=" + Convert.ToString(limit), string.Empty, RestSharp.Method.GET)).Content;
                            if (jsonString == "")
                                throw new Exception("");

                            JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                            string content_string = jobject["VisitorInfo"].ToString();

                            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content_string)))
                            {
                                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(List<VisitorItem>));
                                VisitorsByMonthAndPlace.Add((List<VisitorItem>)deseralizer.ReadObject(ms));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                            return;
                        }
                    }
                }

                MonthSlider.Minimum = 0;
                MonthSlider.Maximum = VisitorsByMonthAndPlace.Count - 1;
                MonthSlider.Value = MonthSlider.Minimum;
                CanDragSlider = true;

                if(VisitorsByMonthAndPlace.Count >= 1)
                {
                    ArcGISSceneCommands.AddVisitorsData.Execute(VisitorsByMonthAndPlace, Application.Current.MainWindow);
                    DateForShow.DisplayDate = StartDate;
                    DateForShow.Visibility = Visibility.Visible;
                }
            }
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PauseBtn.IsChecked == true)
                StartChangeSliderTimer();
            else
                StopChangeSliderTimer();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MonthSlider.Value < MonthSlider.Maximum)
                MonthSlider.Value++;
        }

        private void PreBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MonthSlider.Value > MonthSlider.Minimum)
                MonthSlider.Value--;
        }

        private void Silder_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ArcGISSceneCommands.ChangeVisitorsData.Execute(VisitorsByMonthAndPlace[(int)MonthSlider.Value], Application.Current.MainWindow);
            DateForShow.DisplayDate = StartDate.AddMonths((int)MonthSlider.Value);
        }

        /// <summary>
        /// 启动定时器
        /// </summary>
        private void StartChangeSliderTimer()
        {
            ChangeSliderTimer.Start();
        }

        /// <summary>
        /// 改变Slider值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeSliderTimer_Tick(object sender, EventArgs e)
        {
            if (MonthSlider.Value < MonthSlider.Maximum)
                MonthSlider.Value++;
            else
                StopChangeSliderTimer();
        }

        /// <summary>
        /// 关闭定时器
        /// </summary>
        private void StopChangeSliderTimer()
        {
            ChangeSliderTimer.Stop();
            PauseBtn.IsChecked = false;
            MonthSlider.Value = MonthSlider.Minimum;
        }

        private void TypeButton_Click(object sender, RoutedEventArgs e)
        {
            (new ViewTypeForm()).Show();
        }

        private void VisitorButton_Click(object sender, RoutedEventArgs e)
        {
            (new VisitorsForm()).Show();
        }
    }
}
