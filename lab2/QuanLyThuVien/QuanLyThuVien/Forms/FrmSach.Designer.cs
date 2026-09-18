namespace QuanLyThuVien.Forms
{
    partial class FrmSach
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
            this.txtMa = new System.Windows.Forms.TextBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.numNam = new System.Windows.Forms.NumericUpDown();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.cboTheLoai = new System.Windows.Forms.ComboBox();
            this.cboNXB = new System.Windows.Forms.ComboBox();
            this.dgvSach = new System.Windows.Forms.DataGridView();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(63, 36);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(120, 22);
            this.txtMa.TabIndex = 0;
            this.txtMa.Text = "Mã";
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(63, 78);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(120, 22);
            this.txtTen.TabIndex = 1;
            this.txtTen.Text = "txtTen";
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(410, 131);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(121, 22);
            this.txtTim.TabIndex = 2;
            this.txtTim.Text = "Tìm Kiếm";
            // 
            // numNam
            // 
            this.numNam.Location = new System.Drawing.Point(63, 132);
            this.numNam.Name = "numNam";
            this.numNam.Size = new System.Drawing.Size(120, 22);
            this.numNam.TabIndex = 3;
            this.numNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(63, 177);
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(120, 22);
            this.numSoLuong.TabIndex = 4;
            // 
            // cboTheLoai
            // 
            this.cboTheLoai.FormattingEnabled = true;
            this.cboTheLoai.Location = new System.Drawing.Point(410, 32);
            this.cboTheLoai.Name = "cboTheLoai";
            this.cboTheLoai.Size = new System.Drawing.Size(121, 24);
            this.cboTheLoai.TabIndex = 5;
            this.cboTheLoai.Text = "The Loai";
            // 
            // cboNXB
            // 
            this.cboNXB.FormattingEnabled = true;
            this.cboNXB.Location = new System.Drawing.Point(410, 76);
            this.cboNXB.Name = "cboNXB";
            this.cboNXB.Size = new System.Drawing.Size(121, 24);
            this.cboNXB.TabIndex = 6;
            this.cboNXB.Text = "NXB";
            // 
            // dgvSach
            // 
            this.dgvSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSach.Location = new System.Drawing.Point(63, 267);
            this.dgvSach.Name = "dgvSach";
            this.dgvSach.ReadOnly = true;
            this.dgvSach.RowHeadersWidth = 51;
            this.dgvSach.RowTemplate.Height = 24;
            this.dgvSach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSach.Size = new System.Drawing.Size(690, 150);
            this.dgvSach.TabIndex = 7;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(710, 58);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(75, 23);
            this.btnLamMoi.TabIndex = 17;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(567, 58);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 16;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Location = new System.Drawing.Point(710, 28);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(75, 23);
            this.btnCapNhat.TabIndex = 15;
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(567, 30);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 14;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // FrmSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvSach);
            this.Controls.Add(this.cboNXB);
            this.Controls.Add(this.cboTheLoai);
            this.Controls.Add(this.numSoLuong);
            this.Controls.Add(this.numNam);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.txtMa);
            this.Name = "FrmSach";
            this.Text = "FrmSach";
            this.Load += new System.EventHandler(this.FrmSach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.NumericUpDown numNam;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.ComboBox cboTheLoai;
        private System.Windows.Forms.ComboBox cboNXB;
        private System.Windows.Forms.DataGridView dgvSach;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnThem;
    }
}