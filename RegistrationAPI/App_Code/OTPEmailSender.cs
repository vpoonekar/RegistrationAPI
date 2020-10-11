using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Common;
using System.Net.Mail;
namespace RegistrationAPI.App_Code
{
    public class OTPEmailSender : EmailSender
    {        
        public OTPEmailSender(EmailConfiguration _emailConfig) : base(_emailConfig)
        { }

        public OTPEmailSender()
        { 
        }      
        protected override MailMessage CreateEmailMessage(EmailMessage _message)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(_emailConfig.From);
            emailMessage.To.Add(new MailAddress(_message.To));
            emailMessage.Subject = _message.Subject;
            emailMessage.Body = "The OTP for your email verification is : " + _message.Body;
            emailMessage.IsBodyHtml = true;
            return emailMessage;
        }

        public override void SendEmail(EmailMessage _message)
        {
            throw new NotImplementedException();
        }

        public override async Task SendEmailAsync(EmailMessage _message)
        {
            MailMessage msg = CreateEmailMessage(_message);
            await SendAsync(msg);
        }
    }
}
