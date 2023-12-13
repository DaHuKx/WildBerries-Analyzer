using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WildBerriesAnalyzer.Business.Services;
using WildBerriesAnalyzer.Domain.Interfaces;
using WildBerriesAnalyzer.Domain.Models;
using WildBerriesAnalyzer.Domain.Models.DataBase;
using WildBerriesAnalyzer.PrismCore.Models;
using WildBerriesAnalyzer.PrismCore.Services;

namespace WildBerriesAnalyzer.Modules.Search.ViewModels
{
    public class SearchPageViewModel : BindableBase
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly IProductsService _productsService;

        #region Приватные поля

        private ObservableCollection<WbChartProduct> _products;
        private ICollectionView _productsView;

        private bool _productsLoaded;
        private string _searchText;

        private Sorting _selectedSortFilter;
        private Rating _selectedRatingFilter;
        private FeedBack _selectedFeedBackFilter;

        #endregion

        #region Свойства

        public bool ProductsLoaded
        {
            get => _productsLoaded;
            set
            {
                if (value != _productsLoaded)
                {
                    _productsLoaded = value;
                    RaisePropertyChanged(nameof(ProductsLoaded));
                }
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    RaisePropertyChanged(nameof(SearchText));
                }
            }
        }

        public ObservableCollection<WbChartProduct> Products
        {
            get => _products;
            set
            {
                if (value != _products)
                {
                    _products = value;
                    RaisePropertyChanged(nameof(Products));

                    UpdateViewAsync();
                }
            }
        }
        public ICollectionView ProductsView
        {
            get => _productsView;
            set
            {
                if (value != _productsView)
                {
                    _productsView = value;
                    RaisePropertyChanged(nameof(ProductsView));
                }
            }
        }

        public Sorting SelectedSortFilter 
        {
            get => _selectedSortFilter;
            set
            {
                if (value !=  _selectedSortFilter)
                {
                    _selectedSortFilter = value;

                    SortProductsAsync();
                }
            }
        }
        public Rating SelectedRatingFilter
        {
            get => _selectedRatingFilter;
            set
            {
                if (value != _selectedRatingFilter)
                {
                    _selectedRatingFilter = value;
                    ProductsView?.Refresh();
                }
            }
        }
        public FeedBack SelectedFeedBackFilter
        {
            get => _selectedFeedBackFilter;
            set
            {
                if (value != _selectedFeedBackFilter)
                {
                    _selectedFeedBackFilter = value;
                    ProductsView?.Refresh();
                }
            }
        }

        public List<Sorting> Sortings { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<FeedBack> FeedBacks { get; set; }

        #endregion

        public SearchPageViewModel(IDataBaseService dataBaseService, IProductsService productsService)
        {
            _dataBaseService = dataBaseService;
            _productsService = productsService;

            InitializePageAsync();
        }

        private async void InitializePageAsync()
        {
            InitializeStartCatalogAsync();
        }

        public ICommand SearchProductsCommand => new DelegateCommand(SearchProductsAsync);

        private void InitializeFilters()
        {
            Sortings = Sorting.GetSortings();
            Ratings = Rating.GetRatings();
            FeedBacks = FeedBack.GetFeedBacksRatio();

            RaisePropertyChanged(nameof(Sortings));
            RaisePropertyChanged(nameof(Ratings));
            RaisePropertyChanged(nameof(FeedBacks));

            SelectedSortFilter = Sortings.First();
            SelectedRatingFilter = Ratings.First();
            SelectedFeedBackFilter = FeedBacks.First();
        }

        private async void InitializeStartCatalogAsync()
        {
            Products = await ChartService.InitializeWbChartProducts(await _dataBaseService.GetRandomProductsAsync(10));

            ProductsLoaded = true;

            InitializeFilters();
        }

        private async void SortProductsAsync()
        {
            var sorts = new Dictionary<int, ObservableCollection<WbChartProduct>>()
            {
                [0] = Products,
                [1] = new ObservableCollection<WbChartProduct>(Products.OrderBy(product => product.LastPrice?.Price)),
                [2] = new ObservableCollection<WbChartProduct>(Products.OrderByDescending(product => product.ReviewRating)),
                [3] = new ObservableCollection<WbChartProduct>(Products.OrderByDescending(product => product.FeedBacksCount)),
                [4] = new ObservableCollection<WbChartProduct>(WbChartProduct.ConvertToWbChartProducts(_productsService.OrderByMedianPrice(Products.AsEnumerable())))
            };

            await Task.Run(() =>
            {
                ProductsLoaded = false;

                Products = sorts[_selectedSortFilter.Id];

                ProductsLoaded = true;
            });
        }

        private async void UpdateViewAsync()
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ProductsView = CollectionViewSource.GetDefaultView(Products);
                    ProductsView.Filter = FilterProduct;
                });

            });
        }

        private async void SearchProductsAsync()
        {
            if (string.IsNullOrWhiteSpace(_searchText))
            {
                return;
            }

            ProductsLoaded = false;


            _products = await ChartService.InitializeWbChartProducts(await _dataBaseService.GetProductEqualsForNameAsync(_searchText));

            if (_selectedSortFilter.Id != 0)
            {
                SortProductsAsync();
            }

            ProductsLoaded = true;
        }

        private bool FilterProduct(object obj)
        {
            WbProduct product = obj as WbProduct;

            if (SelectedRatingFilter.Id != 0)
            {
                if (product.ReviewRating < SelectedRatingFilter.Id)
                {
                    return false;
                }
            }

            if (SelectedFeedBackFilter.Id != 0)
            {
                if (product.FeedBacksCount < SelectedFeedBackFilter.Count)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
