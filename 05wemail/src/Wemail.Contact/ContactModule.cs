using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Wemail.Contact.Views;

namespace Wemail.Contact
{
    [Module(ModuleName = "Contact")]
    public class ContactModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            //通过ContentRegion管理导航默认初始页面ContactView
            var contentRegion = regionManager.Regions["ContentRegion"];
            //contentRegion.RequestNavigate(nameof(ContactView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ContactView>();
        }
    }
}