using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace;

namespace r6marketplaceclient
{
    internal static class ApiClient
    {
        private static R6MarketplaceClient client;
        private static readonly string[] settings;
        
        static ApiClient()
        {
            settings = File.ReadAllLines("settings.txt");
            WebProxy proxy = new WebProxy(settings[2]);
            proxy.Credentials = new NetworkCredential(settings[3], settings[4]);
            client = new R6MarketplaceClient(new HttpClient(new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = false,
                AllowAutoRedirect = false
            }), settings[5]);
        }

        private static async Task TryAuth()
        {
            if (!client.isAuthenticated)
            {
                string token = await client.Authenticate(settings[0], settings[1]);
                Debug.WriteLine(token);
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
