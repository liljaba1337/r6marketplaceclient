using System.Windows;
using r6marketplaceclient.ViewModels;
using ScottPlot;

namespace r6marketplaceclient.Windows
{
    /// <summary>
    /// Interaction logic for EnhancedItemCard.xaml
    /// </summary>
    public partial class EnhancedItemCard
    {
        private ExtendedItemViewModel? _eitem;
        private readonly ItemViewModel _item;
        internal string ItemId => _item.Item.ID;
        public EnhancedItemCard(ItemViewModel item)
        {
            InitializeComponent();
            Title = item.Name;
            _item = item;
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
            var history = await ApiClient.GetItemPriceHistory(_item.Item.ID);
            if (history == null)
            {
                MessageBox.Show("Failed to load item price history.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }
            _eitem = new ExtendedItemViewModel(_item.Item, history);
            DataContext = _eitem;
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
            if (_eitem?.History == null || !_eitem.History.Any()) return;

            double[] times = _eitem.History.Select(p => p.Date.ToOADate()).ToArray();
            int[] prices = _eitem.History.Select(p => p.AveragePrice).ToArray();
            double[] volumes = _eitem.History.Select(p => (double)p.ItemsCount).ToArray();

            var averageLine = PriceTrendPlot.Plot.Add.HorizontalLine(_eitem.History.Average, color: Color.FromHex("#FF5733"));
            averageLine.LabelStyle.Text = $"Avg: {_eitem.AveragePrice30}";
            averageLine.LabelStyle.ForeColor = Color.FromHex("#0c0c0d");
            averageLine.LabelStyle.FontSize = 10;
            averageLine.LabelStyle.Rotation = 0;
            averageLine.LabelStyle.Padding = 2;

            var currentLine = PriceTrendPlot.Plot.Add.HorizontalLine(_item.Price, color: Color.FromHex("#00FFB7"));
            currentLine.LabelStyle.Text = $"Now: {_item.Price}";
            currentLine.LabelStyle.ForeColor = Color.FromHex("#0c0c0d");
            currentLine.LabelStyle.FontSize = 10;
            currentLine.LabelStyle.Rotation = 0;
            currentLine.LabelStyle.Padding = 2;

            var scatter = PriceTrendPlot.Plot.Add.Scatter(times, prices);
            scatter.Color = Color.FromHex("#00FFB7");
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
