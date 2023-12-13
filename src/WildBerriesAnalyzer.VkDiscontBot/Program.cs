using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using WildBerriesAnalyzer.Business.Services;
using WildBerriesAnalyzer.Domain.Models.DataBase;
using WildBerriesAnalyzer.VkDiscontBot;
using WildBerriesAnalyzer.VkDiscontBot.Properties;

FileNotifer notifer = new FileNotifer();

WildBerriesService wildBerriesService = new WildBerriesService(notifer);
DataBaseService dataBaseService = new DataBaseService(notifer);
ProductsService productsService = new ProductsService(notifer);

VkApi vk = new VkApi();