using WildBerriesAnalyzer.Business.Services;
using WildBerriesAnalyzer.ConsoleTest;
using WildBerriesAnalyzer.Domain.Models;
using WildBerriesAnalyzer.VkDiscontBot;

WildBerriesService wbService = new WildBerriesService(new ConsoleNotifier());
DataBaseService dbService = new DataBaseService(new ConsoleNotifier());

while (true)
{
    var prods = await dbService.GetProductsAsync();

    foreach (var product in prods)
    {
        await dbService.AddProductPriceAsync((await wbService.ParseProductsPricesAsync(new[] { product })).First());

        Thread.Sleep(500);
    }

    Thread.Sleep(TimeSpan.FromHours(1));
}