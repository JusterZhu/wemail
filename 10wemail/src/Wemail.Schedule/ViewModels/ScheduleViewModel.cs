using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wemail.Common.User;

namespace Wemail.Schedule.ViewModels
{
    public class ScheduleViewModel : BindableBase, INavigationAware
    {
        private IDialogService _dialogService;
        private IUser _user;
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ScheduleViewModel(IDialogService dialogService,IUser user)
        {
            _user = user;
            _dialogService = dialogService;
            Message = "Wemail.Schedule Prism Module";
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
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
