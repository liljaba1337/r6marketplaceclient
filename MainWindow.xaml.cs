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

namespace r6marketplaceclient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserControls.SearchUserControl searchUserControl;
        public static MainWindowFooterViewModel FooterViewModel { get; set; } = new MainWindowFooterViewModel();

        public MainWindow()
        {
            InitializeComponent();
            ItemStarrer.LoadStarredItems();
            searchUserControl = new UserControls.SearchUserControl(this);

            MainContent.Content = searchUserControl;
        }
    }
}