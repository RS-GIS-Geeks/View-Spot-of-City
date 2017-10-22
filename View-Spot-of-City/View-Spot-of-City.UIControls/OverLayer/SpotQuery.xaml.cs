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
using static System.Configuration.ConfigurationManager;

using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.Language.Language;

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

        /// <summary>
        /// 查询按钮响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SpotSearchBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (StartPointAddress.Text == null)
            {
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Input_Empty"), LanguageDictionaryHelper.GetString("MessageBox_Warning_Title"));
                return;
            }
            
            //用户输入的内容
            string input_spot_name = StartPointAddress.Text;

            //逐一分词之后的内容
            char[] input_spot_name_spited = input_spot_name.ToCharArray();

            //数据库信息
            string mysql_host = AppSettings["MYSQL_HOST"];
            string mysql_port = AppSettings["MYSQL_PORT"];
            string mysql_user = AppSettings["MYSQL_USER"];
            string mysql_password = AppSettings["MYSQK_PASSWORD"];
            string mysql_database = AppSettings["MYSQK_DATABASE"];

            //模糊查询中的内容
            string sql_regexp = "%";
            {
                foreach(char siglechar in input_spot_name_spited)
                {
                    sql_regexp += siglechar + "%";
                }
            }

            //查询数量的语句
            string sql_query_count = "SELECT COUNT(*) FROM " + mysql_database + ".ViewSpotData WHERE name LIKE '" + sql_regexp + "';";

            //查询数据的语句
            string sql_query_content = "SELECT * FROM ViewSpotData WHERE name LIKE '" + sql_regexp + "';";

            //执行数量的SQL查询
            System.Data.Common.DbDataReader countReader = await Helper.MySqlHelper.ExecuteReaderAsync(mysql_host, mysql_port, mysql_user, mysql_password, mysql_database, sql_query_count);
            //object result = await Helper.MySqlHelper.ExecuteScalarAsync(mysql_host, mysql_port, mysql_user, mysql_password, mysql_database, sql_query_count);
            //int placeCount = Convert.ToInt32(result.ToString());

            if (countReader == null)
            {
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("SpotSearch_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                return;
            }

            if (!countReader.HasRows)
            {
                if (!countReader.IsClosed)
                    countReader.Close();
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("SpotSearch_Null"), LanguageDictionaryHelper.GetString("MessageBox_Tip_Title"));
                return;
            }
            long placeCount = -1;
            while (countReader.Read())
                placeCount = countReader.GetInt64(0);
            if (!countReader.IsClosed)
                countReader.Close();

            MessageboxMaster.Show("搜索到" + placeCount + "个景点");

            //执行数据的SQL查询
            //System.Data.Common.DbDataReader contentReader = await Helper.MySqlHelper.ExecuteReaderAsync(mysql_host, mysql_port, mysql_user, mysql_password, mysql_database, sql_query_count);
        }
    }
}
