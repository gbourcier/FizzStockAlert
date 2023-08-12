using FizzStockAlert.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FizzStockAlert.Services
{
    public class EmailService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;
        public EmailService(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _appSettings = configuration.GetSection("Configuration").Get<AppSettings>();
        }

        public void SendSuccessEmail(string[] toEmail)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtpClient = new SmtpClient(_appSettings.SMTPHost, _appSettings.SMTPPort)
            {
                Credentials = new System.Net.NetworkCredential(_appSettings.SMTPUser, _appSettings.SMTPPassword),
                EnableSsl = true
            };

            mail.From = new MailAddress(_appSettings.SMTPFromEmail);

            foreach (string email  in toEmail)
            {
                mail.To.Add(email);
            }
            

            mail.Subject = "Phone might be in stock";

            mail.Body = $"Check {_appSettings.Target}";

            smtpClient.Send(mail);
        }

        public void SendCrashEmail(string[] toEmail, Exception e)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtpClient = new SmtpClient(_appSettings.SMTPHost, _appSettings.SMTPPort)
            {
                Credentials = new System.Net.NetworkCredential(_appSettings.SMTPUser, _appSettings.SMTPPassword),
                EnableSsl = true
            };

            mail.From = new MailAddress(_appSettings.SMTPFromEmail);

            foreach (string email in toEmail)
            {
                mail.To.Add(email);
            }

            mail.Subject = "Container just crashed.";

            mail.Body = $"Retrying in 5 min. Check logs. {e}";

            smtpClient.Send(mail);
        }

    }
}
