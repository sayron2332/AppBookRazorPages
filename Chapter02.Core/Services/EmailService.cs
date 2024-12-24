using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using Chapter02.Core.Dtos.Configuration;

namespace Chapter02.Core.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            var emailSettings = _config.GetSection("EmailSetting")
                .Get<EmailSettings>();

            
            MimeMessage email = new();
            email.From.Add(MailboxAddress.Parse(emailSettings?.User));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlMessage;
            email.Body = bodyBuilder.ToMessageBody();

            using (MailKit.Net.Smtp.SmtpClient smtpClient = new())
            {
                smtpClient.Connect(emailSettings?.SMTP, Int32.Parse(emailSettings.PORT),
                    SecureSocketOptions.SslOnConnect);
                smtpClient.Authenticate(emailSettings?.User, emailSettings.Password);
                await smtpClient.SendAsync(email);
                smtpClient.Disconnect(true);
            }
         
        }
    }
}
