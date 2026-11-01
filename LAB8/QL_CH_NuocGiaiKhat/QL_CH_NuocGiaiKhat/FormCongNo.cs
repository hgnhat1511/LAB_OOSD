using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QL_CH_NuocGiaiKhat
{
    public partial class FormCongNo : Form
    {
        // Khai báo toàn cục để dễ thao tác
        private DataGridView dgv;
        private ComboBox cbKhach;
        private TextBox txtSoTien;
        private DateTimePicker dtpHanTra;
        private TextBox txtLyDo;
        private TextBox txtSoHD; // Thêm ô nhập Số Hóa Đơn

        public FormCongNo()
        {
            this.Text = "Quản Lý Công Nợ";
            this.Size = new Size(850, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            Label lblTitle = new Label()
            {
                Text = "PHIẾU GHI NỢ (KHÁCH QUEN)",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(41, 57, 85)
            };

            Panel pnlInput = new Panel() { Dock = DockStyle.Top, Height = 130 };

            // --- DÒNG 1 ---
            Label lblKhach = new Label() { Text = "Khách Hàng:", Location = new Point(20, 20), AutoSize = true };
            cbKhach = new ComboBox() { Location = new Point(110, 18), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblSoTien = new Label() { Text = "Số Tiền Nợ:", Location = new Point(310, 20), AutoSize = true };
            txtSoTien = new TextBox() { Location = new Point(390, 18), Width = 130 };

            Label lblHanTra = new Label() { Text = "Hạn Trả:", Location = new Point(540, 20), AutoSize = true };
            dtpHanTra = new DateTimePicker() { Location = new Point(610, 18), Width = 130, Format = DateTimePickerFormat.Short };

            // --- DÒNG 2 ---
            Label lblSoHD = new Label() { Text = "Số Hóa Đơn:", Location = new Point(20, 70), AutoSize = true };
            txtSoHD = new TextBox() { Location = new Point(110, 68), Width = 100 };

            Label lblLyDo = new Label() { Text = "Lý Do:", Location = new Point(230, 70), AutoSize = true };
            txtLyDo = new TextBox() { Location = new Point(280, 68), Width = 240 };

            Button btnGhiNo = new Button() { Text = "Ghi Nợ", Location = new Point(540, 65), Width = 200, Height = 35, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, Cursor = Cursors.Hand };
            btnGhiNo.Click += BtnGhiNo_Click; // Gắn sự kiện thêm mới

            pnlInput.Controls.AddRange(new Control[] {
                lblKhach, cbKhach, lblSoTien, txtSoTien, lblHanTra, dtpHanTra,
                lblSoHD, txtSoHD, lblLyDo, txtLyDo, btnGhiNo
            });

            // --- LƯỚI DỮ LIỆU ---
            dgv = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoGenerateColumns = true // Tự sinh cột từ SQL
            };

            this.Controls.Add(dgv);
            this.Controls.Add(pnlInput);
            this.Controls.Add(lblTitle);

            // GỌI HÀM LOAD DỮ LIỆU
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // 1. Chỉ load Khách Quen vào ComboBox (Nghiệp vụ: Vãng lai không được nợ)
                string queryKhach = "SELECT MaKH, HoTen FROM KhachHang WHERE LoaiKhach = N'Quen'";
                DataTable dtKhach = Db.Instance.ExecuteQuery(queryKhach);
                cbKhach.DataSource = dtKhach;
                cbKhach.DisplayMember = "HoTen";
                cbKhach.ValueMember = "MaKH";

                // 2. Load danh sách các Phiếu Nợ hiện có
                string queryPhieuNo = @"
                    SELECT 
                        P.SoPGN AS [Số Phiếu], 
                        P.SoHD AS [Số Hóa Đơn], 
                        K.HoTen AS [Khách Hàng], 
                        P.SoTienNo AS [Tiền Nợ], 
                        P.NgayDuKienTra AS [Hạn Trả], 
                        P.TrangThai AS [Trạng Thái]
                    FROM PhieuGhiNo P
                    JOIN KhachHang K ON P.MaKH = K.MaKH";

                dgv.DataSource = Db.Instance.ExecuteQuery(queryPhieuNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void BtnGhiNo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoHD.Text) || string.IsNullOrWhiteSpace(txtSoTien.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Số Hóa Đơn và Số Tiền Nợ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tự động tạo Số Phiếu Ghi Nợ ngẫu nhiên (Ví dụ: PGN + tick)
                string soPGN = "PGN" + DateTime.Now.Ticks.ToString().Substring(10);
                string soHD = txtSoHD.Text.Trim();
                string maKH = cbKhach.SelectedValue.ToString();
                double soTien = Convert.ToDouble(txtSoTien.Text);
                string ngayTra = dtpHanTra.Value.ToString("yyyy-MM-dd");
                string lyDo = txtLyDo.Text;
                string ngayLap = DateTime.Now.ToString("yyyy-MM-dd");
                string maNV = "NV01"; // Mặc định NV01 lập (thực tế lấy từ phiên đăng nhập)

                string query = "INSERT INTO PhieuGhiNo (SoPGN, NgayLap, SoHD, MaKH, SoTienNo, NgayDuKienTra, LyDo, MaNV, TrangThai) " +
                               "VALUES (@SoPGN, @NgayLap, @SoHD, @MaKH, @SoTienNo, @NgayDuKienTra, @LyDo, @MaNV, N'Chưa trả')";

                object[] param = { soPGN, ngayLap, soHD, maKH, soTien, ngayTra, lyDo, maNV };

                int result = Db.Instance.ExecuteNonQuery(query, param);
                if (result > 0)
                {
                    MessageBox.Show("Lập phiếu ghi nợ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Load lại lưới
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm công nợ (Hãy chắc chắn Số Hóa Đơn này tồn tại và thuộc về Khách Hàng này!):\n" + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}