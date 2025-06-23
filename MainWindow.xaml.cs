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
using r6_marketplace.Utils;
using r6marketplaceclient.ViewModels;
using r6marketplaceclient.Windows;

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
        public static ObservableCollection<PurchasableItemViewModel> Items { get; private set; } = new();
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
            ItemStarrer.LoadStarredItems();
            DataContext = this;
        }

        private async void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await PrepareAndPerformSearch();
            }
        }

        private void TryAutoLogin()
        {
            var data = SecureStorage.Decrypt();
            if (data != null && !string.IsNullOrEmpty(data.token)) ApiClient.SetToken(data.token);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Window loaded, trying auto-login...");
            TryAutoLogin();
            try
            {
                await PrepareAndPerformSearch(10);
                Debug.WriteLine("Search performed successfully.");
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error during search: {ex.Message}");

                var data = SecureStorage.Decrypt();
                if (data != null && !string.IsNullOrEmpty(data.email) && !string.IsNullOrEmpty(data.password))
                {
                    bool success = await ApiClient.Authenticate(data.email, data.password);
                    if (!success)
                    {
                        Login loginWindow = new Login(this);
                        loginWindow.ShowDialog();
                    }
                    else
                    {
                        await PrepareAndPerformSearch(10);
                    }
                }
                else
                {
                    Login loginWindow = new Login(this);
                    loginWindow.ShowDialog();
                }
            }
        }
        internal async Task PrepareAndPerformSearch(int count = 500)
        {
            List<string> tags = new List<string>();

            foreach (var comboBox in comboBoxes)
            {
                if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() != "All"
                    && comboBox.Name != "typeFilterComboBox")
                {
                    tags.Add(SearchTags.GetAPIName(comboBox.SelectedItem.ToString()!));
                }
            }

            await backend.PerformSearch(
                tags,
                typeFilterComboBox.SelectedItem?.ToString() ?? "All",
                int.TryParse(minPriceTextBox.Text, out int res) ? res : 10,
                int.TryParse(maxPriceTextBox.Text, out int res1) ? res1 : 10,
                SearchBox.Text,
                OnlyStarsCheck.IsChecked ?? false,
                count
            );
        }

        private async void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem != null)
            {
                Debug.WriteLine($"ComboBox {comboBox.Name} changed to {comboBox.SelectedItem}");
            }
            await PrepareAndPerformSearch();
        }

        private void ItemCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is PurchasableItemViewModel item)
            {
                backend.ShowEnhancedItemCard(item);
            }
        }

        private async void OnlyStarsCheck_Click(object sender, RoutedEventArgs e)
        {
            await PrepareAndPerformSearch();
        }
    }
}