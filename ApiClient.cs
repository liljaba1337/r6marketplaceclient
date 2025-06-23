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
        
        static ApiClient()
        {
            InitializeClient();
        }

        private static void InitializeClient(string? token = null)
        {
            client = new R6MarketplaceClient(token: token);
            client.OnTokenRefreshed += (string oldToken, string newToken, DateTime expirationDate) =>
            {
                Debug.WriteLine($"Token refreshed: {oldToken} -> {newToken}, expires at {expirationDate}");
                SecureStorage.Encrypt("token", newToken);
            };
            //client.SetupTokenRefreshing();
        }

        internal static void SetToken(string token) => InitializeClient(token);
        internal static async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                string token = await client.Authenticate(email, password);
                SecureStorage.Encrypt(email, password, token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Authentication failed: {ex.Message}");
                return false;
            }
            return true;
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
            var items = await client.SearchEndpoints.SearchItemUnrestricted(name, types, tags,
                r6_marketplace.Endpoints.SearchEndpoints.SortBy.PurchaseAvailaible,
                r6_marketplace.Endpoints.SearchEndpoints.SortDirection.DESC, limit, offset, r6_marketplace.Utils.Data.Local.en);
            Debug.WriteLine($"Found {items.Count} items matching the search criteria.");
            return items.Where(item => item.LastSoldAtPrice >= minPrice && item.LastSoldAtPrice <= maxPrice).ToList();
        }
        internal static async Task<r6_marketplace.Classes.Item.ItemPriceHistory?> GetItemPriceHistory(string itemId)
        {
            var history = await client.ItemInfoEndpoints.GetItemPriceHistory(itemId);
            return history;
        }
    }
}
