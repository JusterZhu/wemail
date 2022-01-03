using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Wemail.Common.MVVM;
using Wemail.Common.User;
using Wemail.Contact.Models;
using Wemail.DAL;
using Wemail.DAL.DTOs;

namespace Wemail.Contact.ViewModels
{
    public class ContactViewModel : BindableBase , INavigationAware
    {
        private IDialogService _dialogService;
        private IUser _user;
        private ObservableCollection<ContactModel> _contacts;
        private DelegateCommand _launchCommand;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ObservableCollection<ContactModel> Contacts 
        { 
            get => _contacts ?? (_contacts = new ObservableCollection<ContactModel>()); 
        }

        public IView View { get; set; }
        public DelegateCommand LaunchCommand { get => _launchCommand ?? (_launchCommand = new DelegateCommand(LaunchAction)); }

        private void LaunchAction()
        {
           View.Launch();
        }

        public ContactViewModel(IDialogService dialogService, IUser user)
        {
            _dialogService = dialogService;
            _user = user;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (!_user.IsLogin())
            {
                _dialogService.ShowDialog("UserLoginView", (r) =>
                {
                    var result = r.Result;
                    if (result == ButtonResult.OK)
                    {
                        var loginStatus = r.Parameters.GetValue<bool>("LoginStatus");
                        _user.SetUserLoginState(loginStatus);
                    }
                });

                if (!_user.IsLogin()) return;
            }
            InitData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void InitData() 
        {
            Application.Current.Dispatcher.Invoke(() => 
            {
                Contacts.Clear();
                var contactsDTO = HttpHelper.GetContacts();
                Contacts.AddRange(ConvertToModel(contactsDTO));
            });
        }

        private List<ContactModel> ConvertToModel(List<ContactDTO> contacts) 
        {
            List<ContactModel> result = new List<ContactModel>();
            contacts.ForEach(contact => 
            {
                result.Add(new ContactModel 
                { 
                    Name = contact.Name ,
                    Age = contact.Age , 
                    Mail = contact.Mail ,
                    Phone = contact.Phone ,
                    Sex = contact.Sex 
                });
            });
            return result;
        }
    }
}