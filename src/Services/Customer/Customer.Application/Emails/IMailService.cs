using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Emails
{
    public interface IMailService
    {
        bool SendEmailAsync(EmailMessage message);
    }
}
