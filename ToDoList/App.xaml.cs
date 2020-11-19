using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.DryIoc;
using Prism.Ioc;

using ToDoList;
using Prism.Regions;
using ToDoList.Views;
using ToDoList.Services;

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
        #endregion

    }
}
