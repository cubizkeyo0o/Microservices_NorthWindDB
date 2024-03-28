using SendEmail.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmail.Service
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}

