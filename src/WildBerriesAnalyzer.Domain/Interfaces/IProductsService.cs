using System;
using System.Collections.Generic;
using System.Text;
using WildBerriesAnalyzer.Domain.Models;
using WildBerriesAnalyzer.Domain.Models.DataBase;

namespace WildBerriesAnalyzer.Domain.Interfaces
{
    public interface IProductsService
    {
        IEnumerable<Discont> GetDiscontsFromProducts(IEnumerable<WbProduct> products);
        double GetMedianPrice(WbProduct product);
        IEnumerable<WbProduct> OrderByMedianPrice(IEnumerable<WbProduct> products);
        IEnumerable<WbProduct> GetProductsEqualsForName(IEnumerable<WbProduct> products, string name);
    }
}
