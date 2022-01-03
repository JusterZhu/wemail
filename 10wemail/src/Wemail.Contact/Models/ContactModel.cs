using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wemail.Contact.Models
{
    public class ContactModel
    {
        public string Mail { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int Sex { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
