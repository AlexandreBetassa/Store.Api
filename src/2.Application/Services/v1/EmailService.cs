using Store.CrossCutting.Configurations.v1.Models;
using System.Net.Mail;

namespace Store.Application.Services.v1
{
    public class EmailService(Appsettings appsettingsConfigurations) : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration = appsettingsConfigurations.EmailConfiguration;

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using var smtpClient = new SmtpClient(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort);
            smtpClient.Credentials = new NetworkCredential(_emailConfiguration.FromMail, _emailConfiguration.FromMailPassword);
            smtpClient.EnableSsl = true;

            using var mailMessage = new MailMessage(_emailConfiguration.FromMail, toEmail, subject, body);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}