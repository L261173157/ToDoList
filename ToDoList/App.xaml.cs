using System.Windows;
using Component;
using DoList;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using ToDoList.Views;

namespace ToDoList;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        var w = Container.Resolve<MainView>();
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