
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich
{
    public partial class FrmPhanCongHuongDanVien : Form
    {
        private TextBox txtMaPhanCong;
        private TextBox txtMaHuongDanVien;
        private TextBox txtTenHuongDanVien;
        private TextBox txtMaDangKy;
        private DateTimePicker dtpNgayDi;
        private DateTimePicker dtpNgayVe;
        private DataGridView dgvPhanCong;
        private DataTable bangPhanCong;

        public FrmPhanCongHuongDanVien()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaoBangDuLieu();
        }

        private void TaoGiaoDien()
        {
            Text = "Phân công hướng dẫn viên";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1000, 660);
            MinimumSize = new Size(900, 600);
            Font = new Font("Arial", 10);
            BackColor = Color.WhiteSmoke;

            Controls.Clear();

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "PHÂN CÔNG HƯỚNG DẪN VIÊN";
            lblTieuDe.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 55;
            Controls.Add(lblTieuDe);

            GroupBox gbThongTin = new GroupBox();
            gbThongTin.Text = "Thông tin phân công";
            gbThongTin.Location = new Point(20, 65);
            gbThongTin.Size = new Size(940, 220);
            Controls.Add(gbThongTin);

            TaoNhan(gbThongTin, "Mã phân công:", 20, 32);
            txtMaPhanCong = TaoTextBox(gbThongTin, 150, 28, 270);

            TaoNhan(gbThongTin, "Mã HDV:", 460, 32);
            txtMaHuongDanVien = TaoTextBox(gbThongTin, 570, 28, 320);

            TaoNhan(gbThongTin, "Tên hướng dẫn viên:", 20, 82);
            txtTenHuongDanVien = TaoTextBox(gbThongTin, 150, 78, 270);

            TaoNhan(gbThongTin, "Mã đăng ký:", 460, 82);
            txtMaDangKy = TaoTextBox(gbThongTin, 570, 78, 320);

            TaoNhan(gbThongTin, "Ngày bắt đầu:", 20, 132);
            dtpNgayDi = TaoNgay(gbThongTin, 150, 128);

            TaoNhan(gbThongTin, "Ngày kết thúc:", 460, 132);
            dtpNgayVe = TaoNgay(gbThongTin, 570, 128);

            Label lblGhiChu = new Label();
            lblGhiChu.Text =
                "Lưu ý: Ngày kết thúc phải bằng hoặc sau ngày bắt đầu.";
            lblGhiChu.AutoSize = true;
            lblGhiChu.ForeColor = Color.DimGray;
            lblGhiChu.Location = new Point(20, 180);
            gbThongTin.Controls.Add(lblGhiChu);

            dgvPhanCong = new DataGridView();
            dgvPhanCong.Location = new Point(20, 300);
            dgvPhanCong.Size = new Size(940, 240);
            dgvPhanCong.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                               | AnchorStyles.Left | AnchorStyles.Right;
            dgvPhanCong.AllowUserToAddRows = false;
            dgvPhanCong.AllowUserToDeleteRows = false;
            dgvPhanCong.ReadOnly = true;
            dgvPhanCong.MultiSelect = false;
            dgvPhanCong.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvPhanCong.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhanCong.BackgroundColor = Color.White;
            dgvPhanCong.RowHeadersVisible = false;
            dgvPhanCong.SelectionChanged +=
                DgvPhanCong_SelectionChanged;
            Controls.Add(dgvPhanCong);

            TaoNut("Thêm", 160, 565, BtnThem_Click);
            TaoNut("Sửa", 300, 565, BtnSua_Click);
            TaoNut("Xóa", 440, 565, BtnXoa_Click);
            TaoNut("Làm mới", 580, 565, BtnLamMoi_Click);
            TaoNut("Đóng", 720, 565, (s, e) => Close());
        }

        private void TaoNhan(Control cha, string noiDung, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = noiDung;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(130, 25);
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

        private DateTimePicker TaoNgay(Control cha, int x, int y)
        {
            DateTimePicker dtp = new DateTimePicker();
            dtp.Location = new Point(x, y);
            dtp.Size = new Size(220, 25);
            dtp.Format = DateTimePickerFormat.Short;
            dtp.Value = DateTime.Today;
            cha.Controls.Add(dtp);
            return dtp;
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
            bangPhanCong = new DataTable();

            bangPhanCong.Columns.Add("MaPhanCong", typeof(string));
            bangPhanCong.Columns.Add("MaHDV", typeof(string));
            bangPhanCong.Columns.Add("TenHDV", typeof(string));
            bangPhanCong.Columns.Add("MaDangKy", typeof(string));
            bangPhanCong.Columns.Add("NgayDi", typeof(DateTime));
            bangPhanCong.Columns.Add("NgayVe", typeof(DateTime));

            bangPhanCong.PrimaryKey = new DataColumn[]
            {
                bangPhanCong.Columns["MaPhanCong"]
            };

            dgvPhanCong.DataSource = bangPhanCong;

            dgvPhanCong.Columns["MaPhanCong"].HeaderText = "Mã phân công";
            dgvPhanCong.Columns["MaHDV"].HeaderText = "Mã HDV";
            dgvPhanCong.Columns["TenHDV"].HeaderText = "Tên hướng dẫn viên";
            dgvPhanCong.Columns["MaDangKy"].HeaderText = "Mã đăng ký";
            dgvPhanCong.Columns["NgayDi"].HeaderText = "Ngày bắt đầu";
            dgvPhanCong.Columns["NgayVe"].HeaderText = "Ngày kết thúc";

            dgvPhanCong.Columns["NgayDi"].DefaultCellStyle.Format =
                "dd/MM/yyyy";
            dgvPhanCong.Columns["NgayVe"].DefaultCellStyle.Format =
                "dd/MM/yyyy";
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaPhanCong.Text) ||
                string.IsNullOrWhiteSpace(txtMaHuongDanVien.Text) ||
                string.IsNullOrWhiteSpace(txtTenHuongDanVien.Text) ||
                string.IsNullOrWhiteSpace(txtMaDangKy.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin phân công.");
                return false;
            }

            if (dtpNgayVe.Value.Date < dtpNgayDi.Value.Date)
            {
                MessageBox.Show(
                    "Ngày kết thúc không được trước ngày bắt đầu.");
                return false;
            }

            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            string ma = txtMaPhanCong.Text.Trim();

            if (bangPhanCong.Rows.Find(ma) != null)
            {
                MessageBox.Show("Mã phân công đã tồn tại.");
                return;
            }

            bangPhanCong.Rows.Add(
                ma,
                txtMaHuongDanVien.Text.Trim(),
                txtTenHuongDanVien.Text.Trim(),
                txtMaDangKy.Text.Trim(),
                dtpNgayDi.Value.Date,
                dtpNgayVe.Value.Date
            );

            MessageBox.Show("Thêm phân công thành công.");
            XoaTrang();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvPhanCong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phân công cần sửa.");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            string maCu = dgvPhanCong.CurrentRow
                .Cells["MaPhanCong"].Value.ToString();
            string maMoi = txtMaPhanCong.Text.Trim();

            if (maCu != maMoi &&
                bangPhanCong.Rows.Find(maMoi) != null)
            {
                MessageBox.Show("Mã phân công mới đã tồn tại.");
                return;
            }

            DataRow dong = bangPhanCong.Rows.Find(maCu);

            if (dong != null)
            {
                dong["MaPhanCong"] = maMoi;
                dong["MaHDV"] = txtMaHuongDanVien.Text.Trim();
                dong["TenHDV"] = txtTenHuongDanVien.Text.Trim();
                dong["MaDangKy"] = txtMaDangKy.Text.Trim();
                dong["NgayDi"] = dtpNgayDi.Value.Date;
                dong["NgayVe"] = dtpNgayVe.Value.Date;

                MessageBox.Show("Cập nhật phân công thành công.");
                XoaTrang();
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhanCong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phân công cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa phân công này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                string ma = dgvPhanCong.CurrentRow
                    .Cells["MaPhanCong"].Value.ToString();

                DataRow dong = bangPhanCong.Rows.Find(ma);

                if (dong != null)
                    bangPhanCong.Rows.Remove(dong);

                XoaTrang();
                MessageBox.Show("Đã xóa phân công.");
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
        }

        private void DgvPhanCong_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dgvPhanCong.CurrentRow == null ||
                dgvPhanCong.CurrentRow.IsNewRow ||
                dgvPhanCong.CurrentRow.Cells["MaPhanCong"].Value == null)
                return;

            txtMaPhanCong.Text = dgvPhanCong.CurrentRow
                .Cells["MaPhanCong"].Value.ToString();

            txtMaHuongDanVien.Text = dgvPhanCong.CurrentRow
                .Cells["MaHDV"].Value.ToString();

            txtTenHuongDanVien.Text = dgvPhanCong.CurrentRow
                .Cells["TenHDV"].Value.ToString();

            txtMaDangKy.Text = dgvPhanCong.CurrentRow
                .Cells["MaDangKy"].Value.ToString();

            dtpNgayDi.Value = Convert.ToDateTime(
                dgvPhanCong.CurrentRow.Cells["NgayDi"].Value);

            dtpNgayVe.Value = Convert.ToDateTime(
                dgvPhanCong.CurrentRow.Cells["NgayVe"].Value);
        }

        private void XoaTrang()
        {
            txtMaPhanCong.Clear();
            txtMaHuongDanVien.Clear();
            txtTenHuongDanVien.Clear();
            txtMaDangKy.Clear();

            dtpNgayDi.Value = DateTime.Today;
            dtpNgayVe.Value = DateTime.Today;

            dgvPhanCong.ClearSelection();
            txtMaPhanCong.Focus();
        }
    }
}
