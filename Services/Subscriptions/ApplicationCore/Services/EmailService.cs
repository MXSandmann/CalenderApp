using ApplicationCore.Services.Contracts;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace ApplicationCore.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string userName, string userEmail, string eventName)
        {
            // Get values from config
            var body = _configuration.GetValue<string>("Email:Body");
            body = body!.Replace("{userName}", userName).Replace("{eventName}", eventName);
            var sendFrom = _configuration.GetValue<string>("Email:AddressFrom");
            var sendTo = sendFrom; // userEmail
            var subject = _configuration.GetValue<string>("Email:Subject");
            var host = _configuration.GetValue<string>("Email:Host");
            var port = _configuration.GetValue<int>("Email:Port");
            var password = _configuration.GetValue<string>("Email:Password");

            // Configure email
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(sendFrom));
            email.To.Add(MailboxAddress.Parse(sendTo));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            // Send using SmtpClient
            using var smtp = new SmtpClient();
            smtp.Connect(host, port, SecureSocketOptions.StartTls);
            smtp.Authenticate(sendFrom, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
