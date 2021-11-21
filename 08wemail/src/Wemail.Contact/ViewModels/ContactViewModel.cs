using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wemail.Common.Events;
using Wemail.Common.MVVM;

namespace Wemail.Contact.ViewModels
{
    public class ContactViewModel : BindableBase , INavigationAware
    {
        private IDialogService _dialogService;
        private IEventAggregator _eventAggregator;
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

        public ContactViewModel(IEventAggregator eventAggregator)
        {
            Message = "Wemail.Contact Prism Module";
            Contacts.Add("联系人张某");
            Contacts.Add("联系人王某");
            //获取框架内聚合事件的引用
            _eventAggregator = eventAggregator;
        }

        private void OnSubscribeMessage(TempEventModel eventModel)
        {
            //输出接收到的消息内容
            Debug.WriteLine($" Wemail.Contact receive message : { eventModel.Name },{ eventModel.Age }.");
        }

        //private void OnSubscribeMessage(string message) 
        //{
        //    //输出接收到的消息内容
        //    Debug.WriteLine($" Wemail.Contact receive message : { message }.");
        //}

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //导航进入到当前页面时则订阅事件
            //_eventAggregator.GetEvent<MessagerEvent>().Subscribe(OnSubscribeMessage,false);

            _eventAggregator.GetEvent<MessagerEvent>().Subscribe(OnSubscribeMessage,
                ThreadOption.PublisherThread,false, MessageFilter);
        }

        //private bool MessageFilter(string messageType)
        //{
        //    if (messageType == MessagerType.JusterMessage) return true;

        //    return false;
        //}

        private bool MessageFilter(TempEventModel eventModel) 
        {
            if (eventModel.MessageType == MessagerType.JusterMessage) return true;

            return false;
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

            //离开页面时则取消订阅
            _eventAggregator.GetEvent<MessagerEvent>().Unsubscribe(OnSubscribeMessage);
        }
    }
}
