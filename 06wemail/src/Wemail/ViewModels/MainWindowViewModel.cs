using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
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
        private ObservableCollection<IModuleInfo> _modules;
        private DelegateCommand _loadModulesCommand;
        private DelegateCommand _openViewA;
        private DelegateCommand _openViewB;
        private DelegateCommand _goBackView;
        private DelegateCommand _goForwardView;
        private IModuleInfo _moduleInfo;

        //导航日志
        private IRegionNavigationJournal _navigationJournal;

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

        public DelegateCommand OpenViewA 
        { 
            get => _openViewA ?? (_openViewA = new DelegateCommand(OpenViewAAction));
        }

        public DelegateCommand OpenViewB
        {
            get => _openViewB ?? (_openViewB = new DelegateCommand(OpenViewBAction)); 
        }

        public DelegateCommand GoBackView { get => _goBackView ?? (_goBackView = new DelegateCommand(GoBackViewAction)); }

        public DelegateCommand GoForwardView { get => _goForwardView ?? (_goForwardView = new DelegateCommand(GoForwardViewAction)); }

        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog,ILogger logger)
        {
            //throw new Exception("hello world.");
            logger.LogInformation("hhhhhhh");
            //regionManager.RegisterViewWithRegion("ContentRegion", typeof(TempViewA));
            //regionManager.RegisterViewWithRegion("ContentRegion", typeof(TempViewB));
            _regionManager = regionManager;
            _moduleCatalog = moduleCatalog;
        }

        private void OpenViewAAction()
        {
            //_regionManager.RequestNavigate("ContentRegion", "TempViewA");

            _regionManager.RequestNavigate("ContentRegion", "TempViewA",arg=> 
            {
                //记录导航日志上下文
                _navigationJournal = arg.Context.NavigationService.Journal;
            });
        }

        private void OpenViewBAction()
        {
            //_regionManager.RequestNavigate("ContentRegion", "TempViewB");

            _regionManager.RequestNavigate("ContentRegion", "TempViewB", arg =>
            {
                //记录导航日志上下文
                _navigationJournal = arg.Context.NavigationService.Journal;
            });
        }

        /// <summary>
        /// 导航日志：导航到上一个
        /// </summary>
        private void GoBackViewAction()
        {
            if (_navigationJournal.CanGoBack)
            {
                _navigationJournal.GoBack();
            }
        }

        /// <summary>
        /// 导航日志：导航到下一个
        /// </summary>
        private void GoForwardViewAction()
        {
            if (_navigationJournal.CanGoForward)
            {
                _navigationJournal.GoForward();
            }
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