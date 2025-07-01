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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using r6_marketplace.Classes.Item;
using r6marketplaceclient.ViewModels;
using ScottPlot;

namespace r6marketplaceclient.Windows
{
    /// <summary>
    /// Interaction logic for EnhancedItemCard.xaml
    /// </summary>
    public partial class EnhancedItemCard : Window
    {
        private ExtendedItemViewModel? Eitem;
        private readonly ItemViewModel item;
        internal string ItemId => item._item.ID;
        public EnhancedItemCard(ItemViewModel item)
        {
            InitializeComponent();
            Title = item.Name;
            this.item = item;
            InitializePriceTrendGraph();
        }

        private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var history = await ApiClient.GetItemPriceHistory(item._item.ID);
            if (history == null)
            {
                MessageBox.Show("Failed to load item price history.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }
            Eitem = new ExtendedItemViewModel(item._item, history);
            DataContext = Eitem;
            UpdatePriceTrendGraph();
        }
        private void InitializePriceTrendGraph()
        {
            PriceTrendPlot.Plot.FigureBackground.Color = new("#1c1c1e");
            PriceTrendPlot.Plot.Axes.Color(new("#888888"));
            PriceTrendPlot.Plot.Grid.XAxisStyle.FillColor1 = new Color("#888888").WithAlpha(10);
            PriceTrendPlot.Plot.Grid.YAxisStyle.FillColor1 = new Color("#888888").WithAlpha(10);
            PriceTrendPlot.Plot.Grid.XAxisStyle.MajorLineStyle.Color = Colors.White.WithAlpha(15);
            PriceTrendPlot.Plot.Grid.YAxisStyle.MajorLineStyle.Color = Colors.White.WithAlpha(15);
            PriceTrendPlot.Plot.Grid.XAxisStyle.MinorLineStyle.Color = Colors.White.WithAlpha(5);
            PriceTrendPlot.Plot.Grid.YAxisStyle.MinorLineStyle.Color = Colors.White.WithAlpha(5);
            PriceTrendPlot.Plot.Grid.XAxisStyle.MinorLineStyle.Width = 1;
            PriceTrendPlot.Plot.Grid.YAxisStyle.MinorLineStyle.Width = 1;

            PriceTrendPlot.Refresh();
        }
        private void UpdatePriceTrendGraph()
        {
            if (Eitem?._history == null || !Eitem._history.Any()) return;

            double[] times = Eitem._history.Select(p => p.Date.ToOADate()).ToArray();
            int[] prices = Eitem._history.Select(p => p.AveragePrice).ToArray();
            double[] volumes = Eitem._history.Select(p => (double)p.ItemsCount).ToArray();

            var averageLine = PriceTrendPlot.Plot.Add.HorizontalLine(Eitem._history.Average, color: ScottPlot.Color.FromHex("#FF5733"));
            averageLine.LabelStyle.Text = $"Avg: {Eitem.AveragePrice30}";
            averageLine.LabelStyle.ForeColor = ScottPlot.Color.FromHex("#0c0c0d");
            averageLine.LabelStyle.FontSize = 10;
            averageLine.LabelStyle.Rotation = 0;
            averageLine.LabelStyle.Padding = 2;

            var currentLine = PriceTrendPlot.Plot.Add.HorizontalLine(item.Price, color: ScottPlot.Color.FromHex("#00FFB7"));
            currentLine.LabelStyle.Text = $"Now: {item.Price}";
            currentLine.LabelStyle.ForeColor = ScottPlot.Color.FromHex("#0c0c0d");
            currentLine.LabelStyle.FontSize = 10;
            currentLine.LabelStyle.Rotation = 0;
            currentLine.LabelStyle.Padding = 2;

            var scatter = PriceTrendPlot.Plot.Add.Scatter(times, prices);
            scatter.Color = ScottPlot.Color.FromHex("#00FFB7");
            scatter.LineWidth = 2;
            scatter.MarkerSize = 4;


            PriceTrendPlot.Plot.Axes.DateTimeTicksBottom();
            PriceTrendPlot.Plot.Axes.Margins(0, 0);
            PriceTrendPlot.Plot.Axes.AutoScale();

            InitializePriceTrendGraph();

            int maxIndex = Array.IndexOf(prices, prices.Max());
            int minIndex = Array.IndexOf(prices, prices.Min());

            var maxtext = PriceTrendPlot.Plot.Add.Text(
                text: $"↑ {prices[maxIndex]}",
                x: times[maxIndex],
                y: prices[maxIndex] + 1
            );

            var mintext = PriceTrendPlot.Plot.Add.Text(
                text: $"↓ {prices[minIndex]}",
                x: times[minIndex],
                y: prices[minIndex] + 1
            );
            maxtext.LabelFontColor = Color.FromHex("#0b805f");
            mintext.LabelFontColor = Color.FromHex("#99270f");

            maxtext.LabelBackgroundColor = Color.FromHex("#0c0c0d").WithAlpha(150);
            mintext.LabelBackgroundColor = Color.FromHex("#0c0c0d").WithAlpha(150);

            var bars = PriceTrendPlot.Plot.Add.Bars(times, volumes);
            bars.Color = Color.FromHex("#635b5a").WithAlpha(100);

            var yAxisVolume = PriceTrendPlot.Plot.Axes.AddRightAxis();
            yAxisVolume.Color(Color.FromHex("#888888"));
            bars.Axes.YAxis = yAxisVolume;

            PriceTrendPlot.Refresh();
        }

        private async void BuyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
