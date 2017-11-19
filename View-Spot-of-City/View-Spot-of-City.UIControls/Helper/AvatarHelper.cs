using System;
using System.Net;
using System.IO;
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
            BitmapImage bitmap = new BitmapImage();

            try
            {
                var c = new WebClient();
                var bytes = c.DownloadData(GravatarHelper.NetStandard.Gravatar.GetSecureGravatarImageUrl(userInputMail));
                bitmap = GetImage(bytes);
            }
            catch(Exception ex)
            {
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/View-Spot-of-City;component/Icon/Transparent.png"));
                LogManager.LogManager.Error("网络错误，头像获取失败", ex);
            }

            return bitmap;
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

        /// <summary>
        /// 通过byte流加载图片
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static BitmapImage GetImage(byte[] buffer)
        {
            if (buffer == null || buffer.Length <= 0)
                return null;

            BitmapImage bitmap = null;
            try
            {
                bitmap = new BitmapImage();
                bitmap.DecodePixelHeight = 200; // 确定解码高度，宽度不同时设置
                bitmap.BeginInit();

                bitmap.CacheOption = BitmapCacheOption.OnLoad;

                using (Stream ms = new MemoryStream(buffer))
                {
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze();

                    ms.Dispose();
                }
            }
            catch(Exception ex)
            {
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/View-Spot-of-City;component/Icon/Transparent.png"));
                LogManager.LogManager.Error("头像加载错误", ex);
            }

            return bitmap;
        }
    }
}
