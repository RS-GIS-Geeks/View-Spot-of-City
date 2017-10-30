using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;
using View_Spot_of_City.ClassModel.Interface;

namespace View_Spot_of_City.ClassModel
{
    [DataContract]
    public class ViewSpot : INotifyPropertyChanged, IGetLngLat
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static ViewSpot NullViewSpot = new ViewSpot();

        long _id = -1;
        [DataMember]
        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        string _name = string.Empty;
        [DataMember]
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("name"));
            }
        }

        string _type = string.Empty;
        [DataMember]
        public string type
        {
            get { return _name; }
            set
            {
                _type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("type"));
            }
        }

        string _address = string.Empty;
        [DataMember]
        public string address
        {
            get { return _address; }
            set
            {
                _address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("address"));
            }
        }

        string _pname = string.Empty;
        [DataMember]
        public string pname
        {
            get { return _pname; }
            set
            {
                _pname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("pname"));
            }
        }

        string _cityname = string.Empty;
        [DataMember]
        public string cityname
        {
            get { return _cityname; }
            set
            {
                _cityname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("cityname"));
            }
        }

        string _adminname = string.Empty;
        [DataMember]
        public string adminname
        {
            get { return _adminname; }
            set
            {
                _adminname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("adminname"));
            }
        }

        double _lng = double.MinValue;
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

        double _lat = double.MinValue;
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

        string _photourl1 = string.Empty;
        [DataMember]
        public string photourl1
        {
            get { return _photourl1; }
            set
            {
                _photourl1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("photourl1"));
            }
        }

        string _photourl2 = string.Empty;
        [DataMember]
        public string photourl2
        {
            get { return _photourl2; }
            set
            {
                _photourl2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("photourl2"));
            }
        }

        string _photourl3 = string.Empty;
        [DataMember]
        public string photourl3
        {
            get { return _photourl3; }
            set
            {
                _photourl3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("photourl3"));
            }
        }

        double _biz_ext_rating = double.MinValue;
        [DataMember]
        public double biz_ext_rating
        {
            get { return _biz_ext_rating; }
            set
            {
                _biz_ext_rating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("biz_ext_rating"));
            }
        }

        double _biz_ext_cost = double.MinValue;
        [DataMember]
        public double biz_ext_cost
        {
            get { return _biz_ext_cost; }
            set
            {
                _biz_ext_cost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("biz_ext_cost"));
            }
        }

        string _telephone = string.Empty;
        [DataMember]
        public string telephone
        {
            get { return _telephone; }
            set
            {
                _telephone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("telephone"));
            }
        }

        /// <summary>
        /// 构造一个景点实例
        /// </summary>
        public ViewSpot()
        {

        }

        /// <summary>
        /// 返回经度
        /// </summary>
        /// <returns></returns>
        public double GetLng()
        {
            return lng;
        }

        /// <summary>
        /// 返回纬度
        /// </summary>
        /// <returns></returns>
        public double GetLat()
        {
            return lat;
        }

        /// <summary>
        /// 检查数据是否合理，如果不合理则改正
        /// </summary>
        public void CheckData()
        {
            photourl1 = photourl1 == "-1" ? "http://store.is.autonavi.com/showpic/91b0ec91244053f681abe1afc874f2a1" : photourl1;
            photourl2 = photourl2 == "-1" ? "http://store.is.autonavi.com/showpic/91b0ec91244053f681abe1afc874f2a1" : photourl2;
            photourl3 = photourl3 == "-1" ? "http://store.is.autonavi.com/showpic/91b0ec91244053f681abe1afc874f2a1" : photourl3;
            biz_ext_rating = biz_ext_rating == -1 ? 0 : biz_ext_rating;
            biz_ext_cost = biz_ext_cost == -1 ? 0 : biz_ext_cost;
            telephone = telephone == "-1" ? " - " : telephone;
        }
    }
}
