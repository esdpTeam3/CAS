using CAStest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace CAStest.Services
{
    public class EmailSender : IEmailSender
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string SmtpEmail { get; set; }
        public int SmtpPort { get; set; }


        public EmailSender()
        {
            UserEmail = "castest.mailbox@gmail.com";
            UserPassword = "castest1!";
            SmtpEmail = "smtp.gmail.com";
            SmtpPort = 587; 
        }

        public void SendEmail(Message message)
        {
            MailAddress from = new MailAddress(UserEmail, UserEmail);
            MailAddress to = new MailAddress(message.RecipientEmail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = message.Subject;
            m.Body = message.Text;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(SmtpEmail, SmtpPort);
            smtp.Credentials = new NetworkCredential(UserEmail, UserPassword);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
