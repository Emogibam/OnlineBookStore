using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IEmailHelper
    {
        public void SendEmail(string recipientEmail, string subject, string body, bool isHtml = false);
    }
}
