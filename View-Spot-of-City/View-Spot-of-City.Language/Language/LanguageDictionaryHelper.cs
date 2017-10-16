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
        public static string GetString(string key)
        {
            return Application.Current.FindResource(key) as string;
        }

        public static Dictionary<int, string> languageDictionary = new Dictionary<int, string>(2);

        static LanguageDictionaryHelper()
        {
            languageDictionary.Add(0, "CN");
            languageDictionary.Add(1, "EN");
        }
    }
}
