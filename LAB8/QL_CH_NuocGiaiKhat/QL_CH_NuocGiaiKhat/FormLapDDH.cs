using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QL_CH_NuocGiaiKhat
{
    public partial class FormLapDDH : Form
    {
        // Khai báo các control ở cấp độ class
        private ComboBox cbNCC;
        private DateTimePicker dtpNgayNhan;
        private ComboBox cbNGK;
        private TextBox txtSL;
        private DataGridView dgv;

        public FormLapDDH()
        {
            this.Text = "Lập Đơn Đặt Hàng";
            this.Size = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // --- HEADER ---
            Label lblTitle = new Label()
            {
                Text = "LẬP ĐƠN ĐẶT HÀNG (NHẬP HÀNG TỪ NCC)",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(41, 57, 85)
            };

            // --- PANEL THÔNG TIN ĐƠN & NHẬP LIỆU ---
            Panel pnlInput = new Panel() { Dock = DockStyle.Top, Height = 120, Padding = new Padding(10) };

            Label lblNCC = new Label() { Text = "Nhà Cung Ứng:", Location = new Point(20, 20), AutoSize = true };
            cbNCC = new ComboBox() { Location = new Point(130, 18), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            // Gắn sự kiện để khi đổi NCC, tự động load lại danh sách NGK
            cbNCC.SelectedIndexChanged += CbNCC_SelectedIndexChanged;

            Label lblNgayNhan = new Label() { Text = "Ngày Nhận (DK):", Location = new Point(360, 20), AutoSize = true };
            dtpNgayNhan = new DateTimePicker() { Location = new Point(480, 18), Width = 150, Format = DateTimePickerFormat.Short };

            Label lblNGK = new Label() { Text = "Sản Phẩm:", Location = new Point(20, 70), AutoSize = true };
            cbNGK = new ComboBox() { Location = new Point(130, 68), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblSL = new Label() { Text = "Số Lượng:", Location = new Point(360, 70), AutoSize = true };
            txtSL = new TextBox() { Location = new Point(480, 68), Width = 150 };

            Button btnThem = CreateFlatButton("Thêm Vào Đơn", new Point(660, 60), 120);
            btnThem.Click += BtnThem_Click;

            pnlInput.Controls.AddRange(new Control[] { lblNCC, cbNCC, lblNgayNhan, dtpNgayNhan, lblNGK, cbNGK, lblSL, txtSL, btnThem });

            // --- PANEL CHỨC NĂNG (Dưới cùng) ---
            Panel pnlBottom = new Panel() { Dock = DockStyle.Bottom, Height = 60 };
            Button btnLuu = CreateFlatButton("Lưu Đơn Đặt Hàng", new Point(630, 10), 180);
            btnLuu.BackColor = Color.FromArgb(46, 204, 113);
            btnLuu.ForeColor = Color.White;
            btnLuu.Click += BtnLuu_Click;
            pnlBottom.Controls.Add(btnLuu);

            // --- GRID VIEW HIỂN THỊ DANH SÁCH MẶT HÀNG TRONG ĐƠN ---
            dgv = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true
            };
            dgv.Columns.Add("MaNGK", "Mã NGK");
            dgv.Columns.Add("TenNGK", "Tên Nước Giải Khát");
            dgv.Columns.Add("SL", "Số Lượng Đặt");

            // --- ADD VÀO FORM ---
            this.Controls.Add(dgv);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlInput);
            this.Controls.Add(lblTitle);

            // --- LOAD DỮ LIỆU ---
            LoadNhaCungUng();
        }

        private Button CreateFlatButton(string text, Point loc, int width)
        {
            Button btn = new Button() { Text = text, Location = loc, Width = width, Height = 35, FlatStyle = FlatStyle.Flat, BackColor = Color.White, Cursor = Cursors.Hand };
            btn.FlatAppearance.BorderColor = Color.Gray;
            return btn;
        }

        // 1. TẢI DANH SÁCH NHÀ CUNG ỨNG VÀO COMBOBOX
        private void LoadNhaCungUng()
        {
            string query = "SELECT MaNCU, TenNCU FROM NhaCungUng";
            DataTable dt = Db.Instance.ExecuteQuery(query);
            cbNCC.DataSource = dt;
            cbNCC.DisplayMember = "TenNCU";
            cbNCC.ValueMember = "MaNCU";
        }

        // 2. KHI ĐỔI NHÀ CUNG ỨNG, LỌC LẠI DANH SÁCH NƯỚC GIẢI KHÁT
        private void CbNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xóa lưới nếu đổi nhà cung ứng (vì mỗi đơn chỉ đặt cho 1 NCC)
            dgv.Rows.Clear();

            if (cbNCC.SelectedValue != null)
            {
                // Ép kiểu an toàn (vì SelectedValue ban đầu có thể trả về DataRowView)
                string maNCU = "";
                if (cbNCC.SelectedValue is DataRowView rowView)
                    maNCU = rowView["MaNCU"].ToString();
                else
                    maNCU = cbNCC.SelectedValue.ToString();

                // Lọc NGK thông qua bảng trung gian LoaiNGK
                string query = @"
                    SELECT N.MaNGK, N.TenNGK 
                    FROM NuocGiaiKhat N
                    JOIN LoaiNGK L ON N.MaLoai = L.MaLoai
                    WHERE L.MaNCU = @MaNCU";

                DataTable dt = Db.Instance.ExecuteQuery(query, new object[] { maNCU });
                cbNGK.DataSource = dt;
                cbNGK.DisplayMember = "TenNGK";
                cbNGK.ValueMember = "MaNGK";
            }
        }

        // 3. THÊM VÀO LƯỚI (GIỎ HÀNG ẢO)
        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (cbNGK.SelectedValue == null) return;

            if (!int.TryParse(txtSL.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên lớn hơn 0!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ép kiểu lấy thông tin NGK đang chọn
            DataRowView row = (DataRowView)cbNGK.SelectedItem;
            string maNGK = row["MaNGK"].ToString();
            string tenNGK = row["TenNGK"].ToString();

            // Kiểm tra xem đã thêm mã này vào lưới chưa, nếu có thì cộng dồn SL
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                if (dgvRow.Cells["MaNGK"].Value.ToString() == maNGK)
                {
                    int slCu = Convert.ToInt32(dgvRow.Cells["SL"].Value);
                    dgvRow.Cells["SL"].Value = slCu + soLuong;
                    txtSL.Clear();
                    return;
                }
            }

            // Nếu chưa có thì thêm dòng mới
            dgv.Rows.Add(maNGK, tenNGK, soLuong);
            txtSL.Clear();
        }

        // 4. LƯU ĐƠN ĐẶT HÀNG VÀO CSDL
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Đơn hàng phải có ít nhất 1 mặt hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tự sinh mã ĐĐH (Ví dụ: DDH + số ngẫu nhiên)
                string soDDH = "DDH" + DateTime.Now.Ticks.ToString().Substring(11);
                string ngayDat = DateTime.Now.ToString("yyyy-MM-dd");
                string ngayNhan = dtpNgayNhan.Value.ToString("yyyy-MM-dd");
                string maNCU = cbNCC.SelectedValue.ToString();
                string maNV = "NV01"; // Mặc định NV01 (có thể thay đổi nếu có tính năng đăng nhập)

                // Lưu ý: Dùng một chuỗi SQL duy nhất để gom việc INSERT Đơn + INSERT Chi Tiết vào 1 lần chạy
                string sql = "INSERT INTO DonDatHang (SoDDH, NgayDat, NgayNhan, TrangThai, MaNCU, MaNV) " +
                             $"VALUES ('{soDDH}', '{ngayDat}', '{ngayNhan}', N'Chưa giao', '{maNCU}', '{maNV}');\n";

                // Chạy vòng lặp để tạo các câu lệnh INSERT chi tiết
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    string maNGK = row.Cells["MaNGK"].Value.ToString();
                    string sl = row.Cells["SL"].Value.ToString();

                    sql += $"INSERT INTO ChiTietDDH (SoDDH, MaNGK, SoLuongDat) VALUES ('{soDDH}', '{maNGK}', {sl});\n";
                }

                // Thực thi toàn bộ lệnh
                int result = Db.Instance.ExecuteNonQuery(sql);

                if (result > 0)
                {
                    MessageBox.Show("Lập đơn đặt hàng thành công! Mã đơn: " + soDDH, "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgv.Rows.Clear(); // Làm sạch form
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu đơn hàng: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}