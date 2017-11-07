using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using View_Spot_of_City.ClassModel.Interface;

namespace View_Spot_of_City.ClassModel
{
    [DataContract]
    public class VisitorItem : IGetLngLat, INotifyPropertyChanged
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

        long _ViewId = -1;

        /// <summary>
        /// 景点ID
        /// </summary>
        [DataMember]
        public long ViewId
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

        int _Year = 0;

        /// <summary>
        /// 年份
        /// </summary>
        [DataMember]
        public int Year
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

        int _Month = 0;

        /// <summary>
        /// 月份
        /// </summary>
        [DataMember]
        public int Month
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

        int _Day = 0;

        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        public int Day
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

        int _Visitors = 0;

        /// <summary>
        /// 人流量
        /// </summary>
        [DataMember]
        public int Visitors
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

        double _lng = 0;

        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        public double lng
        {
            get { return _lng; }
            set
            {
                _lng = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("lng"));
            }
        }

        double _lat = 0;

        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public double lat
        {
            get { return _lat; }
            set
            {
                _lat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("lat"));
            }
        }

        public double GetLng()
        {
            return lng;
        }

        public double GetLat()
        {
            return lat;
        }
    }
}
