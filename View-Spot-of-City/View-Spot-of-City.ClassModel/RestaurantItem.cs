using System.Runtime.Serialization;
using System.ComponentModel;
using View_Spot_of_City.ClassModel.Interface;

namespace View_Spot_of_City.ClassModel
{
    public class RestaurantItem : IGetLngLat, INotifyPropertyChanged
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

        string _Name = string.Empty;
        [DataMember]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }


        string _Type = string.Empty;
        [DataMember]
        public string Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type"));
            }
        }

        string _Address = string.Empty;
        [DataMember]
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Address"));
            }
        }

        string _Pname = string.Empty;
        [DataMember]
        public string Pname
        {
            get { return _Pname; }
            set
            {
                _Pname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Pname"));
            }
        }

        string _Cityname = string.Empty;
        [DataMember]
        public string Cityname
        {
            get { return _Cityname; }
            set
            {
                _Cityname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cityname"));
            }
        }


        string _Adminname = string.Empty;
        [DataMember]
        public string Adminname
        {
            get { return _Adminname; }
            set
            {
                _Adminname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Adminname"));
            }
        }


        double _Lng = double.MinValue;
        [DataMember]
        public double Lng
        {
            get { return _Lng; }
            set
            {
                _Lng = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lng"));
            }
        }

        double _Lat = double.MinValue;
        [DataMember]
        public double Lat
        {
            get { return _Lat; }
            set
            {
                _Lat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lat"));
            }
        }

        string _PhotoUrl1 = string.Empty;
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

        double _Rating = double.MinValue;
        [DataMember]
        public double Rating
        {
            get { return _Rating; }
            set
            {
                _Rating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rating"));
            }
        }

        double _Cost = double.MinValue;
        [DataMember]
        public double Cost
        {
            get { return _Cost; }
            set
            {
                _Cost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cost"));
            }
        }

        string _Telephone = string.Empty;
        [DataMember]
        public string Telephone
        {
            get { return _Telephone; }
            set
            {
                _Telephone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Telephone"));
            }
        }

        public double GetLng()
        {
            return Lng;
        }

        public double GetLat()
        {
            return Lat;
        }
    }
}
