using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace View_Spot_of_City.ClassModel
{
    public class CommentInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        long _Id = -1;

        /// <summary>
        /// 评论Id
        /// </summary>
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
        public long SpotId
        {
            get { return _SpotId; }
            set
            {
                _SpotId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SpotId"));
            }
        }

        long _UserId = -1;

        /// <summary>
        /// 用户Id
        /// </summary>
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
        /// 日
        /// </summary>
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
        /// 日
        /// </summary>
        public int Second
        {
            get { return _Second; }
            set
            {
                _Second = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Second"));
            }
        }

        string _CommentData = string.Empty;

        /// <summary>
        /// 日
        /// </summary>
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
        /// 日
        /// </summary>
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
        /// 日
        /// </summary>
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
        /// 日
        /// </summary>
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
