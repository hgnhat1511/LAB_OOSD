using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QL_CH_NuocGiaiKhat
{
    public partial class FormPhieuGiao : Form
    {
        // Khai báo control ở cấp độ class
        private TextBox txtMaDDH;
        private TextBox txtDotGiao;
        private DataGridView dgv;

        public FormPhieuGiao()
        {
            this.Text = "Nhận Hàng Từ NCC";
            this.Size = new Size(950, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            Label lblTitle = new Label()
            {
                Text = "LẬP PHIẾU GIAO HÀNG (NHẬN HÀNG TỪ NCC)",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(41, 57, 85)
            };

            // --- PANEL TÌM KIẾM ---
            Panel pnlSearch = new Panel() { Dock = DockStyle.Top, Height = 80 };
            Label lblMaDDH = new Label() { Text = "Nhập Mã ĐĐH:", Location = new Point(20, 30), AutoSize = true };
            txtMaDDH = new TextBox() { Location = new Point(130, 28), Width = 200 };

            Button btnTim = new Button() { Text = "Tìm Đơn", Location = new Point(350, 25), Width = 100, Height = 30, FlatStyle = FlatStyle.Flat, BackColor = Color.White };
            btnTim.FlatAppearance.BorderColor = Color.Gray;
            btnTim.Click += BtnTim_Click; // Gắn sự kiện Tìm đơn

            Label lblDotGiao = new Label() { Text = "Đợt Giao Thứ:", Location = new Point(500, 30), AutoSize = true };
            txtDotGiao = new TextBox() { Location = new Point(600, 28), Width = 80, ReadOnly = true, Text = "" };

            pnlSearch.Controls.AddRange(new Control[] { lblMaDDH, txtMaDDH, btnTim, lblDotGiao, txtDotGiao });

            // --- PANEL DƯỚI CÙNG (LƯU) ---
            Panel pnlBottom = new Panel() { Dock = DockStyle.Bottom, Height = 60 };
            Button btnLuu = new Button() { Text = "Xác Nhận Nhận Hàng", Location = new Point(730, 10), Width = 180, Height = 40, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White };
            btnLuu.Click += BtnLuu_Click; // Gắn sự kiện Lưu
            pnlBottom.Controls.Add(btnLuu);

            // --- DATAGRIDVIEW ---
            dgv = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };
            dgv.Columns.Add("MaNGK", "Mã NGK");
            dgv.Columns.Add("TenNGK", "Tên Sản Phẩm");
            dgv.Columns.Add("SLDat", "Tổng SL Đặt");
            dgv.Columns.Add("SLDaGiao", "Đã Giao Trước");

            // 2 cột này user sẽ gõ vào
            dgv.Columns.Add("SLThucNhan", "SL Nhận Đợt Này");
            dgv.Columns.Add("DonGiaNhap", "Đơn Giá Nhập (VNĐ)");

            dgv.Columns["MaNGK"].ReadOnly = true;
            dgv.Columns["TenNGK"].ReadOnly = true;
            dgv.Columns["SLDat"].ReadOnly = true;
            dgv.Columns["SLDaGiao"].ReadOnly = true;

            // Đổi màu để user biết ô nào được nhập
            dgv.Columns["SLThucNhan"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Columns["DonGiaNhap"].DefaultCellStyle.BackColor = Color.LightYellow;

            this.Controls.Add(dgv);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlSearch);
            this.Controls.Add(lblTitle);
        }

        // 1. SỰ KIỆN TÌM ĐƠN ĐẶT HÀNG
        private void BtnTim_Click(object sender, EventArgs e)
        {
            string soDDH = txtMaDDH.Text.Trim();
            if (string.IsNullOrEmpty(soDDH)) return;

            // Kiểm tra đơn hàng có tồn tại không
            string checkQuery = "SELECT TrangThai FROM DonDatHang WHERE SoDDH = @SoDDH";
            DataTable dtCheck = Db.Instance.ExecuteQuery(checkQuery, new object[] { soDDH });

            if (dtCheck.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy Đơn Đặt Hàng này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dtCheck.Rows[0]["TrangThai"].ToString() == "Đã nhận đủ")
            {
                MessageBox.Show("Đơn Đặt Hàng này đã được nhận ĐỦ hàng. Không thể giao thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Tính xem đây là đợt giao thứ mấy (Max đợt giao cũ + 1)
            string dotGiaoQuery = "SELECT ISNULL(MAX(DotGiao), 0) + 1 FROM PhieuGiaoHang WHERE SoDDH = @SoDDH";
            DataTable dtDot = Db.Instance.ExecuteQuery(dotGiaoQuery, new object[] { soDDH });
            int dotGiao = Convert.ToInt32(dtDot.Rows[0][0]);

            if (dotGiao > 3)
            {
                MessageBox.Show("Đơn hàng này đã vượt quá số đợt giao cho phép (Tối đa 3 đợt)!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            txtDotGiao.Text = dotGiao.ToString();

            // Load chi tiết mặt hàng cần giao (Tính toán luôn số lượng đã giao ở các đợt trước)
            string detailQuery = @"
                SELECT 
                    C.MaNGK, 
                    N.TenNGK, 
                    C.SoLuongDat, 
                    ISNULL((SELECT SUM(SoLuongGiao) FROM ChiTietPGH CP JOIN PhieuGiaoHang P ON CP.SoPGH = P.SoPGH WHERE P.SoDDH = C.SoDDH AND CP.MaNGK = C.MaNGK), 0) AS SoLuongDaGiao
                FROM ChiTietDDH C
                JOIN NuocGiaiKhat N ON C.MaNGK = N.MaNGK
                WHERE C.SoDDH = @SoDDH";

            DataTable dtDetails = Db.Instance.ExecuteQuery(detailQuery, new object[] { soDDH });
            dgv.Rows.Clear();

            foreach (DataRow row in dtDetails.Rows)
            {
                int slDat = Convert.ToInt32(row["SoLuongDat"]);
                int slDaGiao = Convert.ToInt32(row["SoLuongDaGiao"]);

                // Chỉ hiển thị những mặt hàng còn thiếu (chưa giao đủ)
                if (slDat > slDaGiao)
                {
                    // Tham số cuối là SLThucNhan (mặc định 0) và DonGiaNhap (mặc định 0)
                    dgv.Rows.Add(row["MaNGK"], row["TenNGK"], slDat, slDaGiao, 0, 0);
                }
            }
        }

        // 2. SỰ KIỆN XÁC NHẬN LƯU PHIẾU GIAO HÀNG
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0 || string.IsNullOrEmpty(txtDotGiao.Text))
            {
                MessageBox.Show("Chưa có thông tin đơn hàng để nhận!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string soDDH = txtMaDDH.Text.Trim();
            int dotGiao = Convert.ToInt32(txtDotGiao.Text);
            string soPGH = "PGH" + DateTime.Now.Ticks.ToString().Substring(11);
            string ngayGiao = DateTime.Now.ToString("yyyy-MM-dd");
            double tongTienPhieu = 0;
            bool coHangThucNhan = false;

            // Xây dựng chuỗi SQL thực thi Transaction
            string sqlInsertPhieu = "";
            string sqlInsertChiTiet = "";

            foreach (DataGridViewRow row in dgv.Rows)
            {
                string maNGK = row.Cells["MaNGK"].Value.ToString();
                int slDat = Convert.ToInt32(row.Cells["SLDat"].Value);
                int slDaGiao = Convert.ToInt32(row.Cells["SLDaGiao"].Value);

                // Lấy giá trị user nhập vào, nếu nhập sai chữ thì gán bằng 0
                int.TryParse(row.Cells["SLThucNhan"].Value?.ToString(), out int slNhan);
                double.TryParse(row.Cells["DonGiaNhap"].Value?.ToString(), out double donGiaNhap);

                if (slNhan > 0)
                {
                    if (slDaGiao + slNhan > slDat)
                    {
                        MessageBox.Show($"Mặt hàng {maNGK} nhận thừa! Chỉ còn thiếu {slDat - slDaGiao} sản phẩm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    coHangThucNhan = true;
                    tongTienPhieu += (slNhan * donGiaNhap);

                    // 1. Lưu Chi Tiết Phiếu Giao
                    sqlInsertChiTiet += $"INSERT INTO ChiTietPGH (SoPGH, MaNGK, SoLuongGiao, DonGiaNhap) VALUES ('{soPGH}', '{maNGK}', {slNhan}, {donGiaNhap});\n";

                    // 2. Tăng số lượng tồn kho trong bảng Nước Giải Khát
                    sqlInsertChiTiet += $"UPDATE NuocGiaiKhat SET SoLuongTon = SoLuongTon + {slNhan} WHERE MaNGK = '{maNGK}';\n";
                }
            }

            if (!coHangThucNhan)
            {
                MessageBox.Show("Vui lòng nhập Số Lượng Nhận (>0) cho ít nhất 1 mặt hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo câu lệnh Insert phiếu giao (chạy đầu tiên)
            sqlInsertPhieu = $"INSERT INTO PhieuGiaoHang (SoPGH, SoDDH, DotGiao, NgayGiao, TongTien) VALUES ('{soPGH}', '{soDDH}', {dotGiao}, '{ngayGiao}', {tongTienPhieu});\n";

            // Nối chuỗi
            string finalSql = sqlInsertPhieu + sqlInsertChiTiet;

            try
            {
                int result = Db.Instance.ExecuteNonQuery(finalSql);
                if (result > 0)
                {
                    MessageBox.Show($"Nhận hàng thành công! Đã tăng tồn kho.\nMã phiếu giao: {soPGH}\nTổng tiền: {tongTienPhieu:N0} VNĐ", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgv.Rows.Clear();
                    txtMaDDH.Clear();
                    txtDotGiao.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật CSDL: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}