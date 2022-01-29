using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wemail.Common.Models;

namespace Wemail.Mail.Models
{
    public class MailModel
    {
        public MailModel() 
        {
            Sender = new ContactModel();
            Reciver = new ContactModel();
            CC = new ContactModel();
        }

        public string Subject { get; set; }

        public string Content { get; set; }

        public ContactModel Sender { get; set; }

        public ContactModel Reciver { get; set; }

        public ContactModel CC { get; set; }

        public DateTime SendTime { get; set; }

        public DateTime ReciverTime { get; set; }

        public override string ToString()
        {
            return Subject;
        }
    }
}
