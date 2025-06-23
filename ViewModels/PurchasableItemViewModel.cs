using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using r6_marketplace.Classes.Item;
using r6marketplaceclient.Converters;

namespace r6marketplaceclient.ViewModels
{
    public class PurchasableItemViewModel
    {
        internal readonly PurchasableItem _item;

        public PurchasableItemViewModel(PurchasableItem? item = null)
        {
            _item = item!;
            _item.AssetUrl.Width = 200;
        }

        public bool IsStarred { get => false; set { } } // This should be bound to a property in the main view model
        public Uri ImageUri => _item.AssetUrl.Value;
        public string Name => _item.Name;
        public Color ShadowColor => CompileTimeRarityColorConverter.Convert(Rarity);
        public int Price
        {
            get => _item.SellOrdersStats?.lowestPrice ?? -1; set => Price = value;
        }
        public int Volume => _item.SellOrdersStats?.activeCount ?? -1;
        public double LastSold => _item.LastSoldAtPrice;
        public string Rarity => _item.Tags.FirstOrDefault(x => x.StartsWith("rarity"), "default");
        public double PriceChange
        {
            get => (Price - LastSold) / LastSold * 100; set => PriceChange = value;
        }
    }
    public class ExtendedPurchasableItemViewModel : PurchasableItemViewModel
    {
        internal readonly ItemPriceHistory _history;
        internal readonly ItemPriceHistory _history7;
        public ExtendedPurchasableItemViewModel(PurchasableItem item, ItemPriceHistory history) : base(item)
        {
            _history = history;
            _history7 = _history.Skip(Math.Max(0, _history.Count() - 7));
        }
        public double AveragePrice7 => Math.Round(_history7.Average);
        public double AveragePrice30 => Math.Round(_history.Average);
        public double AverageVolume7 => Math.Round(
            _history7.Select(x => x.ItemsCount).ToList().Average());
        public double AverageVolume30 => Math.Round(
            _history.Select(x => x.ItemsCount).ToList().Average());
        public int SoldYesterday => _history[0].ItemsCount;
    }
}
