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

namespace r6marketplaceclient
{
    internal class MainPageBackend
    {
        private int currentPage = 1;
        private readonly HashSet<string> visibleCards = new HashSet<string>();
        private void ShowEnhancedItemCard(PurchasableItem item)
        {
            if (!visibleCards.Contains(item.ID))
            {
                //var itemCard = new ItemCardForm(item);
                //itemCard.Show();
                //visibleCards.Add(item.ID);
            }
        }
        internal async Task PerformSearch(List<string> tags, string type, int minPrice, int maxPrice, string text)
        {
            List<string> types = new List<string>();
            if (type != "All") types.Add(type);

            var _items = await ApiClient.Search(
                text,
                tags,
                types,
                minPrice,
                maxPrice
            );
            foreach (var item in _items)
            {
                MainWindow.Items.Add(new PurchasableItemViewModel(item));
            }
        }
    }
}
