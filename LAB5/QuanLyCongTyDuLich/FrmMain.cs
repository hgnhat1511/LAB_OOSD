
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            TaoGiaoDien();
            KiemTraDatabase();
        }
        private void KiemTraDatabase()
        {
            if (DatabaseConnection.KiemTraKetNoi())
            {
                MessageBox.Show("Kết nối database thành công!");
            }
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý công ty du lịch";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(950, 540);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.WhiteSmoke;

            Controls.Clear();

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "HỆ THỐNG QUẢN LÝ CÔNG TY DU LỊCH";
            lblTieuDe.Font = new Font("Arial", 15, FontStyle.Bold);
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Location = new Point(150, 25);
            lblTieuDe.Size = new Size(650, 40);
            Controls.Add(lblTieuDe);

            // Cột bên trái
            TaoNut("Quản lý tour du lịch", 190, 110,
                () => MoForm("Tour"));

            TaoNut("Quản lý khách hàng", 190, 220,
                () => MoForm("Khách hàng"));

            TaoNut("Đăng ký tour", 190, 330,
                () => MoForm("Đăng ký tour"));

            // Cột bên phải
            TaoNut("Phân công hướng dẫn viên", 580, 110,
                () => MoForm("Phân công hướng dẫn viên"));

            TaoNut("Quản lý thanh toán", 580, 220,
                () => MoForm("Thanh toán"));

            TaoNut("Thoát", 580, 330, () =>
            {
                DialogResult kq = MessageBox.Show(
                    "Bạn có muốn thoát chương trình không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (kq == DialogResult.Yes)
                    Close();
            });
        }

        private void TaoNut(string noiDung, int x, int y,
            Action suKien)
        {
            Button btn = new Button();
            btn.Text = noiDung;
            btn.Location = new Point(x, y);
            btn.Size = new Size(230, 80);
            btn.Font = new Font("Arial", 11, FontStyle.Regular);
            btn.UseVisualStyleBackColor = true;
            btn.Click += (sender, e) => suKien();

            Controls.Add(btn);
        }



        private void MoForm(string tenForm)
        {
            if (tenForm == "Tour")
            {
                using (FrmTour frm = new FrmTour())
                    frm.ShowDialog(this);
            }
            else if (tenForm == "Khách hàng")
            {
                using (FrmKhachHang frm = new FrmKhachHang())
                    frm.ShowDialog(this);
            }
            else if (tenForm == "Đăng ký tour")
            {
                using (FrmDangKyTour frm = new FrmDangKyTour())
                    frm.ShowDialog(this);
            }
            else if (tenForm == "Phân công hướng dẫn viên")
            {
                using (FrmPhanCongHuongDanVien frm =
                    new FrmPhanCongHuongDanVien())
                {
                    frm.ShowDialog(this);
                }
            }

            else if (tenForm == "Thanh toán")
            {
                using (FrmThanhToan frm = new FrmThanhToan())
                {
                    frm.ShowDialog(this);
                }
            }
            else
            {
                MessageBox.Show(
                    "Form " + tenForm + " sẽ được xây dựng ở bước tiếp theo.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }


    }
}
