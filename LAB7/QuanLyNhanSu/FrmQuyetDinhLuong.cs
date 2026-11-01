
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FrmQuyetDinhLuong : Form
    {
        private TextBox txtMaQD, txtLuongCu, txtLuongMoi, txtLyDo;
        private ComboBox cboNhanVien, cboLoaiQD;
        private DateTimePicker dtpNgayQD, dtpNgayApDung;
        private DataGridView dgvQuyetDinh;

        public FrmQuyetDinhLuong()
        {
            InitializeComponent();
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            Controls.Clear();

            Text = "QUẢN LÝ QUYẾT ĐỊNH LƯƠNG";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1100, 720);
            MinimumSize = new Size(1000, 650);
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10);

            Label lblTieuDe = new Label
            {
                Text = "QUẢN LÝ QUYẾT ĐỊNH LƯƠNG",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Location = new Point(320, 20)
            };
            Controls.Add(lblTieuDe);

            TaoNhan("Mã quyết định:", 30, 80);
            txtMaQD = TaoTextBox(160, 78, 220);

            TaoNhan("Nhân viên:", 430, 80);
            cboNhanVien = TaoCombo(550, 78, 240);
            cboNhanVien.Items.AddRange(new object[]
            {
                "NV001 - Nguyễn Văn An",
                "NV002 - Trần Thị Bình",
                "NV003 - Lê Văn Cường"
            });

            TaoNhan("Loại quyết định:", 30, 125);
            cboLoaiQD = TaoCombo(160, 123, 220);
            cboLoaiQD.Items.AddRange(new object[]
            {
                "Tăng lương",
                "Giảm lương",
                "Điều chỉnh lương"
            });

            TaoNhan("Lương cũ:", 430, 125);
            txtLuongCu = TaoTextBox(550, 123, 240);

            TaoNhan("Lương mới:", 30, 170);
            txtLuongMoi = TaoTextBox(160, 168, 220);

            TaoNhan("Ngày quyết định:", 430, 170);
            dtpNgayQD = TaoNgay(550, 168);

            TaoNhan("Ngày áp dụng:", 30, 215);
            dtpNgayApDung = TaoNgay(160, 213);

            TaoNhan("Lý do:", 430, 215);
            txtLyDo = TaoTextBox(550, 213, 400);
            txtLyDo.Multiline = true;
            txtLyDo.Height = 55;

            Button btnThem = TaoNut("Thêm quyết định", 30, 300, 155);
            Button btnSua = TaoNut("Sửa quyết định", 195, 300, 155);
            Button btnXoa = TaoNut("Xóa quyết định", 360, 300, 155);
            Button btnMoi = TaoNut("Làm mới", 525, 300, 120);
            Button btnTim = TaoNut("Tìm kiếm", 655, 300, 120);

            btnThem.Click += (s, e) => ThemQuyetDinh();
            btnSua.Click += (s, e) => SuaQuyetDinh();
            btnXoa.Click += (s, e) => XoaQuyetDinh();
            btnMoi.Click += (s, e) => LamMoi();
            btnTim.Click += (s, e) => TimQuyetDinh();

            dgvQuyetDinh = new DataGridView
            {
                Location = new Point(25, 360),
                Size = new Size(1030, 290),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };

            dgvQuyetDinh.Columns.Add("MaQD", "Mã quyết định");
            dgvQuyetDinh.Columns.Add("NhanVien", "Nhân viên");
            dgvQuyetDinh.Columns.Add("LoaiQD", "Loại quyết định");
            dgvQuyetDinh.Columns.Add("LuongCu", "Lương cũ");
            dgvQuyetDinh.Columns.Add("LuongMoi", "Lương mới");
            dgvQuyetDinh.Columns.Add("NgayQD", "Ngày quyết định");
            dgvQuyetDinh.Columns.Add("NgayApDung", "Ngày áp dụng");

            dgvQuyetDinh.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    NapDuLieuLenForm();
            };

            Controls.Add(dgvQuyetDinh);

            dgvQuyetDinh.Rows.Add(
                "QD001", "NV001 - Nguyễn Văn An",
                "Tăng lương", "9000000", "10000000",
                "01/01/2026", "01/02/2026"
            );

            dgvQuyetDinh.Rows.Add(
                "QD002", "NV002 - Trần Thị Bình",
                "Điều chỉnh lương", "8000000", "8500000",
                "01/10/2026", "01/11/2026"
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

        private void ThemQuyetDinh()
        {
            if (string.IsNullOrWhiteSpace(txtMaQD.Text) ||
                cboNhanVien.SelectedIndex < 0 ||
                cboLoaiQD.SelectedIndex < 0 ||
                string.IsNullOrWhiteSpace(txtLuongCu.Text) ||
                string.IsNullOrWhiteSpace(txtLuongMoi.Text))
            {
                BaoLoi("Vui lòng nhập đầy đủ thông tin quyết định.");
                return;
            }

            if (!decimal.TryParse(txtLuongCu.Text, out decimal luongCu) ||
                !decimal.TryParse(txtLuongMoi.Text, out decimal luongMoi) ||
                luongCu < 0 || luongMoi < 0)
            {
                BaoLoi("Lương phải là số hợp lệ và không được âm.");
                return;
            }

            if (cboLoaiQD.Text == "Tăng lương" && luongMoi <= luongCu)
            {
                BaoLoi("Lương mới phải lớn hơn lương cũ đối với quyết định tăng lương.");
                return;
            }

            if (cboLoaiQD.Text == "Giảm lương" && luongMoi >= luongCu)
            {
                BaoLoi("Lương mới phải nhỏ hơn lương cũ đối với quyết định giảm lương.");
                return;
            }

            foreach (DataGridViewRow row in dgvQuyetDinh.Rows)
            {
                if (string.Equals(
                    Convert.ToString(row.Cells["MaQD"].Value),
                    txtMaQD.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    BaoLoi("Mã quyết định đã tồn tại.");
                    return;
                }
            }

            dgvQuyetDinh.Rows.Add(
                txtMaQD.Text.Trim(),
                cboNhanVien.Text,
                cboLoaiQD.Text,
                luongCu.ToString("0.##"),
                luongMoi.ToString("0.##"),
                dtpNgayQD.Value.ToString("dd/MM/yyyy"),
                dtpNgayApDung.Value.ToString("dd/MM/yyyy")
            );

            MessageBox.Show("Đã thêm quyết định lương.");
            LamMoi();
        }

        private void SuaQuyetDinh()
        {
            if (dgvQuyetDinh.CurrentRow == null)
            {
                BaoLoi("Vui lòng chọn quyết định cần sửa.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaQD.Text) ||
                cboNhanVien.SelectedIndex < 0 ||
                cboLoaiQD.SelectedIndex < 0 ||
                !decimal.TryParse(txtLuongCu.Text, out decimal luongCu) ||
                !decimal.TryParse(txtLuongMoi.Text, out decimal luongMoi) ||
                luongCu < 0 || luongMoi < 0)
            {
                BaoLoi("Vui lòng kiểm tra mã quyết định, nhân viên, loại quyết định và mức lương.");
                return;
            }

            if (cboLoaiQD.Text == "Tăng lương" && luongMoi <= luongCu)
            {
                BaoLoi("Lương mới phải lớn hơn lương cũ.");
                return;
            }

            if (cboLoaiQD.Text == "Giảm lương" && luongMoi >= luongCu)
            {
                BaoLoi("Lương mới phải nhỏ hơn lương cũ.");
                return;
            }

            DataGridViewRow row = dgvQuyetDinh.CurrentRow;

            foreach (DataGridViewRow item in dgvQuyetDinh.Rows)
            {
                if (item.Index != row.Index &&
                    string.Equals(
                        Convert.ToString(item.Cells["MaQD"].Value),
                        txtMaQD.Text.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    BaoLoi("Mã quyết định đã tồn tại.");
                    return;
                }
            }

            row.Cells["MaQD"].Value = txtMaQD.Text.Trim();
            row.Cells["NhanVien"].Value = cboNhanVien.Text;
            row.Cells["LoaiQD"].Value = cboLoaiQD.Text;
            row.Cells["LuongCu"].Value = luongCu.ToString("0.##");
            row.Cells["LuongMoi"].Value = luongMoi.ToString("0.##");
            row.Cells["NgayQD"].Value = dtpNgayQD.Value.ToString("dd/MM/yyyy");
            row.Cells["NgayApDung"].Value = dtpNgayApDung.Value.ToString("dd/MM/yyyy");

            MessageBox.Show("Đã cập nhật quyết định lương.");
            LamMoi();
        }

        private void XoaQuyetDinh()
        {
            if (dgvQuyetDinh.CurrentRow == null)
            {
                BaoLoi("Vui lòng chọn quyết định cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa quyết định này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (kq == DialogResult.Yes)
            {
                dgvQuyetDinh.Rows.Remove(dgvQuyetDinh.CurrentRow);
                LamMoi();
            }
        }

        private void TimQuyetDinh()
        {
            string tuKhoa = txtMaQD.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                BaoLoi("Nhập mã quyết định để tìm kiếm.");
                return;
            }

            foreach (DataGridViewRow row in dgvQuyetDinh.Rows)
            {
                row.Visible = Convert.ToString(row.Cells["MaQD"].Value)
                    .ToLower().Contains(tuKhoa);
            }
        }

        private void NapDuLieuLenForm()
        {
            if (dgvQuyetDinh.CurrentRow == null) return;

            DataGridViewRow row = dgvQuyetDinh.CurrentRow;

            txtMaQD.Text = Convert.ToString(row.Cells["MaQD"].Value);
            cboNhanVien.SelectedItem = Convert.ToString(row.Cells["NhanVien"].Value);
            cboLoaiQD.SelectedItem = Convert.ToString(row.Cells["LoaiQD"].Value);
            txtLuongCu.Text = Convert.ToString(row.Cells["LuongCu"].Value);
            txtLuongMoi.Text = Convert.ToString(row.Cells["LuongMoi"].Value);

            if (DateTime.TryParse(
                Convert.ToString(row.Cells["NgayQD"].Value), out DateTime ngayQD))
                dtpNgayQD.Value = ngayQD;

            if (DateTime.TryParse(
                Convert.ToString(row.Cells["NgayApDung"].Value), out DateTime ngayApDung))
                dtpNgayApDung.Value = ngayApDung;
        }

        private void LamMoi()
        {
            txtMaQD.Clear();
            txtLuongCu.Clear();
            txtLuongMoi.Clear();
            txtLyDo.Clear();

            cboNhanVien.SelectedIndex = -1;
            cboLoaiQD.SelectedIndex = -1;

            dtpNgayQD.Value = DateTime.Today;
            dtpNgayApDung.Value = DateTime.Today;

            foreach (DataGridViewRow row in dgvQuyetDinh.Rows)
                row.Visible = true;

            dgvQuyetDinh.ClearSelection();
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
