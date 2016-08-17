using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using QLCV.DAO;
namespace QLCV.SentMail
{
    public class MailReminder
    {
        public void DoSentMail(string nguoinhan)
        {
            DAO_User dao_user = new DAO_User();
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
                mailMessage.Subject = ConfigurationManager.AppSettings["MailReminderSubject"];
                mailMessage.Body = Body();
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(nguoinhan));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mailMessage);
            }
        }

        public string Body()
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(ConfigurationManager.AppSettings["MailReminder"]))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{text}", "nhắc nhở việc");
            //body = body.Replace("{tieude}", tieude);
            return body;
        }
    }
}