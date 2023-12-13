using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WildBerriesAnalyzer.Core;
using WildBerriesAnalyzer.Modules.AddById.Views;

namespace WildBerriesAnalyzer.Modules.AddById
{
    public class AddByIdModule : IModule
    {
        private IRegionManager _regionManager;
        public AddByIdModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(AddByIdPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}