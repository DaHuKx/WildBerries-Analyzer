using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WildBerriesAnalyzer.Core;

namespace WildBerriesAnalyzer.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        #region private

        private double _opacity;

        #endregion

        #region public

        public double Opacity
        {
            get => _opacity;
            set
            {
                if (_opacity != value)
                {
                    _opacity = value;
                    RaisePropertyChanged(nameof(Opacity));
                }
            }
        }

        #endregion

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Opacity = 1;
        }

        public ICommand Transition => new DelegateCommand<string>(TransitionAnimationAsync);

        public async void TransitionAnimationAsync(string moduleName)
        {
            await Task.Factory.StartNew(() =>
            {
                //for (double opacity = 1; opacity >= 0; opacity -= 0.05)
                //{
                //    Opacity = opacity;
                //    Thread.Sleep(5);
                //}

                Application.Current.Dispatcher.Invoke(() => _regionManager.RequestNavigate(RegionNames.MainContentRegion, moduleName));

                //for (double opacity = 0; opacity <= 1; opacity += 0.05)
                //{
                //    Opacity = opacity;
                //    Thread.Sleep(5);
                //}
            });
        }
    }
}
