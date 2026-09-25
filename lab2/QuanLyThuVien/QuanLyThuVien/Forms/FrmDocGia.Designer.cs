namespace QuanLyThuVien.Forms
{
    partial class FrmDocGia
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
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.LbNgaySinh = new System.Windows.Forms.Label();
            this.LbPhai = new System.Windows.Forms.Label();
            this.LbHo = new System.Windows.Forms.Label();
            this.lbMa = new System.Windows.Forms.Label();
            this.lbTen = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.cboPhai = new System.Windows.Forms.ComboBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtHo = new System.Windows.Forms.TextBox();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.LbSdtDocGia = new System.Windows.Forms.Label();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.LbTim = new System.Windows.Forms.Label();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnTim = new System.Windows.Forms.Button();
            this.dgvDocGia = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(424, 17);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(100, 22);
            this.txtSDT.TabIndex = 33;
            // 
            // LbNgaySinh
            // 
            this.LbNgaySinh.AutoSize = true;
            this.LbNgaySinh.Location = new System.Drawing.Point(12, 165);
            this.LbNgaySinh.Name = "LbNgaySinh";
            this.LbNgaySinh.Size = new System.Drawing.Size(69, 16);
            this.LbNgaySinh.TabIndex = 31;
            this.LbNgaySinh.Text = "Ngay Sinh";
            // 
            // LbPhai
            // 
            this.LbPhai.AutoSize = true;
            this.LbPhai.Location = new System.Drawing.Point(340, 58);
            this.LbPhai.Name = "LbPhai";
            this.LbPhai.Size = new System.Drawing.Size(34, 16);
            this.LbPhai.TabIndex = 30;
            this.LbPhai.Text = "Phái";
            // 
            // LbHo
            // 
            this.LbHo.AutoSize = true;
            this.LbHo.Location = new System.Drawing.Point(26, 70);
            this.LbHo.Name = "LbHo";
            this.LbHo.Size = new System.Drawing.Size(25, 16);
            this.LbHo.TabIndex = 29;
            this.LbHo.Text = "Họ";
            // 
            // lbMa
            // 
            this.lbMa.AutoSize = true;
            this.lbMa.Location = new System.Drawing.Point(25, 23);
            this.lbMa.Name = "lbMa";
            this.lbMa.Size = new System.Drawing.Size(26, 16);
            this.lbMa.TabIndex = 28;
            this.lbMa.Text = "Mã";
            // 
            // lbTen
            // 
            this.lbTen.AutoSize = true;
            this.lbTen.Location = new System.Drawing.Point(20, 127);
            this.lbTen.Name = "lbTen";
            this.lbTen.Size = new System.Drawing.Size(31, 16);
            this.lbTen.TabIndex = 27;
            this.lbTen.Text = "Tên";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Location = new System.Drawing.Point(92, 165);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(100, 22);
            this.dtpNgaySinh.TabIndex = 26;
            // 
            // cboPhai
            // 
            this.cboPhai.AccessibleName = "";
            this.cboPhai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhai.FormattingEnabled = true;
            this.cboPhai.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.cboPhai.Location = new System.Drawing.Point(424, 62);
            this.cboPhai.Name = "cboPhai";
            this.cboPhai.Size = new System.Drawing.Size(100, 24);
            this.cboPhai.TabIndex = 25;
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(92, 118);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(100, 22);
            this.txtTen.TabIndex = 23;
            // 
            // txtHo
            // 
            this.txtHo.Location = new System.Drawing.Point(92, 70);
            this.txtHo.Name = "txtHo";
            this.txtHo.Size = new System.Drawing.Size(100, 22);
            this.txtHo.TabIndex = 22;
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(92, 18);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(100, 22);
            this.txtMa.TabIndex = 21;
            // 
            // LbSdtDocGia
            // 
            this.LbSdtDocGia.AutoSize = true;
            this.LbSdtDocGia.Location = new System.Drawing.Point(339, 23);
            this.LbSdtDocGia.Name = "LbSdtDocGia";
            this.LbSdtDocGia.Size = new System.Drawing.Size(35, 16);
            this.LbSdtDocGia.TabIndex = 34;
            this.LbSdtDocGia.Text = "SDT";
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(734, 20);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(100, 22);
            this.txtTim.TabIndex = 35;
            // 
            // LbTim
            // 
            this.LbTim.AutoSize = true;
            this.LbTim.Location = new System.Drawing.Point(635, 23);
            this.LbTim.Name = "LbTim";
            this.LbTim.Size = new System.Drawing.Size(62, 16);
            this.LbTim.TabIndex = 36;
            this.LbTim.Text = "Tìm kiếm";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(793, 98);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(75, 23);
            this.btnLamMoi.TabIndex = 40;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(638, 98);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 39;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Location = new System.Drawing.Point(793, 68);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(75, 23);
            this.btnCapNhat.TabIndex = 38;
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(638, 69);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 37;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(866, 20);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 23);
            this.btnTim.TabIndex = 41;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            // 
            // dgvDocGia
            // 
            this.dgvDocGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocGia.Location = new System.Drawing.Point(28, 238);
            this.dgvDocGia.Name = "dgvDocGia";
            this.dgvDocGia.ReadOnly = true;
            this.dgvDocGia.RowHeadersWidth = 51;
            this.dgvDocGia.RowTemplate.Height = 24;
            this.dgvDocGia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDocGia.Size = new System.Drawing.Size(955, 171);
            this.dgvDocGia.TabIndex = 42;
            // 
            // FrmDocGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 450);
            this.Controls.Add(this.dgvDocGia);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.LbTim);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.LbSdtDocGia);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.LbNgaySinh);
            this.Controls.Add(this.LbPhai);
            this.Controls.Add(this.LbHo);
            this.Controls.Add(this.lbMa);
            this.Controls.Add(this.lbTen);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.cboPhai);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.txtHo);
            this.Controls.Add(this.txtMa);
            this.Name = "FrmDocGia";
            this.Text = "FrmDocGia";
            this.Load += new System.EventHandler(this.FrmDocGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label LbNgaySinh;
        private System.Windows.Forms.Label LbPhai;
        private System.Windows.Forms.Label LbHo;
        private System.Windows.Forms.Label lbMa;
        private System.Windows.Forms.Label lbTen;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.ComboBox cboPhai;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtHo;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.Label LbSdtDocGia;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.Label LbTim;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.DataGridView dgvDocGia;
    }
}