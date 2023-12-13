using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WildBerriesAnalyzer.Modules.AddByName.ViewModels;

namespace WildBerriesAnalyzer.Modules.AddByName.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class AddByNamePage : UserControl
    {
        public AddByNamePage()
        {
            InitializeComponent();
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            var sInfo = new System.Diagnostics.ProcessStartInfo(textBlock.Text)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (DataContext as AddByNamePageViewModel).SearchProductsCommand.Execute(null);
            }
        }
    }
}
