using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Net.Mime;

namespace ScreenShotReceiver
{
    public class MailUtil
    {
        public static void SendEmail(string subject, string body, string recipient)
        {
            MailAddress fromAddress = new MailAddress("screenwatchnotifier@gmail.com", "Screen Watch");
            const string fromPassword = "kfqkksxixcxiclpw";
            
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, new MailAddress(recipient)))
            {
                message.Subject = subject;
                message.Body = body;
                smtp.Send(message);
            }
        }
    }
}