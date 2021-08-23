using Component.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;


namespace Component
{
    public class ComponentModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(TranslateView), "TranslateView");
        }
    }
}