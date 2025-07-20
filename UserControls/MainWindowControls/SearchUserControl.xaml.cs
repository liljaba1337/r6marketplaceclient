using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using r6_marketplace.Utils;
using r6marketplaceclient.ViewModels;
using r6marketplaceclient.Windows;

namespace r6marketplaceclient.UserControls.MainWindowControls
{
    /// <summary>
    /// Interaction logic for SearchUserControl.xaml
    /// </summary>
    public partial class SearchUserControl : INotifyPropertyChanged
    {
        private int _offset;
        private readonly MainWindow _mainWindow;
        private readonly List<ComboBox> _comboBoxes = new();
        private readonly MainPageBackend _backend = new();

        public static ObservableCollection<ItemViewModel> Items { get; } = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        public SearchUserControl(MainWindow window)
        {
            InitializeComponent();
            SetupComponents();
            DataContext = this;
            _mainWindow = window;
        }
        private async void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await PrepareAndPerformSearch();
            }
        }
        private void InitComboBox<TEnum>(ComboBox comboBox) where TEnum : Enum
        {
            var items = Enum.GetNames(typeof(TEnum))
                .Select(SearchTags.GetOriginalName)
                .Order()
                .ToList();

            items.Insert(0, "All");
            comboBox.ItemsSource = items;
            comboBox.SelectedIndex = 0;
            _comboBoxes.Add(comboBox);

            comboBox.SelectionChanged += filterComboBox_SelectionChanged;
        }
        private async void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem != null)
            {
                Debug.WriteLine($"ComboBox {comboBox.Name} changed to {comboBox.SelectedItem}");
            }
            await PrepareAndPerformSearch(count: OnlyStarsCheck.IsChecked == true ? 500 : 40);
        }
        internal async Task PrepareAndPerformSearch(int offset = 0, bool clearItems = true, int count = 40)
        {
            List<string> tags = new List<string>();

            foreach (var comboBox in _comboBoxes)
            {
                if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() != "All"
                    && comboBox.Name != "typeFilterComboBox")
                {
                    tags.Add(SearchTags.GetAPIName(comboBox.SelectedItem.ToString()!));
                }
            }

            var items = await _backend.PerformSearch(
                tags,
                TypeFilterComboBox.SelectedItem?.ToString() ?? "All",
                int.TryParse(MinPriceTextBox.Text, out int res) ? res : 10,
                int.TryParse(MaxPriceTextBox.Text, out int res1) ? res1 : 1000000,
                SearchBox.Text,
                OnlyStarsCheck.IsChecked ?? false,
                count,
                offset
            );
            bool found = items.Count > 0;
            if (clearItems) Items.Clear();
            foreach (var item in items) Items.Add(item);
            NoItemsPlaceholder.Visibility = !found && clearItems ? Visibility.Visible : Visibility.Collapsed;
            LoadMoreButton.Visibility = count == 500 || (found && Items.Count > 0) ? Visibility.Visible : Visibility.Collapsed;
        }
        private void SetupComponents()
        {
            InitComboBox<SearchTags.Weapon>(WeaponFilterComboBox);
            InitComboBox<SearchTags.Operator>(OperatorFilterComboBox);
            InitComboBox<SearchTags.EsportsTeam>(TeamFilterComboBox);
            InitComboBox<SearchTags.Event>(EventFilterComboBox);
            InitComboBox<SearchTags.Rarity>(RarityFilterComboBox);
            InitComboBox<SearchTags.Season>(SeasonFilterComboBox);
            InitComboBox<SearchTags.Type>(TypeFilterComboBox);
            LoadMoreButton.Visibility = Visibility.Collapsed;
        }
        private void TryAutoLogin()
        {
            if (ApiClient.IsAuthenticated) return;
            using var data = SecureStorage.Decrypt();
            if (data != null && !string.IsNullOrEmpty(data.Token)) ApiClient.SetToken(data.Token);
        }
        private void ItemCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border { DataContext: ItemViewModel item })
            {
                _backend.ShowEnhancedItemCard(item);
            }
        }
        private async void OnlyStarsCheck_Click(object sender, RoutedEventArgs e)
        {
            _offset = 0;
            await PrepareAndPerformSearch(count: OnlyStarsCheck.IsChecked == true ? 500 : 40);
        }

        private async void LoadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            _offset += OnlyStarsCheck.IsChecked == true ? 500 : 40;
            await PrepareAndPerformSearch(_offset, false, OnlyStarsCheck.IsChecked == true ? 500 : 40);
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Control loaded, trying auto-login...");
            TryAutoLogin();
            try
            {
                await PrepareAndPerformSearch();
                Debug.WriteLine("Search performed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during search: {ex.Message}");

                using SecureStorageFormat? data = SecureStorage.Decrypt();
                if (data != null && !string.IsNullOrEmpty(data.Email) && !string.IsNullOrEmpty(data.Password))
                {
                    Debug.WriteLine("Attempting to reauthenticate with stored credentials");
                    bool success = await ApiClient.Authenticate(data.Email, data.Password);
                    if (!success)
                    {
                        Login loginWindow = new Login(_mainWindow, this);
                        loginWindow.ShowDialog();
                        loginWindow.Activate();
                    }
                    else
                    {
                        await PrepareAndPerformSearch();
                    }
                }
                else
                {
                    Debug.WriteLine("Showing login window.");
                    Login loginWindow = new Login(_mainWindow, this);
                    loginWindow.ShowDialog();
                    loginWindow.Activate();
                }
            }
        }
    }
}
