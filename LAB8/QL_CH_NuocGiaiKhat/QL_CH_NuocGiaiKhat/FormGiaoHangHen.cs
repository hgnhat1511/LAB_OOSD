using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QL_CH_NuocGiaiKhat
{
    public partial class FormGiaoHangHen : Form
    {
        // Khai báo các control ở cấp class để dễ dàng gọi trong các hàm sự kiện
        private DateTimePicker dtpLocNgay;
        private DataGridView dgvPhieu;
        private DataGridView dgvChiTiet;

        public FormGiaoHangHen()
        {
            this.Text = "Xử Lý Giao Hàng Theo Hẹn";
            this.Size = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            Label lblTitle = new Label()
            {
                Text = "GIAO HÀNG THEO PHIẾU HẸN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(41, 57, 85)
            };

            Panel pnlTop = new Panel() { Dock = DockStyle.Top, Height = 70 };
            Label lblNgay = new Label() { Text = "Chọn Ngày Hẹn:", Location = new Point(20, 25), AutoSize = true };
            dtpLocNgay = new DateTimePicker() { Location = new Point(130, 23), Width = 150, Format = DateTimePickerFormat.Short };

            Button btnLoc = new Button() { Text = "Lọc Phiếu", Location = new Point(290, 20), FlatStyle = FlatStyle.Flat };
            btnLoc.Click += BtnLoc_Click; // Gắn sự kiện lọc phiếu

            pnlTop.Controls.AddRange(new Control[] { lblNgay, dtpLocNgay, btnLoc });

            // Chia đôi màn hình: Nửa trên danh sách Phiếu, Nửa dưới Chi tiết
            SplitContainer splitContainer = new SplitContainer() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal };

            // Lưới hiển thị Phiếu Hẹn
            dgvPhieu = new DataGridView() { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgvPhieu.AutoGenerateColumns = true; // Tự động lấy cột từ CSDL
            dgvPhieu.AllowUserToAddRows = false;
            dgvPhieu.SelectionChanged += DgvPhieu_SelectionChanged; // Gắn sự kiện click dòng

            // Lưới hiển thị Chi Tiết Phiếu Hẹn
            dgvChiTiet = new DataGridView() { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.WhiteSmoke, ReadOnly = true };
            dgvChiTiet.AutoGenerateColumns = true; // Tự động lấy cột từ CSDL
            dgvChiTiet.AllowUserToAddRows = false;

            splitContainer.Panel1.Controls.Add(dgvPhieu);
            splitContainer.Panel2.Controls.Add(dgvChiTiet);

            Panel pnlBottom = new Panel() { Dock = DockStyle.Bottom, Height = 60 };
            Button btnXacNhan = new Button() { Text = "Xác Nhận Đã Giao", Location = new Point(650, 10), Width = 160, Height = 40, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White };
            btnXacNhan.Click += BtnXacNhan_Click; // Gắn sự kiện xác nhận giao hàng
            pnlBottom.Controls.Add(btnXacNhan);

            this.Controls.Add(splitContainer);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlTop);
            this.Controls.Add(lblTitle);

            // Load danh sách phiếu hẹn của ngày hiện tại ngay khi mở form
            LoadPhieuHen(dtpLocNgay.Value.ToString("yyyy-MM-dd"));
        }

        // HÀM LỌC PHIẾU THEO NGÀY
        private void BtnLoc_Click(object sender, EventArgs e)
        {
            LoadPhieuHen(dtpLocNgay.Value.ToString("yyyy-MM-dd"));
        }

        // HÀM LẤY DANH SÁCH PHIẾU HẸN TỪ CSDL
        private void LoadPhieuHen(string ngayHen)
        {
            string query = @"
                SELECT P.SoPHG AS [Số Phiếu Hẹn], P.SoHD AS [Số Hóa Đơn], K.HoTen AS [Khách Hàng], P.TrangThai AS [Trạng Thái]
                FROM PhieuHenGiao P
                JOIN HoaDon H ON P.SoHD = H.SoHD
                JOIN KhachHang K ON H.MaKH = K.MaKH
                WHERE P.NgayHen = @NgayHen";

            DataTable dt = Db.Instance.ExecuteQuery(query, new object[] { ngayHen });
            dgvPhieu.DataSource = dt;

            if (dt.Rows.Count == 0)
            {
                dgvChiTiet.DataSource = null; // Xóa lưới chi tiết nếu không có phiếu nào
            }
        }

        // SỰ KIỆN KHI BẤM CHỌN 1 PHIẾU HẸN Ở LƯỚI TRÊN
        private void DgvPhieu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhieu.CurrentRow != null)
            {
                string soPHG = dgvPhieu.CurrentRow.Cells["Số Phiếu Hẹn"].Value.ToString();

                string queryChiTiet = @"
                    SELECT C.MaNGK AS [Mã NGK], N.TenNGK AS [Tên Hàng], C.SoLuongHen AS [Số Lượng Hẹn], N.SoLuongTon AS [Tồn Kho Hiện Tại]
                    FROM ChiTietPhieuHen C
                    JOIN NuocGiaiKhat N ON C.MaNGK = N.MaNGK
                    WHERE C.SoPHG = @SoPHG";

                DataTable dtChiTiet = Db.Instance.ExecuteQuery(queryChiTiet, new object[] { soPHG });
                dgvChiTiet.DataSource = dtChiTiet;
            }
        }

        // SỰ KIỆN KHI BẤM NÚT "XÁC NHẬN ĐÃ GIAO"
        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if (dgvPhieu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu hẹn để xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trangThai = dgvPhieu.CurrentRow.Cells["Trạng Thái"].Value.ToString();
            if (trangThai == "Đã giao")
            {
                MessageBox.Show("Phiếu hẹn này đã được giao rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string soPHG = dgvPhieu.CurrentRow.Cells["Số Phiếu Hẹn"].Value.ToString();

            // Kiểm tra xem kho có đủ hàng để giao đợt này không
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                int slHen = Convert.ToInt32(row.Cells["Số Lượng Hẹn"].Value);
                int tonKho = Convert.ToInt32(row.Cells["Tồn Kho Hiện Tại"].Value);
                string tenHang = row.Cells["Tên Hàng"].Value.ToString();

                if (tonKho < slHen)
                {
                    MessageBox.Show($"Mặt hàng '{tenHang}' chỉ còn {tonKho} trong kho, không đủ để giao {slHen}!\nVui lòng nhập thêm hàng trước khi giao.", "Cảnh báo Tồn Kho", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Thực thi toàn bộ Logic Cập Nhật trong 1 khối lệnh SQL
            string updateSql = @"
                -- 1. Trừ tồn kho
                UPDATE N SET N.SoLuongTon = N.SoLuongTon - C.SoLuongHen
                FROM NuocGiaiKhat N JOIN ChiTietPhieuHen C ON N.MaNGK = C.MaNGK
                WHERE C.SoPHG = @SoPHG;

                -- 2. Cộng số lượng đã giao vào chi tiết hóa đơn gốc
                UPDATE CT SET CT.SoLuongDaGiao = CT.SoLuongDaGiao + C.SoLuongHen
                FROM ChiTietHoaDon CT 
                JOIN ChiTietPhieuHen C ON CT.MaNGK = C.MaNGK
                JOIN PhieuHenGiao P ON C.SoPHG = P.SoPHG AND CT.SoHD = P.SoHD
                WHERE C.SoPHG = @SoPHG;

                -- 3. Cập nhật trạng thái phiếu hẹn
                UPDATE PhieuHenGiao SET TrangThai = N'Đã giao' WHERE SoPHG = @SoPHG;
                
                -- 4. Kiểm tra hóa đơn gốc nếu đã giao đủ thì đóng hóa đơn
                UPDATE H SET H.TrangThai = N'Đã giao đủ'
                FROM HoaDon H
                WHERE H.SoHD = (SELECT SoHD FROM PhieuHenGiao WHERE SoPHG = @SoPHG)
                AND NOT EXISTS (
                    SELECT 1 FROM ChiTietHoaDon CT WHERE CT.SoHD = H.SoHD AND CT.SoLuong > CT.SoLuongDaGiao
                );";

            int result = Db.Instance.ExecuteNonQuery(updateSql, new object[] { soPHG });
            if (result > 0)
            {
                MessageBox.Show("Xác nhận giao hàng thành công! Đã cập nhật tồn kho và hóa đơn.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Load lại danh sách
                LoadPhieuHen(dtpLocNgay.Value.ToString("yyyy-MM-dd"));
            }
        }
    }
}