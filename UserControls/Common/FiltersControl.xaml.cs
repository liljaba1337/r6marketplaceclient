using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using r6_marketplace.Utils;
using r6marketplaceclient.ViewModels;

namespace r6marketplaceclient.UserControls.Common
{
    /// <summary>
    /// Interaction logic for FiltersControl.xaml
    /// </summary>
    public partial class FiltersControl : UserControl
    {
        private readonly List<ComboBox> _comboBoxes = new();
        internal event EventHandler<ComboBoxSelectionChangedEventArgs>? ComboBoxSelectionChanged;
        internal event EventHandler<OnlyStarsCheckChangedEventArgs>? OnlyStarsClick;
        internal event EventHandler<SearchBoxKeyDownEventArgs>? SearchBoxKeyDown;
        public FiltersControl()
        {
            InitializeComponent();
            SetupComponents();
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
        }

        private void OnlyStarsCheck_Click(object sender, RoutedEventArgs e)
        {
            OnlyStarsClick?.Invoke(this, new OnlyStarsCheckChangedEventArgs(OnlyStarsCheck.IsChecked ?? false));
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            SearchBoxKeyDown?.Invoke(this, new SearchBoxKeyDownEventArgs(SearchBox.Text));
        }

        // This method of collecting info seems fine for now,
        // since there is only 6 combo boxes it has to iterate through
        //
        // Not so good for scalability, but I can always change it later
        // (not like there will ever be more than 6 combo boxes anyway lmao)
        public Filters GetAppliedFilters()
        {
            List<string> tags = new();

            foreach (var comboBox in _comboBoxes)
            {
                if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() != "All"
                    && comboBox.Name != "typeFilterComboBox")
                {
                    tags.Add(SearchTags.GetAPIName(comboBox.SelectedItem.ToString()!));
                }
            }

            return new Filters(
                tags,
                TypeFilterComboBox.SelectedItem?.ToString() ?? "All",
                SearchBox.Text,
                int.TryParse(MinPriceTextBox.Text, out int minPrice) ? minPrice : 10,
                int.TryParse(MaxPriceTextBox.Text, out int maxPrice) ? maxPrice : 1000000,
                OnlyStarsCheck.IsChecked ?? false
            );
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
    public readonly struct Filters
    {
        public List<string> Tags { get; }
        public string Type { get; }
        public string TextSearch { get; }
        public int MinPrice { get; }
        public int MaxPrice { get; }
        public bool OnlyStars { get; }

        public Filters(
            List<string> tags,
            string type,
            string textSearch,
            int minPrice,
            int maxPrice,
            bool onlyStars)
        {
            Tags = tags;
            Type = type ?? "All";
            TextSearch = textSearch ?? string.Empty;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            OnlyStars = onlyStars;
        }
    }
}
