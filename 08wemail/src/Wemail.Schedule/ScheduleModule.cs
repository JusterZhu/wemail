using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Wemail.Schedule.Views;

namespace Wemail.Schedule
{
    [Module(ModuleName = "Schedule")]
    public class ScheduleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            ////通过注册RegionManager，添加ContactView
            //var regionManager = containerProvider.Resolve<IRegionManager>();
            ////通过ContentRegion管理导航默认初始页面ContactView
            //var contentRegion = regionManager.Regions["ContentRegion"];
            //contentRegion.RequestNavigate(nameof(ScheduleView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ScheduleView>();
        }
    }
}