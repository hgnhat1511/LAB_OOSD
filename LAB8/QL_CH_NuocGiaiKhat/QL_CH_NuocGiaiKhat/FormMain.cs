using System;
using System.Drawing;
using System.Windows.Forms;

namespace QL_CH_NuocGiaiKhat
{
    // Bắt buộc phải có 'partial' và ': Form' để không bị lỗi Dispose hoặc lỗi override
    public partial class FormMain : Form
    {
        public FormMain()
        {
            // Thiết lập Form cơ bản
            this.Text = "Hệ thống quản lý cửa hàng nước giải khát";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // 1. Tạo Tiêu đề (Header)
            Label lblTitle = new Label()
            {
                Text = "HỆ THỐNG QUẢN LÝ NƯỚC GIẢI KHÁT",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 57, 85),
                Dock = DockStyle.Top,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitle);

            // 2. Tạo Lưới chứa các nút (TableLayoutPanel)
            TableLayoutPanel grid = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                Padding = new Padding(20) // Khoảng cách viền
            };

            // Chia đều tỷ lệ % cho 2 cột và 4 dòng
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < 4; i++)
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

            // 3. Khởi tạo và gán các nút vào lưới (Đã nối sẵn với các Form con)
            grid.Controls.Add(CreateMenuButton("Quản lý danh mục", (s, e) => new FormKhachHang().ShowDialog()), 0, 0);
            grid.Controls.Add(CreateMenuButton("Bán hàng", (s, e) => new FormBanHang().ShowDialog()), 1, 0);

            grid.Controls.Add(CreateMenuButton("Lập đơn đặt hàng", (s, e) => new FormLapDDH().ShowDialog()), 0, 1);
            grid.Controls.Add(CreateMenuButton("Nhận hàng (Phiếu giao)", (s, e) => new FormPhieuGiao().ShowDialog()), 1, 1);

            grid.Controls.Add(CreateMenuButton("Lập phiếu hẹn", (s, e) => new FormPhieuHen().ShowDialog()), 0, 2);
            grid.Controls.Add(CreateMenuButton("Giao hàng theo hẹn", (s, e) => new FormGiaoHangHen().ShowDialog()), 1, 2);

            grid.Controls.Add(CreateMenuButton("Quản lý công nợ", (s, e) => new FormCongNo().ShowDialog()), 0, 3);
            grid.Controls.Add(CreateMenuButton("Thoát", (s, e) => Application.Exit()), 1, 3);

            // Thêm lưới vào form (Lưới sẽ nằm dưới Tiêu đề vì Tiêu đề đã Dock.Top)
            this.Controls.Add(grid);
            grid.BringToFront();
        }

        // Hàm hỗ trợ tạo nút bấm theo chuẩn Flat Design tối giản
        private Button CreateMenuButton(string text, EventHandler clickAction)
        {
            Button btn = new Button()
            {
                Text = text,
                Dock = DockStyle.Fill, // Tự động lấp đầy ô trong lưới
                Margin = new Padding(15), // Khoảng cách giữa các nút
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.LightGray;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240); // Đổi màu xám nhạt khi hover

            if (clickAction != null)
                btn.Click += clickAction;

            return btn;
        }
    }
}