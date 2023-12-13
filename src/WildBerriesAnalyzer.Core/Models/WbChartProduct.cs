using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBerriesAnalyzer.Domain.Models.DataBase;

namespace WildBerriesAnalyzer.PrismCore.Models
{
    public class WbChartProduct : WbProduct
    {
        public WbChartProduct(WbProduct product) 
        {
            Brand = product.Brand;
            Id = product.Id;
            IdInMarket = product.IdInMarket;
            Name = product.Name;
            Link = product.Link;
            LastPrice = product.LastPrice;
            FeedBacksCount = product.FeedBacksCount;
            PriceFromInit = product.PriceFromInit;
            Rating = product.Rating;
            PricesHistory = product.PricesHistory;
            ReviewRating = product.ReviewRating;
            ImageUrl = product.ImageUrl;
        }

        public SeriesCollection Chart { get; set; }

        public static ObservableCollection<WbChartProduct> ConvertToWbChartProduct(IEnumerable<WbProduct> wbProducts)
        {
            ObservableCollection<WbChartProduct> result = new ObservableCollection<WbChartProduct>();

            foreach (WbProduct wbProduct in wbProducts)
            {
                result.Add(new WbChartProduct(wbProduct));
            }

            return result;
        }
    }
}
