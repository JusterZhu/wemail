using Microsoft.Extensions.Logging;
using NLog.Config;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wemail.Common.Helpers;
using Wemail.Common.RegionAdapters;
using Wemail.Views;

namespace Wemail
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        Microsoft.Extensions.Logging.ILogger _logger;

        /// <summary>
        /// 应用程序启动时创建Shell
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            //UI线程未捕获异常处理事件
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            //多线程异常
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException; 
            return Container.Resolve<MainWindow>();
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //通常全局异常捕捉的都是致命信息
            _logger.LogCritical($"{ e.Exception.StackTrace },{ e.Exception.Message }");
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _logger.LogCritical($"{ e.Exception.StackTrace },{ e.Exception.Message }");
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            _logger.LogCritical($"{ ex.StackTrace },{ ex.Message }");

            //记录dump文件
            MiniDump.TryDump($"dumps\\Wemail_{ DateTime.Now.ToString("HH-mm-ss-ms") }.dmp");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //创建Nlog组件实例
            var factory = new NLog.Extensions.Logging.NLogLoggerFactory();
            _logger = factory.CreateLogger("NLog");
            //注入到Prism DI容器中
            containerRegistry.RegisterInstance(_logger);
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
