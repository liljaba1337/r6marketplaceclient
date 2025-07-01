using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using r6marketplaceclient.ViewModels;

namespace r6marketplaceclient.UserControls.Common
{
    /// <summary>
    /// Interaction logic for ItemGridView.xaml
    /// </summary>
    public partial class ItemGridView : UserControl
    {
        public static readonly RoutedEvent OnClickEvent = EventManager.RegisterRoutedEvent(
            "OnClick", RoutingStrategy.Bubble, typeof(EventHandler<ItemClickEventArgs>), typeof(ItemGridView));
        public static readonly RoutedEvent OnLoadMoreClickEvent = EventManager.RegisterRoutedEvent(
            "OnLoadMoreClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ItemGridView));
        public ItemGridView()
        {
            InitializeComponent();
            DataContext = this;
        }
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<ItemViewModel>), typeof(ItemGridView), new PropertyMetadata(null));
        public ObservableCollection<ItemViewModel> Items
        {
            get => (ObservableCollection<ItemViewModel>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }
        public event EventHandler<ItemClickEventArgs> ItemClick
        {
            add { AddHandler(OnClickEvent, value); }
            remove { RemoveHandler(OnClickEvent, value); }
        }
        public event RoutedEventHandler LoadMoreClick
        {
            add { AddHandler(OnLoadMoreClickEvent, value); }
            remove { RemoveHandler(OnLoadMoreClickEvent, value); }
        }

        public static readonly DependencyProperty IsEmptyProperty =
            DependencyProperty.Register("IsEmpty", typeof(Visibility), typeof(ItemGridView), new PropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty IsLoadMoreVisibleProperty =
            DependencyProperty.Register("IsLoadMoreVisible", typeof(Visibility), typeof(ItemGridView), new PropertyMetadata(Visibility.Collapsed));

        public Visibility IsEmpty
        {
            get => (Visibility)GetValue(IsEmptyProperty);
            set => SetValue(IsEmptyProperty, value);
        }
        public Visibility IsLoadMoreVisible
        {
            get => (Visibility)GetValue(IsLoadMoreVisibleProperty);
            set => SetValue(IsLoadMoreVisibleProperty, value);
        }
        private void ItemCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is ItemViewModel item)
            {
                RaiseEvent(new ItemClickEventArgs(OnClickEvent, item));
            }
        }
        private void LoadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OnLoadMoreClickEvent));
        }
    }
    public class ItemClickEventArgs : RoutedEventArgs
    {
        public ItemViewModel ClickedItem { get; }

        public ItemClickEventArgs(RoutedEvent routedEvent, ItemViewModel clickedItem)
            : base(routedEvent)
        {
            ClickedItem = clickedItem;
        }
    }
}
