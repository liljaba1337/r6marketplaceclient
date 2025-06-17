using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using r6_marketplace.Classes.Item;

namespace r6marketplaceclient.ItemCard
{
    public partial class ItemCardForm : Form
    {
        public ItemCardForm(PurchasableItem item)
        {
            InitializeComponent();
        }

        private void ItemCardForm_Load(object sender, EventArgs e)
        {

        }
    }
}
