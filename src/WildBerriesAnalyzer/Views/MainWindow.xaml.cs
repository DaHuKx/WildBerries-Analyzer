using Prism.Regions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WildBerriesAnalyzer.Core;
using WildBerriesAnalyzer.Modules.ActualDisconts.Views;
using WildBerriesAnalyzer.Modules.AddById.Views;
using WildBerriesAnalyzer.Modules.AddByName.Views;
using WildBerriesAnalyzer.Modules.Disconts.Views;
using WildBerriesAnalyzer.Modules.Search;
using WildBerriesAnalyzer.Modules.Search.Views;
using WildBerriesAnalyzer.Modules.StartPage.Views;
using WildBerriesAnalyzer.ViewModels;

namespace WildBerriesAnalyzer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            

            InitializeComponent();
        }

        private void AddProductsButtonClick(object sender, RoutedEventArgs e)
        {
            Transition(typeof(AddByNamePage).FullName);
            LocationTextBlock.Text = "Добавление продуктов по названию";
        }

        private void SearchProductsButtonClick(object sender, RoutedEventArgs e)
        {
            Transition(typeof(SearchPage).FullName);
            LocationTextBlock.Text = "Поиск товара";
        }

        private void DiscontsButtonClick(object sender, RoutedEventArgs e)
        {
            Transition(typeof(DiscontsPage).FullName);
            LocationTextBlock.Text = "Лучшие скидки";
        }

        private void ActualDiscontsButtonClick(object sender, RoutedEventArgs e)
        {
            Transition(typeof(ActualDiscontsPage).FullName);
            LocationTextBlock.Text = "Акутальные скидки";
        }

        private void AddByIdButtonClick(object sender, RoutedEventArgs e)
        {
            Transition(typeof(AddByIdPage).FullName);
            LocationTextBlock.Text = "Добавление товара по артикулу";
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Transition(typeof(StartPage).FullName);
            LocationTextBlock.Text = "Главное меню";
        }

        private void Transition(string moduleName)
        {
            (DataContext as MainWindowViewModel).TransitionAnimationAsync(moduleName);
        }
    }
}
