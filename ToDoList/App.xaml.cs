using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using ToDoList;
using Prism.Regions;

using Component;
using DoList;
using ToDoList.Views;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: PrismApplication
    {
        
        protected override Window CreateShell()
        {
            MainView w = Container.Resolve<MainView>();
            return w;
        }
        #region 服务注册
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
          //  containerRegistry.Register<IEventAggregator,EventAggregator>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<ComponentModule>();
            moduleCatalog.AddModule<DoListModule>();
        }

        #endregion

    }
}
