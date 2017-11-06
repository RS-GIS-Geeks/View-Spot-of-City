using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace View_Spot_of_City.ClassModel
{
    [DataContract]
    public class VisitorItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        long _Id = -1;
        [DataMember]
        public long Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Id"));
                }
            }
        }

        string _ViewId = string.Empty;

        /// <summary>
        /// 景点ID
        /// </summary>
        [DataMember]
        public string ViewId
        {
            get { return _ViewId; }
            set
            {
                if (_ViewId != value)
                {
                    _ViewId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ViewId"));
                }
            }
        }

        string _Year = string.Empty;

        /// <summary>
        /// 年份
        /// </summary>
        [DataMember]
        public string Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Year"));
                }
            }
        }

        string _Month = string.Empty;

        /// <summary>
        /// 月份
        /// </summary>
        [DataMember]
        public string Month
        {
            get { return _Month; }
            set
            {
                if (_Month != value)
                {
                    _Month = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Month"));
                }
            }
        }

        string _Day = string.Empty;

        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        public string Day
        {
            get { return _Day; }
            set
            {
                if (_Day != value)
                {
                    _Day = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Day"));
                }
            }
        }

        string _Visitors = string.Empty;

        /// <summary>
        /// 人流量
        /// </summary>
        [DataMember]
        public string Visitors
        {
            get { return _Visitors; }
            set
            {
                if (_Visitors != value)
                {
                    _Visitors = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Visitors"));
                }
            }
        }
    }
}
