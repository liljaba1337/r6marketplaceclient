using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace;

namespace r6marketplaceclient
{
    internal static class ApiClient
    {
        // you can use this one for testing, it's completely empty
        private const string email = "test@sendmeyourfeet.pics";
        private const string password = "Twenty3??";
        private static R6MarketplaceClient client = new R6MarketplaceClient();
        
        private static async Task TryAuth()
        {
            if (!client.isAuthenticated)
            {
                await client.Authenticate(email, password);
            }
        }

        internal static async Task<List<r6_marketplace.Classes.Item.PurchasableItem>> Search(
            string name,
            List<string> tags,
            List<string> types,
            int minPrice = 10,
            int maxPrice = 1000000,
            int limit = 400,
            int offset = 0)
        {
            await TryAuth();
            var items = await client.SearchEndpoints.SearchItemUnrestricted(name, types, tags,
                r6_marketplace.Endpoints.SearchEndpoints.SortBy.PurchaseAvailaible,
                r6_marketplace.Endpoints.SearchEndpoints.SortDirection.DESC, limit, offset, r6_marketplace.Utils.Data.Local.en);
            Debug.WriteLine($"Found {items.Count} items matching the search criteria.");
            return items.Where(item => item.LastSoldAtPrice >= minPrice && item.LastSoldAtPrice <= maxPrice).ToList();
        }
        internal static async Task<r6_marketplace.Classes.Item.ItemPriceHistory?> GetItemPriceHistory(string itemId)
        {
            await TryAuth();
            var history = await client.ItemInfoEndpoints.GetItemPriceHistory(itemId);
            return history;
        }
    }
}
