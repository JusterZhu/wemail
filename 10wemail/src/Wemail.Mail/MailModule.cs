using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Wemail.Mail.Views;

namespace Wemail.Mail
{
    [Module(ModuleName = "Mail")]
    public class MailModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MailView>();
        }
    }
}