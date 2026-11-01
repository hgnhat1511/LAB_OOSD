
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            DungGiaoDien();
        }

        private void DungGiaoDien()
        {
            Text = "Hệ thống quản lý nhân sự";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(850, 580);
            MinimumSize = new Size(850, 580);
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10);

            Label lblTieuDe = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ NHÂN SỰ",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 55, 90),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 100
            };
            Controls.Add(lblTieuDe);

            TableLayoutPanel menu = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 5,
                Dock = DockStyle.Fill,
                Padding = new Padding(55, 15, 55, 35),
                BackColor = BackColor
            };

            menu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            menu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < 5; i++)
                menu.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            Controls.Add(menu);
            menu.BringToFront();
            lblTieuDe.BringToFront();

            ThemNut(menu, "Quản lý nhân viên", 0, 0,
                () => MoForm<FrmNhanVien>());

            ThemNut(menu, "Quản lý phòng ban", 1, 0,
                () => MoForm<FrmPhongBan>());

            ThemNut(menu, "Quản lý chức vụ", 0, 1,
                () => MoForm<FrmChucVu>());

            ThemNut(menu, "Hợp đồng lao động", 1, 1,
                () => MoForm<FrmHopDong>());

            ThemNut(menu, "Quyết định lương", 0, 2,
                () => MoForm<FrmQuyetDinhLuong>());

            ThemNut(menu, "Quyết định chức vụ", 1, 2,
                () => MoForm<FrmQuyetDinhChucVu>());

            ThemNut(menu, "Quản lý bảo hiểm", 0, 3,
                () => MoForm<FrmBaoHiem>());

            ThemNut(menu, "Quyết định tuyển dụng", 1, 3,
                () => MoForm<FrmQuyetDinhTuyenDung>());

            ThemNut(menu, "Phiếu thu / Phiếu chi", 0, 4,
                () => MoForm<FrmThanhToan>());

            ThemNut(menu, "Thoát", 1, 4, ThoatChuongTrinh);
        }

        private void ThemNut(
            TableLayoutPanel menu,
            string noiDung,
            int cot,
            int hang,
            Action xuLy)
        {
            Button btn = new Button
            {
                Text = noiDung,
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(40, 50, 65),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderColor =
                Color.FromArgb(210, 215, 225);

            btn.FlatAppearance.BorderSize = 1;
            btn.Click += (sender, e) => xuLy();

            menu.Controls.Add(btn, cot, hang);
        }

        private void MoForm<T>() where T : Form, new()
        {
            using (T form = new T())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
            }
        }

        private void ThoatChuongTrinh()
        {
            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc muốn thoát chương trình?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
                Close();
        }
    }
}
