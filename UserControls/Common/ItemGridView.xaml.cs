using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using r6_marketplace.Utils;
using r6marketplaceclient.ViewModels;

namespace r6marketplaceclient.UserControls.Common
{
    /// <summary>
    /// Interaction logic for ItemGridView.xaml
    /// </summary>
    public partial class ItemGridView
    {
        private readonly List<ComboBox> _comboBoxes = new();
        internal event EventHandler<ComboBoxSelectionChangedEventArgs>? ComboBoxSelectionChanged;
        internal event EventHandler<ItemCardMouseEventArgs>? ItemCardMouseClick;
        internal event EventHandler? LoadMoreButtonClick;
        internal event EventHandler<OnlyStarsCheckChangedEventArgs>? OnlyStarsClick;
        internal event EventHandler<SearchBoxKeyDownEventArgs>? SearchBoxKeyDown;

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            nameof(Items),
            typeof(ObservableCollection<ItemViewModel>),
            typeof(ItemGridView),
            new PropertyMetadata(new ObservableCollection<ItemViewModel>()));

        public ObservableCollection<ItemViewModel> Items
        {
            get => (ObservableCollection<ItemViewModel>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }
        public ItemGridView()
        {
            InitializeComponent();
            SetupComponents();
            DataContext = this;
        }
        private void InitComboBox<TEnum>(ComboBox comboBox) where TEnum : Enum
        {
            var items = Enum.GetNames(typeof(TEnum))
                .Select(SearchTags.GetOriginalName)
                .Order()
                .ToList();

            items.Insert(0, "All");
            comboBox.ItemsSource = items;
            comboBox.SelectedIndex = 0;
            _comboBoxes.Add(comboBox);

            comboBox.SelectionChanged += filterComboBox_SelectionChanged;
        }
        private void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem != null)
            {
                Debug.WriteLine($"ComboBox {comboBox.Name} changed to {comboBox.SelectedItem}");

                ComboBoxSelectionChanged?.Invoke(this, new ComboBoxSelectionChangedEventArgs(comboBox, comboBox.SelectedItem));
            }
        }
        private void SetupComponents()
        {
            InitComboBox<SearchTags.Weapon>(WeaponFilterComboBox);
            InitComboBox<SearchTags.Operator>(OperatorFilterComboBox);
            InitComboBox<SearchTags.EsportsTeam>(TeamFilterComboBox);
            InitComboBox<SearchTags.Event>(EventFilterComboBox);
            InitComboBox<SearchTags.Rarity>(RarityFilterComboBox);
            InitComboBox<SearchTags.Season>(SeasonFilterComboBox);
            InitComboBox<SearchTags.Type>(TypeFilterComboBox);
            LoadMoreButton.Visibility = Visibility.Collapsed;
        }

        // item card lmb click handler
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is not Border { DataContext: ItemViewModel item }) return;
            ItemCardMouseClick?.Invoke(this, new ItemCardMouseEventArgs(item));
        }

        private void LoadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            LoadMoreButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void OnlyStarsCheck_Click(object sender, RoutedEventArgs e)
        {
            OnlyStarsClick?.Invoke(this, new OnlyStarsCheckChangedEventArgs(OnlyStarsCheck.IsChecked ?? false));
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            SearchBoxKeyDown?.Invoke(this, new SearchBoxKeyDownEventArgs(SearchBox.Text));
        }
    }
    public class ComboBoxSelectionChangedEventArgs : EventArgs
    {
        public ComboBox ComboBox { get; }
        public object SelectedItem { get; }

        public ComboBoxSelectionChangedEventArgs(ComboBox comboBox, object selectedItem)
        {
            ComboBox = comboBox;
            SelectedItem = selectedItem;
        }
    }
    public class ItemCardMouseEventArgs : EventArgs
    {
        public ItemViewModel Item { get; }
        public ItemCardMouseEventArgs(ItemViewModel item)
        {
            Item = item;
        }
    }
    public class OnlyStarsCheckChangedEventArgs : EventArgs
    {
        public bool IsChecked { get; }
        public OnlyStarsCheckChangedEventArgs(bool isChecked)
        {
            IsChecked = isChecked;
        }
    }
    public class SearchBoxKeyDownEventArgs : EventArgs
    {
        public string Text { get; }
        public SearchBoxKeyDownEventArgs(string text)
        {
            Text = text;
        }
    }
}
