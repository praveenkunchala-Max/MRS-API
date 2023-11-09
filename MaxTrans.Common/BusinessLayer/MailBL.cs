using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace MaxTrans.Common.BusinessLayer
{
    public class MailBL
    {
        public static bool SendEmail(string to, string cc, string subject, string body)
        {
            try
            {
                SmtpClient mailClient = new SmtpClient("smtp.ionos.com", 587);
                mailClient.Credentials = new NetworkCredential("admin@maxtranssystems.com", "MRSadmin@2022");
                mailClient.Port = 587;

                MailMessage message = new MailMessage();
                message.From = new MailAddress("admin@maxtranssystems.com", "Max App");
                message.To.Add(new MailAddress(to));
                if(cc.Length>1)
                message.CC.Add(new MailAddress(cc));
                //message.Bcc.Add(new MailAddress("admin@maxtranssystems.com"));

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                mailClient.EnableSsl = true;
                mailClient.Send(message);

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
