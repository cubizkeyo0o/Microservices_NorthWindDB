using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Emails
{
    public class EmailMessage
    {
        public string? ReceiverEmail { get; set; }
        public string? ReceiverName { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
    }
}
