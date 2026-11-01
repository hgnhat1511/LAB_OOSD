
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FrmHopDong : Form
    {
        private DataGridView dgvHopDong;
        private TextBox txtMaHD, txtLuong, txtGhiChu;
        private ComboBox cboNhanVien, cboLoaiHD, cboTrangThai;
        private DateTimePicker dtpNgayKy, dtpNgayBatDau, dtpNgayKetThuc;

        public FrmHopDong()
        {
            InitializeComponent();
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            Controls.Clear();

            Text = "QUẢN LÝ HỢP ĐỒNG LAO ĐỘNG";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1100, 720);
            MinimumSize = new Size(1000, 650);
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10);

            Label lblTieuDe = new Label
            {
                Text = "QUẢN LÝ HỢP ĐỒNG LAO ĐỘNG",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Location = new Point(300, 20)
            };
            Controls.Add(lblTieuDe);

            TaoNhan("Mã hợp đồng:", 30, 80);
            txtMaHD = TaoTextBox(150, 78, 220);

            TaoNhan("Nhân viên:", 420, 80);
            cboNhanVien = TaoCombo(540, 78, 220);
            cboNhanVien.Items.AddRange(new object[]
            {
                "NV001 - Nguyễn Văn An",
                "NV002 - Trần Thị Bình",
                "NV003 - Lê Văn Cường"
            });

            TaoNhan("Loại hợp đồng:", 30, 125);
            cboLoaiHD = TaoCombo(150, 123, 220);
            cboLoaiHD.Items.AddRange(new object[]
            {
                "Thử việc",
                "Xác định thời hạn",
                "Không xác định thời hạn",
                "Thời vụ"
            });

            TaoNhan("Lương cơ bản:", 420, 125);
            txtLuong = TaoTextBox(540, 123, 220);

            TaoNhan("Ngày ký:", 30, 170);
            dtpNgayKy = TaoNgay(150, 168);

            TaoNhan("Ngày bắt đầu:", 420, 170);
            dtpNgayBatDau = TaoNgay(540, 168);

            TaoNhan("Ngày kết thúc:", 30, 215);
            dtpNgayKetThuc = TaoNgay(150, 213);

            TaoNhan("Trạng thái:", 420, 215);
            cboTrangThai = TaoCombo(540, 213, 220);
            cboTrangThai.Items.AddRange(new object[]
            {
                "Đang hiệu lực",
                "Đã hết hạn",
                "Đã chấm dứt"
            });
            cboTrangThai.SelectedIndex = 0;

            TaoNhan("Ghi chú:", 30, 260);
            txtGhiChu = TaoTextBox(150, 258, 610);
            txtGhiChu.Multiline = true;
            txtGhiChu.Height = 55;

            Button btnThem = TaoNut("Thêm hợp đồng", 30, 335, 145);
            Button btnSua = TaoNut("Sửa hợp đồng", 185, 335, 145);
            Button btnXoa = TaoNut("Xóa hợp đồng", 340, 335, 145);
            Button btnMoi = TaoNut("Làm mới", 495, 335, 120);
            Button btnTim = TaoNut("Tìm kiếm", 625, 335, 120);

            btnThem.Click += (s, e) => ThemHopDong();
            btnSua.Click += (s, e) => SuaHopDong();
            btnXoa.Click += (s, e) => XoaHopDong();
            btnMoi.Click += (s, e) => LamMoi();
            btnTim.Click += (s, e) => TimHopDong();

            dgvHopDong = new DataGridView
            {
                Location = new Point(25, 395),
                Size = new Size(1030, 260),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };

            dgvHopDong.Columns.Add("MaHD", "Mã hợp đồng");
            dgvHopDong.Columns.Add("NhanVien", "Nhân viên");
            dgvHopDong.Columns.Add("LoaiHD", "Loại hợp đồng");
            dgvHopDong.Columns.Add("Luong", "Lương cơ bản");
            dgvHopDong.Columns.Add("NgayKy", "Ngày ký");
            dgvHopDong.Columns.Add("BatDau", "Ngày bắt đầu");
            dgvHopDong.Columns.Add("KetThuc", "Ngày kết thúc");
            dgvHopDong.Columns.Add("TrangThai", "Trạng thái");

            dgvHopDong.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    NapDuLieuLenForm();
            };

            Controls.Add(dgvHopDong);

            dgvHopDong.Rows.Add(
                "HD001", "NV001 - Nguyễn Văn An",
                "Xác định thời hạn", "10000000",
                "01/01/2026", "01/01/2026", "31/12/2026",
                "Đang hiệu lực"
            );

            dgvHopDong.Rows.Add(
                "HD002", "NV002 - Trần Thị Bình",
                "Thử việc", "8000000",
                "01/10/2026", "01/10/2026", "31/12/2026",
                "Đang hiệu lực"
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

        private DateTimePicker TaoNgay(int x, int y)
        {
            DateTimePicker dtp = new DateTimePicker
            {
                Location = new Point(x, y),
                Width = 220,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };

            Controls.Add(dtp);
            return dtp;
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

        private void ThemHopDong()
        {
            if (string.IsNullOrWhiteSpace(txtMaHD.Text) ||
                cboNhanVien.SelectedIndex < 0 ||
                cboLoaiHD.SelectedIndex < 0 ||
                string.IsNullOrWhiteSpace(txtLuong.Text))
            {
                BaoLoi("Vui lòng nhập mã hợp đồng, nhân viên, loại hợp đồng và lương.");
                return;
            }

            if (!decimal.TryParse(txtLuong.Text, out decimal luong) || luong < 0)
            {
                BaoLoi("Lương phải là số hợp lệ và không được âm.");
                return;
            }

            if (dtpNgayKetThuc.Value.Date < dtpNgayBatDau.Value.Date)
            {
                BaoLoi("Ngày kết thúc không được trước ngày bắt đầu.");
                return;
            }

            foreach (DataGridViewRow row in dgvHopDong.Rows)
            {
                if (Convert.ToString(row.Cells["MaHD"].Value)
                    .Equals(txtMaHD.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    BaoLoi("Mã hợp đồng đã tồn tại.");
                    return;
                }
            }

            dgvHopDong.Rows.Add(
                txtMaHD.Text.Trim(),
                cboNhanVien.Text,
                cboLoaiHD.Text,
                luong.ToString("0.##"),
                dtpNgayKy.Value.ToString("dd/MM/yyyy"),
                dtpNgayBatDau.Value.ToString("dd/MM/yyyy"),
                dtpNgayKetThuc.Value.ToString("dd/MM/yyyy"),
                cboTrangThai.Text
            );

            MessageBox.Show("Đã thêm hợp đồng.");
            LamMoi();
        }

        private void SuaHopDong()
        {
            if (dgvHopDong.CurrentRow == null)
            {
                BaoLoi("Vui lòng chọn hợp đồng cần sửa.");
                return;
            }

            if (cboNhanVien.SelectedIndex < 0 ||
                cboLoaiHD.SelectedIndex < 0 ||
                !decimal.TryParse(txtLuong.Text, out decimal luong) ||
                luong < 0)
            {
                BaoLoi("Vui lòng kiểm tra nhân viên, loại hợp đồng và lương.");
                return;
            }

            if (dtpNgayKetThuc.Value.Date < dtpNgayBatDau.Value.Date)
            {
                BaoLoi("Ngày kết thúc không được trước ngày bắt đầu.");
                return;
            }

            DataGridViewRow row = dgvHopDong.CurrentRow;

            row.Cells["MaHD"].Value = txtMaHD.Text.Trim();
            row.Cells["NhanVien"].Value = cboNhanVien.Text;
            row.Cells["LoaiHD"].Value = cboLoaiHD.Text;
            row.Cells["Luong"].Value = luong.ToString("0.##");
            row.Cells["NgayKy"].Value = dtpNgayKy.Value.ToString("dd/MM/yyyy");
            row.Cells["BatDau"].Value = dtpNgayBatDau.Value.ToString("dd/MM/yyyy");
            row.Cells["KetThuc"].Value = dtpNgayKetThuc.Value.ToString("dd/MM/yyyy");
            row.Cells["TrangThai"].Value = cboTrangThai.Text;

            MessageBox.Show("Đã cập nhật hợp đồng.");
            LamMoi();
        }

        private void XoaHopDong()
        {
            if (dgvHopDong.CurrentRow == null)
            {
                BaoLoi("Vui lòng chọn hợp đồng cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa hợp đồng này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (kq == DialogResult.Yes)
            {
                dgvHopDong.Rows.Remove(dgvHopDong.CurrentRow);
                LamMoi();
            }
        }

        private void TimHopDong()
        {
            string tuKhoa = txtMaHD.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                BaoLoi("Nhập mã hợp đồng vào ô Mã hợp đồng để tìm.");
                return;
            }

            foreach (DataGridViewRow row in dgvHopDong.Rows)
            {
                bool khop = Convert.ToString(row.Cells["MaHD"].Value)
                    .ToLower().Contains(tuKhoa);

                row.Visible = khop;
            }
        }

        private void NapDuLieuLenForm()
        {
            if (dgvHopDong.CurrentRow == null) return;

            DataGridViewRow row = dgvHopDong.CurrentRow;

            txtMaHD.Text = Convert.ToString(row.Cells["MaHD"].Value);

            cboNhanVien.SelectedItem = Convert.ToString(row.Cells["NhanVien"].Value);
            cboLoaiHD.SelectedItem = Convert.ToString(row.Cells["LoaiHD"].Value);
            txtLuong.Text = Convert.ToString(row.Cells["Luong"].Value);

            DateTime.TryParse(Convert.ToString(row.Cells["NgayKy"].Value), out DateTime ngayKy);
            DateTime.TryParse(Convert.ToString(row.Cells["BatDau"].Value), out DateTime ngayBD);
            DateTime.TryParse(Convert.ToString(row.Cells["KetThuc"].Value), out DateTime ngayKT);

            dtpNgayKy.Value = ngayKy == DateTime.MinValue ? DateTime.Today : ngayKy;
            dtpNgayBatDau.Value = ngayBD == DateTime.MinValue ? DateTime.Today : ngayBD;
            dtpNgayKetThuc.Value = ngayKT == DateTime.MinValue ? DateTime.Today : ngayKT;

            cboTrangThai.SelectedItem = Convert.ToString(row.Cells["TrangThai"].Value);
        }

        private void LamMoi()
        {
            txtMaHD.Clear();
            txtLuong.Clear();
            txtGhiChu.Clear();

            cboNhanVien.SelectedIndex = -1;
            cboLoaiHD.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = 0;

            dtpNgayKy.Value = DateTime.Today;
            dtpNgayBatDau.Value = DateTime.Today;
            dtpNgayKetThuc.Value = DateTime.Today;

            foreach (DataGridViewRow row in dgvHopDong.Rows)
                row.Visible = true;

            dgvHopDong.ClearSelection();
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
