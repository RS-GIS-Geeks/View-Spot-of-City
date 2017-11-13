using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace View_Spot_of_City.UIControls.Helper
{
    public static class AvatarHelper
    {
        /// <summary>
        /// 通过邮箱在Gravatar上寻找头像
        /// </summary>
        /// <param name="mail">邮箱</param>
        /// <returns>头像</returns>
        public static BitmapImage GetAvatarByEmail(string mail)
        {
            string userInputMail = (mail == null || mail == string.Empty) ? "rsgisgeeks@qq.com" : mail;
            return new BitmapImage(new Uri(GravatarHelper.NetStandard.Gravatar.GetGravatarImageUrl(userInputMail)));
        }

        /// <summary>
        /// 通过邮件名在Gravatar上寻找头像
        /// </summary>
        /// <param name="mail">邮箱</param>
        /// <param name="avatarSize">头像大小[10, 500]</param>
        /// <returns>头像</returns>
        public static BitmapImage GetAvatarByEmail(string mail, int avatarSize)
        {
            avatarSize = avatarSize < 10 ? 10 : (avatarSize > 500 ? 500 : avatarSize);
            string userInputMail = (mail == null || mail == string.Empty) ? "rsgisgeeks@qq.com" : mail;
            return new BitmapImage(new Uri(GravatarHelper.NetStandard.Gravatar.GetGravatarImageUrl(userInputMail, avatarSize)));
        }
    }
}
