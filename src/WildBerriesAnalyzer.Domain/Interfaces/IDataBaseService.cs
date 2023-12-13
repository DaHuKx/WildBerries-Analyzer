using System.Collections.Generic;
using System.Threading.Tasks;
using WildBerriesAnalyzer.Domain.Models.DataBase;

namespace WildBerriesAnalyzer.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс методов работы с базой данных.
    /// </summary>
    public interface IDataBaseService
    {
        /// <summary>
        /// Получение продукта по Id.
        /// </summary>
        /// <param name="productId">Id продукта.</param>
        /// <returns>Продукт с вошедшим Id.</returns>
        Task<WbProduct> GetProductAsync(long productId);
        /// <summary>
        /// Получение всех продуктов.
        /// </summary>
        /// <returns>Набор продуктов.</returns>
        Task<List<WbProduct>> GetProductsAsync();

        /// <summary>
        /// Получение случайного количества продуктов из базы данных.
        /// </summary>
        /// <param name="count">Количество необходимых продуктов.</param>
        /// <returns>Набор продуктов.</returns>
        Task<List<WbProduct>> GetRandomProductsAsync(int count);
        /// <summary>
        /// Добавление продукта.
        /// </summary>
        /// <param name="product">Продукт.</param>
        Task<bool> AddProductAsync(WbProduct product);
        /// <summary>
        /// Добавление набора продуктов.
        /// </summary>
        /// <param name="products">Набор продуктов.</param>
        Task<int> AddProductsAsync(IEnumerable<WbProduct> products);
        /// <summary>
        /// Получает продукты, подходящие по названию к входной строке.
        /// </summary>
        /// <param name="name">Название товара.</param>
        /// <returns>Набор товаров, подходящих по названию.</returns>
        Task<List<WbProduct>> GetProductEqualsForNameAsync(string name);
        /// <summary>
        /// Удаление продукта.
        /// </summary>
        /// <param name="product">Продукт, которые необходимо удалить.</param>
        void RemoveProductAsync(WbProduct product);
        /// <summary>
        /// Получение последней цены продукта.
        /// </summary>
        /// <param name="product">Продукт, для которого нужно получить последнюю цену.</param>
        /// <returns>Последняя цена входного продукта.</returns>
        Task<WbPrice> GetProductLastPriceAsync(WbProduct product);
        /// <summary>
        /// Получает все цены продуктов.
        /// </summary>
        /// <returns></returns>
        Task<List<WbPrice>> GetPricesAsync();
        /// <summary>
        /// Получение истории цен для продукта.
        /// </summary>
        /// <param name="product">Продукт, для которого нужно получить историю цен.</param>
        /// <returns>История цен входного продукта.</returns>
        Task<List<WbPrice>> GetProductPricesHistoryAsync(WbProduct product);
        /// <summary>
        /// Добавление цены продукта в историю.
        /// </summary>
        /// <param name="price">Информация о проверке цены продукта.</param>
        Task<bool> AddProductPriceAsync(WbPrice price);
        /// <summary>
        /// Добавление набора проверок цен продуктов.
        /// </summary>
        /// <param name="prices">Набор проверок.</param>
        Task<int> AddProductsPricesAsync(IEnumerable<WbPrice> prices);
        /// <summary>
        /// Добавляет цену продукта в базу данных.
        /// </summary>
        /// <param name="product">Продукт WildBerries</param>
        void AddPriceFromProductAsync(WbProduct product);
        /// <summary>
        /// Удаление цены продукта.
        /// </summary>
        /// <param name="price">Информация о проверке цены продукта, которую необходимо удалить.</param>
        void RemoveProductPriceAsync(WbPrice price);

        /// <summary>
        /// Получает количество проверок цен из базы данных.
        /// </summary>
        /// <returns></returns>
        Task<long> GetHistoryPricesCountAsync();

        /// <summary>
        /// Получает количество продуктов из базы данных.
        /// </summary>
        /// <returns></returns>
        Task<long> GetProductsCountAsync();
    }
}
