using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WildBerriesAnalyzer.Business.Services;
using WildBerriesAnalyzer.Domain.Interfaces;
using WildBerriesAnalyzer.Domain.Models.DataBase;

namespace WildBerriesAnalyzer.Modules.AddById.ViewModels
{
    public class AddByIdPageViewModel : BindableBase
    {
        private IWildBerriesService _wildBerriesService;
        private IDataBaseService _dataBaseService;

        #region Приватные поля

        private bool _notifyIsVisible;
        private string _notifyMessage;
        private bool _loaded;
        private string _loadingStatus;
        private string _ids;
        private ObservableCollection<WbProduct> _products;

        #endregion

        #region Свойства

        public bool NotifyIsVisible
        {
            get => _notifyIsVisible;
            set
            {
                if (_notifyIsVisible != value)
                {
                    _notifyIsVisible = value;
                    RaisePropertyChanged(nameof(NotifyIsVisible));
                }
            }
        }
        public string NotifyMessage
        {
            get => _notifyMessage;
            set
            {
                if (value != _notifyMessage)
                {
                    _notifyMessage = value;
                    RaisePropertyChanged(nameof(NotifyMessage));
                }
            }
        }
        public string Ids
        {
            get => _ids;
            set
            {
                if (value != _ids)
                {
                    _ids = value;
                    RaisePropertyChanged(nameof(Ids));
                }
            }
        }
        public bool Loaded
        {
            get => _loaded;
            set
            {
                if (value != _loaded)
                {
                    _loaded = value;
                    RaisePropertyChanged(nameof(Loaded));
                }
            }
        }
        public string LoadingStatus
        {
            get => _loadingStatus;
            set
            {
                if (value != _loadingStatus)
                {
                    _loadingStatus = value;
                    RaisePropertyChanged(nameof(LoadingStatus));
                }
            }
        }
        public ObservableCollection<WbProduct> Products
        {
            get => _products;
            set
            {
                if (value != _products)
                {
                    _products = value;
                    RaisePropertyChanged(nameof(Products));
                }
            }
        }

        #endregion

        public AddByIdPageViewModel(IWildBerriesService wildBerriesService, IDataBaseService dataBaseService)
        {
            _wildBerriesService = wildBerriesService;
            _dataBaseService = dataBaseService;

            NotifyMessage = string.Empty;
            NotifyIsVisible = false;
            Loaded = false;
            LoadingStatus = "Введите артикул(ы) товара(ов)";
        }

        public ICommand FindByIdCommand => new DelegateCommand(FindById);
        public ICommand AddInDataBaseCommand => new DelegateCommand(AddInDataBaseAsync);

        private async void AddInDataBaseAsync()
        {

                Loaded = false;

                foreach (var item in Products)
                {
                    LoadingStatus = $"Добавление продуктов ({_products.IndexOf(item)}/{_products.Count})...";
                    await _dataBaseService.AddProductPriceAsync(item.PriceFromInit);
                    await _dataBaseService.AddProductAsync(item);
                }

                Loaded = true;

                Notify("Продукты успешно добавлены!");
        }

        private async void FindById()
        {
            Loaded = false;

            if (string.IsNullOrWhiteSpace(Ids))
            {
                return;
            }

            var ids = Ids.Split('\n');

            LoadingStatus = "Получение продуктов с WildBerries...";

            Products = new ObservableCollection<WbProduct>(await _wildBerriesService.GetProductsForIdAsync(ids));
            Loaded = true;

            Notify($"Найдено {Products.Count} из {ids.Length} продуктов!");
        }

        private void Notify(string message)
        {
            NotifyMessage = message;

            NotifyIsVisible = true;
            Thread.Sleep(3000);
            NotifyIsVisible = false;
        }
    }
}
