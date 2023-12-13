using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;
using WildBerriesAnalyzer.Domain.Models.DataBase;
using WildBerriesAnalyzer.PrismCore.Models;

namespace WildBerriesAnalyzer.PrismCore.Services
{
    public static class ChartService
    {
        public static async Task<ObservableCollection<WbChartProduct>> InitializeWbChartProducts(IEnumerable<WbProduct> products)
        {
            var chartProducts = WbChartProduct.ConvertToWbChartProducts(products);

            await Parallel.ForEachAsync(chartProducts, InitializeProductChart);

            return new ObservableCollection<WbChartProduct>(chartProducts);
        }

        public static SeriesCollection GenerateSeriesCollectionForProduct(WbChartProduct product)
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                return new SeriesCollection()
                {
                    new LineSeries
                    {
                        Values = new ChartValues<double>(product.PricesHistory?.Select(price => price.Price)),
                        Fill = null,
                        PointGeometry = null,
                        AllowDrop = false,
                        BitmapEffect = null,
                        Effect = null,
                        RenderTransform = null,
                        Name = "Цена"
                    }
                };
            });
        }

        public static List<string> GenerateLabelsForProduct(WbChartProduct product)
        { 
            return product.PricesHistory.Select(price => price.CheckTime.ToString("g")).ToList();
        }

        private static async ValueTask InitializeProductChart(WbChartProduct product, CancellationToken token)
        {
            await Task.Run(() =>
            {
                product.Chart = GenerateSeriesCollectionForProduct(product);
                product.ChartLabels = GenerateLabelsForProduct(product);
            });
        }
    }
}
