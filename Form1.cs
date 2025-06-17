using System.Threading.Tasks;
using r6_marketplace;

namespace r6marketplaceclient
{
    public partial class Form1 : Form
    {
        internal int minPrice = 10;
        internal int maxPrice = 1000000;
        private readonly MainPageBackend backend;
        private void SetupComponents()
        {
            weaponFilterComboBox.Items.AddRange(
                Enum.GetNames(typeof(r6_marketplace.Utils.SearchTags.Weapon))
                    .Select(r6_marketplace.Utils.SearchTags.GetOriginalName)
                    .Order()
                    .ToArray()
            );
            rarityFilterComboBox.Items.AddRange(
                Enum.GetNames(typeof(r6_marketplace.Utils.SearchTags.Rarity))
            );
            operatorFilterComboBox.Items.AddRange(
                Enum.GetNames(typeof(r6_marketplace.Utils.SearchTags.Operator))
                    .Order()
                    .ToArray()
            );
            seasonFilterComboBox.Items.AddRange(
                Enum.GetNames(typeof(r6_marketplace.Utils.SearchTags.Season))
            );
            teamFilterComboBox.Items.AddRange(
                Enum.GetNames(typeof(r6_marketplace.Utils.SearchTags.EsportsTeam))
                    .Select(r6_marketplace.Utils.SearchTags.GetOriginalName)
                    .Order()
                    .ToArray()
            );
            eventFilterComboBox.Items.AddRange(
                Enum.GetNames(typeof(r6_marketplace.Utils.SearchTags.Event))
                    .Order()
                    .ToArray()
            );
            typeFilterComboBox.Items.AddRange(
                Enum.GetNames(typeof(r6_marketplace.Utils.SearchTags.Type))
                    .Order()
                    .ToArray()
            );
        }

        public Form1()
        {
            InitializeComponent();
            backend = new MainPageBackend(this);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            SetupComponents();
            await backend.PerformSearch();
        }

        private async void minPriceTextBox_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(minPriceTextBox.Text, out int price) && price >= 10 && price <= 1000000)
            {
                minPrice = price;
                await backend.PerformSearch();
            }
            else
            {
                minPriceTextBox.Text = minPrice.ToString();
            }
        }

        private async void maxPriceTextBox_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(maxPriceTextBox.Text, out int price) && price >= 10 && price <= 1000000)
            {
                maxPrice = price;
                await backend.PerformSearch();
            }
            else
            {
                maxPriceTextBox.Text = maxPrice.ToString();
            }
        }

        private async void searchBox_Leave(object sender, EventArgs e)
        {
            //await backend.PerformSearch();
        }

        private async void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                await backend.PerformSearch();
            }
        }

        private async void Filter_Changed(object sender, EventArgs e)
        {
            await backend.PerformSearch();
        }
    }
}
