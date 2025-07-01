using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using r6_marketplace;
using r6_marketplace.Classes.Item;
using r6_marketplace.Utils;
using r6marketplaceclient.ViewModels;
using r6marketplaceclient.Windows;
using r6marketplaceclient.UserControls.MainWindowControls;

namespace r6marketplaceclient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, UserControl> _contentMap;
        public static MainWindowFooterViewModel FooterViewModel { get; set; } = new MainWindowFooterViewModel();
        private Button lastHeaderButton;

        public MainWindow()
        {
            InitializeComponent();
            ItemStarrer.LoadStarredItems();
            _contentMap = new Dictionary<string, UserControl>
            {
                { "search", new SearchUserControl(this) },
                { "bookmarks", new BookmarksUserControl() }
            };

            MainContent.Content = _contentMap["search"];
            lastHeaderButton = SearchButton;
        }

        private void SwitchInactiveButton(Button button)
        {
            button.IsEnabled = false;
            lastHeaderButton.IsEnabled = true;
            lastHeaderButton = button;
        }

        private void HeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tag && _contentMap.TryGetValue(tag, out var control))
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