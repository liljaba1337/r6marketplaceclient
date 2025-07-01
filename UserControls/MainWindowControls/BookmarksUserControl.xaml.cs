using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using r6_marketplace.Classes.Item;
using r6marketplaceclient.UserControls.Common;
using r6marketplaceclient.ViewModels;
using SkiaSharp;

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
            IEnumerable<string> itemsToLoad = ItemStarrer.StarredItems.Except(Items.Select(i => i._item.ID));
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
