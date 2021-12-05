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
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ScheduleView>();
        }
    }
}