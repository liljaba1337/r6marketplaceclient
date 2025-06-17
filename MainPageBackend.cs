using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using r6_marketplace.Classes.Item;
using r6marketplaceclient.CustomControls;
using r6marketplaceclient.ItemCard;

namespace r6marketplaceclient
{
    internal class MainPageBackend
    {
        private readonly ToolTip priceToolTip = new ToolTip();
        private int currentPage = 1;
        private readonly Form1 form;
        private List<PurchasableItem>? items;
        private readonly HashSet<string> visibleCards = new HashSet<string>();
        internal MainPageBackend(Form1 form)
        {
            this.form = form;
        }
        private void ShowEnhancedItemCard(PurchasableItem item)
        {
            if (!visibleCards.Contains(item.ID))
            {
                var itemCard = new ItemCardForm(item);
                itemCard.Show();
                visibleCards.Add(item.ID);
            }
        }
        private void MakeCardClickable(Control card, EventHandler handler)
        {
            card.Click += handler;

            foreach (Control child in card.Controls)
            {
                child.Click += handler;
                MakeCardClickable(child, handler);
            }
        }
        private async Task AddItemCardAvgPrice(PurchasableItem item, FlowLayoutPanel pricePanel)
        {
            var itemHistory = await ApiClient.GetItemPriceHistory(item.ID);

            // 7-day average label
            Label lblAvg7 = new Label();
            lblAvg7.Text = $"{Math.Round(itemHistory!.Reverse().Take(7).Average)}";
            lblAvg7.AutoSize = true;
            lblAvg7.TextAlign = ContentAlignment.MiddleLeft;
            lblAvg7.ForeColor = Color.DarkOrange;
            priceToolTip.SetToolTip(lblAvg7, "7-day average price");

            // 30-day average label
            Label lblAvg30 = new Label();
            lblAvg30.Text = $"{Math.Round(itemHistory.Average)}";
            lblAvg30.AutoSize = true;
            lblAvg30.TextAlign = ContentAlignment.MiddleLeft;
            lblAvg30.ForeColor = Color.DarkOrange;
            priceToolTip.SetToolTip(lblAvg30, "30-day average price");

            pricePanel.Controls.Add(lblAvg7);
            pricePanel.Controls.Add(lblAvg30);
        }
        private async Task<FlowLayoutPanel> AddItemCard(PurchasableItem item)
        {
            // Create the card panel
            Panel card = new Panel();
            card.Width = 150;
            card.Height = 174;
            card.Margin = new Padding(5);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.BackColor = Color.FromArgb(31, 31, 31);

            // Image (PictureBox)
            PictureBox pic = new PictureBox();
            pic.Image = System.Drawing.Image.FromStream(
                await item.AssetUrl.DownloadImageAsStream()
            );
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Dock = DockStyle.Top;
            pic.Height = 100;

            // Item name label
            Label lblName = new Label();
            lblName.Text = item.Name;
            lblName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblName.Dock = DockStyle.Top;
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            lblName.Height = 30;
            lblName.ForeColor = Color.FromArgb(242, 242, 242);

            // Horizontal panel for price labels
            FlowLayoutPanel pricePanel = new FlowLayoutPanel();
            pricePanel.FlowDirection = FlowDirection.LeftToRight;
            pricePanel.Dock = DockStyle.Bottom;
            pricePanel.Height = 30;
            pricePanel.AutoSize = true;
            pricePanel.WrapContents = false;
            pricePanel.Padding = new Padding(0, 0, 0, 5); // optional bottom padding

            // Price label
            Label lblPrice = new Label();
            lblPrice.Text = item.SellOrdersStats?.lowestPrice.ToString();
            lblPrice.AutoSize = true;
            lblPrice.TextAlign = ContentAlignment.MiddleLeft;
            lblPrice.ForeColor = Color.DarkGreen;
            priceToolTip.SetToolTip(lblPrice, "Current price");

            // Add to price panel
            pricePanel.Controls.Add(lblPrice);

            // Add other elements to the card
            card.Controls.Add(pricePanel);   // Add after pic and lblName if needed
            card.Controls.Add(lblName);
            card.Controls.Add(pic);

            // Add card to FlowLayoutPanel
            form.flowItems.Controls.Add(card);

            MakeCardClickable(card, (s, e) => { ShowEnhancedItemCard(item); });
            return pricePanel; // Return the price panel for further customization
        }
        private async Task RenderItemCards(int page = 1)
        {
            currentPage = page;
            form.flowItems.Controls.Clear();
            if (items == null) return;
            UpdatePaginationControls(page);
            List<FlowLayoutPanel> pricePanels = new List<FlowLayoutPanel>();
            foreach (var item in items.Skip((page-1)*8).Take(8))
            {
                pricePanels.Add(await AddItemCard(item));
            }
            for (int i = 0; i < pricePanels.Count; i++)
            {
                await AddItemCardAvgPrice(items.Skip((page - 1) * 8).ElementAt(i), pricePanels[i]);
            }
        }
        private void UpdatePaginationControls(int page)
        {
            form.paginationPanel.Controls[0].Enabled = true; // First
            form.paginationPanel.Controls[1].Enabled = true; // Previous
            form.paginationPanel.Controls[3].Enabled = true; // Next
            form.paginationPanel.Controls[4].Enabled = true; // Last
            if (items == null) return;
            form.paginationPanel.Controls[2].Text = $"Page {page} / {items.Count / 8 + 1}";
            if (page == 1)
            {
                form.paginationPanel.Controls[0].Enabled = false; // First
                form.paginationPanel.Controls[1].Enabled = false; // Previous
            }
            if (page >= items.Count / 8 + 1)
            {
                form.paginationPanel.Controls[3].Enabled = false; // Next
                form.paginationPanel.Controls[4].Enabled = false; // Last
            }
        }
        private void RenderPagination()
        {
            form.paginationPanel.Controls.Clear();

            Button CreatePageButton(string text, Func<Task> asyncAction)
            {
                var btn = new Button
                {
                    Text = text,
                    Width = 40,
                    Height = 30,
                    Margin = new Padding(5),
                    TabStop = false,
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(242, 242, 242),
                    FlatStyle = FlatStyle.Flat
                };
                
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(70, 70, 70);
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.BorderColor = Color.FromArgb(50, 50, 50);

                btn.Click += async (s, e) =>
                {
                    btn.Enabled = false;
                    await asyncAction();
                };

                return btn;
            }

            // First
            form.paginationPanel.Controls.Add(CreatePageButton("<<", () => RenderItemCards(1)));

            // Previous
            form.paginationPanel.Controls.Add(CreatePageButton("<", () => RenderItemCards(currentPage - 1)));

            // Label
            var lbl = new Label
            {
                Text = $"Page {currentPage} / {items!.Count / 8 + 1}",
                AutoSize = true,
                Padding = new Padding(10),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(242, 242, 242)
            };
            form.paginationPanel.Controls.Add(lbl);

            // Next
            form.paginationPanel.Controls.Add(CreatePageButton(">", () => RenderItemCards(currentPage + 1)));

            // Last
            form.paginationPanel.Controls.Add(CreatePageButton(">>", () => RenderItemCards(items.Count / 8 + 1)));

            // Total
            var totalLabel = new Label
            {
                Text = $"Total items: {items.Count}",
                AutoSize = true,
                Padding = new Padding(10),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(242, 242, 242)
            };
            form.paginationPanel.Controls.Add(totalLabel);
        }
        internal async Task PerformSearch()
        {
            List<string> tags = new List<string>();
            List<string> types = new List<string>();
            if (form.weaponFilterComboBox.SelectedItem != "All") tags.Add(form.weaponFilterComboBox.SelectedItem.ToString());
            if (form.rarityFilterComboBox.SelectedItem != "All") tags.Add(form.rarityFilterComboBox.SelectedItem.ToString());
            if (form.operatorFilterComboBox.SelectedItem != "All") tags.Add(form.operatorFilterComboBox.SelectedItem.ToString());
            if (form.seasonFilterComboBox.SelectedItem != "All") tags.Add(form.seasonFilterComboBox.SelectedItem.ToString());
            if (form.teamFilterComboBox.SelectedItem != "All") tags.Add(form.teamFilterComboBox.SelectedItem.ToString());
            if (form.eventFilterComboBox.SelectedItem != "All") tags.Add(form.eventFilterComboBox.SelectedItem.ToString());
            if (form.typeFilterComboBox.SelectedItem != "All") types.Add(form.typeFilterComboBox.SelectedItem.ToString());

            currentPage = 1;
            items = await ApiClient.Search(
                form.searchBox.Text,
                tags,
                types,
                form.minPrice,
                form.maxPrice
            );
            RenderPagination();
            await RenderItemCards(currentPage);
        }
    }
}
