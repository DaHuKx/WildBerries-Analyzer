using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WildBerriesAnalyzer.Core;
using WildBerriesAnalyzer.Modules.ActualDisconts.Views;

namespace WildBerriesAnalyzer.Modules.ActualDisconts
{
    public class ActualDiscontsModule : IModule
    {
        private IRegionManager _regionManager;

        public ActualDiscontsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ActualDiscontsPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}