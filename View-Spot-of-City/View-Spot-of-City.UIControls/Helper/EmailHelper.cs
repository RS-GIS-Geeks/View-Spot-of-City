using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Mail;
using static System.Configuration.ConfigurationManager;

using static View_Spot_of_City.Language.Language.LanguageDictionaryHelper;

namespace View_Spot_of_City.UIControls.Helper
{
    public static class EmailHelper
    {
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