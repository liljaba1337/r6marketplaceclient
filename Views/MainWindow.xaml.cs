using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using r6marketplaceclient.UserControls.MainWindowControls;
using r6marketplaceclient.ViewModels;
using r6marketplaceclient.Views.Utils;

namespace r6marketplaceclient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, UserControl> _contentMap;
        public static MainWindowFooterViewModel FooterViewModel { get; } = new();
        private Button _lastHeaderButton;

        public MainWindow()
        {
            InitializeComponent();
            _contentMap = new Dictionary<string, UserControl>
            {
                { "search", new SearchUserControl(this) },
                { "bookmarks", new BookmarksUserControl() },
                { "inventory", new InventoryUserControl() }
            };

            MainContent.Content = _contentMap["search"];
            _lastHeaderButton = SearchButton;
            DataContext = FooterViewModel;
        }

        private void SwitchInactiveButton(Button button)
        {
            button.IsEnabled = false;
            _lastHeaderButton.IsEnabled = true;
            _lastHeaderButton = button;
        }

        private void HeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: string tag } button && _contentMap.TryGetValue(tag, out var control))
            {
                MainContent.Content = control;
                SwitchInactiveButton(button);
            }
            else
            {
                Debug.WriteLine("Unknown or missing tag");
            }
        }
    }
}