using System;
using System.Collections.Generic;
using System.Text;

namespace WildBerriesAnalyzer.PrismCore.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<Rating> GetRatings()
        {
            return new List<Rating>()
            {
                new Rating() { Id = 0, Name = "Любой" },
                new Rating() { Id = 1, Name = "Больше 1"},
                new Rating() { Id = 2, Name = "Больше 2"},
                new Rating() { Id = 3, Name = "Больше 3"},
                new Rating() { Id = 4, Name = "Больше 4"},
                new Rating() { Id = 5, Name = "Только 5"}
            };
        }
    }
}
