using ApplicationCore.Services.Contracts;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace ApplicationCore.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string receiverEmail)
        {
            // Get values from config
            var body = _configuration.GetValue<string>("Email:Body");
            var sendFrom = _configuration.GetValue<string>("Email:AddressFrom");
            var sendTo = receiverEmail;
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
