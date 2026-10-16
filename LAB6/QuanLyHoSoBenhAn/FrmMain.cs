
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            Controls.Clear();
            Text = "Quản lý hồ sơ bệnh án";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(900, 600);

            Label title = new Label();
            title.Text = "HỆ THỐNG QUẢN LÝ HỒ SƠ BỆNH ÁN";
            title.Font = new Font("Arial", 18, FontStyle.Bold);
            title.SetBounds(70, 35, 760, 50);
            title.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(title);

            string[] names = {
                "Quản lý bệnh nhân", "Hồ sơ bệnh án",
                "Phiếu khám", "Toa thuốc",
                "Xét nghiệm", "Thanh toán",
                "Lịch khám định kỳ", "Thoát"
            };

            for (int i = 0; i < names.Length; i++)
            {
                Button btn = new Button();
                btn.Text = names[i];
                btn.SetBounds(120 + (i % 2) * 350,
                    120 + (i / 2) * 90, 280, 55);

                string ten = names[i];
                btn.Click += delegate { MoChucNang(ten); };
                Controls.Add(btn);
            }
        }

        private void MoChucNang(string ten)
        {
            if (ten == "Thoát")
            {
                Close();
                return;
            }

            Form f = null;

            switch (ten)
            {
                case "Quản lý bệnh nhân":
                    f = new FrmBenhNhan();
                    break;
                case "Hồ sơ bệnh án":
                    f = new FrmHoSoBenhAn();
                    break;
                case "Phiếu khám":
                    f = new FrmPhieuKham();
                    break;
                case "Toa thuốc":
                    f = new FrmToaThuoc();
                    break;
                case "Xét nghiệm":
                    f = new FrmXetNghiem();
                    break;
                case "Thanh toán":
                    f = new FrmThanhToan();
                    break;
                case "Lịch khám định kỳ":
                    f = new FrmLichKhamDinhKy();
                    break;
            }

            if (f != null)
            {
                using (f)
                {
                    f.ShowDialog(this);
                }
            }
        }
    }
}
