using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBerriesAnalyzer.Domain.Interfaces;
using WildBerriesAnalyzer.Domain.Models;

namespace WildBerriesAnalyzer.Modules.ActualDisconts.ViewModels
{
    public class ActualDiscontsPageViewModel : BindableBase
    {
        private IProductsService _productsService;
        private IDataBaseService _dataBaseService;

        #region Приватные поля

        private ObservableCollection<Discont> _disconts;
        private bool _isDiscontsLoaded;
        private bool _isAnyDiscont;

        #endregion

        #region Свойства

        public bool IsAnyDiscont
        {
            get => _isAnyDiscont;
            set
            {
                if (_isAnyDiscont != value)
                {
                    _isAnyDiscont = value;
                    RaisePropertyChanged(nameof(IsAnyDiscont));
                }
            }
        }
        public bool IsDiscontsLoaded
        {
            get => _isDiscontsLoaded;
            set
            {
                if (value != _isDiscontsLoaded)
                {
                    _isDiscontsLoaded = value;
                    RaisePropertyChanged(nameof(IsDiscontsLoaded));
                }
            }
        }
        public ObservableCollection<Discont> Disconts
        {
            get => _disconts;
            set
            {
                if (value != _disconts)
                {
                    _disconts = value;
                    RaisePropertyChanged(nameof(Disconts));
                }
            }
        }

        #endregion

        public ActualDiscontsPageViewModel(IProductsService productsService, IDataBaseService dataBaseService)
        {
            _productsService = productsService;
            _dataBaseService = dataBaseService;
            IsDiscontsLoaded = false;

            InitializeDiscontsAsync();
        }

        private async void InitializeDiscontsAsync()
        {
             Disconts = new ObservableCollection<Discont>(_productsService.GetDiscontsFromProducts(await _dataBaseService.GetProductsAsync()));
             IsDiscontsLoaded = true;
             IsAnyDiscont = Disconts != null && Disconts.Count > 0;
        }
    }
}
