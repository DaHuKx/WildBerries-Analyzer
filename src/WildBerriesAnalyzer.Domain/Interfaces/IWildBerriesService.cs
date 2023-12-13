using System.Collections.Generic;
using System.Threading.Tasks;
using WildBerriesAnalyzer.Domain.Models.DataBase;

namespace WildBerriesAnalyzer.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс методов работы с WildBerries.
    /// </summary>
    public interface IWildBerriesService
    {
        /// <summary>
        /// Получение продуктов с WildBerries по названию.
        /// </summary>
        /// <param name="name">Название продукта.</param>
        /// <returns>Набор продуктов, полученных с WildBerries.</returns>
        Task<List<WbProduct>> ParseProductsAsync(string name);
        /// <summary>
        /// Получение цен входного набора продуктов.
        /// </summary>
        /// <param name="products">Набор продуктов.</param>
        /// <returns>Набор цен входных продуктов.</returns>
        Task<List<WbPrice>> ParseProductsPricesAsync(IEnumerable<WbProduct> products);

        /// <summary>
        /// Получение продуктов с WildBerries по артикулу.
        /// </summary>
        /// <param name="ids">Список артикулов</param>
        /// <returns></returns>
        Task<List<WbProduct>> GetProductsForIdAsync(IEnumerable<string> ids);
    }
}
