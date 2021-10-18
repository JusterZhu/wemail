using Prism.Ioc;
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

        }

        /// <summary>
        /// 配置区域适配
        /// </summary>
        /// <param name="regionAdapterMappings"></param>
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);

            //添加自定义区域适配对象,会自动适配标记上prism:RegionManager.RegionName的容器控件为Region
            regionAdapterMappings.RegisterMapping(typeof(TabControl),Container.Resolve<TabRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
        }
    }
}
