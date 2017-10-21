using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace View_Spot_of_City.ClassModel.Base
{
    public abstract class CommonPlace : INotifyPropertyChanged
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
    }
}