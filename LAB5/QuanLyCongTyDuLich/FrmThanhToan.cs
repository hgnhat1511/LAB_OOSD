
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich
{
    public partial class FrmThanhToan : Form
    {
        private TextBox txtMaThanhToan;
        private TextBox txtMaDangKy;
        private ComboBox cboLoaiThanhToan;
        private NumericUpDown nudSoTien;
        private DateTimePicker dtpNgayThanhToan;
        private ComboBox cboTrangThai;
        private DataGridView dgvThanhToan;
        private DataTable bangThanhToan;

        public FrmThanhToan()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaoBangDuLieu();
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý thanh toán";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1000, 650);
            MinimumSize = new Size(900, 600);
            Font = new Font("Arial", 10);
            BackColor = Color.WhiteSmoke;

            Controls.Clear();

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "QUẢN LÝ THANH TOÁN";
            lblTieuDe.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 55;
            Controls.Add(lblTieuDe);

            GroupBox gbThongTin = new GroupBox();
            gbThongTin.Text = "Thông tin thanh toán";
            gbThongTin.Location = new Point(20, 65);
            gbThongTin.Size = new Size(940, 230);
            Controls.Add(gbThongTin);

            TaoNhan(gbThongTin, "Mã thanh toán:", 20, 32);
            txtMaThanhToan = TaoTextBox(gbThongTin, 155, 28, 270);

            TaoNhan(gbThongTin, "Mã đăng ký:", 460, 32);
            txtMaDangKy = TaoTextBox(gbThongTin, 570, 28, 320);

            TaoNhan(gbThongTin, "Loại thanh toán:", 20, 82);
            cboLoaiThanhToan = new ComboBox();
            cboLoaiThanhToan.Location = new Point(155, 78);
            cboLoaiThanhToan.Size = new Size(270, 25);
            cboLoaiThanhToan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiThanhToan.Items.AddRange(new object[]
            {
                "Đặt cọc",
                "Thanh toán phần còn lại",
                "Thanh toán toàn bộ",
                "Hoàn tiền"
            });
            cboLoaiThanhToan.SelectedIndex = 0;
            gbThongTin.Controls.Add(cboLoaiThanhToan);

            TaoNhan(gbThongTin, "Số tiền (VNĐ):", 460, 82);
            nudSoTien = new NumericUpDown();
            nudSoTien.Location = new Point(570, 78);
            nudSoTien.Size = new Size(320, 25);
            nudSoTien.Maximum = 1000000000;
            nudSoTien.Increment = 100000;
            nudSoTien.ThousandsSeparator = true;
            gbThongTin.Controls.Add(nudSoTien);

            TaoNhan(gbThongTin, "Ngày thanh toán:", 20, 132);
            dtpNgayThanhToan = new DateTimePicker();
            dtpNgayThanhToan.Location = new Point(155, 128);
            dtpNgayThanhToan.Size = new Size(270, 25);
            dtpNgayThanhToan.Format = DateTimePickerFormat.Short;
            dtpNgayThanhToan.Value = DateTime.Today;
            gbThongTin.Controls.Add(dtpNgayThanhToan);

            TaoNhan(gbThongTin, "Trạng thái:", 460, 132);
            cboTrangThai = new ComboBox();
            cboTrangThai.Location = new Point(570, 128);
            cboTrangThai.Size = new Size(320, 25);
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Items.AddRange(new object[]
            {
                "Chưa thanh toán",
                "Đã thanh toán",
                "Đã hoàn tiền"
            });
            cboTrangThai.SelectedIndex = 0;
            gbThongTin.Controls.Add(cboTrangThai);

            Label lblGhiChu = new Label();
            lblGhiChu.Text =
                "Nhập thông tin thanh toán và chọn trạng thái tương ứng.";
            lblGhiChu.AutoSize = true;
            lblGhiChu.ForeColor = Color.DimGray;
            lblGhiChu.Location = new Point(20, 185);
            gbThongTin.Controls.Add(lblGhiChu);

            dgvThanhToan = new DataGridView();
            dgvThanhToan.Location = new Point(20, 310);
            dgvThanhToan.Size = new Size(940, 220);
            dgvThanhToan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                | AnchorStyles.Left | AnchorStyles.Right;
            dgvThanhToan.AllowUserToAddRows = false;
            dgvThanhToan.AllowUserToDeleteRows = false;
            dgvThanhToan.ReadOnly = true;
            dgvThanhToan.MultiSelect = false;
            dgvThanhToan.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvThanhToan.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvThanhToan.BackgroundColor = Color.White;
            dgvThanhToan.RowHeadersVisible = false;
            dgvThanhToan.SelectionChanged +=
                DgvThanhToan_SelectionChanged;
            Controls.Add(dgvThanhToan);

            TaoNut("Thêm", 160, 555, BtnThem_Click);
            TaoNut("Sửa", 300, 555, BtnSua_Click);
            TaoNut("Xóa", 440, 555, BtnXoa_Click);
            TaoNut("Làm mới", 580, 555, BtnLamMoi_Click);
            TaoNut("Đóng", 720, 555, (s, e) => Close());
        }

        private void TaoNhan(Control cha, string noiDung, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = noiDung;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(135, 25);
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
            bangThanhToan = new DataTable();

            bangThanhToan.Columns.Add("MaThanhToan", typeof(string));
            bangThanhToan.Columns.Add("MaDangKy", typeof(string));
            bangThanhToan.Columns.Add("LoaiThanhToan", typeof(string));
            bangThanhToan.Columns.Add("SoTien", typeof(decimal));
            bangThanhToan.Columns.Add("NgayThanhToan", typeof(DateTime));
            bangThanhToan.Columns.Add("TrangThai", typeof(string));

            bangThanhToan.PrimaryKey = new DataColumn[]
            {
                bangThanhToan.Columns["MaThanhToan"]
            };

            dgvThanhToan.DataSource = bangThanhToan;

            dgvThanhToan.Columns["MaThanhToan"].HeaderText = "Mã thanh toán";
            dgvThanhToan.Columns["MaDangKy"].HeaderText = "Mã đăng ký";
            dgvThanhToan.Columns["LoaiThanhToan"].HeaderText = "Loại thanh toán";
            dgvThanhToan.Columns["SoTien"].HeaderText = "Số tiền (VNĐ)";
            dgvThanhToan.Columns["NgayThanhToan"].HeaderText = "Ngày thanh toán";
            dgvThanhToan.Columns["TrangThai"].HeaderText = "Trạng thái";

            dgvThanhToan.Columns["SoTien"].DefaultCellStyle.Format = "N0";
            dgvThanhToan.Columns["NgayThanhToan"].DefaultCellStyle.Format =
                "dd/MM/yyyy";
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaThanhToan.Text) ||
                string.IsNullOrWhiteSpace(txtMaDangKy.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã thanh toán và mã đăng ký.");
                return false;
            }

            if (nudSoTien.Value <= 0)
            {
                MessageBox.Show("Số tiền thanh toán phải lớn hơn 0.");
                nudSoTien.Focus();
                return false;
            }

            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            string ma = txtMaThanhToan.Text.Trim();

            if (bangThanhToan.Rows.Find(ma) != null)
            {
                MessageBox.Show("Mã thanh toán đã tồn tại.");
                return;
            }

            bangThanhToan.Rows.Add(
                ma,
                txtMaDangKy.Text.Trim(),
                cboLoaiThanhToan.Text,
                nudSoTien.Value,
                dtpNgayThanhToan.Value.Date,
                cboTrangThai.Text
            );

            MessageBox.Show("Thêm thanh toán thành công.");
            XoaTrang();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvThanhToan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thanh toán cần sửa.");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            string maCu = dgvThanhToan.CurrentRow
                .Cells["MaThanhToan"].Value.ToString();
            string maMoi = txtMaThanhToan.Text.Trim();

            if (maCu != maMoi &&
                bangThanhToan.Rows.Find(maMoi) != null)
            {
                MessageBox.Show("Mã thanh toán mới đã tồn tại.");
                return;
            }

            DataRow dong = bangThanhToan.Rows.Find(maCu);

            if (dong != null)
            {
                dong["MaThanhToan"] = maMoi;
                dong["MaDangKy"] = txtMaDangKy.Text.Trim();
                dong["LoaiThanhToan"] = cboLoaiThanhToan.Text;
                dong["SoTien"] = nudSoTien.Value;
                dong["NgayThanhToan"] = dtpNgayThanhToan.Value.Date;
                dong["TrangThai"] = cboTrangThai.Text;

                MessageBox.Show("Cập nhật thanh toán thành công.");
                XoaTrang();
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvThanhToan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thanh toán cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa thanh toán này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                string ma = dgvThanhToan.CurrentRow
                    .Cells["MaThanhToan"].Value.ToString();

                DataRow dong = bangThanhToan.Rows.Find(ma);

                if (dong != null)
                    bangThanhToan.Rows.Remove(dong);

                XoaTrang();
                MessageBox.Show("Đã xóa thanh toán.");
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
        }

        private void DgvThanhToan_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dgvThanhToan.CurrentRow == null ||
                dgvThanhToan.CurrentRow.IsNewRow ||
                dgvThanhToan.CurrentRow.Cells["MaThanhToan"].Value == null)
                return;

            txtMaThanhToan.Text = dgvThanhToan.CurrentRow
                .Cells["MaThanhToan"].Value.ToString();

            txtMaDangKy.Text = dgvThanhToan.CurrentRow
                .Cells["MaDangKy"].Value.ToString();

            cboLoaiThanhToan.Text = dgvThanhToan.CurrentRow
                .Cells["LoaiThanhToan"].Value.ToString();

            nudSoTien.Value = Convert.ToDecimal(
                dgvThanhToan.CurrentRow.Cells["SoTien"].Value);

            dtpNgayThanhToan.Value = Convert.ToDateTime(
                dgvThanhToan.CurrentRow.Cells["NgayThanhToan"].Value);

            cboTrangThai.Text = dgvThanhToan.CurrentRow
                .Cells["TrangThai"].Value.ToString();
        }

        private void XoaTrang()
        {
            txtMaThanhToan.Clear();
            txtMaDangKy.Clear();
            cboLoaiThanhToan.SelectedIndex = 0;
            nudSoTien.Value = 0;
            dtpNgayThanhToan.Value = DateTime.Today;
            cboTrangThai.SelectedIndex = 0;

            dgvThanhToan.ClearSelection();
            txtMaThanhToan.Focus();
        }
    }
}
