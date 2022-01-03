using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Wemail.Mail.Models;

namespace Wemail.Mail.ViewModels
{
    public class NewMailViewModel : BindableBase
    {
        private string server = "smtp.163.com";
        private string _content;
        private MailModel _mailModel;
        private DelegateCommand _sendCommand, _syncCommand;

        public MailModel MailModel { get => _mailModel; set { SetProperty(ref _mailModel, value); } }

        public string Content 
        {
            get => _content;
            set
            { 
                SetProperty(ref _content, value);
            }
        }

        public DelegateCommand SendCommand { get => _sendCommand ?? (_sendCommand = new DelegateCommand(SendAction)); }

        public DelegateCommand SyncCommand { get => _syncCommand ?? (_syncCommand = new DelegateCommand(SyncAction)); }

        public NewMailViewModel() 
        {
            MailModel = new MailModel();
            MailModel.Sender.Mail = "wemailtest2021@163.com";
        }

        private void SendAction()
        {
            Send();
        }

        private void SyncAction()
        {
            
        }

        public void Send() {
            var sender = MailModel.Sender;
            var reciver = MailModel.Reciver;
            var cc = MailModel.CC;

            var from = new MailAddress(sender.Mail, sender.Name);
            var to = new MailAddress(reciver.Mail, reciver.Name);
            var message = new MailMessage(from, to);
            message.Subject = MailModel.Subject;
            message.Body = MailModel.Content;

            //抄送
            if (!string.IsNullOrEmpty(cc.Mail))
            {
                MailAddress copy = new MailAddress(cc.Mail,cc.Name);
                message.CC.Add(copy);
            }

            //添加附件
            //Attachment attachment1 = new Attachment();
            //message.Attachments.Add(attachment1);

            try
            {
                SmtpClient client = new SmtpClient(server);
                client.Credentials = new NetworkCredential(sender.Mail, "WSBKAESFKKUGOQFT");
                client.Send(message);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
