
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FrmPhongBan : Form
    {
        private TextBox txtMaPB, txtTenPB, txtDiaChi, txtSoDienThoai, txtMoTa;
        private ComboBox cboTruongPhong;
        private DataGridView dgvPhongBan;

        public FrmPhongBan()
        {
            InitializeComponent();
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            Controls.Clear();

            Text = "QUẢN LÝ PHÒNG BAN";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1100, 720);
            MinimumSize = new Size(1000, 650);
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10);

            Controls.Add(new Label
            {
                Text = "QUẢN LÝ PHÒNG BAN",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Location = new Point(390, 20)
            });

            TaoNhan("Mã phòng ban:", 30, 80);
            txtMaPB = TaoTextBox(150, 78, 220);

            TaoNhan("Tên phòng ban:", 430, 80);
            txtTenPB = TaoTextBox(560, 78, 300);

            TaoNhan("Địa chỉ:", 30, 125);
            txtDiaChi = TaoTextBox(150, 123, 220);

            TaoNhan("Số điện thoại:", 430, 125);
            txtSoDienThoai = TaoTextBox(560, 123, 220);

            TaoNhan("Trưởng phòng:", 30, 170);
            cboTruongPhong = TaoCombo(150, 168, 220);
            cboTruongPhong.Items.AddRange(new object[]
            {
                "NV001 - Nguyễn Văn An",
                "NV002 - Trần Thị Bình",
                "NV003 - Lê Văn Cường"
            });

            TaoNhan("Mô tả:", 430, 170);
            txtMoTa = TaoTextBox(560, 168, 400);
            txtMoTa.Multiline = true;
            txtMoTa.Height = 55;

            Button btnThem = TaoNut("Thêm phòng ban", 30, 255, 150);
            Button btnSua = TaoNut("Sửa phòng ban", 190, 255, 150);
            Button btnXoa = TaoNut("Xóa phòng ban", 350, 255, 150);
            Button btnMoi = TaoNut("Làm mới", 510, 255, 120);
            Button btnTim = TaoNut("Tìm kiếm", 640, 255, 120);

            btnThem.Click += (s, e) => ThemPhongBan();
            btnSua.Click += (s, e) => SuaPhongBan();
            btnXoa.Click += (s, e) => XoaPhongBan();
            btnMoi.Click += (s, e) => LamMoi();
            btnTim.Click += (s, e) => TimPhongBan();

            dgvPhongBan = new DataGridView
            {
                Location = new Point(25, 315),
                Size = new Size(1030, 335),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };

            dgvPhongBan.Columns.Add("MaPB", "Mã phòng ban");
            dgvPhongBan.Columns.Add("TenPB", "Tên phòng ban");
            dgvPhongBan.Columns.Add("DiaChi", "Địa chỉ");
            dgvPhongBan.Columns.Add("SDT", "Số điện thoại");
            dgvPhongBan.Columns.Add("TruongPhong", "Trưởng phòng");
            dgvPhongBan.Columns.Add("MoTa", "Mô tả");

            dgvPhongBan.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    NapDuLieuLenForm();
            };

            Controls.Add(dgvPhongBan);

            dgvPhongBan.Rows.Add(
                "PB001", "Phòng Nhân sự",
                "TP. Hồ Chí Minh", "0281234567",
                "NV001 - Nguyễn Văn An",
                "Quản lý hồ sơ nhân sự"
            );

            dgvPhongBan.Rows.Add(
                "PB002", "Phòng Kế toán",
                "TP. Hồ Chí Minh", "0281234568",
                "NV002 - Trần Thị Bình",
                "Quản lý tài chính và tiền lương"
            );

            dgvPhongBan.Rows.Add(
                "PB003", "Phòng Kỹ thuật",
                "TP. Hồ Chí Minh", "0281234569",
                "NV003 - Lê Văn Cường",
                "Phụ trách kỹ thuật"
            );
        }

        private void TaoNhan(string noiDung, int x, int y)
        {
            Controls.Add(new Label
            {
                Text = noiDung,
                Location = new Point(x, y + 5),
                AutoSize = true
            });
        }

        private TextBox TaoTextBox(int x, int y, int rong)
        {
            TextBox txt = new TextBox
            {
                Location = new Point(x, y),
                Width = rong
            };

            Controls.Add(txt);
            return txt;
        }

        private ComboBox TaoCombo(int x, int y, int rong)
        {
            ComboBox cbo = new ComboBox
            {
                Location = new Point(x, y),
                Width = rong,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Controls.Add(cbo);
            return cbo;
        }

        private Button TaoNut(string noiDung, int x, int y, int rong)
        {
            Button btn = new Button
            {
                Text = noiDung,
                Location = new Point(x, y),
                Size = new Size(rong, 35),
                BackColor = Color.LightSteelBlue,
                FlatStyle = FlatStyle.Flat
            };

            Controls.Add(btn);
            return btn;
        }

        private void ThemPhongBan()
        {
            if (string.IsNullOrWhiteSpace(txtMaPB.Text) ||
                string.IsNullOrWhiteSpace(txtTenPB.Text))
            {
                BaoLoi("Vui lòng nhập mã phòng ban và tên phòng ban.");
                return;
            }

            foreach (DataGridViewRow row in dgvPhongBan.Rows)
            {
                if (string.Equals(
                    Convert.ToString(row.Cells["MaPB"].Value),
                    txtMaPB.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    BaoLoi("Mã phòng ban đã tồn tại.");
                    return;
                }
            }

            dgvPhongBan.Rows.Add(
                txtMaPB.Text.Trim(),
                txtTenPB.Text.Trim(),
                txtDiaChi.Text.Trim(),
                txtSoDienThoai.Text.Trim(),
                cboTruongPhong.SelectedIndex >= 0
                    ? cboTruongPhong.Text
                    : "",
                txtMoTa.Text.Trim()
            );

            MessageBox.Show("Đã thêm phòng ban.");
            LamMoi();
        }

        private void SuaPhongBan()
        {
            if (dgvPhongBan.CurrentRow == null)
            {
                BaoLoi("Vui lòng chọn phòng ban cần sửa.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaPB.Text) ||
                string.IsNullOrWhiteSpace(txtTenPB.Text))
            {
                BaoLoi("Vui lòng nhập mã và tên phòng ban.");
                return;
            }

            DataGridViewRow row = dgvPhongBan.CurrentRow;

            foreach (DataGridViewRow item in dgvPhongBan.Rows)
            {
                if (item.Index != row.Index &&
                    string.Equals(
                        Convert.ToString(item.Cells["MaPB"].Value),
                        txtMaPB.Text.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    BaoLoi("Mã phòng ban đã tồn tại.");
                    return;
                }
            }

            row.Cells["MaPB"].Value = txtMaPB.Text.Trim();
            row.Cells["TenPB"].Value = txtTenPB.Text.Trim();
            row.Cells["DiaChi"].Value = txtDiaChi.Text.Trim();
            row.Cells["SDT"].Value = txtSoDienThoai.Text.Trim();
            row.Cells["TruongPhong"].Value =
                cboTruongPhong.SelectedIndex >= 0 ? cboTruongPhong.Text : "";
            row.Cells["MoTa"].Value = txtMoTa.Text.Trim();

            MessageBox.Show("Đã cập nhật phòng ban.");
            LamMoi();
        }

        private void XoaPhongBan()
        {
            if (dgvPhongBan.CurrentRow == null)
            {
                BaoLoi("Vui lòng chọn phòng ban cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa phòng ban này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (kq == DialogResult.Yes)
            {
                dgvPhongBan.Rows.Remove(dgvPhongBan.CurrentRow);
                LamMoi();
            }
        }

        private void TimPhongBan()
        {
            string tuKhoa = txtMaPB.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(tuKhoa))
                tuKhoa = txtTenPB.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                BaoLoi("Nhập mã hoặc tên phòng ban để tìm kiếm.");
                return;
            }

            foreach (DataGridViewRow row in dgvPhongBan.Rows)
            {
                string ma = Convert.ToString(row.Cells["MaPB"].Value).ToLower();
                string ten = Convert.ToString(row.Cells["TenPB"].Value).ToLower();

                row.Visible = ma.Contains(tuKhoa) || ten.Contains(tuKhoa);
            }
        }

        private void NapDuLieuLenForm()
        {
            if (dgvPhongBan.CurrentRow == null) return;

            DataGridViewRow row = dgvPhongBan.CurrentRow;

            txtMaPB.Text = Convert.ToString(row.Cells["MaPB"].Value);
            txtTenPB.Text = Convert.ToString(row.Cells["TenPB"].Value);
            txtDiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value);
            txtSoDienThoai.Text = Convert.ToString(row.Cells["SDT"].Value);
            txtMoTa.Text = Convert.ToString(row.Cells["MoTa"].Value);

            string truongPhong = Convert.ToString(row.Cells["TruongPhong"].Value);
            cboTruongPhong.SelectedItem = truongPhong;

            if (cboTruongPhong.SelectedIndex < 0)
                cboTruongPhong.SelectedIndex = -1;
        }

        private void LamMoi()
        {
            txtMaPB.Clear();
            txtTenPB.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            txtMoTa.Clear();
            cboTruongPhong.SelectedIndex = -1;

            foreach (DataGridViewRow row in dgvPhongBan.Rows)
                row.Visible = true;

            dgvPhongBan.ClearSelection();
        }

        private void BaoLoi(string noiDung)
        {
            MessageBox.Show(
                noiDung,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
    }
}
