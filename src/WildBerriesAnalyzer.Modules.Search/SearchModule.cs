using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WildBerriesAnalyzer.Core;
using WildBerriesAnalyzer.Modules.Search.Views;

namespace WildBerriesAnalyzer.Modules.Search
{
    public class SearchModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public SearchModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(SearchPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}