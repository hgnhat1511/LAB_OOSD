
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FrmNhanVien : Form
    {
        private TextBox txtMaNV, txtHoTen, txtCCCD, txtSDT;
        private TextBox txtEmail, txtDiaChi, txtTimKiem;
        private ComboBox cboGioiTinh, cboPhongBan, cboChucVu;
        private DateTimePicker dtpNgaySinh, dtpNgayVaoLam;
        private DataGridView dgvNhanVien;
        private int soThuTu = 4;

        public FrmNhanVien()
        {
            InitializeComponent();
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            Controls.Clear();

            Text = "Quản lý nhân viên - Công ty sơn Đông Á";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1100, 720);
            MinimumSize = new Size(950, 650);
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.White;

            Label title = new Label
            {
                Text = "QUẢN LÝ NHÂN VIÊN",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 10),
                Size = new Size(1080, 42),
                Anchor = AnchorStyles.Top | AnchorStyles.Left |
                         AnchorStyles.Right
            };
            Controls.Add(title);

            int y = 70;

            TaoNhan("Mã nhân viên", 25, y, 110);
            txtMaNV = ThemTextBox(140, y, 300);
            txtMaNV.ReadOnly = true;
            txtMaNV.Text = "NV004";

            TaoNhan("Họ và tên", 565, y, 100);
            txtHoTen = ThemTextBox(670, y, 390);

            y += 42;
            TaoNhan("Ngày sinh", 25, y, 110);
            dtpNgaySinh = ThemNgay(140, y, 300);

            TaoNhan("Giới tính", 565, y, 100);
            cboGioiTinh = ThemCombo(670, y, 390);
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;

            y += 42;
            TaoNhan("CCCD", 25, y, 110);
            txtCCCD = ThemTextBox(140, y, 300);

            TaoNhan("Số điện thoại", 565, y, 100);
            txtSDT = ThemTextBox(670, y, 390);

            y += 42;
            TaoNhan("Email", 25, y, 110);
            txtEmail = ThemTextBox(140, y, 300);

            TaoNhan("Địa chỉ", 565, y, 100);
            txtDiaChi = ThemTextBox(670, y, 390);

            y += 42;
            TaoNhan("Phòng ban", 25, y, 110);
            cboPhongBan = ThemCombo(140, y, 300);
            cboPhongBan.Items.AddRange(new object[]
            {
                "Nhân sự", "Kế toán", "Sản xuất", "Kinh doanh"
            });
            cboPhongBan.SelectedIndex = 0;

            TaoNhan("Chức vụ", 565, y, 100);
            cboChucVu = ThemCombo(670, y, 390);
            cboChucVu.Items.AddRange(new object[]
            {
                "Nhân viên", "Tổ trưởng", "Trưởng phòng", "Giám đốc"
            });
            cboChucVu.SelectedIndex = 0;

            y += 42;
            TaoNhan("Ngày vào làm", 25, y, 110);
            dtpNgayVaoLam = ThemNgay(140, y, 300);

            Button btnThem = ThemNut("Thêm", 565, y, 90);
            Button btnSua = ThemNut("Sửa", 665, y, 90);
            Button btnXoa = ThemNut("Xóa", 765, y, 90);
            Button btnMoi = ThemNut("Làm mới", 865, y, 100);

            Controls.AddRange(new Control[]
            {
                btnThem, btnSua, btnXoa, btnMoi
            });

            y += 48;
            TaoNhan("Tìm nhân viên", 25, y, 110);
            txtTimKiem = ThemTextBox(140, y, 300);

            Button btnTim = ThemNut("Tìm kiếm", 450, y, 100);
            Controls.Add(btnTim);

            dgvNhanVien = new DataGridView
            {
                Location = new Point(20, y + 42),
                Size = new Size(1060, 300),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White
            };

            dgvNhanVien.Columns.Add("MaNV", "Mã NV");
            dgvNhanVien.Columns.Add("HoTen", "Họ và tên");
            dgvNhanVien.Columns.Add("NgaySinh", "Ngày sinh");
            dgvNhanVien.Columns.Add("GioiTinh", "Giới tính");
            dgvNhanVien.Columns.Add("CCCD", "CCCD");
            dgvNhanVien.Columns.Add("SDT", "Số điện thoại");
            dgvNhanVien.Columns.Add("Email", "Email");
            dgvNhanVien.Columns.Add("DiaChi", "Địa chỉ");
            dgvNhanVien.Columns.Add("PhongBan", "Phòng ban");
            dgvNhanVien.Columns.Add("ChucVu", "Chức vụ");
            dgvNhanVien.Columns.Add("NgayVaoLam", "Ngày vào làm");

            Controls.Add(dgvNhanVien);

            dgvNhanVien.Rows.Add(
                "NV001", "Nguyễn Văn An", "10/05/1998",
                "Nam", "012345678901", "0901000001",
                "an@example.com", "TP. Hồ Chí Minh",
                "Nhân sự", "Nhân viên", "01/01/2024");

            dgvNhanVien.Rows.Add(
                "NV002", "Trần Thị Bình", "20/08/1999",
                "Nữ", "012345678902", "0901000002",
                "binh@example.com", "TP. Hồ Chí Minh",
                "Kế toán", "Nhân viên", "15/02/2024");

            dgvNhanVien.Rows.Add(
                "NV003", "Lê Văn Cường", "15/03/1997",
                "Nam", "012345678903", "0901000003",
                "cuong@example.com", "Bình Dương",
                "Sản xuất", "Tổ trưởng", "01/03/2024");

            btnThem.Click += (s, e) => ThemNhanVien();
            btnSua.Click += (s, e) => SuaNhanVien();
            btnXoa.Click += (s, e) => XoaNhanVien();
            btnMoi.Click += (s, e) => LamMoi();
            btnTim.Click += (s, e) => TimNhanVien();

            dgvNhanVien.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    NapDuLieuLenForm(e.RowIndex);
            };
        }

        private void ThemNhanVien()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtCCCD.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                BaoLoi("Vui lòng nhập họ tên, CCCD và số điện thoại.");
                return;
            }

            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            {
                if (Convert.ToString(row.Cells["CCCD"].Value) ==
                    txtCCCD.Text.Trim())
                {
                    BaoLoi("CCCD này đã tồn tại.");
                    return;
                }
            }

            string maNV = "NV" + soThuTu.ToString("D3");

            dgvNhanVien.Rows.Add(
                maNV,
                txtHoTen.Text.Trim(),
                dtpNgaySinh.Value.ToString("dd/MM/yyyy"),
                cboGioiTinh.Text,
                txtCCCD.Text.Trim(),
                txtSDT.Text.Trim(),
                txtEmail.Text.Trim(),
                txtDiaChi.Text.Trim(),
                cboPhongBan.Text,
                cboChucVu.Text,
                dtpNgayVaoLam.Value.ToString("dd/MM/yyyy"));

            soThuTu++;
            MessageBox.Show("Đã thêm nhân viên vào bảng tạm.");
            LamMoi();
        }

        private void SuaNhanVien()
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                BaoLoi("Vui lòng chọn nhân viên cần sửa.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                BaoLoi("Họ và tên không được để trống.");
                return;
            }

            DataGridViewRow row = dgvNhanVien.SelectedRows[0];

            row.Cells["HoTen"].Value = txtHoTen.Text.Trim();
            row.Cells["NgaySinh"].Value =
                dtpNgaySinh.Value.ToString("dd/MM/yyyy");
            row.Cells["GioiTinh"].Value = cboGioiTinh.Text;
            row.Cells["CCCD"].Value = txtCCCD.Text.Trim();
            row.Cells["SDT"].Value = txtSDT.Text.Trim();
            row.Cells["Email"].Value = txtEmail.Text.Trim();
            row.Cells["DiaChi"].Value = txtDiaChi.Text.Trim();
            row.Cells["PhongBan"].Value = cboPhongBan.Text;
            row.Cells["ChucVu"].Value = cboChucVu.Text;
            row.Cells["NgayVaoLam"].Value =
                dtpNgayVaoLam.Value.ToString("dd/MM/yyyy");

            MessageBox.Show("Đã cập nhật nhân viên.");
            LamMoi();
        }

        private void XoaNhanVien()
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                BaoLoi("Vui lòng chọn nhân viên cần xóa.");
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc muốn xóa nhân viên đang chọn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
            {
                dgvNhanVien.Rows.RemoveAt(
                    dgvNhanVien.SelectedRows[0].Index);
                LamMoi();
            }
        }

        private void TimNhanVien()
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            {
                bool khop = string.IsNullOrWhiteSpace(tuKhoa);

                if (!khop)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null &&
                            cell.Value.ToString().IndexOf(
                                tuKhoa,
                                StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            khop = true;
                            break;
                        }
                    }
                }

                row.Visible = khop;
            }
        }

        private void NapDuLieuLenForm(int index)
        {
            DataGridViewRow row = dgvNhanVien.Rows[index];

            txtMaNV.Text = Convert.ToString(row.Cells["MaNV"].Value);
            txtHoTen.Text = Convert.ToString(row.Cells["HoTen"].Value);

            if (DateTime.TryParse(
                Convert.ToString(row.Cells["NgaySinh"].Value),
                out DateTime ngaySinh))
            {
                dtpNgaySinh.Value = ngaySinh;
            }

            cboGioiTinh.Text =
                Convert.ToString(row.Cells["GioiTinh"].Value);
            txtCCCD.Text = Convert.ToString(row.Cells["CCCD"].Value);
            txtSDT.Text = Convert.ToString(row.Cells["SDT"].Value);
            txtEmail.Text = Convert.ToString(row.Cells["Email"].Value);
            txtDiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value);
            cboPhongBan.Text =
                Convert.ToString(row.Cells["PhongBan"].Value);
            cboChucVu.Text =
                Convert.ToString(row.Cells["ChucVu"].Value);

            if (DateTime.TryParse(
                Convert.ToString(row.Cells["NgayVaoLam"].Value),
                out DateTime ngayVaoLam))
            {
                dtpNgayVaoLam.Value = ngayVaoLam;
            }
        }

        private void LamMoi()
        {
            txtMaNV.Text = "NV" + soThuTu.ToString("D3");
            txtHoTen.Clear();
            txtCCCD.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtTimKiem.Clear();

            dtpNgaySinh.Value = DateTime.Today;
            dtpNgayVaoLam.Value = DateTime.Today;

            cboGioiTinh.SelectedIndex = 0;
            cboPhongBan.SelectedIndex = 0;
            cboChucVu.SelectedIndex = 0;

            foreach (DataGridViewRow row in dgvNhanVien.Rows)
                row.Visible = true;

            dgvNhanVien.ClearSelection();
            txtHoTen.Focus();
        }

        private Label TaoNhan(string text, int x, int y, int width)
        {
            Label lbl = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 28),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Controls.Add(lbl);
            return lbl;
        }

        private TextBox ThemTextBox(int x, int y, int width)
        {
            TextBox txt = new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 27)
            };

            Controls.Add(txt);
            return txt;
        }

        private ComboBox ThemCombo(int x, int y, int width)
        {
            ComboBox cbo = new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 27),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Controls.Add(cbo);
            return cbo;
        }

        private DateTimePicker ThemNgay(int x, int y, int width)
        {
            DateTimePicker dtp = new DateTimePicker
            {
                Location = new Point(x, y),
                Size = new Size(width, 27),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };

            Controls.Add(dtp);
            return dtp;
        }

        private Button ThemNut(string text, int x, int y, int width)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 30),
                Cursor = Cursors.Hand
            };
        }

        private void BaoLoi(string noiDung)
        {
            MessageBox.Show(
                noiDung,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
