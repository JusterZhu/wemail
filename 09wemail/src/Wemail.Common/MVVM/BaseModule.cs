using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wemail.Common.MVVM
{
    public class BaseModule : IModule
    {
        public string Name { get; set; }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //通过注册RegionManager，添加ContactView
            var regionManager = containerProvider.Resolve<IRegionManager>();
            //通过ContentRegion管理导航默认初始页面ContactView
            var contentRegion = regionManager.Regions["ContentRegion"];
            contentRegion.RequestNavigate(nameof(Name));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IView>();
        }
    }
}
