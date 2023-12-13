using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WildBerriesAnalyzer.PrismCore.Models;

namespace WildBerriesAnalyzer.PrismCore.Components
{
    /// <summary>
    /// Логика взаимодействия для ProductCard.xaml
    /// </summary>
    public partial class ProductCard : UserControl
    {
        public ProductCard()
        {
            InitializeComponent();
        }

        public WbChartProduct Product
        {
            get => (WbChartProduct)GetValue(ProductProperty);
            set => SetValue(ProductProperty, value);
        }

        public static readonly DependencyProperty ProductProperty =
            DependencyProperty.Register("Product", typeof(WbChartProduct), typeof(ProductCard), new PropertyMetadata(default(WbChartProduct), OnProductPropertyChanged));

        public static event PropertyChangedEventHandler? PropertyChanged;

        private static void OnProductPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(dependencyObject, new PropertyChangedEventArgs(e.Property.Name));
            }
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
    }
}
