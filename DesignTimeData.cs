using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6marketplaceclient.ViewModels;

namespace r6marketplaceclient.DesignTimeData
{
    public class DesignTimePurchasableItemViewModel
    {
        public string Name { get; } = "Design Time Item";
        public Uri ImageUri { get; } = new Uri("https://sun9-64.userapi.com/impg/ECQvGgCHxC2UDs_fThPTfkvmV3j65qOWwYQ3Lw/gEIwIhTWS-k.jpg?size=604x583&quality=95&sign=f0ca10aeb8a1ee097419e519458bd497&type=album");
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

    }
}
