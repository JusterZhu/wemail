using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using Wemail.Common.RegionAdapters;
using Wemail.Views;

namespace Wemail
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// 应用程序启动时创建Shell
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册服务、依赖、View
            containerRegistry.RegisterForNavigation<TempViewA>();
            containerRegistry.RegisterForNavigation<TempViewB>();
        }

        /// <summary>
        /// 配置区域适配
        /// </summary>
        /// <param name="regionAdapterMappings"></param>
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //new ConfigurationModuleCatalog()

            //指定模块加载方式为从文件夹中以反射发现并加载module(推荐用法)
            return new DirectoryModuleCatalog() { ModulePath = @".\Apps" };
        }
    }
}
