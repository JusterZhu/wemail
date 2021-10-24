using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
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
        private DelegateCommand _loadModules;
        private IModuleInfo _moduleInfo;

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

        public DelegateCommand LoadModules { get => _loadModules = new DelegateCommand(InitModules); }

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

        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog)
        {
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
            _regionManager.RequestNavigate("ContentRegion", $"{ info.ModuleName }View");
        }
    }
}
