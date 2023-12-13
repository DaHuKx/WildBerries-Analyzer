using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WildBerriesAnalyzer.PrismCore.Models;

namespace WildBerriesAnalyzer.PrismCore.Services
{
    public static class ChartService
    {
        public static SeriesCollection GenerateSeriesCollectionForProduct(WbChartProduct product)
        {
            SeriesCollection seriesCollection = new SeriesCollection()
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(product.PricesHistory.Select(price => price.Price)),
                    Fill = null
                }
            }
        }
    }
}
