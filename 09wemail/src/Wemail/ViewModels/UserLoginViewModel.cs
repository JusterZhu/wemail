using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using Wemail.DAL;

namespace Wemail.ViewModels
{
    public class UserLoginViewModel : BindableBase, IDialogAware
    {
        private string _account, _passworld;

        private DelegateCommand _loginCommand;
        private DelegateCommand _cancelCommand;

        public string Title => "用户登录";

        public DelegateCommand LoginCommand { get => _loginCommand = new DelegateCommand(LoginAction); }
        public DelegateCommand CancelCommand { get => _cancelCommand = new DelegateCommand(CancelAction); }
        public string Account { get => _account; set { SetProperty(ref _account, value); } }
        public string Passworld { get => _passworld; set { SetProperty(ref _passworld, value); } }

        private void CancelAction()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel, null));
        }

        private void LoginAction()
        {
            var userDto = HttpHelper.Login(Account, Passworld);

            if (userDto != null && !string.IsNullOrEmpty(userDto.Token))
            {
                var parameter = new DialogParameters();
                parameter.Add("LoginStatus", true);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameter));
            }
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}