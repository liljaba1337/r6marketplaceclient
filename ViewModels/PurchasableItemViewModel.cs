using System.Windows.Media;
using r6_marketplace.Classes.Item;
using r6marketplaceclient.Converters;

namespace r6marketplaceclient.ViewModels
{
    public class ItemViewModel
    {
        internal readonly Item Item;

        public ItemViewModel(Item? item = null)
        {
            if (item == null) throw new NullReferenceException("Item is null");
            Item = item;
            Item.AssetUrl.Width = 200;
        }

        public bool IsStarred
        {
            get => ItemStarrer.IsItemStarred(Item.ID);
            set => ItemStarrer.ToggleStarItem(Item.ID, value);
        }
        public Uri ImageUri => Item.AssetUrl.Value;
        public string Name => Item.Name;
        public Color ShadowColor => CompileTimeRarityColorConverter.Convert(Rarity);
        public int Price
        {
            get => Item.SellOrdersStats?.lowestPrice ?? -1;
            set => Price = value;
        }
        public int Volume => Item.SellOrdersStats?.activeCount ?? -1;
        private double LastSold => Item.LastSoldAtPrice;
        private string Rarity => Item.Tags.FirstOrDefault(x => x.StartsWith("rarity"), "default");
        public double PriceChange
        {
            get => (Price - LastSold) / LastSold * 100; set => PriceChange = value;
        }
    }
    public class ExtendedItemViewModel : ItemViewModel
    {
        internal readonly ItemPriceHistory History;
        private readonly ItemPriceHistory _history7;
        public ExtendedItemViewModel(Item item, ItemPriceHistory history) : base(item)
        {
            History = history;
            _history7 = History.Skip(Math.Max(0, History.Count() - 7));
        }
        public double AveragePrice7 => Math.Round(_history7.Average);
        public double AveragePrice30 => Math.Round(History.Average);
        public double AverageVolume7 => Math.Round(
            _history7.Select(x => x.ItemsCount).ToList().Average());
        public double AverageVolume30 => Math.Round(
            History.Select(x => x.ItemsCount).ToList().Average());
        public int SoldYesterday => History[0].ItemsCount;
    }
}
