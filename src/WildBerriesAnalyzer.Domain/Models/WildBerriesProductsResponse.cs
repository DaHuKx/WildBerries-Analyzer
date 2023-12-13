using System;
using System.Collections.Generic;
using System.Text;

namespace WildBerriesAnalyzer.Domain.Models
{
    public class WildBerriesProductsResponse
    {
        public int state { get; set; }
        public Params _params { get; set; }
        public Data data { get; set; }
    }

    public class Params
    {
        public int version { get; set; }
        public string curr { get; set; }
        public int spp { get; set; }
    }

    public class Data
    {
        public Product[] products { get; set; }
    }

    public class Product
    {
        public int id { get; set; }
        public int root { get; set; }
        public int kindId { get; set; }
        public int subjectId { get; set; }
        public int subjectParentId { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public int brandId { get; set; }
        public int siteBrandId { get; set; }
        public int supplierId { get; set; }
        public int priceU { get; set; }
        public int salePriceU { get; set; }
        public int logisticsCost { get; set; }
        public int sale { get; set; }
        public Extended extended { get; set; }
        public int saleConditions { get; set; }
        public int pics { get; set; }
        public int rating { get; set; }
        public float reviewRating { get; set; }
        public int feedbacks { get; set; }
        public int volume { get; set; }
        public object[] colors { get; set; }
        public int[] promotions { get; set; }
        public Size[] sizes { get; set; }
        public bool diffPrice { get; set; }
        public int time1 { get; set; }
        public int time2 { get; set; }
        public int wh { get; set; }
    }

    public class Extended
    {
        public int basicSale { get; set; }
        public int basicPriceU { get; set; }
        public int? clientPriceU { get; set; }
    }

    public class Size
    {
        public string name { get; set; }
        public string origName { get; set; }
        public int rank { get; set; }
        public int optionId { get; set; }
        public Stock[] stocks { get; set; }
        public int time1 { get; set; }
        public int time2 { get; set; }
        public int wh { get; set; }
        public string sign { get; set; }
    }

    public class Stock
    {
        public int wh { get; set; }
        public int qty { get; set; }
        public int time1 { get; set; }
        public int time2 { get; set; }
    }


}
