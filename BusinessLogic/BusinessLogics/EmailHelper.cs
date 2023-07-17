using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using BusinessLogic.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BusinessLogic.BusinessLogics
{
    public class EmailHelper : IEmailHelper
    {
   
        private readonly string smtpUsername = "eogidan22@gmail.com";

     

        public async void SendEmail(string recipientEmail, string subject, string body, bool isHtml = false)
        {
            //var apiKey = Environment.GetEnvironmentVariable("8c9d2f0279db30b374a76e3a16a7c743");
            var client = new SendGridClient("ACcbe958b5caac5c398aa22256269d9110");
            var from_email = new EmailAddress(smtpUsername, "Example User");
            var to_email = new EmailAddress(recipientEmail, "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }


    }
}
