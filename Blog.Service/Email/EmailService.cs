using Blog.Service.Email.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _settings;
        private readonly SmtpClient _client;

        public EmailService(IOptions<SmtpSettings> options)
        {
            _settings = options.Value;
            _client = new SmtpClient(_settings.Server)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
            };
        }

        public Task SendEmail(string email, string subject, string message)
        {
            var mailMessage = new MailMessage(
                _settings.From,
                email,
                subject,
                message);

            return _client.SendMailAsync(mailMessage);
        }
    }
}
