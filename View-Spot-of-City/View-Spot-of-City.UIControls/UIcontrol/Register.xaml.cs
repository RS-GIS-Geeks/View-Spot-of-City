using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Text.RegularExpressions;
using static System.Configuration.ConfigurationManager;

using View_Spot_of_City.UIControls.Command;
using View_Spot_of_City.ClassModel;
using View_Spot_of_City.UIControls.Helper;
using static View_Spot_of_City.UIControls.Converter.Enum2LoginUI;
using static View_Spot_of_City.Language.Language.LanguageDictionaryHelper;
using static View_Spot_of_City.UIControls.Helper.CreateValidateCodeImageHelper;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string _title = null;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        public Register()
        {
            InitializeComponent();
            Title = GetString("LoginTitle");
            validateImage.Source = CreateValidateCodeImage();

            #region 测试
            mailTextBox.Text = "990296951@qq.com";
            passwordTextBox.Password = "19970108";
            password1TextBox.Password = "19970108";
            validateCodeTextBox.Text = "1234";
            #endregion
        }

        private async void btnRegister_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                //用户注册信息
                string user_mail = mailTextBox.Text;
                string user_password = passwordTextBox.Password;
                string user_password1 = password1TextBox.Password;
                string user_identifyingCode = validateCodeTextBox.Text;

                //验证输入
                if (user_mail == string.Empty || user_password == string.Empty || user_password1 == string.Empty || user_identifyingCode == string.Empty)
                {
                    MessageBox.Show(GetString("Input_Empty"), AppSettings["MessageBox_Error_Title"]);
                    return;
                }
                if (user_mail == string.Empty)
                {
                    MessageBox.Show(GetString("Mail_Error"), AppSettings["MessageBox_Error_Title"]);
                    return;
                }
                if (user_password != user_password1)
                {
                    MessageBox.Show(GetString("Two_Passwords_Mismatching"), AppSettings["MessageBox_Error_Title"]);
                    return;
                }
                if (user_password.Length > 16)
                {
                    MessageBox.Show(GetString("Password_Too_Long"), AppSettings["MessageBox_Error_Title"]);
                    return;
                }

                //密码加密
                string password_encoded = MD5_Encryption.MD5Encode(user_password);

                //数据库信息
                string mysql_host = AppSettings["MYSQL_HOST"];
                string mysql_port = AppSettings["MYSQL_PORT"];
                string mysql_user = AppSettings["MYSQL_USER"];
                string mysql_password = AppSettings["MYSQK_PASSWORD"];
                string mysql_database = AppSettings["MYSQK_DATABASE"];

                string sql_string = "INSERT INTO users(mail, password) VALUES('" + user_mail + "','" + password_encoded + "')";

                string qury_result = await MySqlHelper.ExcuteSQL(mysql_host, mysql_port, mysql_user, mysql_password, mysql_database, sql_string);

                //判断返回
                if (qury_result != "true" && qury_result != "false")
                {
                    MessageBox.Show(GetString("Server_Connect_Error"), GetString("MessageBox_Exception_Title"));
                    return;
                }
                else if(qury_result != "true")
                {
                    MessageBox.Show(GetString("RegisterMailEcho"), GetString("MessageBox_Tip_Title"));
                    return;
                }
                else
                {
                    MessageBox.Show(GetString("RegisterOK"), GetString("MessageBox_Tip_Title"));
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(GetString("Config_File_Error"), GetString("MessageBox_Error_Title"));
                return;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoginDlgCommands.CancelAndCloseFormCommand.Execute(null, this);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginDlgCommands.ChangePageCommand.Execute(LoginControls.Login, this);
        }

        public static bool IsEmail(string str)
        {
            string expression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            return Regex.IsMatch(str, expression, RegexOptions.Compiled);
        }
    }
}
