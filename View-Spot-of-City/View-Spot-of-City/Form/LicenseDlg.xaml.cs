using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization.Formatters.Binary;

using View_Spot_of_City.Helper;

namespace View_Spot_of_City.Form
{
    /// <summary>
    /// LicenseDlg.xaml 的交互逻辑
    /// </summary>
    public partial class LicenseDlg : Window, INotifyPropertyChanged
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "License.dat";
        private bool _IsCorrect;
        public LicenseDlg()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public bool IsCorrect
        {
            get
            {
                return _IsCorrect;
            }
            set
            {
                _IsCorrect = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("IsCorrect"));
                    if (_IsCorrect)
                    {
                        image = "pack://Application:,,,/Icon/Correct.png";
                        toolTip = "Correct";
                    }
                    else
                    {
                        image = "pack://Application:,,,/Icon/Error.png";
                        toolTip = "Error";
                    }
                }
            }
        }

        private string _image;
        public string image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("image"));
                }
            }
        }

        private string _toolTip;
        public string toolTip
        {
            get
            {
                return _toolTip;
            }
            set
            {
                if (_toolTip != value)
                {
                    _toolTip = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("toolTip"));
                }
            }
        }

        private Visibility _visibility;
        public Visibility visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                if (_visibility != value)
                {
                    if (_visibility != value)
                    {
                        _visibility = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("visibility"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Stream s = File.Open(path, FileMode.Create);//创建a.bat文件 如果之前错在a.bat文件则覆盖，无则创建
            BinaryFormatter b = new BinaryFormatter();//创建一个序列化的对象
            string Date = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd hh:mm:ss");//使用期限1年
            b.Serialize(s, Date);//将数据序列化后给s
            s.Close();
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void txtFieldName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime Now = DateTime.Now;
            DateTime Last = Now.AddMinutes(-1);
            string resultNow = MD5_Encryption.MD5Encode(Now.ToShortDateString() + " " + Now.ToShortTimeString());
            string resultLast = MD5_Encryption.MD5Encode(Last.ToShortDateString() + " " + Last.ToShortTimeString());
            string resultDevelop = MD5_Encryption.MD5Encode("rsgisgeeks");
            if (this.txtFieldName.Text == resultNow || this.txtFieldName.Text == resultLast || this.txtFieldName.Text == resultDevelop)
            {
                IsCorrect = true;
            }
            else
            {
                IsCorrect = false;
            }
        }
    }
}
