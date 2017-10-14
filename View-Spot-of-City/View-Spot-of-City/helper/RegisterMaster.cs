using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using View_Spot_of_City.Form;

namespace View_Spot_of_City.UIControls.Helper
{
    public class RegisterMaster
    {
        private static string path = AppDomain.CurrentDomain.BaseDirectory + "License.dat";
        public static bool CanStart()
        {
            if (!File.Exists(path))
            {
                LicenseDlg f = new LicenseDlg();
                if (f.ShowDialog() == true)
                {
                    return true;
                }
                return false;
            }
            Stream s = File.Open(path, FileMode.Open);//打开License.bat文件
            BinaryFormatter b = new BinaryFormatter();//创建一个序列化的对象
            String result = (String)b.Deserialize(s);//将s反序列化回原来的数据格式
            try
            {
                DateTime date = Convert.ToDateTime(result);
                if ((date - DateTime.Now).Seconds > 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
