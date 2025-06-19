using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using r6_marketplace;
using r6_marketplace.Classes.Item;
using r6marketplaceclient.ViewModels;

namespace r6marketplaceclient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly List<ComboBox> comboBoxes = new List<ComboBox>();
        private readonly MainPageBackend backend = new MainPageBackend();

        #region inotifyproperty stuff
        public static ObservableCollection<PurchasableItemViewModel> Items { get; } = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        private void InitComboBox<TEnum>(ComboBox comboBox) where TEnum : Enum
        {
            var items = Enum.GetNames(typeof(TEnum))
                .Select(r6_marketplace.Utils.SearchTags.GetOriginalName)
                .Order()
                .ToList();

            items.Insert(0, "All");
            comboBox.ItemsSource = items;
            comboBox.SelectedIndex = 0;
            comboBoxes.Add(comboBox);

            comboBox.SelectionChanged += filterComboBox_SelectionChanged;
        }
        private void SetupComponents()
        {
            InitComboBox<r6_marketplace.Utils.SearchTags.Weapon>(weaponFilterComboBox);
            InitComboBox<r6_marketplace.Utils.SearchTags.Operator>(operatorFilterComboBox);
            InitComboBox<r6_marketplace.Utils.SearchTags.EsportsTeam>(teamFilterComboBox);
            InitComboBox<r6_marketplace.Utils.SearchTags.Event>(eventFilterComboBox);
            InitComboBox<r6_marketplace.Utils.SearchTags.Rarity>(rarityFilterComboBox);
            InitComboBox<r6_marketplace.Utils.SearchTags.Season>(seasonFilterComboBox);
            InitComboBox<r6_marketplace.Utils.SearchTags.Type>(typeFilterComboBox);
        }
        public MainWindow()
        {
            InitializeComponent();
            SetupComponents();
            DataContext = this;
        }

        private async void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await PrepareAndPerformSearch();
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await PrepareAndPerformSearch(5);
        }
        private async Task PrepareAndPerformSearch(int count = 500)
        {
            string type = typeFilterComboBox.SelectedItem?.ToString() ?? "All";
            List<string> tags = new List<string>();

            foreach (var comboBox in comboBoxes)
            {
                if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() != "All"
                    && comboBox.Name != "typeFilterComboBox")
                {
                    tags.Add(comboBox.SelectedItem.ToString()!);
                }
            }

            await backend.PerformSearch(new List<string>(), "All", 0, 1000000, string.Empty, count);
        }

        private async void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await PrepareAndPerformSearch();
        }

        private void ItemCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is PurchasableItemViewModel item)
            {
                backend.ShowEnhancedItemCard(item);
            }
        }
    }
}