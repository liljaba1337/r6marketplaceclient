using System.Collections.ObjectModel;
using System.ComponentModel;
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

        internal event EventHandler<ItemCardMouseEventArgs>? ItemCardMouseClick;
        internal event EventHandler? LoadMoreButtonClick;
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
        }

        // item card lmb click handler
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Border { DataContext: ItemViewModel item }) return;
            ItemCardMouseClick?.Invoke(this, new ItemCardMouseEventArgs(item));
        }
        private void LoadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            LoadMoreButtonClick?.Invoke(this, EventArgs.Empty);
        }
        public void SetNoItemsPlaceholderVisibility(bool isVisible)
        {
            NoItemsPlaceholder.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
        }
        public void SetLoadMoreButtonVisibility(bool isVisible)
        {
            LoadMoreButton.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
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
}
