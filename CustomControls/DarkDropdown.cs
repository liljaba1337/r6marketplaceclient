using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6marketplaceclient.CustomControls
{
    public class DarkDropdown : UserControl
    {
        private Label selectedLabel;
        private Button dropdownButton;
        private ListBox listBox;
        private Form dropdownForm;
        bool justClosed = false;

        public event EventHandler SelectedIndexChanged;
        public List<string> Items { get; private set; } = new List<string>();
        public int SelectedIndex { get; private set; } = -1;
        public string SelectedItem => SelectedIndex >= 0 && SelectedIndex < Items.Count ? Items[SelectedIndex] : "";

        public Color DarkBackColor { get; set; } = Color.FromArgb(30, 30, 30);
        public Color DarkForeColor { get; set; } = Color.White;
        public Color SelectedItemColor { get; set; } = Color.FromArgb(60, 60, 60);

        public DarkDropdown()
        {
            this.Height = 30;
            this.Width = 150;
            this.BackColor = DarkBackColor;

            selectedLabel = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 0, 0),
                BackColor = DarkBackColor,
                ForeColor = DarkForeColor
            };

            dropdownButton = new Button
            {
                Dock = DockStyle.Right,
                Width = 30,
                Text = "▼",
                FlatStyle = FlatStyle.Flat,
                BackColor = DarkBackColor,
                ForeColor = DarkForeColor
            };
            dropdownButton.FlatAppearance.BorderSize = 0;

            dropdownButton.Click += (s, e) =>
            {
                if (!dropdownForm.Visible && !justClosed)
                {
                    DropdownButton_Click(s, e);
                }
            };

            this.Controls.Add(selectedLabel);
            this.Controls.Add(dropdownButton);

            CreateDropdownForm();
        }

        private void CreateDropdownForm()
        {
            listBox = new ListBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = DarkBackColor,
                ForeColor = DarkForeColor,
                SelectionMode = SelectionMode.One,
                ItemHeight = 20
            };
            listBox.Click += ListBox_Click;
            listBox.MouseMove += (s, e) => listBox.Focus();

            dropdownForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                BackColor = DarkBackColor
            };
            dropdownForm.Deactivate += (s, e) =>
            {
                // I didn't find a better way to prevent the dropdown from opening when clicking the button to close it
                dropdownForm.Hide();
                justClosed = true;
                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 200; // ms delay
                timer.Tick += (s2, e2) =>
                {
                    justClosed = false;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            };
            dropdownForm.Controls.Add(listBox);
        }

        private void DropdownButton_Click(object sender, EventArgs e)
        {
            if (dropdownForm.Visible)
            {
                dropdownForm.Hide();
                return;
            }

            listBox.Items.Clear();
            foreach (var item in Items)
                listBox.Items.Add(item);

            listBox.Height = Math.Min(Items.Count * listBox.ItemHeight, 200);
            listBox.Width = this.Width;
            dropdownForm.Size = listBox.Size;

            var screenPos = this.PointToScreen(Point.Empty);
            dropdownForm.Location = new Point(screenPos.X, screenPos.Y + this.Height);
            dropdownForm.Show();
            listBox.Focus();
        }

        private void ListBox_Click(object sender, EventArgs e)
        {
            SelectedIndex = listBox.SelectedIndex;
            selectedLabel.Text = SelectedItem;
            dropdownForm.Hide();
            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddItem(string item)
        {
            Items.Add(item);
        }

        public void ClearItems()
        {
            Items.Clear();
            SelectedIndex = -1;
            selectedLabel.Text = "";
        }
    }
}
