using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using View_Spot_of_City.UIControls.Helper;

namespace View_Spot_of_City.UIControls.Converter
{
    public class Mail2AvatarBruahConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueStr = value as string;
            string userInputMail = valueStr == string.Empty ? "rsgisgeeks@qq.com" : valueStr;
            return new ImageBrush(AvatarHelper.GetAvatarByEmail(userInputMail));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
