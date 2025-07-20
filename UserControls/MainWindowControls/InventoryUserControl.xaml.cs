using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using r6_marketplace.Utils;
using r6marketplaceclient.UserControls.Common;
using r6marketplaceclient.ViewModels;
using SkiaSharp;

namespace r6marketplaceclient.UserControls.MainWindowControls;

public partial class InventoryUserControl : UserControl, INotifyPropertyChanged
{
    private int _offset = 0;
    private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
    public ObservableCollection<ItemViewModel> Items
    {
        get => _items;
        set
        {
            _items = value;
            OnPropertyChanged(nameof(Items));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private readonly MainPageBackend _backend = new();
    public InventoryUserControl()
    {
        InitializeComponent();
        DataContext = this;
    }
    private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await PrepareAndPerformSearch();
    }

    internal async Task PrepareAndPerformSearch(int offset = 0, bool clearItems = true, int count = 40)
    {
        Filters filters = FiltersControl.GetAppliedFilters();

        var resultitems = await _backend.PerformSearch(
            filters.Tags,
            filters.Type,
            filters.MinPrice,
            filters.MaxPrice,
            filters.TextSearch,
            filters.OnlyStars,
            filters.OnlyStars ? 500 : count,
            offset,
            isInventorySearch:true
        );
        if (clearItems) Items.Clear();
        foreach (var item in resultitems) Items.Add(item);
        ItemGrid.SetNoItemsPlaceholderVisibility(resultitems.Count == 0 && clearItems);
        ItemGrid.SetLoadMoreButtonVisibility(count == 500 || (resultitems.Count > 0 && Items.Count > 0));
    }
    private async void ItemGrid_SearchBoxKeyDown(object sender, Common.SearchBoxKeyDownEventArgs e)
    {
        await PrepareAndPerformSearch();
    }

    private async void ItemGrid_OnlyStarsClick(object sender, Common.OnlyStarsCheckChangedEventArgs e)
    {
        _offset = 0;
        await PrepareAndPerformSearch();
    }

    private void ItemGrid_ItemCardMouseClick(object sender, ItemCardMouseEventArgs e)
    {
        _backend.ShowEnhancedItemCard(e.Item);
    }

    private async void ItemGrid_ComboBoxSelectionChanged(object sender, Common.ComboBoxSelectionChangedEventArgs e)
    {
        await PrepareAndPerformSearch();
    }

    private async void ItemGrid_LoadMoreButtonClick(object sender, EventArgs e)
    {
        _offset += FiltersControl.GetAppliedFilters().OnlyStars ? 500 : 40;
        await PrepareAndPerformSearch(_offset, false);
    }
}