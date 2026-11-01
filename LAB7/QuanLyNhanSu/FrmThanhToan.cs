
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FrmThanhToan : Form
    {
        private ComboBox cboLoaiPhieu;
        private ComboBox cboQuyetDinh;
        private ComboBox cboNhanVien;
        private ComboBox cboHinhThuc;
        private TextBox txtSoTien;
        private TextBox txtLyDo;
        private DateTimePicker dtpNgayLap;
        private DataGridView dgvPhieu;
        private int soThuTu = 1;

        public FrmThanhToan()
        {
            InitializeComponent();
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            Controls.Clear();

            Text = "Quản lý phiếu chi / phiếu thu - Công ty sơn Đông Á";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1050, 680);
            MinimumSize = new Size(900, 600);
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.White;

            Label lblTieuDe = new Label
            {
                Text = "QUẢN LÝ PHIẾU CHI / PHIẾU THU",
                Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 12),
                Size = new Size(1030, 38),
                Anchor = AnchorStyles.Top | AnchorStyles.Left |
                         AnchorStyles.Right
            };
            Controls.Add(lblTieuDe);

            Controls.Add(TaoNhan("Loại phiếu", 25, 68, 120));

            cboLoaiPhieu = TaoCombo(150, 68, 350);
            cboLoaiPhieu.Items.AddRange(new object[]
            {
                "Phiếu chi khen thưởng",
                "Phiếu thu nộp phạt"
            });
            cboLoaiPhieu.SelectedIndex = 0;
            Controls.Add(cboLoaiPhieu);

            Controls.Add(TaoNhan("Quyết định", 530, 68, 100));

            cboQuyetDinh = TaoCombo(635, 68, 380);
            cboQuyetDinh.Items.AddRange(new object[]
            {
                "-- Chọn quyết định --",
                "QDKT001 - Khen thưởng",
                "QDKT002 - Khen thưởng",
                "QDKL001 - Kỷ luật",
                "QDKL002 - Kỷ luật"
            });
            cboQuyetDinh.SelectedIndex = 0;
            Controls.Add(cboQuyetDinh);

            Controls.Add(TaoNhan("Nhân viên", 25, 110, 120));

            cboNhanVien = TaoCombo(150, 110, 350);
            cboNhanVien.Items.AddRange(new object[]
            {
                "-- Chọn nhân viên --",
                "NV001 - Nguyễn Văn An",
                "NV002 - Trần Thị Bình",
                "NV003 - Lê Văn Cường"
            });
            cboNhanVien.SelectedIndex = 0;
            Controls.Add(cboNhanVien);

            Controls.Add(TaoNhan("Ngày lập", 530, 110, 100));

            dtpNgayLap = new DateTimePicker
            {
                Location = new Point(635, 110),
                Size = new Size(380, 27),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };
            Controls.Add(dtpNgayLap);

            Controls.Add(TaoNhan("Số tiền (VNĐ)", 25, 152, 120));

            txtSoTien = new TextBox
            {
                Location = new Point(150, 152),
                Size = new Size(350, 27)
            };
            Controls.Add(txtSoTien);

            Controls.Add(TaoNhan("Hình thức", 530, 152, 100));

            cboHinhThuc = TaoCombo(635, 152, 380);
            cboHinhThuc.Items.AddRange(new object[]
            {
                "Tiền mặt",
                "Chuyển khoản",
                "Trừ vào bảng lương"
            });
            cboHinhThuc.SelectedIndex = 0;
            Controls.Add(cboHinhThuc);

            Controls.Add(TaoNhan("Lý do / ghi chú", 25, 194, 120));

            txtLyDo = new TextBox
            {
                Location = new Point(150, 194),
                Size = new Size(865, 48),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(txtLyDo);

            Button btnGhi = TaoNut("Ghi nhận", 25, 258, 110);
            Button btnLamMoi = TaoNut("Làm mới", 145, 258, 110);
            Button btnXoa = TaoNut("Xóa dòng", 265, 258, 110);
            Button btnTim = TaoNut("Tìm kiếm", 385, 258, 110);

            Controls.AddRange(new Control[]
            {
                btnGhi, btnLamMoi, btnXoa, btnTim
            });

            dgvPhieu = new DataGridView
            {
                Location = new Point(20, 305),
                Size = new Size(1005, 350),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            dgvPhieu.Columns.Add("MaPhieu", "Mã phiếu");
            dgvPhieu.Columns.Add("LoaiPhieu", "Loại phiếu");
            dgvPhieu.Columns.Add("QuyetDinh", "Quyết định");
            dgvPhieu.Columns.Add("NhanVien", "Nhân viên");
            dgvPhieu.Columns.Add("SoTien", "Số tiền");
            dgvPhieu.Columns.Add("NgayLap", "Ngày lập");
            dgvPhieu.Columns.Add("HinhThuc", "Hình thức");
            dgvPhieu.Columns.Add("TrangThai", "Trạng thái");

            Controls.Add(dgvPhieu);

            btnGhi.Click += (s, e) => GhiNhan();
            btnLamMoi.Click += (s, e) => LamMoi();
            btnXoa.Click += (s, e) => XoaDong();
            btnTim.Click += (s, e) => TimKiem();
            cboLoaiPhieu.SelectedIndexChanged +=
                (s, e) => CapNhatLoaiPhieu();
        }

        private void GhiNhan()
        {
            if (cboQuyetDinh.SelectedIndex <= 0)
            {
                BaoLoi("Vui lòng chọn quyết định.");
                return;
            }

            if (cboNhanVien.SelectedIndex <= 0)
            {
                BaoLoi("Vui lòng chọn nhân viên.");
                return;
            }

            if (!decimal.TryParse(
                txtSoTien.Text.Trim(), out decimal soTien)
                || soTien <= 0)
            {
                BaoLoi("Số tiền phải lớn hơn 0.");
                txtSoTien.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLyDo.Text))
            {
                BaoLoi("Vui lòng nhập lý do hoặc ghi chú.");
                txtLyDo.Focus();
                return;
            }

            bool laPhieuChi = cboLoaiPhieu.SelectedIndex == 0;

            if (laPhieuChi &&
                cboQuyetDinh.Text.StartsWith("QDKL"))
            {
                BaoLoi("Phiếu chi khen thưởng phải gắn với quyết định khen thưởng.");
                return;
            }

            if (!laPhieuChi &&
                cboQuyetDinh.Text.StartsWith("QDKT"))
            {
                BaoLoi("Phiếu thu nộp phạt phải gắn với quyết định kỷ luật.");
                return;
            }

            string maPhieu =
                (laPhieuChi ? "PC" : "PT") +
                soThuTu.ToString("D3");

            dgvPhieu.Rows.Add(
                maPhieu,
                cboLoaiPhieu.Text,
                cboQuyetDinh.Text,
                cboNhanVien.Text,
                soTien.ToString("N0"),
                dtpNgayLap.Value.ToString("dd/MM/yyyy"),
                cboHinhThuc.Text,
                "Đã ghi nhận"
            );

            soThuTu++;

            MessageBox.Show(
                "Đã ghi nhận phiếu vào bảng tạm.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            LamMoi();
        }

        private void LamMoi()
        {
            cboQuyetDinh.SelectedIndex = 0;
            cboNhanVien.SelectedIndex = 0;
            txtSoTien.Clear();
            txtLyDo.Clear();
            dtpNgayLap.Value = DateTime.Today;
            cboHinhThuc.SelectedIndex = 0;
            txtSoTien.Focus();

            foreach (DataGridViewRow row in dgvPhieu.Rows)
                row.Visible = true;
        }

        private void XoaDong()
        {
            if (dgvPhieu.SelectedRows.Count == 0)
            {
                BaoLoi("Vui lòng chọn dòng cần xóa.");
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc muốn xóa dòng đã chọn?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (ketQua == DialogResult.Yes)
                dgvPhieu.Rows.RemoveAt(
                    dgvPhieu.SelectedRows[0].Index);
        }

        private void TimKiem()
        {
            string tuKhoa = txtLyDo.Text.Trim();

            foreach (DataGridViewRow row in dgvPhieu.Rows)
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

        private void CapNhatLoaiPhieu()
        {
            if (cboLoaiPhieu.SelectedIndex == 0)
            {
                cboHinhThuc.Items.Remove("Trừ vào bảng lương");
            }
            else if (!cboHinhThuc.Items.Contains("Trừ vào bảng lương"))
            {
                cboHinhThuc.Items.Add("Trừ vào bảng lương");
            }

            if (cboHinhThuc.Items.Count > 0)
                cboHinhThuc.SelectedIndex = 0;
        }

        private Label TaoNhan(
            string noiDung, int x, int y, int width)
        {
            return new Label
            {
                Text = noiDung,
                Location = new Point(x, y),
                Size = new Size(width, 28),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private ComboBox TaoCombo(int x, int y, int width)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        private Button TaoNut(
            string noiDung, int x, int y, int width)
        {
            return new Button
            {
                Text = noiDung,
                Location = new Point(x, y),
                Size = new Size(width, 30),
                Cursor = Cursors.Hand
            };
        }

        private void BaoLoi(string noiDung)
        {
            MessageBox.Show(
                noiDung,
                "Kiểm tra dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
    }
}
