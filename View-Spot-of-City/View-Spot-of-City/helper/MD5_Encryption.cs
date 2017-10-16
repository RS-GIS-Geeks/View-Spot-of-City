using System;
using System.Security.Cryptography;
using System.Text;

namespace View_Spot_of_City.helper
{
    class MD5_Encryption
    {
        public static string MD5Encode(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(strText));
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }
}
