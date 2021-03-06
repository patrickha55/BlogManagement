using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BlogManagement.Application.Services.ClientServices
{
    /// <summary>
    /// This class implement IEmailSender interface to handle the email sending logic.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string fromEmailAddress;

        public EmailSender(string smtpServer, int smtpPort, string fromEmailAddress)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.fromEmailAddress = fromEmailAddress;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            using var client = new SmtpClient(smtpServer, smtpPort);
            client.Send(message);

            return Task.CompletedTask;
        }
    }
}
