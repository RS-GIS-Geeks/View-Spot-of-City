using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View_Spot_of_City.UIControls.Helper
{
    public static class MySqlHelper
    {
        /// <summary>
        /// 连接mysql并执行SQL语句
        /// </summary>
        /// <param name="server">IP</param>
        /// <param name="port">端口</param>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <param name="database">数据库</param>
        /// <param name="sql_string">SQL语句</param>
        /// <returns>返回状态信息，如果成功则返回"true"</returns>
        public static async Task<string> ExcuteSQL(string server, string port, string user, string password, string database, string sql_string)
        {
            try
            {
                string connectStr = "server=" + server +";port="+ port + ";User Id=" +user+ ";password=" + password + ";Database=" + database;
                MySqlConnection myConnection = new MySqlConnection(connectStr);
                myConnection.Open();
                MySqlCommand mycmd = new MySqlCommand(sql_string, myConnection);
                int code = await mycmd.ExecuteNonQueryAsync();
                if (code > 0)
                {
                    Console.ReadLine();
                    myConnection.Close();
                    return "true";
                }
                else
                {
                    Console.ReadLine();
                    myConnection.Close();
                    return "false";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
