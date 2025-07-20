using r6_marketplace.Classes.Item;
using r6marketplaceclient.ViewModels;
using r6marketplaceclient.Windows;

namespace r6marketplaceclient
{
    internal class MainPageBackend
    {
        private readonly HashSet<EnhancedItemCard> _visibleCards = new();
        internal void ShowEnhancedItemCard(ItemViewModel item)
        {
            if (_visibleCards.Any(x => x.ItemId == item.Item.ID)) return;
            var window = new EnhancedItemCard(item);
            _visibleCards.Add(window);
            window.Show();
            window.Activate();
            window.Closed += (_, _) => _visibleCards.Remove(window);
        }
        internal void CloseAllWindows()
        {
            foreach (var window in _visibleCards) window.Close();
        }
        internal async Task<List<ItemViewModel>> PerformSearch(List<string> tags, string type, int minPrice,
            int maxPrice, string text, bool onlyStars, int count, int offset)
        {
            List<string> types = new List<string>();
            if (type != "All") types.Add(type);

            MainWindow.FooterViewModel.Balance = await ApiClient.GetBalance();

            List<PurchasableItem> items = await ApiClient.Search(
                text,
                tags,
                types,
                minPrice,
                maxPrice,
                onlyStars,
                count,
                offset
            );
            if (items.Count == 0) return new();
            return items.Select(item => new ItemViewModel(item)).ToList();
        }
    }
}
