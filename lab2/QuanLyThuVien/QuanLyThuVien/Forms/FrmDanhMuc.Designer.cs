namespace QuanLyThuVien.Forms
{
    partial class FrmDanhMuc
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtNVSDT = new System.Windows.Forms.TextBox();
            this.dgvNV = new System.Windows.Forms.DataGridView();
            this.LbChucVu = new System.Windows.Forms.Label();
            this.LbNgaySinh = new System.Windows.Forms.Label();
            this.LbPhai = new System.Windows.Forms.Label();
            this.LbHo = new System.Windows.Forms.Label();
            this.lbMa = new System.Windows.Forms.Label();
            this.btnNVMoi = new System.Windows.Forms.Button();
            this.btnNVXoa = new System.Windows.Forms.Button();
            this.btnNVCapNhat = new System.Windows.Forms.Button();
            this.btnNVThem = new System.Windows.Forms.Button();
            this.lbTen = new System.Windows.Forms.Label();
            this.dtNVNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.cboNVPhai = new System.Windows.Forms.ComboBox();
            this.txtNVChucVu = new System.Windows.Forms.TextBox();
            this.txtNVTen = new System.Windows.Forms.TextBox();
            this.txtNVHo = new System.Windows.Forms.TextBox();
            this.txtNVMa = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnTLMoi = new System.Windows.Forms.Button();
            this.btnTLXoa = new System.Windows.Forms.Button();
            this.btnTLCapNhat = new System.Windows.Forms.Button();
            this.btnTLThem = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTL = new System.Windows.Forms.DataGridView();
            this.txtTLMa = new System.Windows.Forms.TextBox();
            this.txtTLTen = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvNXB = new System.Windows.Forms.DataGridView();
            this.btnNXBMoi = new System.Windows.Forms.Button();
            this.btnNXBXoa = new System.Windows.Forms.Button();
            this.btnNXBCapNhat = new System.Windows.Forms.Button();
            this.btnNXBThem = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNXBSDT = new System.Windows.Forms.TextBox();
            this.txtNXBDiaChi = new System.Windows.Forms.TextBox();
            this.txtNXBMa = new System.Windows.Forms.TextBox();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTL)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNXB)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Controls.Add(this.tabPage3);
            this.tabs.Location = new System.Drawing.Point(1, 2);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1189, 754);
            this.tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtNVSDT);
            this.tabPage1.Controls.Add(this.dgvNV);
            this.tabPage1.Controls.Add(this.LbChucVu);
            this.tabPage1.Controls.Add(this.LbNgaySinh);
            this.tabPage1.Controls.Add(this.LbPhai);
            this.tabPage1.Controls.Add(this.LbHo);
            this.tabPage1.Controls.Add(this.lbMa);
            this.tabPage1.Controls.Add(this.btnNVMoi);
            this.tabPage1.Controls.Add(this.btnNVXoa);
            this.tabPage1.Controls.Add(this.btnNVCapNhat);
            this.tabPage1.Controls.Add(this.btnNVThem);
            this.tabPage1.Controls.Add(this.lbTen);
            this.tabPage1.Controls.Add(this.dtNVNgaySinh);
            this.tabPage1.Controls.Add(this.cboNVPhai);
            this.tabPage1.Controls.Add(this.txtNVChucVu);
            this.tabPage1.Controls.Add(this.txtNVTen);
            this.tabPage1.Controls.Add(this.txtNVHo);
            this.tabPage1.Controls.Add(this.txtNVMa);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1181, 725);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Nhân viên";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtNVSDT
            // 
            this.txtNVSDT.Location = new System.Drawing.Point(405, 16);
            this.txtNVSDT.Name = "txtNVSDT";
            this.txtNVSDT.Size = new System.Drawing.Size(100, 22);
            this.txtNVSDT.TabIndex = 20;
            // 
            // dgvNV
            // 
            this.dgvNV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNV.Location = new System.Drawing.Point(40, 184);
            this.dgvNV.Name = "dgvNV";
            this.dgvNV.RowHeadersWidth = 51;
            this.dgvNV.RowTemplate.Height = 24;
            this.dgvNV.Size = new System.Drawing.Size(977, 165);
            this.dgvNV.TabIndex = 19;
            this.dgvNV.SelectionChanged += new System.EventHandler(this.dgvNV_SelectionChanged);
            // 
            // LbChucVu
            // 
            this.LbChucVu.AutoSize = true;
            this.LbChucVu.Location = new System.Drawing.Point(306, 114);
            this.LbChucVu.Name = "LbChucVu";
            this.LbChucVu.Size = new System.Drawing.Size(56, 16);
            this.LbChucVu.TabIndex = 18;
            this.LbChucVu.Text = "Chức Vụ";
            // 
            // LbNgaySinh
            // 
            this.LbNgaySinh.AutoSize = true;
            this.LbNgaySinh.Location = new System.Drawing.Point(306, 71);
            this.LbNgaySinh.Name = "LbNgaySinh";
            this.LbNgaySinh.Size = new System.Drawing.Size(69, 16);
            this.LbNgaySinh.TabIndex = 17;
            this.LbNgaySinh.Text = "Ngay Sinh";
            // 
            // LbPhai
            // 
            this.LbPhai.AutoSize = true;
            this.LbPhai.Location = new System.Drawing.Point(317, 14);
            this.LbPhai.Name = "LbPhai";
            this.LbPhai.Size = new System.Drawing.Size(34, 16);
            this.LbPhai.TabIndex = 16;
            this.LbPhai.Text = "Phái";
            // 
            // LbHo
            // 
            this.LbHo.AutoSize = true;
            this.LbHo.Location = new System.Drawing.Point(96, 66);
            this.LbHo.Name = "LbHo";
            this.LbHo.Size = new System.Drawing.Size(25, 16);
            this.LbHo.TabIndex = 15;
            this.LbHo.Text = "Họ";
            // 
            // lbMa
            // 
            this.lbMa.AutoSize = true;
            this.lbMa.Location = new System.Drawing.Point(95, 19);
            this.lbMa.Name = "lbMa";
            this.lbMa.Size = new System.Drawing.Size(26, 16);
            this.lbMa.TabIndex = 14;
            this.lbMa.Text = "Mã";
            // 
            // btnNVMoi
            // 
            this.btnNVMoi.Location = new System.Drawing.Point(845, 49);
            this.btnNVMoi.Name = "btnNVMoi";
            this.btnNVMoi.Size = new System.Drawing.Size(75, 23);
            this.btnNVMoi.TabIndex = 13;
            this.btnNVMoi.Text = "Làm Mới";
            this.btnNVMoi.UseVisualStyleBackColor = true;
            this.btnNVMoi.Click += new System.EventHandler(this.btnNVMoi_Click);
            // 
            // btnNVXoa
            // 
            this.btnNVXoa.Location = new System.Drawing.Point(690, 49);
            this.btnNVXoa.Name = "btnNVXoa";
            this.btnNVXoa.Size = new System.Drawing.Size(75, 23);
            this.btnNVXoa.TabIndex = 12;
            this.btnNVXoa.Text = "Xóa";
            this.btnNVXoa.UseVisualStyleBackColor = true;
            this.btnNVXoa.Click += new System.EventHandler(this.btnNVXoa_Click);
            // 
            // btnNVCapNhat
            // 
            this.btnNVCapNhat.Location = new System.Drawing.Point(845, 19);
            this.btnNVCapNhat.Name = "btnNVCapNhat";
            this.btnNVCapNhat.Size = new System.Drawing.Size(75, 23);
            this.btnNVCapNhat.TabIndex = 11;
            this.btnNVCapNhat.Text = "Cập nhật";
            this.btnNVCapNhat.UseVisualStyleBackColor = true;
            this.btnNVCapNhat.Click += new System.EventHandler(this.btnNVCapNhat_Click);
            // 
            // btnNVThem
            // 
            this.btnNVThem.Location = new System.Drawing.Point(690, 20);
            this.btnNVThem.Name = "btnNVThem";
            this.btnNVThem.Size = new System.Drawing.Size(75, 23);
            this.btnNVThem.TabIndex = 10;
            this.btnNVThem.Text = "Thêm";
            this.btnNVThem.UseVisualStyleBackColor = true;
            this.btnNVThem.Click += new System.EventHandler(this.btnNVThem_Click);
            // 
            // lbTen
            // 
            this.lbTen.AutoSize = true;
            this.lbTen.Location = new System.Drawing.Point(90, 120);
            this.lbTen.Name = "lbTen";
            this.lbTen.Size = new System.Drawing.Size(31, 16);
            this.lbTen.TabIndex = 9;
            this.lbTen.Text = "Tên";
            // 
            // dtNVNgaySinh
            // 
            this.dtNVNgaySinh.Location = new System.Drawing.Point(405, 64);
            this.dtNVNgaySinh.Name = "dtNVNgaySinh";
            this.dtNVNgaySinh.Size = new System.Drawing.Size(100, 22);
            this.dtNVNgaySinh.TabIndex = 6;
            // 
            // cboNVPhai
            // 
            this.cboNVPhai.AccessibleName = "";
            this.cboNVPhai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNVPhai.FormattingEnabled = true;
            this.cboNVPhai.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.cboNVPhai.Location = new System.Drawing.Point(405, 14);
            this.cboNVPhai.Name = "cboNVPhai";
            this.cboNVPhai.Size = new System.Drawing.Size(100, 24);
            this.cboNVPhai.TabIndex = 5;
            // 
            // txtNVChucVu
            // 
            this.txtNVChucVu.Location = new System.Drawing.Point(405, 114);
            this.txtNVChucVu.Name = "txtNVChucVu";
            this.txtNVChucVu.Size = new System.Drawing.Size(100, 22);
            this.txtNVChucVu.TabIndex = 3;
            // 
            // txtNVTen
            // 
            this.txtNVTen.Location = new System.Drawing.Point(150, 114);
            this.txtNVTen.Name = "txtNVTen";
            this.txtNVTen.Size = new System.Drawing.Size(100, 22);
            this.txtNVTen.TabIndex = 2;
            // 
            // txtNVHo
            // 
            this.txtNVHo.Location = new System.Drawing.Point(150, 66);
            this.txtNVHo.Name = "txtNVHo";
            this.txtNVHo.Size = new System.Drawing.Size(100, 22);
            this.txtNVHo.TabIndex = 1;
            // 
            // txtNVMa
            // 
            this.txtNVMa.Location = new System.Drawing.Point(150, 14);
            this.txtNVMa.Name = "txtNVMa";
            this.txtNVMa.Size = new System.Drawing.Size(100, 22);
            this.txtNVMa.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnTLMoi);
            this.tabPage2.Controls.Add(this.btnTLXoa);
            this.tabPage2.Controls.Add(this.btnTLCapNhat);
            this.tabPage2.Controls.Add(this.btnTLThem);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.dgvTL);
            this.tabPage2.Controls.Add(this.txtTLMa);
            this.tabPage2.Controls.Add(this.txtTLTen);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1181, 725);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Thể loại";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnTLMoi
            // 
            this.btnTLMoi.Location = new System.Drawing.Point(894, 65);
            this.btnTLMoi.Name = "btnTLMoi";
            this.btnTLMoi.Size = new System.Drawing.Size(75, 23);
            this.btnTLMoi.TabIndex = 17;
            this.btnTLMoi.Text = "Làm Mới";
            this.btnTLMoi.UseVisualStyleBackColor = true;
            this.btnTLMoi.Click += new System.EventHandler(this.btnTLMoi_Click);
            // 
            // btnTLXoa
            // 
            this.btnTLXoa.Location = new System.Drawing.Point(739, 65);
            this.btnTLXoa.Name = "btnTLXoa";
            this.btnTLXoa.Size = new System.Drawing.Size(75, 23);
            this.btnTLXoa.TabIndex = 16;
            this.btnTLXoa.Text = "Xóa";
            this.btnTLXoa.UseVisualStyleBackColor = true;
            this.btnTLXoa.Click += new System.EventHandler(this.btnTLXoa_Click);
            // 
            // btnTLCapNhat
            // 
            this.btnTLCapNhat.Location = new System.Drawing.Point(894, 35);
            this.btnTLCapNhat.Name = "btnTLCapNhat";
            this.btnTLCapNhat.Size = new System.Drawing.Size(75, 23);
            this.btnTLCapNhat.TabIndex = 15;
            this.btnTLCapNhat.Text = "Cập nhật";
            this.btnTLCapNhat.UseVisualStyleBackColor = true;
            this.btnTLCapNhat.Click += new System.EventHandler(this.btnTLCapNhat_Click);
            // 
            // btnTLThem
            // 
            this.btnTLThem.Location = new System.Drawing.Point(739, 36);
            this.btnTLThem.Name = "btnTLThem";
            this.btnTLThem.Size = new System.Drawing.Size(75, 23);
            this.btnTLThem.TabIndex = 14;
            this.btnTLThem.Text = "Thêm";
            this.btnTLThem.UseVisualStyleBackColor = true;
            this.btnTLThem.Click += new System.EventHandler(this.btnTLThem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tên";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mã";
            // 
            // dgvTL
            // 
            this.dgvTL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTL.Location = new System.Drawing.Point(53, 203);
            this.dgvTL.Name = "dgvTL";
            this.dgvTL.RowHeadersWidth = 51;
            this.dgvTL.RowTemplate.Height = 24;
            this.dgvTL.Size = new System.Drawing.Size(950, 150);
            this.dgvTL.TabIndex = 2;
            this.dgvTL.SelectionChanged += new System.EventHandler(this.dgvTL_SelectionChanged);
            // 
            // txtTLMa
            // 
            this.txtTLMa.Location = new System.Drawing.Point(175, 30);
            this.txtTLMa.Name = "txtTLMa";
            this.txtTLMa.Size = new System.Drawing.Size(100, 22);
            this.txtTLMa.TabIndex = 1;
            // 
            // txtTLTen
            // 
            this.txtTLTen.Location = new System.Drawing.Point(175, 72);
            this.txtTLTen.Name = "txtTLTen";
            this.txtTLTen.Size = new System.Drawing.Size(100, 22);
            this.txtTLTen.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvNXB);
            this.tabPage3.Controls.Add(this.btnNXBMoi);
            this.tabPage3.Controls.Add(this.btnNXBXoa);
            this.tabPage3.Controls.Add(this.btnNXBCapNhat);
            this.tabPage3.Controls.Add(this.btnNXBThem);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.txtNXBSDT);
            this.tabPage3.Controls.Add(this.txtNXBDiaChi);
            this.tabPage3.Controls.Add(this.txtNXBMa);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1181, 725);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Nhà xuất bản";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvNXB
            // 
            this.dgvNXB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNXB.Location = new System.Drawing.Point(16, 206);
            this.dgvNXB.Name = "dgvNXB";
            this.dgvNXB.RowHeadersWidth = 51;
            this.dgvNXB.RowTemplate.Height = 24;
            this.dgvNXB.Size = new System.Drawing.Size(1009, 150);
            this.dgvNXB.TabIndex = 22;
            this.dgvNXB.SelectionChanged += new System.EventHandler(this.dgvNXB_SelectionChanged);
            // 
            // btnNXBMoi
            // 
            this.btnNXBMoi.Location = new System.Drawing.Point(871, 85);
            this.btnNXBMoi.Name = "btnNXBMoi";
            this.btnNXBMoi.Size = new System.Drawing.Size(75, 23);
            this.btnNXBMoi.TabIndex = 21;
            this.btnNXBMoi.Text = "Làm Mới";
            this.btnNXBMoi.UseVisualStyleBackColor = true;
            this.btnNXBMoi.Click += new System.EventHandler(this.btnNXBMoi_Click);
            // 
            // btnNXBXoa
            // 
            this.btnNXBXoa.Location = new System.Drawing.Point(716, 85);
            this.btnNXBXoa.Name = "btnNXBXoa";
            this.btnNXBXoa.Size = new System.Drawing.Size(75, 23);
            this.btnNXBXoa.TabIndex = 20;
            this.btnNXBXoa.Text = "Xóa";
            this.btnNXBXoa.UseVisualStyleBackColor = true;
            this.btnNXBXoa.Click += new System.EventHandler(this.btnNXBXoa_Click);
            // 
            // btnNXBCapNhat
            // 
            this.btnNXBCapNhat.Location = new System.Drawing.Point(871, 55);
            this.btnNXBCapNhat.Name = "btnNXBCapNhat";
            this.btnNXBCapNhat.Size = new System.Drawing.Size(75, 23);
            this.btnNXBCapNhat.TabIndex = 19;
            this.btnNXBCapNhat.Text = "Cập nhật";
            this.btnNXBCapNhat.UseVisualStyleBackColor = true;
            this.btnNXBCapNhat.Click += new System.EventHandler(this.btnNXBCapNhat_Click);
            // 
            // btnNXBThem
            // 
            this.btnNXBThem.Location = new System.Drawing.Point(716, 56);
            this.btnNXBThem.Name = "btnNXBThem";
            this.btnNXBThem.Size = new System.Drawing.Size(75, 23);
            this.btnNXBThem.TabIndex = 18;
            this.btnNXBThem.Text = "Thêm";
            this.btnNXBThem.UseVisualStyleBackColor = true;
            this.btnNXBThem.Click += new System.EventHandler(this.btnNXBThem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "sdt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Địa chỉ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mã";
            // 
            // txtNXBSDT
            // 
            this.txtNXBSDT.Location = new System.Drawing.Point(142, 133);
            this.txtNXBSDT.Name = "txtNXBSDT";
            this.txtNXBSDT.Size = new System.Drawing.Size(100, 22);
            this.txtNXBSDT.TabIndex = 2;
            // 
            // txtNXBDiaChi
            // 
            this.txtNXBDiaChi.Location = new System.Drawing.Point(142, 86);
            this.txtNXBDiaChi.Name = "txtNXBDiaChi";
            this.txtNXBDiaChi.Size = new System.Drawing.Size(100, 22);
            this.txtNXBDiaChi.TabIndex = 1;
            // 
            // txtNXBMa
            // 
            this.txtNXBMa.Location = new System.Drawing.Point(142, 42);
            this.txtNXBMa.Name = "txtNXBMa";
            this.txtNXBMa.Size = new System.Drawing.Size(100, 22);
            this.txtNXBMa.TabIndex = 0;
            // 
            // FrmDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 673);
            this.Controls.Add(this.tabs);
            this.Name = "FrmDanhMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục và nhân viên";
            this.Load += new System.EventHandler(this.FrmDanhMuc_Load);
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTL)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNXB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtNVMa;
        private System.Windows.Forms.TextBox txtNVHo;
        private System.Windows.Forms.TextBox txtNVTen;
        private System.Windows.Forms.TextBox txtNVChucVu;
        private System.Windows.Forms.ComboBox cboNVPhai;
        private System.Windows.Forms.DateTimePicker dtNVNgaySinh;
        private System.Windows.Forms.Label lbTen;
        private System.Windows.Forms.Button btnNVThem;
        private System.Windows.Forms.Button btnNVCapNhat;
        private System.Windows.Forms.Button btnNVXoa;
        private System.Windows.Forms.Button btnNVMoi;
        private System.Windows.Forms.Label lbMa;
        private System.Windows.Forms.Label LbHo;
        private System.Windows.Forms.Label LbPhai;
        private System.Windows.Forms.Label LbNgaySinh;
        private System.Windows.Forms.Label LbChucVu;
        private System.Windows.Forms.DataGridView dgvNV;
        private System.Windows.Forms.TextBox txtTLTen;
        private System.Windows.Forms.TextBox txtTLMa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTLMoi;
        private System.Windows.Forms.Button btnTLXoa;
        private System.Windows.Forms.Button btnTLCapNhat;
        private System.Windows.Forms.Button btnTLThem;
        private System.Windows.Forms.TextBox txtNXBSDT;
        private System.Windows.Forms.TextBox txtNXBDiaChi;
        private System.Windows.Forms.TextBox txtNXBMa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvNXB;
        private System.Windows.Forms.Button btnNXBMoi;
        private System.Windows.Forms.Button btnNXBXoa;
        private System.Windows.Forms.Button btnNXBCapNhat;
        private System.Windows.Forms.Button btnNXBThem;
        private System.Windows.Forms.TextBox txtNVSDT;
    }
}