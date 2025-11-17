using r6_marketplace.Classes.Item;
using r6marketplaceclient.Services;
using r6marketplaceclient.UserControls.Common;
using r6marketplaceclient.Utils;
using r6marketplaceclient.ViewModels;
using r6marketplaceclient.Views.Utils;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace r6marketplaceclient.UserControls.MainWindowControls
{
    /// <summary>
    /// Interaction logic for BookmarksUserControl.xaml
    /// </summary>
    public partial class BookmarksUserControl : UserControl
    {
        public ObservableCollection<ItemViewModel> Items { get; private set; } = new();
        private readonly MainWindowBackend backend = new MainWindowBackend();
        public BookmarksUserControl()
        {
            InitializeComponent();
            DataContext = this;
            ItemGrid.SetLoadMoreButtonVisibility(false);
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
        private void ItemGrid_ItemCardMouseClick(object sender, ItemCardMouseEventArgs e)
        {
            backend.ShowEnhancedItemCard(e.Item);
        }
    }
}
