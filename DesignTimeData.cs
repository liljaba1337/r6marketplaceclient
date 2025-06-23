using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using r6marketplaceclient.ViewModels;

namespace r6marketplaceclient.DesignTimeData
{
    public class DesignTimePurchasableItemViewModelCollection
    {
        public ObservableCollection<DesignTimePurchasableItemViewModel> Items { get; set; } = new ObservableCollection<DesignTimePurchasableItemViewModel>
        {
            new DesignTimePurchasableItemViewModel(),
            new DesignTimePurchasableItemViewModel() {ImageUri = new Uri("https://ubiservices.cdn.ubi.com/0d2ae42d-4c27-4cb7-af6c-2099062302bb/DeployerAssetsJune2023/02143738_7e78_0d62_f5e3_41458fa33730.png"),
            ShadowColor = Converters.CompileTimeRarityColorConverter.Convert("rarity_legendary"), PriceChange = 234, IsStarred = true}
        };
    }
    public class DesignTimePurchasableItemViewModel
    {
        public DesignTimePurchasableItemViewModel()
        {
        }
        public string Name { get; } = "Design Time Item";
        public Uri ImageUri { get; set; } = new Uri("https://sun9-64.userapi.com/impg/ECQvGgCHxC2UDs_fThPTfkvmV3j65qOWwYQ3Lw/gEIwIhTWS-k.jpg?size=604x583&quality=95&sign=f0ca10aeb8a1ee097419e519458bd497&type=album");
        public bool IsStarred { get; set; } = false;
        public double Price { get; set; } = 19.99;
        public double AveragePrice7 { get; } = 19.99;
        public double AveragePrice30 { get; } = 19.99;
        public double Volume { get; } = 19.99;
        public double AverageVolume7 { get; } = 19.99;
        public double AverageVolume30 { get; } = 19.99;
        public double SoldYesterday { get; } = 19.99;
        public double PriceChange { get; set; } = -19.99;
        public double LastSold { get; } = 19.99;
        public string Rarity { get; } = "rarity_superrare";
        public Color ShadowColor { get; set; } = Color.FromRgb(153, 0, 153); // Purple
    }
}
