
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FrmBaoHiem : Form
    {
        private TextBox txtMaBH, txtSoSo, txtGhiChu;
        private ComboBox cboNhanVien, cboLoaiBH, cboTrangThai;
        private DateTimePicker dtpNgayCap, dtpNgayHetHan;
        private DataGridView dgvBaoHiem;

        public FrmBaoHiem()
        {
            InitializeComponent();
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            Controls.Clear();

            Text = "QUẢN LÝ BẢO HIỂM NHÂN VIÊN";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1100, 700);
            MinimumSize = new Size(1000, 650);
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10);

            Controls.Add(new Label
            {
                Text = "QUẢN LÝ BẢO HIỂM NHÂN VIÊN",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Location = new Point(300, 20)
            });

            TaoNhan("Mã bảo hiểm:", 30, 80);
            txtMaBH = TaoTextBox(150, 78, 220);

            TaoNhan("Nhân viên:", 430, 80);
            cboNhanVien = TaoCombo(550, 78, 260);
            cboNhanVien.Items.AddRange(new object[]
            {
                "NV001 - Nguyễn Văn An",
                "NV002 - Trần Thị Bình",
                "NV003 - Lê Văn Cường"
            });

            TaoNhan("Loại bảo hiểm:", 30, 125);
            cboLoaiBH = TaoCombo(150, 123, 220);
            cboLoaiBH.Items.AddRange(new object[]
            {
                "Bảo hiểm xã hội (BHXH)",
                "Bảo hiểm y tế (BHYT)",
                "Bảo hiểm thất nghiệp (BHTN)"
            });

            TaoNhan("Số sổ bảo hiểm:", 430, 125);
            txtSoSo = TaoTextBox(550, 123, 260);

            TaoNhan("Ngày cấp:", 30, 170);
            dtpNgayCap = TaoNgay(150, 168);

            TaoNhan("Ngày hết hạn:", 430, 170);
            dtpNgayHetHan = TaoNgay(550, 168);

            TaoNhan("Trạng thái:", 30, 215);
            cboTrangThai = TaoCombo(150, 213, 220);
            cboTrangThai.Items.AddRange(new object[]
            {
                "Đang tham gia",
                "Ngừng tham gia",
                "Hết hạn"
            });
            cboTrangThai.SelectedIndex = 0;

            TaoNhan("Ghi chú:", 430, 215);
            txtGhiChu = TaoTextBox(550, 213, 400);
            txtGhiChu.Multiline = true;
            txtGhiChu.Height = 55;

            Button btnThem = TaoNut("Thêm bảo hiểm", 30, 300, 145);
            Button btnSua = TaoNut("Sửa thông tin", 185, 300, 145);
            Button btnXoa = TaoNut("Xóa bảo hiểm", 340, 300, 145);
            Button btnMoi = TaoNut("Làm mới", 495, 300, 120);
            Button btnTim = TaoNut("Tìm kiếm", 625, 300, 120);

            btnThem.Click += (s, e) => ThemBaoHiem();
            btnSua.Click += (s, e) => SuaBaoHiem();
            btnXoa.Click += (s, e) => XoaBaoHiem();
            btnMoi.Click += (s, e) => LamMoi();
            btnTim.Click += (s, e) => TimBaoHiem();

            dgvBaoHiem = new DataGridView
            {
                Location = new Point(25, 360),
                Size = new Size(1030, 270),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };

            dgvBaoHiem.Columns.Add("MaBH", "Mã bảo hiểm");
            dgvBaoHiem.Columns.Add("NhanVien", "Nhân viên");
            dgvBaoHiem.Columns.Add("LoaiBH", "Loại bảo hiểm");
            dgvBaoHiem.Columns.Add("SoSo", "Số sổ");
            dgvBaoHiem.Columns.Add("NgayCap", "Ngày cấp");
            dgvBaoHiem.Columns.Add("NgayHetHan", "Ngày hết hạn");
            dgvBaoHiem.Columns.Add("TrangThai", "Trạng thái");

            dgvBaoHiem.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    NapDuLieuLenForm();
            };

            Controls.Add(dgvBaoHiem);

            dgvBaoHiem.Rows.Add(
                "BH001", "NV001 - Nguyễn Văn An",
                "Bảo hiểm xã hội (BHXH)", "BH123456",
                "01/01/2026", "31/12/2026", "Đang tham gia"
            );

            dgvBaoHiem.Rows.Add(
                "BH002", "NV002 - Trần Thị Bình",
                "Bảo hiểm y tế (BHYT)", "YT789012",
                "01/03/2026", "28/02/2027", "Đang tham gia"
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

        private void ThemBaoHiem()
        {
            if (string.IsNullOrWhiteSpace(txtMaBH.Text) ||
                cboNhanVien.SelectedIndex < 0 ||
                cboLoaiBH.SelectedIndex < 0 ||
                string.IsNullOrWhiteSpace(txtSoSo.Text))
            {
                BaoLoi("Vui lòng nhập đầy đủ mã bảo hiểm, nhân viên, loại bảo hiểm và số sổ.");
                return;
            }

            if (dtpNgayHetHan.Value.Date < dtpNgayCap.Value.Date)
            {
                BaoLoi("Ngày hết hạn không được trước ngày cấp.");
                return;
            }

            foreach (DataGridViewRow row in dgvBaoHiem.Rows)
            {
                if (string.Equals(
                    Convert.ToString(row.Cells["MaBH"].Value),
                    txtMaBH.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    BaoLoi("Mã bảo hiểm đã tồn tại.");
                    return;
                }
            }

            dgvBaoHiem.Rows.Add(
                txtMaBH.Text.Trim(),
                cboNhanVien.Text,
                cboLoaiBH.Text,
                txtSoSo.Text.Trim(),
                dtpNgayCap.Value.ToString("dd/MM/yyyy"),
                dtpNgayHetHan.Value.ToString("dd/MM/yyyy"),
                cboTrangThai.Text
            );

            MessageBox.Show("Đã thêm thông tin bảo hiểm.");
            LamMoi();
        }

        private void SuaBaoHiem()
        {
            if (dgvBaoHiem.CurrentRow == null)
            {
                BaoLoi("Vui lòng chọn thông tin bảo hiểm cần sửa.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaBH.Text) ||
                cboNhanVien.SelectedIndex < 0 ||
                cboLoaiBH.SelectedIndex < 0 ||
                string.IsNullOrWhiteSpace(txtSoSo.Text))
            {
                BaoLoi("Vui lòng nhập đầy đủ thông tin bảo hiểm.");
                return;
            }

            if (dtpNgayHetHan.Value.Date < dtpNgayCap.Value.Date)
            {
                BaoLoi("Ngày hết hạn không được trước ngày cấp.");
                return;
            }

            DataGridViewRow row = dgvBaoHiem.CurrentRow;

            foreach (DataGridViewRow item in dgvBaoHiem.Rows)
            {
                if (item.Index != row.Index &&
                    string.Equals(
                        Convert.ToString(item.Cells["MaBH"].Value),
                        txtMaBH.Text.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    BaoLoi("Mã bảo hiểm đã tồn tại.");
                    return;
                }
            }

            row.Cells["MaBH"].Value = txtMaBH.Text.Trim();
            row.Cells["NhanVien"].Value = cboNhanVien.Text;
            row.Cells["LoaiBH"].Value = cboLoaiBH.Text;
            row.Cells["SoSo"].Value = txtSoSo.Text.Trim();
            row.Cells["NgayCap"].Value = dtpNgayCap.Value.ToString("dd/MM/yyyy");
            row.Cells["NgayHetHan"].Value = dtpNgayHetHan.Value.ToString("dd/MM/yyyy");
            row.Cells["TrangThai"].Value = cboTrangThai.Text;

            MessageBox.Show("Đã cập nhật thông tin bảo hiểm.");
            LamMoi();
        }

        private void XoaBaoHiem()
        {
            if (dgvBaoHiem.CurrentRow == null)
            {
                BaoLoi("Vui lòng chọn thông tin bảo hiểm cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa thông tin bảo hiểm này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (kq == DialogResult.Yes)
            {
                dgvBaoHiem.Rows.Remove(dgvBaoHiem.CurrentRow);
                LamMoi();
            }
        }

        private void TimBaoHiem()
        {
            string tuKhoa = txtMaBH.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                BaoLoi("Nhập mã bảo hiểm để tìm kiếm.");
                return;
            }

            foreach (DataGridViewRow row in dgvBaoHiem.Rows)
            {
                row.Visible = Convert.ToString(row.Cells["MaBH"].Value)
                    .ToLower().Contains(tuKhoa);
            }
        }

        private void NapDuLieuLenForm()
        {
            if (dgvBaoHiem.CurrentRow == null) return;

            DataGridViewRow row = dgvBaoHiem.CurrentRow;

            txtMaBH.Text = Convert.ToString(row.Cells["MaBH"].Value);
            cboNhanVien.SelectedItem = Convert.ToString(row.Cells["NhanVien"].Value);
            cboLoaiBH.SelectedItem = Convert.ToString(row.Cells["LoaiBH"].Value);
            txtSoSo.Text = Convert.ToString(row.Cells["SoSo"].Value);
            cboTrangThai.SelectedItem = Convert.ToString(row.Cells["TrangThai"].Value);

            if (DateTime.TryParse(
                Convert.ToString(row.Cells["NgayCap"].Value), out DateTime ngayCap))
                dtpNgayCap.Value = ngayCap;

            if (DateTime.TryParse(
                Convert.ToString(row.Cells["NgayHetHan"].Value), out DateTime ngayHetHan))
                dtpNgayHetHan.Value = ngayHetHan;
        }

        private void LamMoi()
        {
            txtMaBH.Clear();
            txtSoSo.Clear();
            txtGhiChu.Clear();

            cboNhanVien.SelectedIndex = -1;
            cboLoaiBH.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = 0;

            dtpNgayCap.Value = DateTime.Today;
            dtpNgayHetHan.Value = DateTime.Today;

            foreach (DataGridViewRow row in dgvBaoHiem.Rows)
                row.Visible = true;

            dgvBaoHiem.ClearSelection();
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
