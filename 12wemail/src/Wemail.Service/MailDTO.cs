namespace Wemail.Service
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
    }

    public class ContactDTO
    {
        public string Mail { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int Sex { get; set; }
    }
}
