using System;
using System.Collections.Generic;
using System.Text;

namespace WildBerriesAnalyzer.PrismCore.Models
{
    public class Sorting
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<Sorting> GetSortings()
        {
            return new List<Sorting>
            {
                new Sorting() { Id = 0, Name = "Без сортировки" },
                new Sorting() { Id = 1, Name = "По цене" },
                new Sorting() { Id = 2, Name = "По рейтингу" },
                new Sorting() { Id = 3, Name = "По количеству отзывов" },
                new Sorting() { Id = 4, Name = "По медиане цены"}
            };
        }
    }
}
