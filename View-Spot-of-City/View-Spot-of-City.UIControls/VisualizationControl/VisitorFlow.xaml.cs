using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

using LiveCharts;
using LiveCharts.Wpf;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.Language.Language;
using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.UIControls.Helper;

namespace View_Spot_of_City.UIControls.VisualizationControl
{
    /// <summary>
    /// VisitorFlow.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorFlow : VisualizationControlBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<VisitorItem> _VisitorflowList;
        List<int> visitornum = new List<int>();
        List<string> ViewName = new List<string>();

        public SeriesCollection SeriesCollection { get; set; }
        public string[] ViewSpotName { get; set; }//景点名称string[]
        public int[] VisitorNumber { get; set; }//人流量
        public Func<int, string> Formatter { get; set; }
        public VisitorFlow()
        {
            InitializeComponent();
            GetVisitorFlowDataAsync();
          
            
         
        }

        public ObservableCollection<VisitorItem>VisitorflowList
        {
            get { return _VisitorflowList; }
            set
            {

                _VisitorflowList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VisitorflowList"));
            }

        }

        //获取数据
        private async void GetVisitorFlowDataAsync()
        {
           // int VisitorCount = -1;
           
            string jsonString = string.Empty;
          
            List<VisitorItem> visitorflowList = new List<VisitorItem>(15);
            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponseAsync("http://39.108.171.209:2901/viewspot/viewbyvisitor?year=2015&month=1&limit=15", string.Empty, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");

                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                string content_string = jobject["ViewInfo"].ToString();

                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content_string)))
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(List<VisitorItem>));
                    visitorflowList = (List<VisitorItem>)deseralizer.ReadObject(ms);
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }
            VisitorflowList = new ObservableCollection<VisitorItem>(visitorflowList);
            GetXYData();
            //显示柱状图
            SeriesCollection = new SeriesCollection
             {
                
                 new RowSeries
                 {
                     StrokeThickness =0.2,
                    // Stroke=new SolidColorBrush(Color.FromArgb(150,100,100,100))
                    Stroke=Brushes.Olive,
                    
                     Width =0.1,
                     Title="人流量",
                     Values =new ChartValues<int>(VisitorNumber),
                 }
             };
            
            Formatter = value => value.ToString("N");
            
            DataContext = this;
        }
        
        //数组链表操作
        public void GetXYData()
        {
            for(int i=0;i<15;i++)
            {
                visitornum.Add(VisitorflowList[i].Visitors);
                ViewName.Add(VisitorflowList[i].ViewSpotName);
            }

            VisitorNumber = visitornum.ToArray();
            ViewSpotName = ViewName.ToArray();
            Array.Reverse(VisitorNumber);
            Array.Reverse(ViewSpotName);
        }
    }
}
