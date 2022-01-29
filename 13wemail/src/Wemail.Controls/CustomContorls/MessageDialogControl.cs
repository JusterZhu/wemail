using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wemail.Controls.CustomContorls
{
    public class MessageDialogControl : BindableBase , IDialogAware
    {
        private DelegateCommand _getMessageCommand;
        private DelegateCommand _cancelMessageCommand;
        private string _messageContent;

        public string MessageContent 
        { 
            get => _messageContent;
            set 
            {
                _messageContent = value;
                SetProperty(ref _messageContent, value);
            }
        }

        public DelegateCommand GetMessageCommand 
        {
            get => _getMessageCommand = new DelegateCommand(() => 
            {
                var parameter = new DialogParameters();
                parameter.Add("MessageContent", MessageContent);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameter));
            });
        }

        public DelegateCommand CancelMessageCommand 
        { 
            get => _cancelMessageCommand = new DelegateCommand(() => 
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
            });
        }

        public string Title => "Message";
        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// 允许用户手动关闭当前窗口
        /// </summary>
        /// <returns></returns>
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// 关闭dialog的操作
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDialogClosed()
        {
            
        }

        /// <summary>
        /// dialog接收参数传递
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDialogOpened(IDialogParameters parameters)
        {
            var parameterContent = parameters.GetValue<string>("Value");

        }
    }
}
