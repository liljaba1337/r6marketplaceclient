﻿using System;
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
        private readonly HashSet<EnhancedItemCard> visibleCards = new HashSet<EnhancedItemCard>();
        internal void ShowEnhancedItemCard(ItemViewModel item)
        {
            if (visibleCards.Any(x => x.ItemId == item._item.ID)) return;
            var window = new EnhancedItemCard(item);
            visibleCards.Add(window);
            window.Show();
            window.Activate();
            window.Closed += (s, e) => visibleCards.Remove(window);
        }
        internal void CloseAllWindows()
        {
            foreach (var window in visibleCards)
            {
                window.Close();
            }
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
            if (clearItems) UserControls.MainWindowControls.SearchUserControl.Items.Clear();
            if (_items.Count == 0) return false;
            foreach (var item in _items)
            {
                UserControls.MainWindowControls.SearchUserControl.Items.Add(new ItemViewModel(item));
            }
            return true;
        }
    }
}
