using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using r6_marketplace.Classes.Item;
using r6marketplaceclient.ViewModels;

namespace r6marketplaceclient.UserControls.MainWindowControls
{
    /// <summary>
    /// Interaction logic for BookmarksUserControl.xaml
    /// </summary>
    public partial class BookmarksUserControl : UserControl
    {
        public ObservableCollection<ItemViewModel> Items { get; private set; } = new();
        private readonly MainPageBackend backend = new MainPageBackend();
        public BookmarksUserControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadStarredItems();
        }

        private async Task LoadStarredItems()
        {
            IEnumerable<string> itemsToLoad = ItemStarrer.StarredItems.Except(Items.Select(i => i.Item.ID));
            Debug.WriteLine($"Loading {itemsToLoad.Count()} items");
            foreach (var item in itemsToLoad)
            {
                Item? itemData = await ApiClient.GetItemById(item);
                if (itemData != null) Items.Add(new ItemViewModel(itemData));
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is ItemViewModel item)
            {
                backend.ShowEnhancedItemCard(item);
            }
        }
    }
}
