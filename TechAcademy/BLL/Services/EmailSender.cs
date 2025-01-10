using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser; // Gmail account
        private readonly string _smtpPassword; // App password or Gmail password

        // Constructor that accepts credentials as parameters
        public EmailSender(string smtpUser, string smtpPassword)
        {
            _smtpUser = smtpUser;
            _smtpPassword = smtpPassword;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_smtpHost)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUser, _smtpPassword),
                EnableSsl = true, // Gmail requires SSL
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("no-reply@yourdomain.com"), // This can be any email address you choose for the sender
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email); // Recipient's email
            return client.SendMailAsync(mailMessage);
        }
    }
}
