using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wemail.DAL.DTOs
{
    public class MailDTO
    {
        public MailDTO()
        {
            Sender = new ContactDTO();
            Reciver = new ContactDTO();
            CC = new ContactDTO();
        }

        public int Id { get; set; }
        public string Subject { get; set; }

        public string Content { get; set; }

        public ContactDTO Sender { get; set; }

        public ContactDTO Reciver { get; set; }

        public ContactDTO CC { get; set; }

        public DateTime SendTime { get; set; }

        public DateTime ReciverTime { get; set; }

        public override string ToString()
        {
            return Subject;
        }
    }
}
