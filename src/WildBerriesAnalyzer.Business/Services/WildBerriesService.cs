using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WildBerriesAnalyzer.Domain.Interfaces;
using WildBerriesAnalyzer.Domain.Models;
using WildBerriesAnalyzer.Domain.Models.DataBase;

namespace WildBerriesAnalyzer.Business.Services
{
    /// <summary>
    /// Сервис взаимодействия с WildBerries.
    /// </summary>
    public class WildBerriesService : IWildBerriesService
    {
        private INotifier _notifier;

        public WildBerriesService(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<List<WbProduct>> ParseProductsAsync(string name)
        {
            string link = $"https://search.wb.ru/exactmatch/ru/common/v4/search?curr=rub&dest=-1257786&query={WebUtility.UrlEncode(name)}&resultset=catalog&sort=popular";

            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.All;

            HttpClient client = new HttpClient(handler);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, link);
            InitializeRequestHeaders(ref request);

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            WildBerriesProductsResponse desserializedWbResponse = JsonConvert.DeserializeObject<WildBerriesProductsResponse>(responseBody);

            _notifier.Ok($"{DateTime.Now:g}, WildBerriesService: {desserializedWbResponse.data?.products.Length} продуктов получено.");

            return InitializeProductsFromResponse(desserializedWbResponse);
        }

        
        public async Task<List<WbPrice>> ParseProductsPricesAsync(IEnumerable<WbProduct> products)
        {
            List<WbPrice> prices = new List<WbPrice>();

            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.All;

            HttpClient client = new HttpClient(handler);

            foreach (WbProduct product in products)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Empty);
                InitializeRequestHeaders(ref request);

                request.RequestUri = new Uri($"https://card.wb.ru/cards/detail?curr=rub&dest=-1257786&regions=80,64,38,4,115,83,33,68,70,69,30,86,75,40,1,66,48,110,31,22,71,114&spp=0&nm={product.IdInMarket}");

                HttpResponseMessage response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                WildBerriesProductsResponse desserializedWbResponse = JsonConvert.DeserializeObject<WildBerriesProductsResponse>(responseBody);

                //Продукт всегда приходит один.
                if (desserializedWbResponse.data.products.First().sizes != null &&
                    desserializedWbResponse.data.products.First().sizes.First().stocks != null)
                {
                    WbProduct initializedProduct = InitializeProductsFromResponse(desserializedWbResponse).First();

                    initializedProduct.PriceFromInit.ProductId = product.Id;

                    _notifier.Ok($"{DateTime.Now:g}, WildBerriesService: Получена цена продукта '{product.Name} ({product.Id})' - {initializedProduct.PriceFromInit.Price}руб.");

                    prices.Add(initializedProduct.PriceFromInit);
                }
                else
                {
                    _notifier.Warning($"{DateTime.Now:g}, WildBerriesService: Продукт '{product.Name} ({product.Id})' не в наличии.");
                }
            }

            return prices;
        }

        /// <summary>
        /// Добавление необходимых заголовков для запроса.
        /// </summary>
        /// <param name="request">Запрос.</param>
        private void InitializeRequestHeaders(ref HttpRequestMessage request)
        {
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            request.Headers.Add("Cache-Control", "max-age=0");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Sec-Fetch-Dest", "document");
            request.Headers.Add("Sec-Fetch-Mode", "navigate");
            request.Headers.Add("Sec-Fetch-Site", "none");
            request.Headers.Add("Sec-Fetch-User", "?1");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36 OPR/99.0.0.0 (Edition Yx GX)");
            request.Headers.Add("sec-ch-ua", "\"Opera GX\";v=\"99\", \"Chromium\";v=\"113\", \"Not-A.Brand\";v=\"24\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
        }

        /// <summary>
        /// Инициализация продуктов из ответа WildBerries.
        /// </summary>
        /// <param name="response">Ответ WildBerries.</param>
        /// <returns>Список продуктов.</returns>
        private List<WbProduct> InitializeProductsFromResponse(WildBerriesProductsResponse response)
        {
            List<WbProduct> products = new List<WbProduct>();

            if (response.data == null)
            {
                return products;
            }

            foreach (var product in response.data.products)
            {
                string productId = product.id.ToString();

                products.Add(new WbProduct()
                {
                    IdInMarket = product.id,
                    Brand = product.brand,
                    FeedBacksCount = product.feedbacks,
                    Link = $"https://www.wildberries.ru/catalog/{productId}/detail.aspx",
                    Name = product.name,
                    Rating = product.rating,
                    ReviewRating = product.reviewRating,
                    ImageUrl = $"https://basket-10.wb.ru/vol{string.Concat(productId.Take(4))}/part{string.Concat(productId.Take(6))}/{productId}/images/big/1.jpg",
                    PriceFromInit = new WbPrice()
                    {
                        CheckTime = DateTime.Now,
                        Price = product.extended?.clientPriceU != null ? (double)product.extended?.clientPriceU / 100 : product.salePriceU / 100
                    }
                });
            }

            return products;
        }

        public async Task<List<WbProduct>> GetProductsForIdAsync(IEnumerable<string> ids)
        {
            List<WbProduct> products = new List<WbProduct>();

            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.All;

            HttpClient client = new HttpClient(handler);

            foreach (string id in ids)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Empty);
                InitializeRequestHeaders(ref request);

                request.RequestUri = new Uri($"https://card.wb.ru/cards/detail?curr=rub&dest=-1257786&regions=80,64,38,4,115,83,33,68,70,69,30,86,75,40,1,66,48,110,31,22,71,114&spp=0&nm={id}");

                HttpResponseMessage response = await client.SendAsync(request);
                try
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = Task.Run(() => response.Content.ReadAsStringAsync()).Result;

                    WildBerriesProductsResponse desserializedWbResponse = JsonConvert.DeserializeObject<WildBerriesProductsResponse>(responseBody);

                    WbProduct initializedProduct = InitializeProductsFromResponse(desserializedWbResponse).First();

                    _notifier.Ok($"{DateTime.Now:g}, WildBerriesService: Получен продукт '{initializedProduct.Name} ({initializedProduct.Id})' - {initializedProduct.PriceFromInit.Price}руб.");

                    products.Add(initializedProduct);
                }
                catch
                {
                    _notifier.Warning($"{DateTime.Now:g}, WildBerriesService: Не удалось получить продукт с Id '{id}'.");
                }
            }

            return products;
        }
    }
}
