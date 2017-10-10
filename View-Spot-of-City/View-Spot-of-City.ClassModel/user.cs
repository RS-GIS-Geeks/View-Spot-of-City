using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace View_Spot_of_City.ClassModel
{
    [DataContract]
    public class user
    {
        /// <summary>
        /// 用户id
        /// </summary>
        string _id = string.Empty;
        [DataMember]
        public string id
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
            set { _mail = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        string _password = string.Empty;
        [DataMember]
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        string _name = string.Empty;
        [DataMember]
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 昵称
        /// </summary>
        string _display_name = string.Empty;
        [DataMember]
        public string dispaly_name
        {
            get { return _display_name; }
            set { _display_name = value; }
        }

        /// <summary>
        /// 省
        /// </summary>
        string _province = string.Empty;
        [DataMember]
        public string province
        {
            get { return _province; }
            set { _province = value; }
        }

        /// <summary>
        /// 城市
        /// </summary>
        string _city = string.Empty;
        [DataMember]
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }
    }
}
