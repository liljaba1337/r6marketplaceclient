using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using r6_marketplace.Utils;
using r6marketplaceclient.ViewModels;
using SkiaSharp;

namespace r6marketplaceclient.UserControls.MainWindowControls;

public partial class InventoryUserControl : UserControl
{
    public ObservableCollection<ItemViewModel> Items { get; } = new();
    private readonly MainPageBackend _backend = new();
    public InventoryUserControl()
    {
        InitializeComponent();
        DataContext = this;
    }
    internal async Task PrepareAndPerformSearch(int offset = 0, int count = 40)
    {
        List<string> tags = new List<string>();

        var items = await _backend.PerformSearch(
            tags,
            "All",
            10,
            1000000,
            "",
            false,
            count,
            offset
        );
        foreach (var item in items) Items.Add(item);
    }
    private void ItemGrid_SearchBoxKeyDown(object sender, Common.SearchBoxKeyDownEventArgs e)
    {

    }

    private void ItemGrid_OnlyStarsClick(object sender, Common.OnlyStarsCheckChangedEventArgs e)
    {

    }

    private void ItemGrid_ItemCardMouseClick(object sender, Common.ItemCardMouseEventArgs e)
    {

    }

    private void ItemGrid_ComboBoxSelectionChanged(object sender, Common.ComboBoxSelectionChangedEventArgs e)
    {

    }

    private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await PrepareAndPerformSearch();
    }
}