using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WildBerriesAnalyzer.Data;
using WildBerriesAnalyzer.Domain.Interfaces;
using WildBerriesAnalyzer.Domain.Models;
using WildBerriesAnalyzer.Domain.Models.DataBase;

namespace WildBerriesAnalyzer.Business.Services
{
    public class ProductsService : IProductsService
    {
        private INotifier _notifier;

        public ProductsService(INotifier notifier)
        {
            _notifier = notifier;
        }

        public IEnumerable<Discont> GetDiscontsFromProducts(IEnumerable<WbProduct> products)
        {
            List<Discont> disconts = new List<Discont>();

            foreach (var product in products)
            {
                if (product.PricesHistory == null || product.PricesHistory.Count < 2)
                {
                    continue;
                }

                var prevLastPrice = product.PricesHistory[product.PricesHistory.IndexOf(product.LastPrice) - 1];

                if ((product.LastPrice.Price / prevLastPrice.Price) < 0.85)
                {
                    disconts.Add(new Discont()
                    {
                        CurrentPrice = product.LastPrice.Price,
                        CurrentCheckTime = product.LastPrice.CheckTime,
                        LastPrice = prevLastPrice.Price,
                        LastCheckTime = prevLastPrice.CheckTime,
                        Product = product
                    });
                }
            }

            return disconts;
        }

        public IEnumerable<WbProduct> OrderByMedianPrice(IEnumerable<WbProduct> products)
        {
            return products.OrderBy(product => product.PricesHistory.Last().Price / GetMedianPrice(product));
        }

        public double GetMedianPrice(WbProduct product)
        {
            IEnumerable<WbPrice> orderedHistory = product.PricesHistory.OrderBy(price => price.Price);

            return orderedHistory.ToList()[orderedHistory.Count() / 2].Price; 
        }

        public IEnumerable<WbProduct> GetProductsEqualsForName(IEnumerable<WbProduct> products, string name) 
        {
            return products.Where(product => GetTextsEqualsPercent(name, product.Name) > 75);
        }

        private double GetTextsEqualsPercent(string str1, string str2)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                var firstText = str1.Replace('-', ' ').ToLower().Split(' ');
                var secondText = str2.ToLower();

                var diffCount = 0;
                foreach (var word in firstText)
                {
                    if (secondText.Contains(word))
                    {
                        diffCount++;
                    }
                }

                var answer = (double)diffCount / firstText.Length * 100;

                return answer;
            }
        }
    }

}
