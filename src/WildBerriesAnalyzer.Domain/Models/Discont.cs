using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WildBerriesAnalyzer.Domain.Models.DataBase;

namespace WildBerriesAnalyzer.Domain.Models
{
    /// <summary>
    /// Скидка
    /// </summary>
    public class Discont
    {

        /// <summary>
        /// Текущая цена
        /// </summary>
        public double CurrentPrice { get; set; }

        /// <summary>
        /// Время фиксации текущей цены
        /// </summary>
        public DateTime CurrentCheckTime { get; set; }

        /// <summary>
        /// Последняя цена до текущей
        /// </summary>
        public double LastPrice { get; set; }

        /// <summary>
        /// Время фиксации последней до текущей цены
        /// </summary>
        public DateTime LastCheckTime { get; set; }

        /// <summary>
        /// Размер скидки в %
        /// </summary>
        [NotMapped]
        public double DiscontPercent => (1 - (CurrentPrice / LastPrice)) * 100;

        /// <summary>
        /// Продукт
        /// </summary>
        public WbProduct Product { get; set; }
    }
}
