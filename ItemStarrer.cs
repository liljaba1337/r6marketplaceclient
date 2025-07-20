using System.Diagnostics;
using System.IO;

namespace r6marketplaceclient
{
    public static class ItemStarrer
    {
        static ItemStarrer()
        {
            LoadStarredItems();
        }
        public static HashSet<string> StarredItems { get; private set; } = new();
        public static bool IsItemStarred(string itemId) => StarredItems.Contains(itemId);
        private static void LoadStarredItems()
        {
            if (!File.Exists("data/starred.dat"))
            {
                File.Create("data/starred.dat");
                return;
            }
            string[] starredItems = File.ReadAllLines("data/starred.dat");
            StarredItems = new(starredItems);
        }
        private static void StarItem(string itemId)
        {
            bool add = StarredItems.Add(itemId);
            if (add) File.AppendAllText("data/starred.dat", itemId + Environment.NewLine);
            Debug.WriteLine($"Item with ID {itemId} has been starred.");
        }
        private static void UnstarItem(string itemId)
        {
            bool remove = StarredItems.Remove(itemId);
            if (remove)
            {
                IEnumerable<string> lines = File.ReadAllLines("data/starred.dat").Where(line => line != itemId);
                File.WriteAllLines("data/starred.dat", lines);
            }
            Console.WriteLine($"Item with ID {itemId} has been unstarred.");
        }
        
        public static void ToggleStarItem(string itemId, bool isStarred)
        {
            if (isStarred)
            {
                StarItem(itemId);
            }
            else
            {
                UnstarItem(itemId);
            }
        }
    }
}
