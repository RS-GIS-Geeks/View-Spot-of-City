using LiveCharts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Controls;

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.Language.Language;
using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.UIControls.Helper;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;

namespace View_Spot_of_City.UIControls.VisualizationControl
{
    /// <summary>
    /// VisitorsByYear.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorsByYear : UserControl
    {
        public VisitorsByYear()
        {
            InitializeComponent();
            Labels = new [] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            Formatter = value => value.ToString("N");
            SeriesCollection = new SeriesCollection();

            List<List<VisitorItem>> visitorItemList = new List<List<VisitorItem>>();
            {
                visitorItemList.Add(new List<VisitorItem>());
                visitorItemList.Add(new List<VisitorItem>());
                visitorItemList.Add(new List<VisitorItem>());
            }
            List<List<int>> visitorMonthList = new List<List<int>>();
            {
                visitorMonthList.Add(new List<int>());
                visitorMonthList.Add(new List<int>());
                visitorMonthList.Add(new List<int>());
            }
            GetVisitorsInfoFromJsonAsync(SeriesCollection,visitorItemList[0], visitorMonthList[0], 2015, 3);
            GetVisitorsInfoFromJsonAsync(SeriesCollection,visitorItemList[1], visitorMonthList[1], 2016, 3);
            GetVisitorsInfoFromJsonAsync(SeriesCollection,visitorItemList[2], visitorMonthList[2], 2017, 3);

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private async void GetVisitorsInfoFromJsonAsync(SeriesCollection SeriesCollection, List<VisitorItem> visitorItemList, List<int> visitorMonthList, int year, int viewid)
        {
            string jsonString = string.Empty;
            for (int j = 0; j < 13; j++)
            {
                visitorMonthList.Add(new int());
            }
            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync("http://39.108.171.209:2901/viewspot/visitorbyyear" + "?year=" + Convert.ToString(year) + "&viewid=" + Convert.ToString(viewid), string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                string content_string = jobject["VisitorInfo"].ToString();

                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content_string)))
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(List<VisitorItem>));
                    visitorItemList = (List<VisitorItem>)deseralizer.ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }
            for (int i = 0; i < visitorItemList.Count; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    if (visitorItemList[i].Month == j)
                    {
                        visitorMonthList[j] += visitorItemList[i].Visitors;
                    }
                }
            }
            SeriesCollection.Add(new LineSeries
            {
                Title = Convert.ToString(year)+"年",
                Values = new ChartValues<int> { visitorMonthList[1], visitorMonthList[2], visitorMonthList[3], visitorMonthList[4], visitorMonthList[5], visitorMonthList[6], visitorMonthList[7], visitorMonthList[8], visitorMonthList[9], visitorMonthList[10], visitorMonthList[11], visitorMonthList[12] },
            });
        }
    }

}
