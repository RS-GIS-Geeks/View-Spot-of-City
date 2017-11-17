using System.ComponentModel;

using View_Spot_of_City.ClassModel.Interface;

namespace View_Spot_of_City.ClassModel.Base
{
    public abstract class CommonPlace : INotifyPropertyChanged, IGetLngLat
    {
        public event PropertyChangedEventHandler PropertyChanged;

        long _ID = long.MaxValue;
        /// <summary>
        /// 标识符
        /// </summary>
        public long ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
            }
        }

        double _Lng = double.MaxValue;
        /// <summary>
        /// 经度
        /// </summary>
        public double Lng
        {
            get { return _Lng; }
            set
            {
                _Lng = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lng"));
            }
        }

        double _Lat = double.MaxValue;
        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat
        {
            get { return _Lat; }
            set
            {
                _Lat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lat"));
            }
        }

        /// <summary>
        /// 构造一个可显示地点的
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        public CommonPlace(long id, double lng, double lat)
        {
            ID = id;
            Lng = lng;
            Lat = lat;
        }

        /// <summary>
        /// 获得经度
        /// </summary>
        /// <returns>经度</returns>
        public double GetLng()
        {
            return Lng;
        }

        /// <summary>
        /// 返回纬度
        /// </summary>
        /// <returns>纬度</returns>
        public double GetLat()
        {
            return Lat;
        }
    }
}