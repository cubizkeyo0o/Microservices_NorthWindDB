using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Customer.Application.Emails
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailsettings;

        public MailService(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailsettings = mailSettingsOptions.Value;
        }

        public bool SendEmailAsync(EmailMessage message)
        {
            MailMessage mesgmail = new MailMessage();

            mesgmail.From = new MailAddress(_mailsettings.SenderEmail);
            mesgmail.To.Add(new MailAddress(message.ReceiverEmail));
            mesgmail.Subject = message.Title;
            mesgmail.Body = message.Body;

            using (SmtpClient smtpClient = new SmtpClient(_mailsettings.Server, _mailsettings.Port))
            {
                smtpClient.Credentials = new NetworkCredential(_mailsettings.SenderEmail, _mailsettings.Password);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mesgmail);
            }
            return true;
        }
    }
}