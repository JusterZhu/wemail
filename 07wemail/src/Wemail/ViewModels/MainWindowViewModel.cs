using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows;
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
        private ObservableCollection<IModuleInfo> _modules;
        private DelegateCommand _loadModulesCommand;
        private DelegateCommand _showDialogCommand;
        
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

        public DelegateCommand ShowDialogCommand { get => _showDialogCommand = new DelegateCommand(ShowDialogAction); }

        private void ShowDialogAction()
        {
            _dialogService.ShowDialog("MessageDialogView", (r) => 
            {
                var result = r.Result;
                if (result == ButtonResult.OK) 
                {
                    var parameter = r.Parameters.GetValue<string>("MessageContent");
                }
            });
        }

        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog,ILogger logger,IDialogService dialogService)
        {
            _dialogService = dialogService;
            _logger = logger;
            _regionManager = regionManager;
            _moduleCatalog = moduleCatalog;
        }

        public void InitModules() 
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