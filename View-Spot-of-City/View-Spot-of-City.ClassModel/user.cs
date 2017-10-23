using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace View_Spot_of_City.ClassModel
{
    [DataContract]
    public class user : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static user NoBody = new user();

        /// <summary>
        /// 用户id
        /// </summary>
        long _id = long.MinValue;
        [DataMember]
        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        string _mail = string.Empty;
        [DataMember]
        public string mail
        {
            get { return _mail; }
            set
            {
                if(_mail != value)
                {
                    _mail = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("mail"));
                }
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        string _password = string.Empty;
        [DataMember]
        public string password
        {
            get { return _password; }
            set
            {
                if(_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("password"));
                }
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        string _name = string.Empty;
        [DataMember]
        public string name
        {
            get { return _name; }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("name"));
                }
            }
        }

        /// <summary>
        /// 昵称
        /// </summary>
        string _display_name = string.Empty;
        [DataMember]
        public string dispaly_name
        {
            get { return _display_name; }
            set
            {
                if(_display_name != value)
                {
                    _display_name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("dispaly_name"));
                }
            }
        }

        /// <summary>
        /// 省
        /// </summary>
        string _province = string.Empty;
        [DataMember]
        public string province
        {
            get { return _province; }
            set
            {
                if(_province != value)
                {
                    _province = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("province"));
                }
            }
        }

        /// <summary>
        /// 城市
        /// </summary>
        string _city = string.Empty;

        [DataMember]
        public string city
        {
            get { return _city; }
            set
            {
                if(_city != value)
                {
                    _city = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("city"));
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public user()
        {

        }
    }
}
