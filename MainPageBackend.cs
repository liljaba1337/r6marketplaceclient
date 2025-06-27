using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using r6_marketplace.Classes.Item;
using r6marketplaceclient.ViewModels;
using r6marketplaceclient.Windows;

namespace r6marketplaceclient
{
    internal class MainPageBackend
    {
        private readonly HashSet<string> visibleCards = new HashSet<string>();
        internal void ShowEnhancedItemCard(PurchasableItemViewModel item)
        {
            if (visibleCards.Contains(item._item.ID)) return;
            visibleCards.Add(item._item.ID);
            var window = new EnhancedItemCard(item);
            window.Show();
            window.Closed += (s, e) => visibleCards.Remove(item._item.ID);
        }
        internal async Task<bool> PerformSearch(List<string> tags, string type, int minPrice, int maxPrice, string text, bool onlyStars, int count, int offset, bool clearItems)
        {
            List<string> types = new List<string>();
            if (type != "All") types.Add(type);

            MainWindow.FooterViewModel.Balance = await ApiClient.GetBalance();

            List<PurchasableItem> _items = await ApiClient.Search(
                text,
                tags,
                types,
                minPrice,
                maxPrice,
                onlyStars,
                count,
                offset
            );
            if (clearItems) MainWindow.Items.Clear();
            if (_items.Count == 0) return false;
            foreach (var item in _items)
            {
                MainWindow.Items.Add(new PurchasableItemViewModel(item));
            }
            return true;
        }
    }
}
