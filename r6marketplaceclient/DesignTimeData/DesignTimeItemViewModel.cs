using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using r6marketplaceclient.ViewModels;

namespace r6marketplaceclient.DesignTimeData
{
    public class DesignTimeItemViewModelCollection
    {
        public MainWindowFooterViewModel FooterViewModel { get; set; } = new MainWindowFooterViewModel
        {
            Username = "Design Time User",
            Balance = 80085
        };
        public ObservableCollection<DesignTimeItemViewModel> Items { get; set; } = new ObservableCollection<DesignTimeItemViewModel>
        {
            new DesignTimeItemViewModel(),
            new DesignTimeItemViewModel() {ImageUri = new Uri("https://ubiservices.cdn.ubi.com/0d2ae42d-4c27-4cb7-af6c-2099062302bb/DeployerAssetsJune2023/02143738_7e78_0d62_f5e3_41458fa33730.png"),
            ShadowColor = Converters.CompileTimeRarityColorConverter.Convert("rarity_legendary"), PriceChange = 234, IsStarred = true},
            new DesignTimeItemViewModel(),
            new DesignTimeItemViewModel(),
            new DesignTimeItemViewModel(),
            new DesignTimeItemViewModel(),
            new DesignTimeItemViewModel(),
            new DesignTimeItemViewModel(),
            new DesignTimeItemViewModel(),
            new DesignTimeItemViewModel(),
            new DesignTimeItemViewModel(),
            new DesignTimeItemViewModel()
        };
    }
    public class DesignTimeItemViewModel
    {
        public DesignTimeItemViewModel()
        {
        }
        public string Name { get; } = "DESIGN TIME ITEM";
        public Uri ImageUri { get; init; } = new Uri("https://cdn.sendmeyourfeet.pics/uploads/4a46zq84a6rcl38jjqlz.jpg");
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
