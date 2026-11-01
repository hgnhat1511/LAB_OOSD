
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FrmQuyetDinhTuyenDung : Form
    {
        private TextBox txtMaQD;
        private TextBox txtHoTen;
        private TextBox txtViTri;
        private TextBox txtPhongBan;
        private TextBox txtMucLuong;
        private TextBox txtGhiChu;
        private TextBox txtTimKiem;

        private DateTimePicker dtpNgayQD;
        private DateTimePicker dtpNgayNhanViec;
        private ComboBox cboTrangThai;
        private DataGridView dgvTuyenDung;

        public FrmQuyetDinhTuyenDung()
        {
            InitializeComponent();
            DungGiaoDien();
            NapDuLieuMau();
        }

        private void DungGiaoDien()
        {
            Text = "Quản lý quyết định tuyển dụng";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1100, 720);
            MinimumSize = new Size(950, 650);
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9);

            Label lblTieuDe = new Label
            {
                Text = "QUẢN LÝ QUYẾT ĐỊNH TUYỂN DỤNG",
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 55, 90)
            };
            Controls.Add(lblTieuDe);

            Panel pnlChinh = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 5, 20, 15),
                BackColor = Color.White
            };
            Controls.Add(pnlChinh);
            pnlChinh.BringToFront();
            lblTieuDe.BringToFront();

            TableLayoutPanel tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 215,
                ColumnCount = 4,
                RowCount = 4
            };

            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));

            for (int i = 0; i < 4; i++)
                tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 25));

            txtMaQD = TaoTextBox();
            txtHoTen = TaoTextBox();
            txtViTri = TaoTextBox();
            txtPhongBan = TaoTextBox();
            txtMucLuong = TaoTextBox();
            txtGhiChu = TaoTextBox();

            dtpNgayQD = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Dock = DockStyle.Fill
            };

            dtpNgayNhanViec = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Dock = DockStyle.Fill
            };

            cboTrangThai = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboTrangThai.Items.AddRange(new object[]
            {
                "Chờ nhận việc",
                "Đã nhận việc",
                "Đã hủy"
            });
            cboTrangThai.SelectedIndex = 0;

            ThemTruong(tbl, "Mã quyết định:", txtMaQD, 0, 0);
            ThemTruong(tbl, "Họ tên nhân viên:", txtHoTen, 2, 0);

            ThemTruong(tbl, "Vị trí tuyển dụng:", txtViTri, 0, 1);
            ThemTruong(tbl, "Phòng ban:", txtPhongBan, 2, 1);

            ThemTruong(tbl, "Ngày quyết định:", dtpNgayQD, 0, 2);
            ThemTruong(tbl, "Ngày nhận việc:", dtpNgayNhanViec, 2, 2);

            ThemTruong(tbl, "Mức lương:", txtMucLuong, 0, 3);
            ThemTruong(tbl, "Trạng thái:", cboTrangThai, 2, 3);

            pnlChinh.Controls.Add(tbl);

            Label lblGhiChu = new Label
            {
                Text = "Ghi chú:",
                Location = new Point(0, 230),
                Size = new Size(100, 25)
            };
            pnlChinh.Controls.Add(lblGhiChu);

            txtGhiChu.Multiline = true;
            txtGhiChu.Location = new Point(100, 228);
            txtGhiChu.Size = new Size(700, 45);
            pnlChinh.Controls.Add(txtGhiChu);

            Label lblTim = new Label
            {
                Text = "Tìm kiếm:",
                Location = new Point(0, 290),
                Size = new Size(80, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlChinh.Controls.Add(lblTim);

            txtTimKiem = new TextBox
            {
                Location = new Point(100, 292),
                Size = new Size(350, 27),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            pnlChinh.Controls.Add(txtTimKiem);

            Button btnTim = TaoNut("Tìm kiếm", 465, 289, 100);
            btnTim.Click += (s, e) => TimKiem();
            pnlChinh.Controls.Add(btnTim);

            Button btnThem = TaoNut("Thêm", 0, 335, 100);
            btnThem.Click += (s, e) => Them();
            pnlChinh.Controls.Add(btnThem);

            Button btnSua = TaoNut("Sửa", 110, 335, 100);
            btnSua.Click += (s, e) => Sua();
            pnlChinh.Controls.Add(btnSua);

            Button btnXoa = TaoNut("Xóa", 220, 335, 100);
            btnXoa.Click += (s, e) => Xoa();
            pnlChinh.Controls.Add(btnXoa);

            Button btnLamMoi = TaoNut("Làm mới", 330, 335, 100);
            btnLamMoi.Click += (s, e) => LamMoi();
            pnlChinh.Controls.Add(btnLamMoi);

            Button btnDong = TaoNut("Đóng", 440, 335, 100);
            btnDong.Click += (s, e) => Close();
            pnlChinh.Controls.Add(btnDong);

            dgvTuyenDung = new DataGridView
            {
                Location = new Point(0, 385),
                Size = new Size(1030, 240),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right,
                AllowUserToAddRows = false,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            dgvTuyenDung.Columns.Add("MaQD", "Mã quyết định");
            dgvTuyenDung.Columns.Add("HoTen", "Họ tên nhân viên");
            dgvTuyenDung.Columns.Add("ViTri", "Vị trí tuyển dụng");
            dgvTuyenDung.Columns.Add("PhongBan", "Phòng ban");
            dgvTuyenDung.Columns.Add("NgayQD", "Ngày quyết định");
            dgvTuyenDung.Columns.Add("NgayNhanViec", "Ngày nhận việc");
            dgvTuyenDung.Columns.Add("MucLuong", "Mức lương");
            dgvTuyenDung.Columns.Add("TrangThai", "Trạng thái");
            dgvTuyenDung.Columns.Add("GhiChu", "Ghi chú");

            dgvTuyenDung.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    DoDuLieuLenForm(e.RowIndex);
            };

            pnlChinh.Controls.Add(dgvTuyenDung);
        }

        private TextBox TaoTextBox()
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(3)
            };
        }

        private void ThemTruong(
            TableLayoutPanel tbl,
            string ten,
            Control control,
            int cot,
            int hang)
        {
            Label lbl = new Label
            {
                Text = ten,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            tbl.Controls.Add(lbl, cot, hang);
            tbl.Controls.Add(control, cot + 1, hang);
        }

        private Button TaoNut(string ten, int x, int y, int rong)
        {
            return new Button
            {
                Text = ten,
                Location = new Point(x, y),
                Size = new Size(rong, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(235, 241, 250),
                ForeColor = Color.FromArgb(30, 55, 90),
                Cursor = Cursors.Hand
            };
        }

        private void NapDuLieuMau()
        {
            dgvTuyenDung.Rows.Add(
                "QD001", "Nguyễn Văn An", "Nhân viên kinh doanh",
                "Kinh doanh", "01/10/2026", "15/10/2026",
                "12000000", "Đã nhận việc", "");

            dgvTuyenDung.Rows.Add(
                "QD002", "Trần Thị Bình", "Nhân viên kế toán",
                "Kế toán", "05/10/2026", "01/11/2026",
                "11000000", "Chờ nhận việc", "Đã gửi thông báo");

            dgvTuyenDung.Rows.Add(
                "QD003", "Lê Văn Cường", "Nhân viên IT",
                "Công nghệ thông tin", "10/10/2026", "20/10/2026",
                "15000000", "Đã nhận việc", "");
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaQD.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtViTri.Text) ||
                string.IsNullOrWhiteSpace(txtPhongBan.Text) ||
                string.IsNullOrWhiteSpace(txtMucLuong.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ mã quyết định, họ tên, vị trí, phòng ban và mức lương.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtMucLuong.Text.Trim(), out decimal luong)
                || luong < 0)
            {
                MessageBox.Show("Mức lương phải là số không âm.");
                txtMucLuong.Focus();
                return false;
            }

            if (dtpNgayNhanViec.Value.Date < dtpNgayQD.Value.Date)
            {
                MessageBox.Show(
                    "Ngày nhận việc không được trước ngày quyết định.");
                return false;
            }

            return true;
        }

        private void Them()
        {
            if (!KiemTraDuLieu())
                return;

            foreach (DataGridViewRow row in dgvTuyenDung.Rows)
            {
                if (row.Cells["MaQD"].Value.ToString()
                    .Equals(txtMaQD.Text.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã quyết định đã tồn tại.");
                    return;
                }
            }

            dgvTuyenDung.Rows.Add(
                txtMaQD.Text.Trim(),
                txtHoTen.Text.Trim(),
                txtViTri.Text.Trim(),
                txtPhongBan.Text.Trim(),
                dtpNgayQD.Value.ToString("dd/MM/yyyy"),
                dtpNgayNhanViec.Value.ToString("dd/MM/yyyy"),
                txtMucLuong.Text.Trim(),
                cboTrangThai.Text,
                txtGhiChu.Text.Trim());

            MessageBox.Show("Thêm quyết định tuyển dụng thành công.");
            LamMoi();
        }

        private void Sua()
        {
            if (dgvTuyenDung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn quyết định cần sửa trong bảng.");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            int index = dgvTuyenDung.SelectedRows[0].Index;
            string maMoi = txtMaQD.Text.Trim();

            foreach (DataGridViewRow row in dgvTuyenDung.Rows)
            {
                if (row.Index != index &&
                    row.Cells["MaQD"].Value.ToString()
                    .Equals(maMoi, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã quyết định đã tồn tại ở dòng khác.");
                    return;
                }
            }

            DataGridViewRow dong = dgvTuyenDung.Rows[index];

            dong.Cells["MaQD"].Value = maMoi;
            dong.Cells["HoTen"].Value = txtHoTen.Text.Trim();
            dong.Cells["ViTri"].Value = txtViTri.Text.Trim();
            dong.Cells["PhongBan"].Value = txtPhongBan.Text.Trim();
            dong.Cells["NgayQD"].Value =
                dtpNgayQD.Value.ToString("dd/MM/yyyy");
            dong.Cells["NgayNhanViec"].Value =
                dtpNgayNhanViec.Value.ToString("dd/MM/yyyy");
            dong.Cells["MucLuong"].Value = txtMucLuong.Text.Trim();
            dong.Cells["TrangThai"].Value = cboTrangThai.Text;
            dong.Cells["GhiChu"].Value = txtGhiChu.Text.Trim();

            MessageBox.Show("Cập nhật quyết định thành công.");
            LamMoi();
        }

        private void Xoa()
        {
            if (dgvTuyenDung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn quyết định cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa quyết định này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                dgvTuyenDung.Rows.RemoveAt(
                    dgvTuyenDung.SelectedRows[0].Index);

                LamMoi();
            }
        }

        private void TimKiem()
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            foreach (DataGridViewRow row in dgvTuyenDung.Rows)
            {
                bool timThay = false;

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    string giaTri =
                        Convert.ToString(row.Cells[i].Value) ?? "";

                    if (giaTri.IndexOf(
                        tuKhoa,
                        StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        timThay = true;
                        break;
                    }
                }

                row.Visible = timThay;
            }
        }

        private void DoDuLieuLenForm(int index)
        {
            DataGridViewRow row = dgvTuyenDung.Rows[index];

            txtMaQD.Text = Convert.ToString(row.Cells["MaQD"].Value);
            txtHoTen.Text = Convert.ToString(row.Cells["HoTen"].Value);
            txtViTri.Text = Convert.ToString(row.Cells["ViTri"].Value);
            txtPhongBan.Text = Convert.ToString(row.Cells["PhongBan"].Value);

            DateTime ngayQD, ngayNhanViec;

            if (DateTime.TryParse(
                Convert.ToString(row.Cells["NgayQD"].Value), out ngayQD))
                dtpNgayQD.Value = ngayQD;

            if (DateTime.TryParse(
                Convert.ToString(row.Cells["NgayNhanViec"].Value),
                out ngayNhanViec))
                dtpNgayNhanViec.Value = ngayNhanViec;

            txtMucLuong.Text =
                Convert.ToString(row.Cells["MucLuong"].Value);

            cboTrangThai.SelectedItem =
                Convert.ToString(row.Cells["TrangThai"].Value);

            txtGhiChu.Text =
                Convert.ToString(row.Cells["GhiChu"].Value);
        }

        private void LamMoi()
        {
            txtMaQD.Clear();
            txtHoTen.Clear();
            txtViTri.Clear();
            txtPhongBan.Clear();
            txtMucLuong.Clear();
            txtGhiChu.Clear();
            txtTimKiem.Clear();

            dtpNgayQD.Value = DateTime.Today;
            dtpNgayNhanViec.Value = DateTime.Today;
            cboTrangThai.SelectedIndex = 0;

            foreach (DataGridViewRow row in dgvTuyenDung.Rows)
                row.Visible = true;

            dgvTuyenDung.ClearSelection();
            txtMaQD.Focus();
        }
    }
}
