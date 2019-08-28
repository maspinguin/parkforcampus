namespace ParkirClientWindows
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.labelArduino2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.labelArduino1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelServerAddress = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonPegawai_tambah = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPegawai_search = new System.Windows.Forms.TextBox();
            this.buttonPegawai_search = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.labelPegawai_activePage = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelPegawai_totalPage = new System.Windows.Forms.Label();
            this.buttonPegawai_previous = new System.Windows.Forms.Button();
            this.buttonPegawai_next = new System.Windows.Forms.Button();
            this.dataGridViewPegawai = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonMahasiswa_tambah = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxMahasiswa_search = new System.Windows.Forms.TextBox();
            this.buttonMahasiswa_search = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.labelMahasiswa_activePage = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelMahasiswa_totalPage = new System.Windows.Forms.Label();
            this.buttonMahasiswa_previous = new System.Windows.Forms.Button();
            this.buttonMahasiswa_next = new System.Windows.Forms.Button();
            this.dataGridViewMahasiswa = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPegawai)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMahasiswa)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1185, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1185, 509);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 477);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1179, 29);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label4);
            this.flowLayoutPanel3.Controls.Add(this.labelArduino2);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(789, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(387, 23);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Arduino 2:";
            // 
            // labelArduino2
            // 
            this.labelArduino2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelArduino2.AutoSize = true;
            this.labelArduino2.Location = new System.Drawing.Point(82, 0);
            this.labelArduino2.Name = "labelArduino2";
            this.labelArduino2.Size = new System.Drawing.Size(94, 17);
            this.labelArduino2.TabIndex = 2;
            this.labelArduino2.Text = "Port 0 Com 0 ";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.labelArduino1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(396, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(387, 23);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Arduino 1:";
            // 
            // labelArduino1
            // 
            this.labelArduino1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelArduino1.AutoSize = true;
            this.labelArduino1.Location = new System.Drawing.Point(82, 0);
            this.labelArduino1.Name = "labelArduino1";
            this.labelArduino1.Size = new System.Drawing.Size(94, 17);
            this.labelArduino1.TabIndex = 2;
            this.labelArduino1.Text = "Port 0 Com 0 ";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.labelServerAddress);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(387, 23);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server:";
            // 
            // labelServerAddress
            // 
            this.labelServerAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelServerAddress.AutoSize = true;
            this.labelServerAddress.Location = new System.Drawing.Point(63, 0);
            this.labelServerAddress.Name = "labelServerAddress";
            this.labelServerAddress.Size = new System.Drawing.Size(106, 17);
            this.labelServerAddress.TabIndex = 2;
            this.labelServerAddress.Text = "Server Address";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1179, 468);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1171, 439);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data Parkir";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1171, 439);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Data Pengguna";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1171, 439);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Data Pegawai";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dataGridViewPegawai, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1165, 433);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.buttonPegawai_tambah);
            this.flowLayoutPanel4.Controls.Add(this.label3);
            this.flowLayoutPanel4.Controls.Add(this.textBoxPegawai_search);
            this.flowLayoutPanel4.Controls.Add(this.buttonPegawai_search);
            this.flowLayoutPanel4.Controls.Add(this.label5);
            this.flowLayoutPanel4.Controls.Add(this.labelPegawai_activePage);
            this.flowLayoutPanel4.Controls.Add(this.label6);
            this.flowLayoutPanel4.Controls.Add(this.labelPegawai_totalPage);
            this.flowLayoutPanel4.Controls.Add(this.buttonPegawai_previous);
            this.flowLayoutPanel4.Controls.Add(this.buttonPegawai_next);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(1159, 44);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // buttonPegawai_tambah
            // 
            this.buttonPegawai_tambah.Location = new System.Drawing.Point(3, 3);
            this.buttonPegawai_tambah.Name = "buttonPegawai_tambah";
            this.buttonPegawai_tambah.Size = new System.Drawing.Size(186, 34);
            this.buttonPegawai_tambah.TabIndex = 0;
            this.buttonPegawai_tambah.Text = "Tambah Data Pegawai";
            this.buttonPegawai_tambah.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(195, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Search";
            // 
            // textBoxPegawai_search
            // 
            this.textBoxPegawai_search.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxPegawai_search.Location = new System.Drawing.Point(254, 9);
            this.textBoxPegawai_search.Name = "textBoxPegawai_search";
            this.textBoxPegawai_search.Size = new System.Drawing.Size(307, 22);
            this.textBoxPegawai_search.TabIndex = 2;
            this.textBoxPegawai_search.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // buttonPegawai_search
            // 
            this.buttonPegawai_search.Location = new System.Drawing.Point(567, 3);
            this.buttonPegawai_search.Name = "buttonPegawai_search";
            this.buttonPegawai_search.Size = new System.Drawing.Size(80, 34);
            this.buttonPegawai_search.TabIndex = 9;
            this.buttonPegawai_search.Text = "Cari";
            this.buttonPegawai_search.UseVisualStyleBackColor = true;
            this.buttonPegawai_search.Click += new System.EventHandler(this.buttonPegawai_search_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(653, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Page:";
            // 
            // labelPegawai_activePage
            // 
            this.labelPegawai_activePage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPegawai_activePage.AutoSize = true;
            this.labelPegawai_activePage.Location = new System.Drawing.Point(704, 11);
            this.labelPegawai_activePage.Name = "labelPegawai_activePage";
            this.labelPegawai_activePage.Size = new System.Drawing.Size(16, 17);
            this.labelPegawai_activePage.TabIndex = 4;
            this.labelPegawai_activePage.Text = "0";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(726, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Of";
            // 
            // labelPegawai_totalPage
            // 
            this.labelPegawai_totalPage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPegawai_totalPage.AutoSize = true;
            this.labelPegawai_totalPage.Location = new System.Drawing.Point(755, 11);
            this.labelPegawai_totalPage.Name = "labelPegawai_totalPage";
            this.labelPegawai_totalPage.Size = new System.Drawing.Size(16, 17);
            this.labelPegawai_totalPage.TabIndex = 6;
            this.labelPegawai_totalPage.Text = "0";
            // 
            // buttonPegawai_previous
            // 
            this.buttonPegawai_previous.Location = new System.Drawing.Point(777, 3);
            this.buttonPegawai_previous.Name = "buttonPegawai_previous";
            this.buttonPegawai_previous.Size = new System.Drawing.Size(80, 34);
            this.buttonPegawai_previous.TabIndex = 7;
            this.buttonPegawai_previous.Text = "Previous";
            this.buttonPegawai_previous.UseVisualStyleBackColor = true;
            this.buttonPegawai_previous.Click += new System.EventHandler(this.buttonPegawai_previous_Click);
            // 
            // buttonPegawai_next
            // 
            this.buttonPegawai_next.Location = new System.Drawing.Point(863, 3);
            this.buttonPegawai_next.Name = "buttonPegawai_next";
            this.buttonPegawai_next.Size = new System.Drawing.Size(80, 34);
            this.buttonPegawai_next.TabIndex = 8;
            this.buttonPegawai_next.Text = "Next";
            this.buttonPegawai_next.UseVisualStyleBackColor = true;
            this.buttonPegawai_next.Click += new System.EventHandler(this.buttonPegawai_next_Click);
            // 
            // dataGridViewPegawai
            // 
            this.dataGridViewPegawai.AllowUserToAddRows = false;
            this.dataGridViewPegawai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPegawai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPegawai.Location = new System.Drawing.Point(3, 53);
            this.dataGridViewPegawai.Name = "dataGridViewPegawai";
            this.dataGridViewPegawai.RowTemplate.Height = 24;
            this.dataGridViewPegawai.Size = new System.Drawing.Size(1159, 377);
            this.dataGridViewPegawai.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel4);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1171, 439);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Data Mahasiswa";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.dataGridViewMahasiswa, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1165, 433);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.buttonMahasiswa_tambah);
            this.flowLayoutPanel5.Controls.Add(this.label7);
            this.flowLayoutPanel5.Controls.Add(this.textBoxMahasiswa_search);
            this.flowLayoutPanel5.Controls.Add(this.buttonMahasiswa_search);
            this.flowLayoutPanel5.Controls.Add(this.label8);
            this.flowLayoutPanel5.Controls.Add(this.labelMahasiswa_activePage);
            this.flowLayoutPanel5.Controls.Add(this.label10);
            this.flowLayoutPanel5.Controls.Add(this.labelMahasiswa_totalPage);
            this.flowLayoutPanel5.Controls.Add(this.buttonMahasiswa_previous);
            this.flowLayoutPanel5.Controls.Add(this.buttonMahasiswa_next);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(1159, 44);
            this.flowLayoutPanel5.TabIndex = 0;
            // 
            // buttonMahasiswa_tambah
            // 
            this.buttonMahasiswa_tambah.Location = new System.Drawing.Point(3, 3);
            this.buttonMahasiswa_tambah.Name = "buttonMahasiswa_tambah";
            this.buttonMahasiswa_tambah.Size = new System.Drawing.Size(186, 34);
            this.buttonMahasiswa_tambah.TabIndex = 0;
            this.buttonMahasiswa_tambah.Text = "Tambah Data Mahasiswa";
            this.buttonMahasiswa_tambah.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(195, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Search";
            // 
            // textBoxMahasiswa_search
            // 
            this.textBoxMahasiswa_search.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxMahasiswa_search.Location = new System.Drawing.Point(254, 9);
            this.textBoxMahasiswa_search.Name = "textBoxMahasiswa_search";
            this.textBoxMahasiswa_search.Size = new System.Drawing.Size(307, 22);
            this.textBoxMahasiswa_search.TabIndex = 2;
            this.textBoxMahasiswa_search.TextChanged += new System.EventHandler(this.textBoxMahasiswa_search_TextChanged);
            // 
            // buttonMahasiswa_search
            // 
            this.buttonMahasiswa_search.Location = new System.Drawing.Point(567, 3);
            this.buttonMahasiswa_search.Name = "buttonMahasiswa_search";
            this.buttonMahasiswa_search.Size = new System.Drawing.Size(80, 34);
            this.buttonMahasiswa_search.TabIndex = 9;
            this.buttonMahasiswa_search.Text = "Cari";
            this.buttonMahasiswa_search.UseVisualStyleBackColor = true;
            this.buttonMahasiswa_search.Click += new System.EventHandler(this.buttonMahasiswa_search_Click);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(653, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Page:";
            // 
            // labelMahasiswa_activePage
            // 
            this.labelMahasiswa_activePage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMahasiswa_activePage.AutoSize = true;
            this.labelMahasiswa_activePage.Location = new System.Drawing.Point(704, 11);
            this.labelMahasiswa_activePage.Name = "labelMahasiswa_activePage";
            this.labelMahasiswa_activePage.Size = new System.Drawing.Size(16, 17);
            this.labelMahasiswa_activePage.TabIndex = 4;
            this.labelMahasiswa_activePage.Text = "0";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(726, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 17);
            this.label10.TabIndex = 5;
            this.label10.Text = "Of";
            // 
            // labelMahasiswa_totalPage
            // 
            this.labelMahasiswa_totalPage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMahasiswa_totalPage.AutoSize = true;
            this.labelMahasiswa_totalPage.Location = new System.Drawing.Point(755, 11);
            this.labelMahasiswa_totalPage.Name = "labelMahasiswa_totalPage";
            this.labelMahasiswa_totalPage.Size = new System.Drawing.Size(16, 17);
            this.labelMahasiswa_totalPage.TabIndex = 6;
            this.labelMahasiswa_totalPage.Text = "0";
            // 
            // buttonMahasiswa_previous
            // 
            this.buttonMahasiswa_previous.Location = new System.Drawing.Point(777, 3);
            this.buttonMahasiswa_previous.Name = "buttonMahasiswa_previous";
            this.buttonMahasiswa_previous.Size = new System.Drawing.Size(80, 34);
            this.buttonMahasiswa_previous.TabIndex = 7;
            this.buttonMahasiswa_previous.Text = "Previous";
            this.buttonMahasiswa_previous.UseVisualStyleBackColor = true;
            this.buttonMahasiswa_previous.Click += new System.EventHandler(this.buttonMahasiswa_previous_Click);
            // 
            // buttonMahasiswa_next
            // 
            this.buttonMahasiswa_next.Location = new System.Drawing.Point(863, 3);
            this.buttonMahasiswa_next.Name = "buttonMahasiswa_next";
            this.buttonMahasiswa_next.Size = new System.Drawing.Size(80, 34);
            this.buttonMahasiswa_next.TabIndex = 8;
            this.buttonMahasiswa_next.Text = "Next";
            this.buttonMahasiswa_next.UseVisualStyleBackColor = true;
            this.buttonMahasiswa_next.Click += new System.EventHandler(this.buttonMahasiswa_next_Click);
            // 
            // dataGridViewMahasiswa
            // 
            this.dataGridViewMahasiswa.AllowUserToAddRows = false;
            this.dataGridViewMahasiswa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMahasiswa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMahasiswa.Location = new System.Drawing.Point(3, 53);
            this.dataGridViewMahasiswa.Name = "dataGridViewMahasiswa";
            this.dataGridViewMahasiswa.RowTemplate.Height = 24;
            this.dataGridViewMahasiswa.Size = new System.Drawing.Size(1159, 377);
            this.dataGridViewMahasiswa.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 537);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPegawai)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMahasiswa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelServerAddress;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelArduino1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelArduino2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Button buttonPegawai_tambah;
        private System.Windows.Forms.DataGridView dataGridViewPegawai;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPegawai_search;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelPegawai_activePage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelPegawai_totalPage;
        private System.Windows.Forms.Button buttonPegawai_previous;
        private System.Windows.Forms.Button buttonPegawai_next;
        private System.Windows.Forms.Button buttonPegawai_search;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Button buttonMahasiswa_tambah;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxMahasiswa_search;
        private System.Windows.Forms.Button buttonMahasiswa_search;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelMahasiswa_activePage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelMahasiswa_totalPage;
        private System.Windows.Forms.Button buttonMahasiswa_previous;
        private System.Windows.Forms.Button buttonMahasiswa_next;
        private System.Windows.Forms.DataGridView dataGridViewMahasiswa;
    }
}