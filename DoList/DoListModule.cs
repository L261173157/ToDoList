using DoList.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DoList
{
    public class DoListModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(MainView), "MainView");
        }
    }
}