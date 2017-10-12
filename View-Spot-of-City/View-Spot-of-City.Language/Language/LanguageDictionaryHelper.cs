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
    }
}
