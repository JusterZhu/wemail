using Prism.Mvvm;
using Prism.Regions;
using Wemail.Views;

namespace Wemail.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //Region管理对象
        private IRegionManager _regionManager;
        private string _title = "Prism Application";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            //Prism框架内依赖注入的RegionManager
            _regionManager = regionManager;

            //_regionManager.RegisterViewWithRegion("StackPanelRegion", typeof(TempView));

            //在ContentRegion中注册视图TempView
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(TempView));
            //在ContentRegion中注册视图TempView
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(Temp2View));

            _regionManager.RegisterViewWithRegion("WorkRegion", typeof(Temp3View));

            //var contentRegion = _regionManager.Regions["ContentRegion"];
            //contentRegion.Context
            //contentRegion.Remove()
            //contentRegion.Activate()
            //foreach (var item in contentRegion.ActiveViews)
            //{
            //    contentRegion.Activate(item);
            //}
        }
    }
}
