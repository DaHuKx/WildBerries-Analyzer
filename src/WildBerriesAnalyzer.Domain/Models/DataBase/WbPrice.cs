using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WildBerriesAnalyzer.Domain.Models.DataBase
{
    /// <summary>
    /// Цена продукта
    /// </summary>
    public class WbPrice
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id продукта
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Время фиксирования
        /// </summary>
        public DateTime CheckTime { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public WbProduct Product { get; set; }
    }
}
