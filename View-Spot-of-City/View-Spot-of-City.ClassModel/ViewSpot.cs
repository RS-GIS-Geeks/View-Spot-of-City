using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace View_Spot_of_City.ClassModel
{
    [DataContract]
    public class ViewSpot
    {
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
            set { _name = value; }
        }

        string _type = string.Empty;
        [DataMember]
        public string type
        {
            get { return _name; }
            set { _type = value; }
        }

        string _address = string.Empty;
        [DataMember]
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }

        string _pname = string.Empty;
        [DataMember]
        public string pname
        {
            get { return pname; }
            set { _pname = value; }
        }

        string _cityname = string.Empty;
        [DataMember]
        public string cityname
        {
            get { return _cityname; }
            set { _cityname = value; }
        }

        string _adminname = string.Empty;
        [DataMember]
        public string adminname
        {
            get { return _adminname; }
            set { _adminname = value; }
        }

        double _lng = double.MinValue;
        [DataMember]
        public double lng
        {
            get { return _lng; }
            set { _lng = value; }
        }

        double _lat = double.MinValue;
        [DataMember]
        public double lat
        {
            get { return _lat; }
            set { _lat = value; }
        }

        string _photourl1 = string.Empty;
        [DataMember]
        public string photourl1
        {
            get { return _photourl1; }
            set { _photourl1 = value; }
        }

        string _photourl2 = string.Empty;
        [DataMember]
        public string photourl2
        {
            get { return _photourl2; }
            set { _photourl2 = value; }
        }

        string _photourl3 = string.Empty;
        [DataMember]
        public string photourl3
        {
            get { return _photourl3; }
            set { _photourl3 = value; }
        }

        double _biz_ext_rating = double.MinValue;
        [DataMember]
        public double biz_ext_rating
        {
            get { return _biz_ext_rating; }
            set { _biz_ext_rating = value; }
        }

        double _biz_ext_cost = double.MinValue;
        [DataMember]
        public double biz_ext_cost
        {
            get { return _biz_ext_cost; }
            set { _biz_ext_cost = value; }
        }

        string _telephone = string.Empty;
        [DataMember]
        public string telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }
    }
}
