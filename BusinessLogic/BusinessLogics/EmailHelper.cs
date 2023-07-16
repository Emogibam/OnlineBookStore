using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace BusinessLogic.BusinessLogics
{
    internal class EmailHelper
    {
        public static void SendEmail(string recipientEmail, string subject, string body)
        {
            string senderEmail = "eogidan22@gmail.com";
            string senderPassword = "EMMAnuel1994$"; // Use your App Password if 2FA is enabled

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body)
            {
                IsBodyHtml = true // Set to true if you are sending HTML content in the body.
            };

            smtpClient.Send(mailMessage);
        }


    }
}
