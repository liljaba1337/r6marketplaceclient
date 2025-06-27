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
        private int offset = 0;
        private readonly List<ComboBox> comboBoxes = new List<ComboBox>();
        private readonly MainPageBackend backend = new MainPageBackend();
        public static MainWindowFooterViewModel FooterViewModel { get; set; } = new MainWindowFooterViewModel();

        #region inotifyproperty stuff
        public static ObservableCollection<PurchasableItemViewModel> Items { get; private set; } = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            SetupComponents();
            ItemStarrer.LoadStarredItems();
            DataContext = this;
        }

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
            LoadMoreButton.Visibility = Visibility.Collapsed;
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
            using var data = SecureStorage.Decrypt();
            if (data != null && !string.IsNullOrEmpty(data.token)) ApiClient.SetToken(data.token);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Window loaded, trying auto-login...");
            TryAutoLogin();
            try
            {
                await PrepareAndPerformSearch();
                Debug.WriteLine("Search performed successfully.");
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error during search: {ex.Message}");

                using SecureStorageFormat? data = SecureStorage.Decrypt();
                if (data != null && !string.IsNullOrEmpty(data.email) && !string.IsNullOrEmpty(data.password))
                {
                    Debug.WriteLine("Attempting to reauthenticate with stored credentials");
                    bool success = await ApiClient.Authenticate(data.email, data.password);
                    if (!success)
                    {
                        Login loginWindow = new Login(this);
                        loginWindow.ShowDialog();
                    }
                    else
                    {
                        await PrepareAndPerformSearch();
                    }
                }
                else
                {
                    Debug.WriteLine("Showing login window.");
                    Login loginWindow = new Login(this);
                    loginWindow.ShowDialog();
                }
            }
        }
        internal async Task PrepareAndPerformSearch(int offset = 0, bool clearItems = true, int count = 40)
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

            bool found = await backend.PerformSearch(
                tags,
                typeFilterComboBox.SelectedItem?.ToString() ?? "All",
                int.TryParse(minPriceTextBox.Text, out int res) ? res : 10,
                int.TryParse(maxPriceTextBox.Text, out int res1) ? res1 : 1000000,
                SearchBox.Text,
                OnlyStarsCheck.IsChecked ?? false,
                count,
                offset,
                clearItems
            );
            NoItemsPlaceholder.Visibility = !found && clearItems ? Visibility.Visible : Visibility.Collapsed;
            LoadMoreButton.Visibility = count == 500 || (found && Items.Count > 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        private async void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem != null)
            {
                Debug.WriteLine($"ComboBox {comboBox.Name} changed to {comboBox.SelectedItem}");
            }
            await PrepareAndPerformSearch(count: OnlyStarsCheck.IsChecked == true ? 500 : 40);
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
            offset = 0;
            await PrepareAndPerformSearch(count: OnlyStarsCheck.IsChecked == true ? 500 : 40);
        }

        private async void LoadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            offset += OnlyStarsCheck.IsChecked == true ? 500 : 40;
            await PrepareAndPerformSearch(offset, false, OnlyStarsCheck.IsChecked == true ? 500 : 40);
        }
    }
}