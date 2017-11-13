using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace View_Spot_of_City.Language.Language
{
    public static class LanguageDictionaryHelper
    {
        /// <summary>
        /// 返回语言资源文件中对应键的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            return Application.Current.FindResource(key) as string;
        }

        /// <summary>
        /// 语言资源字典
        /// </summary>
        public static Dictionary<int, string> languageDictionary = new Dictionary<int, string>(2);

        static LanguageDictionaryHelper()
        {
            languageDictionary.Add(0, "CN");
            languageDictionary.Add(1, "EN");
        }
    }
}
