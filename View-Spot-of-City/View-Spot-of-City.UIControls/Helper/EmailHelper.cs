using System;
using System.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;
using static System.Configuration.ConfigurationManager;

namespace View_Spot_of_City.UIControls.Helper
{
    public static class EmailHelper
    {
        /// <summary>
        /// 判断是否为邮箱
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>是否为邮箱</returns>
        public static bool IsEmail(string str)
        {
            try
            {
                string expression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                return Regex.IsMatch(str, expression, RegexOptions.Compiled);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail">收件邮箱</param>
        /// <param name="title">邮件主题</param>
        /// <param name="content">邮件内容</param>
        /// <returns>是否发送成功</returns>
        public static bool SendEmail(string mail, string title, string content)
        {
            MailMessage message = new MailMessage();
            {
                message.To.Add(mail);
                message.From = new MailAddress(AppSettings["MANAGER_MAIL_NUM"], AppSettings["MANAGER_MAIL_NAME"], Encoding.UTF8);
                message.Subject =title;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = content;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = false;
                message.Priority = MailPriority.Normal;
            }

            SmtpClient smtp = new SmtpClient();
            {
                smtp.Host = AppSettings["SmtpClient_HOST"];
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(AppSettings["MANAGER_MAIL_NUM"], AppSettings["MANAGER_MAIL_PASSWORD"]);
            }

            object userState = message;

            try
            {
                smtp.SendAsync(message, userState);
                return true;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
    }
}