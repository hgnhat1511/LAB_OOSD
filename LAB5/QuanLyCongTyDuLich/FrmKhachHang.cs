
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich
{
    public partial class FrmKhachHang : Form
    {
        private TextBox txtMaKH;
        private TextBox txtHoTen;
        private TextBox txtDiaChi;
        private TextBox txtDienThoai;
        private TextBox txtEmail;
        private DataGridView dgvKhachHang;
        private DataTable bangKhachHang;

        public FrmKhachHang()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaoBangDuLieu();
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý khách hàng";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1000, 650);
            MinimumSize = new Size(900, 600);
            Font = new Font("Arial", 10);
            BackColor = Color.WhiteSmoke;

            Controls.Clear();

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "QUẢN LÝ KHÁCH HÀNG";
            lblTieuDe.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 55;
            Controls.Add(lblTieuDe);

            GroupBox gbThongTin = new GroupBox();
            gbThongTin.Text = "Thông tin khách hàng";
            gbThongTin.Location = new Point(20, 65);
            gbThongTin.Size = new Size(940, 180);
            Controls.Add(gbThongTin);

            TaoNhan(gbThongTin, "Mã khách hàng:", 20, 32);
            txtMaKH = TaoTextBox(gbThongTin, 145, 28, 280);

            TaoNhan(gbThongTin, "Họ và tên:", 460, 32);
            txtHoTen = TaoTextBox(gbThongTin, 560, 28, 340);

            TaoNhan(gbThongTin, "Điện thoại:", 20, 82);
            txtDienThoai = TaoTextBox(gbThongTin, 145, 78, 280);

            TaoNhan(gbThongTin, "Email:", 460, 82);
            txtEmail = TaoTextBox(gbThongTin, 560, 78, 340);

            TaoNhan(gbThongTin, "Địa chỉ:", 20, 132);
            txtDiaChi = TaoTextBox(gbThongTin, 145, 128, 755);

            dgvKhachHang = new DataGridView();
            dgvKhachHang.Location = new Point(20, 260);
            dgvKhachHang.Size = new Size(940, 265);
            dgvKhachHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                | AnchorStyles.Left | AnchorStyles.Right;
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.AllowUserToDeleteRows = false;
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.MultiSelect = false;
            dgvKhachHang.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.BackgroundColor = Color.White;
            dgvKhachHang.RowHeadersVisible = false;
            dgvKhachHang.SelectionChanged +=
                DgvKhachHang_SelectionChanged;
            Controls.Add(dgvKhachHang);

            TaoNut("Thêm", 160, 545, BtnThem_Click);
            TaoNut("Sửa", 300, 545, BtnSua_Click);
            TaoNut("Xóa", 440, 545, BtnXoa_Click);
            TaoNut("Làm mới", 580, 545, BtnLamMoi_Click);
            TaoNut("Đóng", 720, 545, (s, e) => Close());
        }

        private void TaoNhan(Control cha, string noiDung, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = noiDung;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(125, 25);
            cha.Controls.Add(lbl);
        }

        private TextBox TaoTextBox(Control cha, int x, int y, int rong)
        {
            TextBox txt = new TextBox();
            txt.Location = new Point(x, y);
            txt.Size = new Size(rong, 25);
            cha.Controls.Add(txt);
            return txt;
        }

        private void TaoNut(string noiDung, int x, int y,
            EventHandler suKien)
        {
            Button btn = new Button();
            btn.Text = noiDung;
            btn.Location = new Point(x, y);
            btn.Size = new Size(110, 38);
            btn.Font = new Font("Arial", 10);
            btn.Cursor = Cursors.Hand;
            btn.Click += suKien;
            Controls.Add(btn);
        }

        private void TaoBangDuLieu()
        {
            bangKhachHang = new DataTable();
            bangKhachHang.Columns.Add("MaKH", typeof(string));
            bangKhachHang.Columns.Add("HoTen", typeof(string));
            bangKhachHang.Columns.Add("DienThoai", typeof(string));
            bangKhachHang.Columns.Add("Email", typeof(string));
            bangKhachHang.Columns.Add("DiaChi", typeof(string));

            bangKhachHang.PrimaryKey = new DataColumn[]
            {
                bangKhachHang.Columns["MaKH"]
            };

            dgvKhachHang.DataSource = bangKhachHang;

            dgvKhachHang.Columns["MaKH"].HeaderText = "Mã khách hàng";
            dgvKhachHang.Columns["HoTen"].HeaderText = "Họ và tên";
            dgvKhachHang.Columns["DienThoai"].HeaderText = "Điện thoại";
            dgvKhachHang.Columns["Email"].HeaderText = "Email";
            dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa chỉ";
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng.");
                txtMaKH.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng.");
                txtHoTen.Focus();
                return false;
            }

            if (txtDienThoai.Text.Trim() != "" &&
                !System.Text.RegularExpressions.Regex.IsMatch(
                    txtDienThoai.Text.Trim(), @"^[0-9+\-\s]{8,15}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.");
                txtDienThoai.Focus();
                return false;
            }

            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            string ma = txtMaKH.Text.Trim();

            if (bangKhachHang.Rows.Find(ma) != null)
            {
                MessageBox.Show("Mã khách hàng đã tồn tại.");
                return;
            }

            bangKhachHang.Rows.Add(
                ma,
                txtHoTen.Text.Trim(),
                txtDienThoai.Text.Trim(),
                txtEmail.Text.Trim(),
                txtDiaChi.Text.Trim()
            );

            MessageBox.Show("Thêm khách hàng thành công.");
            XoaTrang();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa.");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            string maCu =
                dgvKhachHang.CurrentRow.Cells["MaKH"].Value.ToString();
            string maMoi = txtMaKH.Text.Trim();

            DataRow dongMoi = bangKhachHang.Rows.Find(maMoi);
            if (dongMoi != null && maMoi != maCu)
            {
                MessageBox.Show("Mã khách hàng mới đã tồn tại.");
                return;
            }

            DataRow dong = bangKhachHang.Rows.Find(maCu);

            if (dong != null)
            {
                dong["MaKH"] = maMoi;
                dong["HoTen"] = txtHoTen.Text.Trim();
                dong["DienThoai"] = txtDienThoai.Text.Trim();
                dong["Email"] = txtEmail.Text.Trim();
                dong["DiaChi"] = txtDiaChi.Text.Trim();

                MessageBox.Show("Cập nhật khách hàng thành công.");
                XoaTrang();
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa khách hàng này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                string ma =
                    dgvKhachHang.CurrentRow.Cells["MaKH"].Value.ToString();

                DataRow dong = bangKhachHang.Rows.Find(ma);
                if (dong != null)
                    bangKhachHang.Rows.Remove(dong);

                XoaTrang();
                MessageBox.Show("Đã xóa khách hàng.");
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
        }

        private void DgvKhachHang_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null ||
                dgvKhachHang.CurrentRow.IsNewRow)
                return;

            if (dgvKhachHang.CurrentRow.Cells["MaKH"].Value == null)
                return;

            txtMaKH.Text =
                dgvKhachHang.CurrentRow.Cells["MaKH"].Value.ToString();
            txtHoTen.Text =
                dgvKhachHang.CurrentRow.Cells["HoTen"].Value.ToString();
            txtDienThoai.Text =
                dgvKhachHang.CurrentRow.Cells["DienThoai"].Value.ToString();
            txtEmail.Text =
                dgvKhachHang.CurrentRow.Cells["Email"].Value.ToString();
            txtDiaChi.Text =
                dgvKhachHang.CurrentRow.Cells["DiaChi"].Value.ToString();
        }

        private void XoaTrang()
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();

            dgvKhachHang.ClearSelection();
            txtMaKH.Focus();
        }
    }
}
