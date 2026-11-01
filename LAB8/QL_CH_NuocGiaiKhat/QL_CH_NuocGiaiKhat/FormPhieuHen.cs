using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QL_CH_NuocGiaiKhat
{
    public partial class FormPhieuHen : Form
    {
        // Khai báo các control ở cấp độ class
        private TextBox txtSoHD;
        private DateTimePicker dtpNgayHen;
        private DataGridView dgv;

        public FormPhieuHen()
        {
            this.Text = "Lập Phiếu Hẹn Giao Hàng";
            this.Size = new Size(850, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // --- HEADER ---
            Label lblTitle = new Label()
            {
                Text = "LẬP PHIẾU HẸN GIAO HÀNG",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(41, 57, 85)
            };

            // --- PANEL TÌM KIẾM & THÔNG TIN ---
            Panel pnlTop = new Panel() { Dock = DockStyle.Top, Height = 100 };

            Label lblSoHD = new Label() { Text = "Nhập Số Hóa Đơn:", Location = new Point(20, 20), AutoSize = true };
            txtSoHD = new TextBox() { Location = new Point(140, 18), Width = 150 };

            Button btnTim = new Button() { Text = "Tìm Hóa Đơn", Location = new Point(300, 16), FlatStyle = FlatStyle.Flat, BackColor = Color.White };
            btnTim.Click += BtnTim_Click; // Gắn sự kiện TÌM

            Label lblNgayHen = new Label() { Text = "Ngày Hẹn Giao:", Location = new Point(20, 60), AutoSize = true };
            dtpNgayHen = new DateTimePicker() { Location = new Point(140, 58), Width = 150, Format = DateTimePickerFormat.Short };

            pnlTop.Controls.AddRange(new Control[] { lblSoHD, txtSoHD, btnTim, lblNgayHen, dtpNgayHen });

            // --- GRID VIEW CHI TIẾT HÀNG THIẾU ---
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
            dgv.Columns.Add("SLMua", "SL Mua");
            dgv.Columns.Add("SLDaGiao", "Đã Giao");
            dgv.Columns.Add("SLThieu", "Còn Thiếu (chưa hẹn)");

            // Cột này user sẽ nhập vào
            dgv.Columns.Add("SLHenGiao", "SL Hẹn Đợt Này");

            dgv.Columns["MaNGK"].ReadOnly = true;
            dgv.Columns["TenNGK"].ReadOnly = true;
            dgv.Columns["SLMua"].ReadOnly = true;
            dgv.Columns["SLDaGiao"].ReadOnly = true;
            dgv.Columns["SLThieu"].ReadOnly = true;
            dgv.Columns["SLHenGiao"].DefaultCellStyle.BackColor = Color.LightYellow;

            // --- PANEL BUTTON ---
            Panel pnlBottom = new Panel() { Dock = DockStyle.Bottom, Height = 60 };
            Button btnLuu = new Button() { Text = "Lưu Phiếu Hẹn", Location = new Point(680, 10), Width = 130, Height = 35, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(243, 156, 18), ForeColor = Color.White };
            btnLuu.Click += BtnLuu_Click; // Gắn sự kiện LƯU
            pnlBottom.Controls.Add(btnLuu);

            this.Controls.Add(dgv);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlTop);
            this.Controls.Add(lblTitle);
        }

        // 1. SỰ KIỆN TÌM HÓA ĐƠN
        private void BtnTim_Click(object sender, EventArgs e)
        {
            string soHD = txtSoHD.Text.Trim();
            if (string.IsNullOrEmpty(soHD)) return;

            // Nghiệp vụ 1: Kiểm tra hóa đơn tồn tại và KHÁCH QUEN
            string checkQuery = @"
                SELECT H.TrangThai, K.LoaiKhach, H.NgayLap 
                FROM HoaDon H JOIN KhachHang K ON H.MaKH = K.MaKH 
                WHERE H.SoHD = @SoHD";

            DataTable dtCheck = Db.Instance.ExecuteQuery(checkQuery, new object[] { soHD });

            if (dtCheck.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy Hóa Đơn này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dtCheck.Rows[0]["LoaiKhach"].ToString() != "Quen")
            {
                MessageBox.Show("Chỉ được lập Phiếu Hẹn cho KHÁCH QUEN!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (dtCheck.Rows[0]["TrangThai"].ToString() == "Đã giao đủ")
            {
                MessageBox.Show("Hóa Đơn này đã được giao ĐỦ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Ép ngày hẹn giao không được trước ngày lập hóa đơn
            DateTime ngayLap = Convert.ToDateTime(dtCheck.Rows[0]["NgayLap"]);
            dtpNgayHen.MinDate = ngayLap;

            // Nghiệp vụ 2: Tính toán số lượng còn thiếu = SL Mua - SL Đã Giao - SL Đã hẹn trước đó (nhưng chưa giao)
            string detailQuery = @"
                SELECT 
                    C.MaNGK, 
                    N.TenNGK, 
                    C.SoLuong, 
                    C.SoLuongDaGiao,
                    ISNULL((
                        SELECT SUM(CP.SoLuongHen) 
                        FROM ChiTietPhieuHen CP JOIN PhieuHenGiao P ON CP.SoPHG = P.SoPHG 
                        WHERE P.SoHD = C.SoHD AND CP.MaNGK = C.MaNGK AND P.TrangThai = N'Chưa giao'
                    ), 0) AS SoLuongDaHenTruocDo
                FROM ChiTietHoaDon C
                JOIN NuocGiaiKhat N ON C.MaNGK = N.MaNGK
                WHERE C.SoHD = @SoHD";

            DataTable dtDetails = Db.Instance.ExecuteQuery(detailQuery, new object[] { soHD });
            dgv.Rows.Clear();

            foreach (DataRow row in dtDetails.Rows)
            {
                int slMua = Convert.ToInt32(row["SoLuong"]);
                int slDaGiao = Convert.ToInt32(row["SoLuongDaGiao"]);
                int slDaHenTruoc = Convert.ToInt32(row["SoLuongDaHenTruocDo"]);

                int slThieu = slMua - slDaGiao - slDaHenTruoc;

                // Chỉ đưa lên lưới những món CÒN THIẾU
                if (slThieu > 0)
                {
                    // Tham số cuối là SLHenGiao (mặc định để trống hoặc 0)
                    dgv.Rows.Add(row["MaNGK"], row["TenNGK"], slMua, slDaGiao, slThieu, 0);
                }
            }

            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Tất cả các mặt hàng thiếu của Hóa đơn này đều đã được lập phiếu hẹn rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 2. SỰ KIỆN LƯU PHIẾU HẸN
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có thông tin hàng hóa để hẹn giao!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string soHD = txtSoHD.Text.Trim();
            string soPHG = "PHG" + DateTime.Now.Ticks.ToString().Substring(11);
            string ngayHen = dtpNgayHen.Value.ToString("yyyy-MM-dd");
            bool coHangHen = false;

            string sqlInsertPhieu = $"INSERT INTO PhieuHenGiao (SoPHG, SoHD, NgayHen, TrangThai) VALUES ('{soPHG}', '{soHD}', '{ngayHen}', N'Chưa giao');\n";
            string sqlInsertChiTiet = "";

            foreach (DataGridViewRow row in dgv.Rows)
            {
                string maNGK = row.Cells["MaNGK"].Value.ToString();
                int slThieu = Convert.ToInt32(row.Cells["SLThieu"].Value);

                int.TryParse(row.Cells["SLHenGiao"].Value?.ToString(), out int slHen);

                if (slHen > 0)
                {
                    if (slHen > slThieu)
                    {
                        MessageBox.Show($"Mặt hàng {maNGK} hẹn thừa! Khách chỉ còn thiếu {slThieu} sản phẩm (đã trừ các đợt hẹn trước).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    coHangHen = true;
                    sqlInsertChiTiet += $"INSERT INTO ChiTietPhieuHen (SoPHG, MaNGK, SoLuongHen) VALUES ('{soPHG}', '{maNGK}', {slHen});\n";
                }
            }

            if (!coHangHen)
            {
                MessageBox.Show("Vui lòng nhập Số Lượng Hẹn (>0) cho ít nhất 1 mặt hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string finalSql = sqlInsertPhieu + sqlInsertChiTiet;

            try
            {
                int result = Db.Instance.ExecuteNonQuery(finalSql);
                if (result > 0)
                {
                    MessageBox.Show($"Lập phiếu hẹn giao thành công!\nMã phiếu: {soPHG}", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgv.Rows.Clear();
                    txtSoHD.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu Phiếu hẹn vào CSDL: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}