using System;
using System.Collections.Generic;
using System.Text;

namespace WildBerriesAnalyzer.PrismCore.Models
{
    public class FeedBack
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }

        public static List<FeedBack> GetFeedBacksRatio()
        {
            return new List<FeedBack>
            {
                new FeedBack() { Id = 0, Name = "Любое" },
                new FeedBack() { Id = 1, Name = "Больше 1", Count = 1 },
                new FeedBack() { Id = 2, Name = "Больше 5", Count = 5 },
                new FeedBack() { Id = 3, Name = "Больше 10", Count = 10 },
                new FeedBack() { Id = 4, Name = "Больше 50", Count = 50 },
                new FeedBack() { Id = 5, Name = "Больше 100", Count = 100 }
            };
        }
    }
}
