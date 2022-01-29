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
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ContactView>();
        }
    }
}