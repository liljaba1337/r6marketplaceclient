using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6marketplaceclient
{
    class ItemStarrer
    {
        private static HashSet<string> StarredItems = new();
        public static bool IsItemStarred(string itemId) => StarredItems.Contains(itemId);
        public static void LoadStarredItems()
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
                var lines = File.ReadAllLines("data/starred.dat").Where(line => line != itemId);
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
