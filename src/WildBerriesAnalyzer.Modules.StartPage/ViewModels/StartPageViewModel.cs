using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBerriesAnalyzer.Business.Services;
using WildBerriesAnalyzer.Domain.Interfaces;

namespace WildBerriesAnalyzer.Modules.StartPage.ViewModels
{
    public class StartPageViewModel : BindableBase
    {
        #region private

        private long _productsCount;
        private long _historyPricesCount;
        private string _message;

        #endregion

        #region public
        public long ProductsCount
        {
            get => _productsCount;
            set
            {
                if (value !=  _productsCount)
                {
                    _productsCount = value;
                    RaisePropertyChanged(nameof(ProductsCount));
                }
            }
        }

        public long HistoryPricesCount
        {
            get => _historyPricesCount;
            set
            {
                if (value != _historyPricesCount)
                {
                    _historyPricesCount = value;
                    RaisePropertyChanged(nameof(HistoryPricesCount));
                }
            }
        }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        #endregion

        public StartPageViewModel(IDataBaseService dataBaseService)
        {
            InitializeStartPageAsync(dataBaseService);
        }

        private async void InitializeStartPageAsync(IDataBaseService dataBaseService)
        {
            ProductsCount = await dataBaseService.GetProductsCountAsync();
            HistoryPricesCount = await dataBaseService.GetHistoryPricesCountAsync();
        }
    }
}
