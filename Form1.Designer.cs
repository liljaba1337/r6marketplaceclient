using r6marketplaceclient.CustomControls;

namespace r6marketplaceclient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutMain = new TableLayoutPanel();
            panelTopNav = new Panel();
            searchBox = new TextBox();
            splitMain = new SplitContainer();
            maxPriceTextBox = new TextBox();
            minPriceTextBox = new TextBox();
            seasonFilterComboBox = new DarkDropdown();
            eventFilterComboBox = new DarkDropdown();
            label5 = new Label();
            typeFilterComboBox = new DarkDropdown();
            rarityFilterComboBox = new DarkDropdown();
            label8 = new Label();
            operatorFilterComboBox = new DarkDropdown();
            label7 = new Label();
            label3 = new Label();
            teamFilterComboBox = new DarkDropdown();
            label4 = new Label();
            label6 = new Label();
            weaponFilterComboBox = new DarkDropdown();
            label2 = new Label();
            label10 = new Label();
            label9 = new Label();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowItems = new FlowLayoutPanel();
            paginationPanel = new FlowLayoutPanel();
            tableLayoutMain.SuspendLayout();
            panelTopNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutMain
            // 
            tableLayoutMain.ColumnCount = 1;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 123F));
            tableLayoutMain.Controls.Add(panelTopNav, 0, 0);
            tableLayoutMain.Controls.Add(splitMain, 0, 1);
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Location = new Point(0, 0);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.RowCount = 3;
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutMain.Size = new Size(812, 478);
            tableLayoutMain.TabIndex = 0;
            // 
            // panelTopNav
            // 
            panelTopNav.BackColor = Color.FromArgb(20, 20, 20);
            panelTopNav.Controls.Add(searchBox);
            panelTopNav.Dock = DockStyle.Fill;
            panelTopNav.Location = new Point(3, 3);
            panelTopNav.Name = "panelTopNav";
            panelTopNav.Size = new Size(806, 29);
            panelTopNav.TabIndex = 0;
            // 
            // searchBox
            // 
            searchBox.BackColor = Color.FromArgb(43, 46, 50);
            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.ForeColor = Color.FromArgb(153, 154, 155);
            searchBox.Location = new Point(3, 3);
            searchBox.Name = "searchBox";
            searchBox.PlaceholderText = "Search";
            searchBox.Size = new Size(651, 23);
            searchBox.TabIndex = 0;
            searchBox.TabStop = false;
            searchBox.KeyPress += searchBox_KeyPress;
            searchBox.Leave += searchBox_Leave;
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.IsSplitterFixed = true;
            splitMain.Location = new Point(3, 38);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.BackColor = Color.FromArgb(13, 13, 13);
            splitMain.Panel1.Controls.Add(maxPriceTextBox);
            splitMain.Panel1.Controls.Add(minPriceTextBox);
            splitMain.Panel1.Controls.Add(seasonFilterComboBox);
            splitMain.Panel1.Controls.Add(eventFilterComboBox);
            splitMain.Panel1.Controls.Add(label5);
            splitMain.Panel1.Controls.Add(typeFilterComboBox);
            splitMain.Panel1.Controls.Add(rarityFilterComboBox);
            splitMain.Panel1.Controls.Add(label8);
            splitMain.Panel1.Controls.Add(operatorFilterComboBox);
            splitMain.Panel1.Controls.Add(label7);
            splitMain.Panel1.Controls.Add(label3);
            splitMain.Panel1.Controls.Add(teamFilterComboBox);
            splitMain.Panel1.Controls.Add(label4);
            splitMain.Panel1.Controls.Add(label6);
            splitMain.Panel1.Controls.Add(weaponFilterComboBox);
            splitMain.Panel1.Controls.Add(label2);
            splitMain.Panel1.Controls.Add(label10);
            splitMain.Panel1.Controls.Add(label9);
            splitMain.Panel1.Controls.Add(label1);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.BackColor = Color.RosyBrown;
            splitMain.Panel2.Controls.Add(tableLayoutPanel1);
            splitMain.Size = new Size(806, 413);
            splitMain.SplitterDistance = 150;
            splitMain.TabIndex = 1;
            splitMain.TabStop = false;
            // 
            // maxPriceTextBox
            // 
            maxPriceTextBox.BackColor = Color.FromArgb(52, 52, 52);
            maxPriceTextBox.BorderStyle = BorderStyle.FixedSingle;
            maxPriceTextBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            maxPriceTextBox.ForeColor = Color.White;
            maxPriceTextBox.Location = new Point(82, 362);
            maxPriceTextBox.Name = "maxPriceTextBox";
            maxPriceTextBox.Size = new Size(65, 23);
            maxPriceTextBox.TabIndex = 2;
            maxPriceTextBox.TabStop = false;
            maxPriceTextBox.Text = "1000000";
            maxPriceTextBox.TextAlign = HorizontalAlignment.Center;
            maxPriceTextBox.Leave += maxPriceTextBox_Leave;
            // 
            // minPriceTextBox
            // 
            minPriceTextBox.BackColor = Color.FromArgb(52, 52, 52);
            minPriceTextBox.BorderStyle = BorderStyle.FixedSingle;
            minPriceTextBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            minPriceTextBox.ForeColor = Color.White;
            minPriceTextBox.Location = new Point(3, 362);
            minPriceTextBox.Name = "minPriceTextBox";
            minPriceTextBox.Size = new Size(57, 23);
            minPriceTextBox.TabIndex = 2;
            minPriceTextBox.TabStop = false;
            minPriceTextBox.Text = "10";
            minPriceTextBox.TextAlign = HorizontalAlignment.Center;
            minPriceTextBox.Leave += minPriceTextBox_Leave;
            // 
            // seasonFilterComboBox
            // 
            seasonFilterComboBox.BackColor = Color.FromArgb(52, 52, 52);
            seasonFilterComboBox.DarkBackColor = Color.FromArgb(30, 30, 30);
            seasonFilterComboBox.DarkForeColor = Color.White;
            seasonFilterComboBox.ForeColor = Color.FromArgb(153, 154, 155);
            seasonFilterComboBox.Location = new Point(3, 178);
            seasonFilterComboBox.Name = "seasonFilterComboBox";
            seasonFilterComboBox.SelectedItemColor = Color.FromArgb(60, 60, 60);
            seasonFilterComboBox.Size = new Size(144, 23);
            seasonFilterComboBox.TabIndex = 1;
            seasonFilterComboBox.TabStop = false;
            seasonFilterComboBox.SelectedIndexChanged += Filter_Changed;
            // 
            // eventFilterComboBox
            // 
            eventFilterComboBox.BackColor = Color.FromArgb(52, 52, 52);
            eventFilterComboBox.DarkBackColor = Color.FromArgb(30, 30, 30);
            eventFilterComboBox.DarkForeColor = Color.White;
            eventFilterComboBox.ForeColor = Color.FromArgb(153, 154, 155);
            eventFilterComboBox.Location = new Point(3, 265);
            eventFilterComboBox.Name = "eventFilterComboBox";
            eventFilterComboBox.SelectedItemColor = Color.FromArgb(60, 60, 60);
            eventFilterComboBox.Size = new Size(144, 23);
            eventFilterComboBox.TabIndex = 1;
            eventFilterComboBox.TabStop = false;
            eventFilterComboBox.SelectedIndexChanged += Filter_Changed;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(153, 154, 155);
            label5.Location = new Point(3, 160);
            label5.Name = "label5";
            label5.Size = new Size(49, 15);
            label5.TabIndex = 0;
            label5.Text = "Season:";
            // 
            // typeFilterComboBox
            // 
            typeFilterComboBox.BackColor = Color.FromArgb(52, 52, 52);
            typeFilterComboBox.DarkBackColor = Color.FromArgb(30, 30, 30);
            typeFilterComboBox.DarkForeColor = Color.White;
            typeFilterComboBox.ForeColor = Color.FromArgb(153, 154, 155);
            typeFilterComboBox.Location = new Point(3, 308);
            typeFilterComboBox.Name = "typeFilterComboBox";
            typeFilterComboBox.SelectedItemColor = Color.FromArgb(60, 60, 60);
            typeFilterComboBox.Size = new Size(144, 23);
            typeFilterComboBox.TabIndex = 1;
            typeFilterComboBox.TabStop = false;
            typeFilterComboBox.SelectedIndexChanged += Filter_Changed;
            // 
            // rarityFilterComboBox
            // 
            rarityFilterComboBox.BackColor = Color.FromArgb(52, 52, 52);
            rarityFilterComboBox.DarkBackColor = Color.FromArgb(30, 30, 30);
            rarityFilterComboBox.DarkForeColor = Color.White;
            rarityFilterComboBox.ForeColor = Color.FromArgb(153, 154, 155);
            rarityFilterComboBox.Location = new Point(3, 92);
            rarityFilterComboBox.Name = "rarityFilterComboBox";
            rarityFilterComboBox.SelectedItemColor = Color.FromArgb(60, 60, 60);
            rarityFilterComboBox.Size = new Size(144, 23);
            rarityFilterComboBox.TabIndex = 1;
            rarityFilterComboBox.TabStop = false;
            rarityFilterComboBox.SelectedIndexChanged += Filter_Changed;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(153, 154, 155);
            label8.Location = new Point(3, 247);
            label8.Name = "label8";
            label8.Size = new Size(42, 15);
            label8.TabIndex = 0;
            label8.Text = "Event:";
            // 
            // operatorFilterComboBox
            // 
            operatorFilterComboBox.BackColor = Color.FromArgb(52, 52, 52);
            operatorFilterComboBox.DarkBackColor = Color.FromArgb(30, 30, 30);
            operatorFilterComboBox.DarkForeColor = Color.White;
            operatorFilterComboBox.ForeColor = Color.FromArgb(153, 154, 155);
            operatorFilterComboBox.Location = new Point(3, 135);
            operatorFilterComboBox.Name = "operatorFilterComboBox";
            operatorFilterComboBox.SelectedItemColor = Color.FromArgb(60, 60, 60);
            operatorFilterComboBox.Size = new Size(144, 23);
            operatorFilterComboBox.TabIndex = 1;
            operatorFilterComboBox.TabStop = false;
            operatorFilterComboBox.SelectedIndexChanged += Filter_Changed;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(153, 154, 155);
            label7.Location = new Point(3, 290);
            label7.Name = "label7";
            label7.Size = new Size(36, 15);
            label7.TabIndex = 0;
            label7.Text = "Type:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(153, 154, 155);
            label3.Location = new Point(3, 74);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 0;
            label3.Text = "Rarity:";
            // 
            // teamFilterComboBox
            // 
            teamFilterComboBox.BackColor = Color.FromArgb(52, 52, 52);
            teamFilterComboBox.DarkBackColor = Color.FromArgb(30, 30, 30);
            teamFilterComboBox.DarkForeColor = Color.White;
            teamFilterComboBox.ForeColor = Color.FromArgb(153, 154, 155);
            teamFilterComboBox.Location = new Point(3, 222);
            teamFilterComboBox.Name = "teamFilterComboBox";
            teamFilterComboBox.SelectedItemColor = Color.FromArgb(60, 60, 60);
            teamFilterComboBox.Size = new Size(144, 23);
            teamFilterComboBox.TabIndex = 1;
            teamFilterComboBox.TabStop = false;
            teamFilterComboBox.SelectedIndexChanged += Filter_Changed;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(153, 154, 155);
            label4.Location = new Point(3, 117);
            label4.Name = "label4";
            label4.Size = new Size(61, 15);
            label4.TabIndex = 0;
            label4.Text = "Operator:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(153, 154, 155);
            label6.Location = new Point(3, 204);
            label6.Name = "label6";
            label6.Size = new Size(82, 15);
            label6.TabIndex = 0;
            label6.Text = "Esports team:";
            // 
            // weaponFilterComboBox
            // 
            weaponFilterComboBox.BackColor = Color.FromArgb(52, 52, 52);
            weaponFilterComboBox.DarkBackColor = Color.FromArgb(30, 30, 30);
            weaponFilterComboBox.DarkForeColor = Color.White;
            weaponFilterComboBox.ForeColor = Color.FromArgb(153, 154, 155);
            weaponFilterComboBox.Location = new Point(3, 49);
            weaponFilterComboBox.Name = "weaponFilterComboBox";
            weaponFilterComboBox.SelectedItemColor = Color.FromArgb(60, 60, 60);
            weaponFilterComboBox.Size = new Size(144, 23);
            weaponFilterComboBox.TabIndex = 1;
            weaponFilterComboBox.TabStop = false;
            weaponFilterComboBox.SelectedIndexChanged += Filter_Changed;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(153, 154, 155);
            label2.Location = new Point(3, 31);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 0;
            label2.Text = "Weapon:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(153, 154, 155);
            label10.Location = new Point(66, 365);
            label10.Name = "label10";
            label10.Size = new Size(12, 15);
            label10.TabIndex = 0;
            label10.Text = "-";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label9.ForeColor = Color.FromArgb(153, 154, 155);
            label9.Location = new Point(43, 334);
            label9.Name = "label9";
            label9.Size = new Size(70, 15);
            label9.TabIndex = 0;
            label9.Text = "Price range";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(153, 154, 155);
            label1.Location = new Point(43, 0);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 0;
            label1.Text = "Filters";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(13, 13, 13);
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(flowItems, 0, 0);
            tableLayoutPanel1.Controls.Add(paginationPanel, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.Size = new Size(652, 413);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // flowItems
            // 
            flowItems.BackColor = Color.FromArgb(13, 13, 13);
            flowItems.Dock = DockStyle.Fill;
            flowItems.Location = new Point(3, 3);
            flowItems.Name = "flowItems";
            flowItems.Size = new Size(646, 365);
            flowItems.TabIndex = 0;
            // 
            // paginationPanel
            // 
            paginationPanel.BackColor = Color.FromArgb(13, 13, 13);
            paginationPanel.Dock = DockStyle.Fill;
            paginationPanel.Location = new Point(3, 374);
            paginationPanel.Name = "paginationPanel";
            paginationPanel.Size = new Size(646, 36);
            paginationPanel.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            ClientSize = new Size(812, 478);
            Controls.Add(tableLayoutMain);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "Form1";
            ShowIcon = false;
            Text = "Rainbow Six Siege Marketplace Client";
            Load += Form1_Load;
            tableLayoutMain.ResumeLayout(false);
            panelTopNav.ResumeLayout(false);
            panelTopNav.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel1.PerformLayout();
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutMain;
        private Panel panelTopNav;
        private SplitContainer splitMain;
        private Label label1;
        private Label label5;
        private Label label8;
        private Label label7;
        private Label label3;
        private Label label4;
        private Label label6;
        private Label label2;
        private Label label10;
        private Label label9;
        internal TextBox searchBox;
        internal DarkDropdown weaponFilterComboBox;
        internal DarkDropdown seasonFilterComboBox;
        internal DarkDropdown eventFilterComboBox;
        internal DarkDropdown typeFilterComboBox;
        internal DarkDropdown rarityFilterComboBox;
        internal DarkDropdown operatorFilterComboBox;
        internal DarkDropdown teamFilterComboBox;
        internal TextBox maxPriceTextBox;
        internal TextBox minPriceTextBox;
        private TableLayoutPanel tableLayoutPanel1;
        internal FlowLayoutPanel flowItems;
        internal FlowLayoutPanel paginationPanel;
    }
}
