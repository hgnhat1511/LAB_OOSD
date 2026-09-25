using System;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Forms
{
    public partial class FrmThongKe : Form
    {
        private readonly ThongKeService service = new ThongKeService();

        public FrmThongKe()
        {
            InitializeComponent();

            // Tự động gán sự kiện
            this.Load += FrmThongKe_Load;
            this.btnThongKe.Click += btnThongKe_Click;
            this.btnDong.Click += btnDong_Click;
        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            // Mặc định lấy từ đầu tháng đến ngày hiện tại
            dtTu.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtDen.Value = DateTime.Today;
            TaiDuLieu();
        }

        private void TaiDuLieu()
        {
            ThongKeTongHop t = service.LayTongHop(dtTu.Value, dtDen.Value);
            lblMuon.Text = "Lượt sách mượn: " + t.LuotSachMuon;
            lblQuaHan.Text = "Sách quá hạn: " + t.SachQuaHan;
            lblMat.Text = "Sách mất: " + t.SachMat;
            lblHuHong.Text = "Sách hư hỏng: " + t.SachHuHong;
            lblPhiPhat.Text = "Tổng phí phạt: " + t.TongPhiPhat.ToString("N0") + " đ";

            dgvPhat.DataSource = service.LayChiTietPhat(dtTu.Value, dtDen.Value);
            dgvPhat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnThongKe_Click(object sender, EventArgs e) { TaiDuLieu(); }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}