using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MaxTrans.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        public bool SendEmail(string to, string subject, string body)
        {
            try
            {
                SmtpClient mailClient = new SmtpClient("smtp.ionos.com", 587);
                mailClient.Credentials = new NetworkCredential("admin@maxtranssystems.com", "MRSadmin@2022");
                mailClient.Port = 587;

                MailMessage message = new MailMessage();
                message.From = new MailAddress("admin@maxtranssystems.com", "Max App");
                message.To.Add(new MailAddress(to));
                message.Bcc.Add(new MailAddress("admin@maxtranssystems.com"));

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
