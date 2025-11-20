using r6_marketplace.Utils;
using r6marketplaceclient.Services;
using r6marketplaceclient.UserControls.Common;
using r6marketplaceclient.ViewModels;
using r6marketplaceclient.Views;
using r6marketplaceclient.Views.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private readonly MainWindowBackend _backend = new();

        public static ObservableCollection<ItemViewModel> Items { get; } = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        public SearchUserControl(MainWindow window)
        {
            InitializeComponent();
            DataContext = this;
            _mainWindow = window;
            ItemGrid.SetLoadMoreButtonVisibility(false);
        }
        internal async Task PrepareAndPerformSearch(int offset = 0, bool clearItems = true, int count = 40)
        {
            Filters filters = FiltersControl.GetAppliedFilters();

            var resultitems = await MainWindowBackend.PerformSearch(
                filters.Tags,
                filters.Type,
                filters.MinPrice,
                filters.MaxPrice,
                filters.TextSearch,
                filters.OnlyStars,
                filters.OnlyStars ? 500 : count,
                offset,
                isInventorySearch: true
            );
            if (clearItems) Items.Clear();
            foreach (var item in resultitems) Items.Add(item);
            ItemGrid.SetNoItemsPlaceholderVisibility(resultitems.Count == 0 && clearItems);
            ItemGrid.SetLoadMoreButtonVisibility(count == 500 || (resultitems.Count > 0 && Items.Count > 0));
        }
        private async void ItemGrid_SearchBoxKeyDown(object sender, Common.SearchBoxKeyDownEventArgs e)
        {
            await PrepareAndPerformSearch();
        }

        private async void ItemGrid_OnlyStarsClick(object sender, Common.OnlyStarsCheckChangedEventArgs e)
        {
            _offset = 0;
            await PrepareAndPerformSearch();
        }

        private void ItemGrid_ItemCardMouseClick(object sender, ItemCardMouseEventArgs e)
        {
            _backend.ShowEnhancedItemCard(e.Item);
        }

        private async void ItemGrid_ComboBoxSelectionChanged(object sender, Common.ComboBoxSelectionChangedEventArgs e)
        {
            await PrepareAndPerformSearch();
        }

        private async void ItemGrid_LoadMoreButtonClick(object sender, EventArgs e)
        {
            _offset += FiltersControl.GetAppliedFilters().OnlyStars ? 500 : 40;
            await PrepareAndPerformSearch(_offset, false);
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Control loaded, trying auto-login...");
            using SecureStorageFormat? data = SecureStorage.Decrypt();
            if (data == null)
            {
                ShowLoginWindow();
                return;
            }
            TryAutoLogin(data);
            try
            {
                await FirstStart();
                Debug.WriteLine("Search performed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during search: {ex.Message}");

                if (!string.IsNullOrEmpty(data.Email) && !string.IsNullOrEmpty(data.Password))
                {
                    Debug.WriteLine("Attempting to reauthenticate with stored credentials");
                    bool success = await ApiClient.Authenticate(data.Email, data.Password);
                    if (!success)
                    {
                        
                    }
                    else
                    {
                        await FirstStart();
                    }
                }
                else
                {
                    Debug.WriteLine("Showing login window.");
                    ShowLoginWindow();
                }
            }
        }

        private async Task FirstStart()
        {
            await PrepareAndPerformSearch();
            await MainWindowBackend.UpdateBalance();
        }
        private void ShowLoginWindow()
        {
            Login loginWindow = new Login(_mainWindow, this);
            loginWindow.ShowDialog();
        }
        private void TryAutoLogin(SecureStorageFormat data)
        {
            if (ApiClient.IsAuthenticated) return;
            if (data != null && !string.IsNullOrEmpty(data.Token)) ApiClient.SetToken(data.Token);
        }
    }
}
