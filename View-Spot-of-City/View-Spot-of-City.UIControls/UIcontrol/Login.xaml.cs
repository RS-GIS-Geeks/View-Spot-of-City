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
using System.Runtime.Serialization.Json;
using static System.Configuration.ConfigurationManager;

using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using View_Spot_of_City.UIControls.Command;
using View_Spot_of_City.UIControls.Helper;
using View_Spot_of_City.ClassModel;
using static View_Spot_of_City.UIControls.Converter.Enum2LoginUI;
using static View_Spot_of_City.Language.Language.LanguageDictionaryHelper;
using static View_Spot_of_City.UIControls.Helper.CreateValidateCodeImageHelper;

namespace View_Spot_of_City.UIControls.UIcontrol
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : UserControl, INotifyPropertyChanged
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

        /// <summary>
        /// 验证码
        /// </summary>
        char[] validateCode = new char[4];

        public Login()
        {
            InitializeComponent();
            Title = GetString("LoginTitle");
            mailTextBox.Text = AppSettings["DEFAULT_USER_MAIL"];

            //生成验证码
            validateCode = CreatFourRandomChar();
            validateImage.Source = CreateValidateCodeImage(validateCode);

            #region 测试
            passwordTextBox.Password = "19970108";
            #endregion
        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoginDlgCommands.CancelAndCloseFormCommand.Execute(null, this);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            LoginDlgCommands.ChangePageCommand.Execute(LoginControls.Register, this);
        }

        private async void btnLogin_ClickAsync(object sender, RoutedEventArgs e)
        {
            #region 测试
            //LoginDlgCommands.OKAndCloseFormCommand.Execute(null, this);
            //return;
            #endregion

            string user_mail = mailTextBox.Text;
            string user_password = passwordTextBox.Password;
            string user_validateCode = validateCodeTextBox.Text;

            //验证输入
            if (user_mail == string.Empty || user_password == string.Empty)
            {
                MessageBox.Show(GetString("Input_Empty"), AppSettings["MessageBox_Error_Title"]);
                return;
            }

            string validateCodeStr = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(validateCode));
            if (user_validateCode.ToLower() != validateCodeStr.ToLower())
            {
                MessageBox.Show(GetString("LoginValidateCodeError"), GetString("MessageBox_Error_Title"));
                return;
            }

            //密码加密
            string password_encoded = MD5_Encryption.MD5Encode(user_password);

            string url = AppSettings["WEB_API_GET_USER_INFO"] + "?mail=" + user_mail;

            string jsonString = string.Empty;

            user user_obiect = null;

            try
            {
                jsonString = (await WebServiceHelper.GetHttpResponse(url, null, RestSharp.Method.GET)).Content;
                if (jsonString == "")
                    throw new Exception("");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(GetString("Server_Connect_Error"), GetString("MessageBox_Error_Title"));
                return;
            }

            try
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(jsonString);

                string content_string = jobject["UserInfo"][0].ToString();

                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content_string)))
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(user));
                    user_obiect = (user)deseralizer.ReadObject(ms);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(GetString("LoginMailError"), GetString("MessageBox_Tip_Title"));
                return;
            }
            
            if(user_obiect.password == password_encoded)
            {
                //MessageBox.Show("登录成功", "CS-Tao");
                CommandForMainWindow.ChangeCurrentUserCommand.Execute(user_obiect, this);
                LoginDlgCommands.OKAndCloseFormCommand.Execute(null, this);
            }
            else
            {
                MessageBox.Show(GetString("LoginPasswordError"), GetString("MessageBox_Tip_Title"));
                return;
            }
        }

        private void validateImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //生成验证码
            validateCode = CreatFourRandomChar();
            validateImage.Source = CreateValidateCodeImage(validateCode);
        }
    }
}
