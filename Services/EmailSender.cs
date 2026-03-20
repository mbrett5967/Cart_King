using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Cart_King.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpSettings = _configuration.GetSection("Mailtrap");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cart King", smtpSettings["FromEmail"] ?? "noreply@yourdomain.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlMessage
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(
                smtpSettings["Host"] ?? "sandbox.smtp.mailtrap.io",
                int.Parse(smtpSettings["Port"] ?? "587"),
                SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                smtpSettings["Username"],
                smtpSettings["Password"]);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}