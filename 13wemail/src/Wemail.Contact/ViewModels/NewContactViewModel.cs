using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wemail.Common.MVVM;
using Wemail.Contact.Models;
using Wemail.DAL;

namespace Wemail.Contact.ViewModels
{
    public class NewContactViewModel : BindableBase
    {
        private DelegateCommand _addContactCommand;
        private ContactModel _contact;
        private bool _isInvalid;

        public ContactModel Contact { get => _contact; set { SetProperty(ref _contact, value); } }

        public DelegateCommand AddContactCommand { get => _addContactCommand ?? (_addContactCommand = new DelegateCommand(AddContactAction)); }

        public IView View { get; set; }

        public bool IsInvalid 
        {
            get => _isInvalid; 
            set
            { 
                SetProperty(ref _isInvalid, value); 
            } 
        }

        public NewContactViewModel() 
        {
            Contact = new ContactModel();
        }

        private void AddContactAction()
        {
            if (IsInvalid) return;

            //HttpHelper.Insert(Contact.Mail, Contact.Phone, Contact.Name, Contact.Age, Contact.Sex);
            View.Shutdown();
        }
    }
}
