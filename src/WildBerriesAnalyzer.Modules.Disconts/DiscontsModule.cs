using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WildBerriesAnalyzer.Core;
using WildBerriesAnalyzer.Modules.Disconts.Views;

namespace WildBerriesAnalyzer.Modules.Disconts
{
    public class DiscontsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public DiscontsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(DiscontsPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}