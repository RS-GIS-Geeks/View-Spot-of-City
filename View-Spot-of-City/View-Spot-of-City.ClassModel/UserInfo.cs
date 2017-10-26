using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace View_Spot_of_City.ClassModel
{
    [DataContract]
    public class UserInfo :ICloneable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 创建一个实例
        /// </summary>
        public static UserInfo NoBody = new UserInfo();

        long _Id = long.MinValue;

        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        string _Mail = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string Mail
        {
            get { return _Mail; }
            set
            {
                if(_Mail != value)
                {
                    _Mail = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mail"));
                }
            }
        }

        string _Password = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string Password
        {
            get { return _Password; }
            set
            {
                if(_Password != value)
                {
                    _Password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
        }

        string _Name = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return _Name; }
            set
            {
                if(_Name != value)
                {
                    _Name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        string _DisplayName = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        [DataMember]
        public string DisplayName
        {
            get { return _DisplayName; }
            set
            {
                if(_DisplayName != value)
                {
                    _DisplayName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayName"));
                }
            }
        }

        string _Country = string.Empty;

        /// <summary>
        /// 国家
        /// </summary>
        [DataMember]
        public string Country
        {
            get { return _Country; }
            set
            {
                if (_Country != value)
                {
                    _Country = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Country"));
                }
            }
        }

        string _Province = string.Empty;

        /// <summary>
        /// 省
        /// </summary>
        [DataMember]
        public string Province
        {
            get { return _Province; }
            set
            {
                if(_Province != value)
                {
                    _Province = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Province"));
                }
            }
        }

        string _City = string.Empty;

        /// <summary>
        /// 城市
        /// </summary>
        [DataMember]
        public string City
        {
            get { return _City; }
            set
            {
                if(_City != value)
                {
                    _City = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("City"));
                }
            }
        }

        string _Admin = string.Empty;

        /// <summary>
        /// 区
        /// </summary>
        [DataMember]
        public string Admin
        {
            get { return _Admin; }
            set
            {
                if (_Admin != value)
                {
                    _Admin = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Admin"));
                }
            }
        }

        string _Company = string.Empty;

        /// <summary>
        /// 公司
        /// </summary>
        [DataMember]
        public string Company
        {
            get { return _Company; }
            set
            {
                if (_Company != value)
                {
                    _Company = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Company"));
                }
            }
        }

        string _SchoolOrUniversity = string.Empty;

        /// <summary>
        /// 学校
        /// </summary>
        [DataMember]
        public string SchoolOrUniversity
        {
            get { return _SchoolOrUniversity; }
            set
            {
                if (_SchoolOrUniversity != value)
                {
                    _SchoolOrUniversity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SchoolOrUniversity"));
                }
            }
        }

        short _Age = 0;

        /// <summary>
        /// 年龄
        /// </summary>
        [DataMember]
        public short Age
        {
            get { return _Age; }
            set
            {
                if (_Age != value)
                {
                    _Age = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Age"));
                }
            }
        }

        string _Gender = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public string Gender
        {
            get { return _Gender; }
            set
            {
                if (_Gender != value)
                {
                    _Gender = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Gender"));
                }
            }
        }

        string _Constellation = string.Empty;

        /// <summary>
        /// 星座
        /// </summary>
        [DataMember]
        public string Constellation
        {
            get { return _Constellation; }
            set
            {
                if (_Constellation != value)
                {
                    _Constellation = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Constellation"));
                }
            }
        }

        string _Hometown = string.Empty;

        /// <summary>
        /// 故乡
        /// </summary>
        [DataMember]
        public string Hometown
        {
            get { return _Hometown; }
            set
            {
                if (_Hometown != value)
                {
                    _Hometown = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hometown"));
                }
            }
        }

        string _Profession = string.Empty;

        /// <summary>
        /// 职业
        /// </summary>
        [DataMember]
        public string Profession
        {
            get { return _Profession; }
            set
            {
                if (_Profession != value)
                {
                    _Profession = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Profession"));
                }
            }
        }

        string _HomePage = string.Empty;

        /// <summary>
        /// 个人主页
        /// </summary>
        [DataMember]
        public string HomePage
        {
            get { return _HomePage; }
            set
            {
                if (_HomePage != value)
                {
                    _HomePage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HomePage"));
                }
            }
        }

        bool _IsEmptyOrNullReplaced = false;

        /// <summary>
        /// 创建一个实例
        /// </summary>
        public UserInfo()
        {

        }

        /// <summary>
        /// 替换所有的空字符串
        /// </summary>
        /// <param name="str"></param>
        public void ReplaceAllEmptyOrNullBy(string str)
        {
            if(!_IsEmptyOrNullReplaced)
            {
                _IsEmptyOrNullReplaced = true;
            }
        }

        /// <summary>
        /// 深克隆一个实例
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
