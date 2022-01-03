using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wemail.Common.Models;
using Wemail.Common.User;
using Wemail.DAL;
using Wemail.DAL.DTOs;
using Wemail.Mail.Models;
using Wemail.Mail.Views;

namespace Wemail.Mail.ViewModels
{
    public class MailViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<MailModel> _mails;
        private DelegateCommand _sendCommand,_syncCommand;
        private IDialogService _dialogService;
        private IUser _user;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ObservableCollection<MailModel> Mails { get => _mails ?? (_mails = new ObservableCollection<MailModel>()); }

        public DelegateCommand SendCommand { get => _sendCommand ?? (_sendCommand = new DelegateCommand(SendAction)); }

        public DelegateCommand SyncCommand { get => _syncCommand ?? (_syncCommand = new DelegateCommand(SyncAction)); }

        public MailViewModel(IDialogService dialogService, IUser user)
        {
            _dialogService = dialogService;
            _user = user;
        }

        private void SendAction()
        {
            NewMailView newMailView = new NewMailView();
            newMailView.ShowDialog();
        }

        private void SyncAction()
        {
            Mails.Clear();
            var mails = HttpHelper.GetMails();
            Mails.AddRange(ConvertToModel(mails));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

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

        private void InitData()
        {
            Mails.Clear();
            var mails = HttpHelper.GetMails();
            Mails.AddRange(ConvertToModel(mails));
        }

        private List<MailModel> ConvertToModel(List<MailDTO> contacts)
        {
            var result = new List<MailModel>();
            contacts.ForEach(mail =>
            {
                var sender = new ContactModel { Name = mail.Sender.Name, Age = mail.Sender.Age, Mail = mail.Sender.Mail, Phone = mail.Sender.Phone, Sex = mail.Sender.Sex };
                var reciver = new ContactModel { Name = mail.Reciver.Name, Age = mail.Reciver.Age, Mail = mail.Reciver.Mail, Phone = mail.Reciver.Phone, Sex = mail.Reciver.Sex };
                var cc = new ContactModel { Name = mail.CC.Name, Age = mail.CC.Age, Mail = mail.CC.Mail, Phone = mail.CC.Phone, Sex = mail.CC.Sex };
                result.Add(new MailModel { Subject = mail.Subject , Content = mail.Content , Sender = sender, Reciver = reciver , CC = cc, ReciverTime = mail.ReciverTime , SendTime = mail.SendTime });
            });
            return result;
        }
    }
}
