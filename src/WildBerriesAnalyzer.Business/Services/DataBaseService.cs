using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WildBerriesAnalyzer.Data;
using WildBerriesAnalyzer.Domain.Interfaces;
using WildBerriesAnalyzer.Domain.Models.DataBase;

namespace WildBerriesAnalyzer.Business.Services
{
    /// <summary>
    /// Сервис работы с базой данных.
    /// </summary>
    public class DataBaseService : IDataBaseService
    {
        private INotifier _notifier;

        public DataBaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<WbProduct> GetProductAsync(long id)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                var product = await dataBase.Products.FirstOrDefaultAsync(p => p.Id == id);

                return product;
            }
        }

        public async Task<List<WbProduct>> GetProductsAsync()
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                return await dataBase.Products.Include(product => product.PricesHistory)
                                              .ToListAsync();
            }
        }

        public async Task<List<WbProduct>> GetRandomProductsAsync(int count)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                if (dataBase.Products.Count() < count)
                {
                    return await dataBase.Products.Include(product => product.PricesHistory)
                                                  .ToListAsync();
                }

                Random random = new Random();

                return await dataBase.Products.Skip(random.Next(1, dataBase.Products.Count() - count))
                                              .Take(count)
                                              .Include(product => product.PricesHistory)
                                              .ToListAsync();
            }
        }

        public async Task<bool> AddProductAsync(WbProduct product)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                dataBase.Products.AddAsync(product);

                try
                {
                    await dataBase.SaveChangesAsync();

                    _notifier.Ok($"{DateTime.Now:g}, DataBaseService: Продукт '{product.Name}' добавлен успешно.");

                    return true;
                }
                catch (Exception ex)
                {
                    _notifier.Error($"{DateTime.Now:g}, DataBaseService: Не удалось добавить продукт '{product.Name} ({product.Id})' - {ex.Message}.");

                    return false;
                }
            }
        }

        public async Task<int> AddProductsAsync(IEnumerable<WbProduct> products)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                var ids = products.Select(product => product.IdInMarket);

                var existProducts = dataBase.Products.Where(product => ids.Contains(product.IdInMarket))
                                                     .Select(product => product.IdInMarket);

                await dataBase.Products.AddRangeAsync(products.Where(product => existProducts.Contains(product.IdInMarket)));

                return products.Count() - existProducts.Count();
            }
        }

        public async Task<List<WbProduct>> GetProductEqualsForNameAsync(string name)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                name = name.ToLower();

                return await dataBase.Products.Where(product => product.Name.ToLower().Contains(name))
                                              .Include(product => product.PricesHistory)
                                              .ToListAsync();
            }
        }

        public async void RemoveProductAsync(WbProduct product)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                dataBase.Products.Remove(product);

                try
                {
                    await dataBase.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _notifier.Error($"{DateTime.Now:g}, DataBaseService: Не удалось удалить продукт '{product.Name} (Id - {product.Id})' - {ex.Message}");
                }
            }
        }

        public async Task<WbPrice?> GetProductLastPriceAsync(WbProduct product)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                if (!dataBase.PricesHistory.Any(price => price.ProductId == product.Id))
                {
                    return new WbPrice()
                    {
                        CheckTime = DateTime.Now,
                        Id = 0,
                        Price = 0,
                        ProductId = product.Id,
                    };
                }

                var price = await dataBase.PricesHistory.Where(price => price.ProductId == product.Id)?
                                                        .OrderBy(price => price.CheckTime)
                                                        .LastAsync();

                if (price == null)
                {
                    _notifier.Warning($"{DateTime.Now:g}, DataBaseService: Продукт '{product.Name} (Id - {product.Id})' не имеет истории цен.");
                }

                return price;
            }
        }

        public async Task<List<WbPrice>> GetPricesAsync()
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                return await dataBase.PricesHistory.ToListAsync();
            }
        }

        public async Task<List<WbPrice>> GetProductPricesHistoryAsync(WbProduct product)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                var historyPrices = await dataBase.PricesHistory.Where(price => price.ProductId == product.Id)
                                                          .ToListAsync();

                if (historyPrices != null && !historyPrices.Any())
                {
                    _notifier.Warning($"{DateTime.Now:g}, DataBaseService: Продукт '{product.Name} (Id - {product.Id})' не имеет истории цен.");
                }

                return historyPrices;
            }
        }

        public async Task<bool> AddProductPriceAsync(WbPrice price)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                try
                {
                    await dataBase.PricesHistory.AddAsync(price);
                    await dataBase.SaveChangesAsync();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<int> AddProductsPricesAsync(IEnumerable<WbPrice> prices)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                await dataBase.AddRangeAsync(prices);
                return await dataBase.SaveChangesAsync();
            }
        }

        public async void AddPriceFromProductAsync(WbProduct product)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                var dbProduct = await dataBase.Products.FirstOrDefaultAsync(prod => prod.IdInMarket == product.IdInMarket);

                if (dbProduct == null)
                {
                    _notifier.Error($"{DateTime.Now}, DataBaseService: Продукт '{product.Name}' не найден в базе данных.");
                    return;
                }

                product.PriceFromInit.ProductId = dbProduct.Id;
                await dataBase.PricesHistory.AddAsync(product.PriceFromInit);
                await dataBase.SaveChangesAsync();
            }
        }

        public async void RemoveProductPriceAsync(WbPrice price)
        {
            using (WbDataBase dataBase = new WbDataBase())
            {
                dataBase.Remove(price);
                await dataBase.SaveChangesAsync();
            }
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


        public async Task<long> GetProductsCountAsync()
        {
            using (var dataBase = new WbDataBase())
            {
                return await dataBase.Products.CountAsync();
            }
        }

        public async Task<long> GetHistoryPricesCountAsync()
        {
            using (var dataBase = new WbDataBase())
            {
                return await dataBase.PricesHistory.CountAsync();
            }
        }
    }
}
