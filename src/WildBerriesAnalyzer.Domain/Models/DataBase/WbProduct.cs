using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WildBerriesAnalyzer.Domain.Models.DataBase
{
    /// <summary>
    /// Продукт WildBerries
    /// </summary>
    public class WbProduct
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id в магазине WildBerries
        /// </summary>
        public long IdInMarket { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Бренд
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Рейтинг "Wb"
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Рейтинг пользователей
        /// </summary>
        public double ReviewRating { get; set; }

        /// <summary>
        /// Количество отзывов
        /// </summary>
        public int FeedBacksCount { get; set; }

        /// <summary>
        /// Ссылка на изображение
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Ссылка на товар
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// История цен продукта
        /// </summary>
        public List<WbPrice> PricesHistory { get; set; }

        /// <summary>
        /// Цена при инициализации продукта
        /// </summary>
        [NotMapped]
        public WbPrice PriceFromInit { get; set; }

        /// <summary>
        /// Последняя цена продукта
        /// </summary>
        [NotMapped]
        public WbPrice LastPrice => PricesHistory.Any() ? PricesHistory.OrderBy(price => price.CheckTime).Last() 
                                                        : new WbPrice() { CheckTime = DateTime.Now, Price = 0, ProductId = Id };
    }
}
