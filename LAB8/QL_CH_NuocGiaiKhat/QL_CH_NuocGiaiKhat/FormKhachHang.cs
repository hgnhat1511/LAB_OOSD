using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QL_CH_NuocGiaiKhat
{
    public partial class FormKhachHang : Form
    {
        // Phải đưa dgv ra ngoài hàm khởi tạo để hàm LoadKhachHang có thể truy cập được
        private DataGridView dgv;

        public FormKhachHang()
        {
            this.Text = "Quản Lý Khách Hàng";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            Label lblTitle = new Label()
            {
                Text = "DANH MỤC KHÁCH HÀNG",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(41, 57, 85)
            };

            TableLayoutPanel tlpInput = new TableLayoutPanel() { Dock = DockStyle.Top, Height = 120, ColumnCount = 4 };
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            tlpInput.Controls.Add(new Label() { Text = "Mã KH:", Anchor = AnchorStyles.Left }, 0, 0);
            tlpInput.Controls.Add(new TextBox() { Width = 200 }, 1, 0);
            tlpInput.Controls.Add(new Label() { Text = "Họ Tên:", Anchor = AnchorStyles.Left }, 2, 0);
            tlpInput.Controls.Add(new TextBox() { Width = 200 }, 3, 0);

            tlpInput.Controls.Add(new Label() { Text = "Điện Thoại:", Anchor = AnchorStyles.Left }, 0, 1);
            tlpInput.Controls.Add(new TextBox() { Width = 200 }, 1, 1);
            tlpInput.Controls.Add(new Label() { Text = "Loại Khách:", Anchor = AnchorStyles.Left }, 2, 1);

            ComboBox cbLoai = new ComboBox() { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cbLoai.Items.AddRange(new string[] { "Quen", "Vãng lai" });
            cbLoai.SelectedIndex = 0;
            tlpInput.Controls.Add(cbLoai, 3, 1);

            tlpInput.Controls.Add(new Label() { Text = "Địa Chỉ:", Anchor = AnchorStyles.Left }, 0, 2);
            tlpInput.Controls.Add(new TextBox() { Width = 550 }, 1, 2);
            tlpInput.SetColumnSpan(tlpInput.GetControlFromPosition(1, 2), 3);

            Panel pnlAction = new Panel() { Dock = DockStyle.Top, Height = 50 };
            pnlAction.Controls.Add(new Button() { Text = "Thêm", Location = new Point(20, 10), FlatStyle = FlatStyle.Flat });
            pnlAction.Controls.Add(new Button() { Text = "Sửa", Location = new Point(120, 10), FlatStyle = FlatStyle.Flat });
            pnlAction.Controls.Add(new Button() { Text = "Xóa", Location = new Point(220, 10), FlatStyle = FlatStyle.Flat });

            // KHỞI TẠO LƯỚI
            dgv = new DataGridView() { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White, ReadOnly = true };

            // QUAN TRỌNG: Để true để nó tự nhận diện cột từ câu SQL
            dgv.AutoGenerateColumns = true;
            dgv.AllowUserToAddRows = false;

            this.Controls.Add(dgv);
            this.Controls.Add(pnlAction);
            this.Controls.Add(tlpInput);
            this.Controls.Add(lblTitle);

            // QUAN TRỌNG NHẤT: Bắt buộc phải gọi hàm này thì dữ liệu mới lên
            LoadKhachHang();
        }

        // HÀM KẾT NỐI VÀ LẤY DỮ LIỆU TỪ CSDL
        private void LoadKhachHang()
        {
            try
            {
                // Lấy 4 cột từ bảng KhachHang
                string query = "SELECT MaKH AS [Mã KH], HoTen AS [Họ Tên], SoDienThoai AS [Điện Thoại], LoaiKhach AS [Loại Khách] FROM KhachHang";

                // Gọi lớp Db
                DataTable data = Db.Instance.ExecuteQuery(query);

                // Đổ vào lưới
                dgv.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị dữ liệu: " + ex.Message);
            }
        }
    }
}