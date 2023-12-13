using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WildBerriesAnalyzer.Core;
using WildBerriesAnalyzer.Modules.AddByName.Views;

namespace WildBerriesAnalyzer.Modules.AddByName
{
    public class AddByNameModule : IModule
    {
        private IRegionManager _regionManager;

        public AddByNameModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(AddByNamePage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}