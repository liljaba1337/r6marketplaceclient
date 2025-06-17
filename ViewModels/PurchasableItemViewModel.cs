using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Classes.Item;

namespace r6marketplaceclient.ViewModels
{
    public class PurchasableItemViewModel
    {
        private readonly PurchasableItem _item;

        public PurchasableItemViewModel(PurchasableItem item)
        {
            _item = item;
        }

        public Uri ImageUri => _item.AssetUrl.Value;
        public string Name => _item.Name;
        public decimal? Price => _item.SellOrdersStats?.lowestPrice;
        public string ItemId => _item.ID;
    }
}
