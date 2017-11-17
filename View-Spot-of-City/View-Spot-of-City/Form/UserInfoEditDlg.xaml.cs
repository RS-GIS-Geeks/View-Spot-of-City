using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using static System.Configuration.ConfigurationManager;

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.UIControls.Form;
using View_Spot_of_City.Language.Language;
using View_Spot_of_City.UIControls.Helper;

namespace View_Spot_of_City.Form
{
    /// <summary>
    /// EditDlg.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoEditDlg : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        App _CurrentApp = null;

        /// <summary>
        /// 当前应用程序
        /// </summary>
        public App CurrentApp
        {
            get { return _CurrentApp; }
            set
            {
                _CurrentApp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentApp"));
            }
        }

        UserInfo _CurrentUser_Copy = null;

        /// <summary>
        /// 当前用户的克隆
        /// </summary>
        public UserInfo CurrentUser_Copy
        {
            get { return _CurrentUser_Copy; }
            set
            {
                _CurrentUser_Copy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentUser_Copy"));
            }
        }


        /// <summary>
        /// 是否已经编辑
        /// </summary>
        bool _HaveEdited = false;

        /// <summary>
        /// 是否修改
        /// </summary>
        public bool HaveEdited
        {
            get { return _HaveEdited; }
            set
            {
                _HaveEdited = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HaveEdited"));
            }
        }

        /// <summary>
        /// 是否点击了叉
        /// </summary>
        bool _IsClickFork = true;

        ObservableCollection<short> _AgeSource = null;

        /// <summary>
        /// 年龄集
        /// </summary>
        public ObservableCollection<short> AgeSource
        {
            get { return _AgeSource; }
            set
            {
                if (_AgeSource != value)
                {
                    _AgeSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AgeSource"));
                }
            }
        }

        public UserInfoEditDlg()
        {
            InitializeComponent();
            this.CurrentApp = Application.Current as App;
            this.CurrentUser_Copy = CurrentApp.CurrentUser.Clone() as UserInfo;

            List<short> ageList = new List<short>(120);
            for (short i = 0; i < 120; i++)
            {
                ageList.Add(i);
            }
            AgeSource = new ObservableCollection<short>(ageList);
        }

        private async void SaveButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                //数据库信息
                string mysql_host = AppSettings["MYSQL_HOST"];
                string mysql_port = AppSettings["MYSQL_PORT"];
                string mysql_user = AppSettings["MYSQL_USER"];
                string mysql_password = AppSettings["MYSQK_PASSWORD"];
                string mysql_database = AppSettings["MYSQK_DATABASE"];

                //用户信息
                string DisplayName = CurrentUser_Copy.DisplayName;
                string Name = CurrentUser_Copy.Name;
                string Gender = CurrentUser_Copy.Gender;
                string Age = CurrentUser_Copy.Age.ToString();
                string Constellation = CurrentUser_Copy.Constellation;
                string Hometown = CurrentUser_Copy.Hometown;
                string Country = CurrentUser_Copy.Country;
                string Province = CurrentUser_Copy.Province;
                string City = CurrentUser_Copy.City;
                string Admin = CurrentUser_Copy.Admin;
                string Profession = CurrentUser_Copy.Profession;
                string Company = CurrentUser_Copy.Company;
                string SchoolOrUniversity = CurrentUser_Copy.SchoolOrUniversity;
                string Mail = CurrentApp.CurrentUser.Mail;

                string sql_string = "UPDATE " + AppSettings["MYSQK_TABLE_USER"] + " SET Name='" + Name + "', DisplayName='" + DisplayName + 
                    "',Gender='" + Gender + "',Age='" + Age + "',Constellation='" + Constellation + "',Hometown='" + Hometown + "',Country='" + 
                    Country + "',Province='" + Province + "',City='" + City + "',Admin='" + Admin + "',Profession='" + Profession + "',Company='" +
                    Company + "',SchoolOrUniversity='" + SchoolOrUniversity + "' WHERE Mail='" + Mail + "';";

                //执行SQL查询
                string qury_result = await MySqlHelper.ExcuteNonQueryAsync(mysql_host, mysql_port, mysql_user, mysql_password, mysql_database, sql_string);

                //判断返回
                if (qury_result != "true" && qury_result != "false")
                {
                    //连接服务器错误
                    MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Server_Connect_Error"), LanguageDictionaryHelper.GetString("MessageBox_Exception_Title"));
                    return;
                }
                else if (qury_result != "true")
                {
                    MessageboxMaster.Show(LanguageDictionaryHelper.GetString("UserInfoEdit_EditError"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                    LogManager.LogManager.Error(LanguageDictionaryHelper.GetString("UserInfoEdit_EditError"));
                    return;
                }
                else
                {
                    CurrentApp.CurrentUser = _CurrentUser_Copy;
                    _IsClickFork = false;
                    HaveEdited = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageboxMaster.Show(LanguageDictionaryHelper.GetString("Config_File_Error"), LanguageDictionaryHelper.GetString("MessageBox_Error_Title"));
                LogManager.LogManager.Warn(LanguageDictionaryHelper.GetString("Config_File_Error"), ex);
                return;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _IsClickFork = false;
            this.DialogResult = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            HaveEdited = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HaveEdited = true;
        }

        private void mainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (HaveEdited && _IsClickFork)
            {
                MessageboxMaster.DialogResults result = MessageboxMaster.Show(LanguageDictionaryHelper.GetString("UserInfoEdit_EditedTip"),
                    LanguageDictionaryHelper.GetString("MessageBox_Tip_Title"), MessageboxMaster.MyMessageBoxButtons.YesNoCancel,
                    MessageboxMaster.MyMessageBoxButton.Yes);
                if (result == MessageboxMaster.DialogResults.Yes)
                    SaveButton_ClickAsync(null, null);
                else if (result == MessageboxMaster.DialogResults.No)
                    CancelButton_Click(null, null);
                else
                    this.DialogResult = false;
            }
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            HaveEdited = false;
        }
    }
}
