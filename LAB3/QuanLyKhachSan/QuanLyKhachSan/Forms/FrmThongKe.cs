using System;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmThongKe : Form
    {
        readonly ThongKeService s = new ThongKeService();

        public FrmThongKe()
        {
            InitializeComponent();

            // Thiết lập ngày mặc định: Từ ngày 1 của tháng hiện tại đến hôm nay
            dtTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtDenNgay.Value = DateTime.Now;

            // GÁN SỰ KIỆN TỰ ĐỘNG
            btnThongKe.Click += btnThongKe_Click;

            // Tự động tải dữ liệu lần đầu khi mở Form
            TaiDuLieu();
        }

        void TaiDuLieu()
        {
            // Đổ dữ liệu vào 2 bảng từ Service
            dgvDoanhThu.DataSource = s.ThongKeDoanhThu(dtTuNgay.Value, dtDenNgay.Value);
            dgvTanSuat.DataSource = s.ThongKeTanSuatPhong(dtTuNgay.Value, dtDenNgay.Value);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            // Kiểm tra lỗi nếu chọn ngày sai logic
            if (dtTuNgay.Value.Date > dtDenNgay.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Cảnh báo");
                return;
            }

            // Gọi hàm tải lại dữ liệu theo mốc thời gian mới
            TaiDuLieu();
        }
    }
}