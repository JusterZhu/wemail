using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using Wemail.Common.MVVM;
using Wemail.Common.User;
using Wemail.Models;

namespace Wemail.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private MainModel _currentModel;

        private IRegionManager _regionManager;
        private IModuleCatalog _moduleCatalog;
        private IDialogService _dialogService;
        private ILogger _logger;
        private IUser _user;

        private ObservableCollection<MainModel> _modules;
        private DelegateCommand _loadModulesCommand;
        private DelegateCommand _loginCommand;
        

        public IView View { get; set; }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<MainModel> Modules
        {
            get => _modules ?? (_modules = new ObservableCollection<MainModel>());
        }

        public DelegateCommand LoadModulesCommand { get => _loadModulesCommand = new DelegateCommand(InitModules); }

        public MainModel CurrentModel
        { 
            get 
            {
                return _currentModel; 
            }

            set 
            {
                _currentModel = value;
                Navigate(value);
            }
        }

        public DelegateCommand LoginCommand { get => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAtion)); }

        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog,
          IDialogService dialogService,  ILogger logger,IUser user)
        {
            _user = user;
            _logger = logger;
            _regionManager = regionManager;
            _moduleCatalog = moduleCatalog;
            _dialogService = dialogService;
        }

        private void LoginAtion()
        {
            _dialogService.ShowDialog("UserLoginView", (r)=> 
            {
                var result = r.Result;
                if (result == ButtonResult.OK)
                {
                    var loginStatus = r.Parameters.GetValue<bool>("LoginStatus");
                    _user.SetUserLoginState(loginStatus);
                }
            });
        }


        private void InitModules() 
        {
            var dirModuleCatalog = _moduleCatalog as DirectoryModuleCatalog;
            foreach (var module in dirModuleCatalog.Items)
            {
                var tempModule = module as ModuleInfo;
                switch (tempModule.ModuleName)
                {
                    case "Contact":
                        Modules.Add(new MainModel() { DisplayName = "联系人" , Name = tempModule.ModuleName, IConPath = "/Wemail.Resource;component/imgs/contact_icon.png" });
                        break;
                    case "Schedule":
                        Modules.Add(new MainModel() { DisplayName = "规划" , Name = tempModule.ModuleName, IConPath = "/Wemail.Resource;component/imgs/schedule_icon.png" });
                        break;
                }
            }
        }

        private void Navigate(MainModel model) 
        {
            var paramete = new NavigationParameters();
            paramete.Add($"{ model.Name }", DateTime.Now.ToString());
            _regionManager.RequestNavigate("ContentRegion", $"{ model.Name }View", paramete);
        }
    }
}