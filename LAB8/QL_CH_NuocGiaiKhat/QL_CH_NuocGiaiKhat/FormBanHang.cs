using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QL_CH_NuocGiaiKhat
{
    public partial class FormBanHang : Form
    {
        // Khai báo các control ở cấp độ class để dễ dàng lấy dữ liệu
        private ComboBox cbKhachHang;
        private ComboBox cbSanPham;
        private TextBox txtSoLuong;
        private DataGridView dgv;
        private Label lblTongTien;
        private double tongTienHoaDon = 0;

        public FormBanHang()
        {
            this.Text = "Nghiệp vụ Bán Hàng";
            this.Size = new Size(850, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // 1. Header
            Label lblTitle = new Label()
            {
                Text = "LẬP HÓA ĐƠN BÁN HÀNG",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(41, 57, 85)
            };

            // 2. Panel chọn Khách hàng
            Panel pnlKhachHang = new Panel() { Dock = DockStyle.Top, Height = 50, Padding = new Padding(20) };
            pnlKhachHang.Controls.Add(new Label() { Text = "Khách hàng:", Location = new Point(20, 15), AutoSize = true });
            cbKhachHang = new ComboBox() { Location = new Point(110, 12), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            pnlKhachHang.Controls.Add(cbKhachHang);

            // 3. Panel chọn Sản phẩm
            Panel pnlInput = new Panel() { Dock = DockStyle.Top, Height = 70, Padding = new Padding(20) };

            pnlInput.Controls.Add(new Label() { Text = "Sản phẩm:", Location = new Point(20, 20), AutoSize = true });
            cbSanPham = new ComboBox() { Location = new Point(110, 17), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };

            pnlInput.Controls.Add(new Label() { Text = "Số lượng:", Location = new Point(430, 20), AutoSize = true });
            txtSoLuong = new TextBox() { Location = new Point(500, 18), Width = 100 };

            Button btnThem = new Button() { Text = "Thêm vào đơn", Location = new Point(620, 15), Width = 120, Height = 30, FlatStyle = FlatStyle.Flat, BackColor = Color.LightBlue };
            btnThem.Click += BtnThem_Click; // Gắn sự kiện click

            pnlInput.Controls.Add(cbSanPham);
            pnlInput.Controls.Add(txtSoLuong);
            pnlInput.Controls.Add(btnThem);

            // 4. Lưới hiển thị chi tiết hóa đơn (Giỏ hàng)
            dgv = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgv.Columns.Add("MaNGK", "Mã NGK");
            dgv.Columns.Add("TenNGK", "Tên Sản Phẩm");
            dgv.Columns.Add("SL", "Số Lượng");
            dgv.Columns.Add("DonGia", "Đơn Giá");
            dgv.Columns.Add("ThanhTien", "Thành Tiền");

            // 5. Panel thanh toán (Dưới cùng)
            Panel pnlBottom = new Panel() { Dock = DockStyle.Bottom, Height = 80 };
            lblTongTien = new Label() { Text = "Tổng tiền: 0 VNĐ", Location = new Point(20, 25), Font = new Font("Segoe UI", 14, FontStyle.Bold), AutoSize = true, ForeColor = Color.Red };
            Button btnThanhToan = new Button() { Text = "THANH TOÁN", Location = new Point(650, 20), Width = 150, Height = 40, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            pnlBottom.Controls.Add(lblTongTien);
            pnlBottom.Controls.Add(btnThanhToan);

            // Ráp các thành phần vào Form
            this.Controls.Add(dgv);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlInput);
            this.Controls.Add(pnlKhachHang);
            this.Controls.Add(lblTitle);

            // GỌI HÀM LẤY DỮ LIỆU TỪ DB LÊN COMBOBOX
            LoadComboboxData();
        }

        // HÀM LẤY DỮ LIỆU TỪ CSDL
        private void LoadComboboxData()
        {
            try
            {
                // 1. Load Khách Hàng
                DataTable dtKH = Db.Instance.ExecuteQuery("SELECT MaKH, HoTen FROM KhachHang");
                cbKhachHang.DataSource = dtKH;
                cbKhachHang.DisplayMember = "HoTen"; // Chữ hiển thị
                cbKhachHang.ValueMember = "MaKH";    // Giá trị ẩn bên dưới

                // 2. Load Nước Giải Khát
                // Nối tên NGK với số lượng tồn để nhân viên dễ nhìn
                DataTable dtSP = Db.Instance.ExecuteQuery("SELECT MaNGK, TenNGK + ' (Tồn: ' + CAST(SoLuongTon AS VARCHAR) + ')' AS TenHienThi, DonGiaBan, SoLuongTon FROM NuocGiaiKhat");
                cbSanPham.DataSource = dtSP;
                cbSanPham.DisplayMember = "TenHienThi";
                cbSanPham.ValueMember = "MaNGK";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
        }

        // SỰ KIỆN KHI BẤM NÚT "THÊM VÀO ĐƠN"
        private void BtnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra số lượng nhập vào có phải là số không
            if (!int.TryParse(txtSoLuong.Text, out int soLuongMua) || soLuongMua <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (> 0)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin sản phẩm đang được chọn trên ComboBox
            DataRowView rowView = (DataRowView)cbSanPham.SelectedItem;
            if (rowView != null)
            {
                string maNGK = rowView["MaNGK"].ToString();
                string tenNGK = rowView["TenHienThi"].ToString();
                double donGia = Convert.ToDouble(rowView["DonGiaBan"]);
                int tonKho = Convert.ToInt32(rowView["SoLuongTon"]);

                // Nghiệp vụ: Kiểm tra tồn kho
                if (soLuongMua > tonKho)
                {
                    MessageBox.Show($"Kho chỉ còn {tonKho} sản phẩm, không đủ để bán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tính tiền và đưa vào lưới
                double thanhTien = soLuongMua * donGia;
                dgv.Rows.Add(maNGK, tenNGK, soLuongMua, donGia, thanhTien);

                // Cộng dồn tổng tiền
                tongTienHoaDon += thanhTien;
                lblTongTien.Text = $"Tổng tiền: {tongTienHoaDon:N0} VNĐ";

                // Xóa ô nhập số lượng để nhập món tiếp theo
                txtSoLuong.Text = "";
            }
        }
    }
}