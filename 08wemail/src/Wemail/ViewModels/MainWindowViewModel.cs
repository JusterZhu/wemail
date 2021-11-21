using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using Wemail.Common.Events;
using Wemail.Common.MVVM;
using Wemail.Views;

namespace Wemail.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";

        //Region管理对象
        private IRegionManager _regionManager;
        private IModuleCatalog _moduleCatalog;
        private IModuleInfo _moduleInfo;
        private ILogger _logger;
        private IDialogService _dialogService;
        //聚合事件
        private IEventAggregator _eventAggregator;
        private ObservableCollection<IModuleInfo> _modules;
        private DelegateCommand _loadModulesCommand;
        private DelegateCommand _printMsg1Command;
        private DelegateCommand _printMsg2Command;
        private CompositeCommand _tempCompoCommand;
        //带参命令‘<>’里可写任意类型作为command的参数
        private DelegateCommand<string> _prameterCommand;

        public IView View { get; set; }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<IModuleInfo> Modules
        {
            get => _modules ?? (_modules = new ObservableCollection<IModuleInfo>());
        }

        public DelegateCommand LoadModulesCommand { get => _loadModulesCommand = new DelegateCommand(InitModules); }

        public IModuleInfo ModuleInfo 
        { 
            get 
            {
                return _moduleInfo; 
            }

            set 
            {
                _moduleInfo = value;
                Navigate(value);
            }
        }

        public CompositeCommand TempCompoCommand { get => _tempCompoCommand ?? (_tempCompoCommand = new CompositeCommand()); }
        public DelegateCommand PrintMsg1Command { get => _printMsg1Command ?? (_printMsg1Command = new DelegateCommand(PrintMsgAction)); }
        public DelegateCommand PrintMsg2Command { get => _printMsg2Command ?? (_printMsg2Command = new DelegateCommand(PrintMsgAction)); }
        
        public DelegateCommand<string> PrameterCommand { get => _prameterCommand ?? (_prameterCommand = new DelegateCommand<string>(PrameterAction)); }

        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog,
            ILogger logger,IDialogService dialogService,IEventAggregator eventAggregator)
        {
            _dialogService = dialogService;
            _logger = logger;
            _regionManager = regionManager;
            _moduleCatalog = moduleCatalog;
            _eventAggregator = eventAggregator;
            TempCompoCommand.RegisterCommand(PrintMsg1Command);
            TempCompoCommand.RegisterCommand(PrintMsg2Command);
        }

        int i = 0;
        private void PrintMsgAction() 
        {
            Debug.WriteLine($"{ ++i }");
        }

        private void PrameterAction(string prameter) 
        {
            Debug.WriteLine(prameter);

            var eventModel = new TempEventModel();
            eventModel.Age = 18;
            eventModel.Name = prameter;
            eventModel.MessageType = MessagerType.BillMessage;

            //发布消息
            _eventAggregator.GetEvent<MessagerEvent>().Publish(eventModel);
        }

        private void InitModules() 
        {
            var dirModuleCatalog = _moduleCatalog as DirectoryModuleCatalog;
            Modules.AddRange(dirModuleCatalog.Modules);
        }

        private void Navigate(IModuleInfo info) 
        {
            var paramete = new NavigationParameters();
            //任意定义key，value。导航到的视图按照约定key获取value即可。
            paramete.Add($"{ info.ModuleName }", DateTime.Now.ToString());
            _regionManager.RequestNavigate("ContentRegion", $"{ info.ModuleName }View", paramete);
        }
    }
}