using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace View_Spot_of_City.ClassModel
{
    /// <summary>
    /// 评论数据
    /// </summary>
    [DataContract]
    public class CommentInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        long _Id = -1;

        /// <summary>
        /// 评论Id
        /// </summary>
        [DataMember]
        public long Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Id"));
            }
        }

        long _SpotId = -1;

        /// <summary>
        /// 景点Id
        /// </summary>
        [DataMember]
        public long SpotId
        {
            get { return _SpotId; }
            set
            {
                _SpotId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SpotId"));
            }
        }

        ViewSpot _Spot = ViewSpot.NullViewSpot;

        /// <summary>
        /// 景点
        /// </summary>
        public ViewSpot Spot
        {
            get { return _Spot; }
            set
            {
                _Spot = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Spot"));
            }
        }

        long _UserId = -1;

        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember]
        public long UserId
        {
            get { return _UserId; }
            set
            {
                _UserId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserId"));
            }
        }

        string _UserName = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserName"));
            }
        }

        double _Stars = -1;

        /// <summary>
        /// 评分
        /// </summary>
        [DataMember]
        public double Stars
        {
            get { return _Stars; }
            set
            {
                _Stars = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Stars"));
            }
        }

        int _Goods = -1;

        /// <summary>
        /// 赞
        /// </summary>
        [DataMember]
        public int Goods
        {
            get { return _Goods; }
            set
            {
                _Goods = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Goods"));
            }
        }

        int _Year = -1;

        /// <summary>
        /// 年
        /// </summary>
        [DataMember]
        public int Year
        {
            get { return _Year; }
            set
            {
                _Year = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Year"));
            }
        }

        int _Month = -1;

        /// <summary>
        /// 月
        /// </summary>
        [DataMember]
        public int Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Month"));
            }
        }

        int _Day = -1;

        /// <summary>
        /// 日
        /// </summary>
        [DataMember]
        public int Day
        {
            get { return _Day; }
            set
            {
                _Day = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Day"));
            }
        }

        int _Hour = -1;

        /// <summary>
        /// 时
        /// </summary>
        [DataMember]
        public int Hour
        {
            get { return _Hour; }
            set
            {
                _Hour = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hour"));
            }
        }

        int _Minute = -1;

        /// <summary>
        /// 分
        /// </summary>
        [DataMember]
        public int Minute
        {
            get { return _Minute; }
            set
            {
                _Minute = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Minute"));
            }
        }

        int _Second = -1;

        /// <summary>
        /// 秒
        /// </summary>
        [DataMember]
        public int Second
        {
            get { return _Second; }
            set
            {
                _Second = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Second"));
            }
        }

        string _TimedForShow = string.Empty;

        /// <summary>
        /// 用于显示的字符串
        /// </summary>
        [DataMember]
        public string TimedForShow
        {
            get { return _TimedForShow; }
            set
            {
                _TimedForShow = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TimedForShow"));
            }
        }

        string _CommentData = string.Empty;

        /// <summary>
        /// 评论
        /// </summary>
        [DataMember]
        public string CommentData
        {
            get { return _CommentData; }
            set
            {
                _CommentData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CommentData"));
            }
        }

        string _PhotoUrl1 = string.Empty;

        /// <summary>
        /// 照片URL
        /// </summary>
        [DataMember]
        public string PhotoUrl1
        {
            get { return _PhotoUrl1; }
            set
            {
                _PhotoUrl1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PhotoUrl1"));
            }
        }

        string _PhotoUrl2 = string.Empty;

        /// <summary>
        /// 照片URL
        /// </summary>
        [DataMember]
        public string PhotoUrl2
        {
            get { return _PhotoUrl2; }
            set
            {
                _PhotoUrl2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PhotoUrl2"));
            }
        }

        string _PhotoUrl3 = string.Empty;

        /// <summary>
        /// 照片URL
        /// </summary>
        [DataMember]
        public string PhotoUrl3
        {
            get { return _PhotoUrl3; }
            set
            {
                _PhotoUrl3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PhotoUrl3"));
            }
        }

        /// <summary>
        /// 空构造函数
        /// </summary>
        public CommentInfo()
        {

        }

        /// <summary>
        /// 检查并替换不合理数据
        /// </summary>
        public void CheckData()
        {

        }
    }
}
