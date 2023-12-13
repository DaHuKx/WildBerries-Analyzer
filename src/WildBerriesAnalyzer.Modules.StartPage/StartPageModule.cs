using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Reflection.Metadata;
using WildBerriesAnalyzer.Core;
using WildBerriesAnalyzer.Modules.StartPage.Views;

namespace WildBerriesAnalyzer.Modules.StartPage
{
    public class StartPageModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public StartPageModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(Views.StartPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}