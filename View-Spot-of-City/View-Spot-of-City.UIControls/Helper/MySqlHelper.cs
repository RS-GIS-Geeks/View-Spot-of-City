using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data;

namespace View_Spot_of_City.UIControls.Helper
{
    public static class MySqlHelper
    {
        /// <summary>
        /// 连接mysql并执行非查询的SQL语句
        /// </summary>
        /// <param name="server">IP</param>
        /// <param name="port">端口</param>
        /// <param name="UserInfo">用户</param>
        /// <param name="password">密码</param>
        /// <param name="database">数据库</param>
        /// <param name="sql_string">SQL语句</param>
        /// <returns>返回状态信息，如果成功则返回"true"</returns>
        public static async Task<string> ExcuteNonQueryAsync(string server, string port, string UserInfo, string password, string database, string sql_string)
        {
            try
            {
                string connectStr = "server=" + server +";port="+ port + ";User Id=" +UserInfo+ ";password=" + password + ";Database=" + database;
                MySqlConnection myConnection = new MySqlConnection(connectStr);
                await myConnection.OpenAsync();
                MySqlCommand mycmd = new MySqlCommand(sql_string, myConnection);
                int code = await mycmd.ExecuteNonQueryAsync();
                if (code > 0)
                {
                    await myConnection.CloseAsync();
                    return "true";
                }
                else
                {
                    await myConnection.CloseAsync();
                    return "false";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        
        /// <summary>
        /// 连接mysql并执行查询数据的SQL语句
        /// </summary>
        /// <param name="server">IP</param>
        /// <param name="port">端口</param>
        /// <param name="UserInfo">用户</param>
        /// <param name="password">密码</param>
        /// <param name="database">数据库</param>
        /// <param name="sql_string">SQL语句</param>
        /// <returns>从数据源读取流</returns>
        public static async Task<System.Data.Common.DbDataReader> ExecuteReaderAsync(string server, string port, string UserInfo, string password, string database, string sql_string)
        {
            try
            {
                string connectStr = "server=" + server + ";port=" + port + ";User Id=" + UserInfo + ";password=" + password + ";Database=" + database;
                MySqlConnection myConnection = new MySqlConnection(connectStr);
                await myConnection.OpenAsync();
                MySqlCommand mycmd = new MySqlCommand(sql_string, myConnection);
                System.Data.Common.DbDataReader dataReader = await mycmd.ExecuteReaderAsync();
                //await myConnection.CloseAsync();

                return dataReader;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 连接mysql并执行查询单个值的SQL语句
        /// </summary>
        /// <param name="server">IP</param>
        /// <param name="port">端口</param>
        /// <param name="UserInfo">用户</param>
        /// <param name="password">密码</param>
        /// <param name="database">数据库</param>
        /// <param name="sql_string">SQL语句</param>
        /// <returns>单个对象</returns>
        public static async Task<object> ExecuteScalarAsync(string server, string port, string UserInfo, string password, string database, string sql_string)
        {
            try
            {
                string connectStr = "server=" + server + ";port=" + port + ";User Id=" + UserInfo + ";password=" + password + ";Database=" + database;
                MySqlConnection myConnection = new MySqlConnection(connectStr);
                await myConnection.OpenAsync();
                MySqlCommand mycmd = new MySqlCommand(sql_string, myConnection);
                object dataReader = await mycmd.ExecuteScalarAsync();
                //await myConnection.CloseAsync();

                return dataReader;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
