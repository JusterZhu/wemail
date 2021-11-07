using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wemail.Contact.ViewModels
{
    public class ContactViewModel : BindableBase , INavigationAware
    {
        private ObservableCollection<string> _contacts;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ObservableCollection<string> Contacts 
        { 
            get => _contacts ?? (_contacts = new ObservableCollection<string>()); 
        }

        public ContactViewModel()
        {
            Message = "Wemail.Contact Prism Module";
            Contacts.Add("联系人张某");
            Contacts.Add("联系人王某");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var parameter = navigationContext.Parameters["Contact"];

            if (parameter == null) return;
            //导航到当前页面前, 此处可以传递过来的参数以及是否允许导航等动作的控制
            Debug.WriteLine(parameter.ToString() + "To Contact View.");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //根据业务需要调整该视图，是否创建新示例。为true的时候表示不创建新实例，页面还是之前的；
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //导航离开当前页面前。
            Debug.WriteLine("Leave Contact View.");
        }
    }
}
