using System;
using System.Data;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmTraPhong : Form
    {
        readonly TraPhongService s = new TraPhongService();
        readonly DanhMucService dm = new DanhMucService();

        // Các biến lưu trữ tạm thời để tính toán
        decimal tienPhong = 0;
        decimal tienDichVu = 0;
        decimal tienCoc = 0;
        int soNgay = 0;

        public FrmTraPhong()
        {
            InitializeComponent();

            // Khởi tạo dữ liệu
            ThietLapBanDau();

            // TỰ ĐỘNG GÁN SỰ KIỆN TẠI ĐÂY
            btnTinhTien.Click += btnTinhTien_Click;
            btnThanhToan.Click += btnThanhToan_Click;
            dgvPhong.SelectionChanged += dgvPhong_SelectionChanged;
        }

        private void ThietLapBanDau()
        {
            cboNV.DataSource = dm.LayNhanVien();
            cboNV.DisplayMember = "HoTen";
            cboNV.ValueMember = "MaNV";

            Tai();
        }

        void Tai()
        {
            dgvPhong.DataSource = s.LayPhongDangO();
            XoaTrangThongTin();
        }

        void XoaTrangThongTin()
        {
            txtSoNgay.Text = "";
            txtTienPhong.Text = "";
            txtTienDichVu.Text = "";
            txtTienCoc.Text = "";
            txtTongTien.Text = "";
            tienPhong = 0;
            tienDichVu = 0;
            tienCoc = 0;
            soNgay = 0;
        }

        private void dgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhong.CurrentRow == null) return;

            string soPhieuDat = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhieuDat"].Value);
            string soPhong = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhong"].Value);

            // Load danh sách dịch vụ mà phòng này đã dùng lên bảng bên phải
            dgvDichVu.DataSource = s.LayDichVuDaDung(soPhieuDat, soPhong);

            // Xóa trắng kết quả tính tiền cũ khi chuyển sang chọn phòng khác
            XoaTrangThongTin();
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            if (dgvPhong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phòng cần thanh toán.");
                return;
            }

            string soPhieuDat = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhieuDat"].Value);
            string soPhong = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhong"].Value);

            // 1. Tiền cọc (Lấy trực tiếp từ dòng đang chọn trên bảng phòng)
            tienCoc = Convert.ToDecimal(dgvPhong.CurrentRow.Cells["TienCoc"].Value);

            // 2. Tiền phòng & Số ngày ở (Gọi hàm từ Service)
            tienPhong = s.TinhTienPhong(soPhieuDat, soPhong, out soNgay);

            // 3. Tiền dịch vụ (Tính tổng cột ThanhTien trên bảng dgvDichVu)
            tienDichVu = 0;
            if (dgvDichVu.DataSource != null)
            {
                DataTable dt = (DataTable)dgvDichVu.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    tienDichVu += Convert.ToDecimal(row["ThanhTien"]);
                }
            }

            // 4. Tính Tổng tiền = (Tiền phòng + Tiền dịch vụ) - Tiền cọc
            decimal tongTien = tienPhong + tienDichVu - tienCoc;

            // Đẩy dữ liệu ra các TextBox (Thêm chuỗi "N0" để định dạng dấu phẩy hàng nghìn cho đẹp)
            txtSoNgay.Text = soNgay.ToString();
            txtTienPhong.Text = tienPhong.ToString("N0");
            txtTienDichVu.Text = tienDichVu.ToString("N0");
            txtTienCoc.Text = tienCoc.ToString("N0");
            txtTongTien.Text = tongTien.ToString("N0");
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTongTien.Text))
            {
                MessageBox.Show("Vui lòng bấm 'Tính tiền' để chốt sổ trước khi thanh toán.");
                return;
            }

            if (dgvPhong.CurrentRow == null) return;

            string soPhieuDat = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhieuDat"].Value);
            string maNV = cboNV.SelectedValue == null ? "" : cboNV.SelectedValue.ToString();
            string soHD = txtSoHoaDon.Text.Trim();

            // Gọi hàm thực thi tạo hóa đơn, đổi trạng thái phiếu và trạng thái phòng về 'Trống'
            var k = s.ThanhToan(soHD, soPhieuDat, maNV, soNgay, tienPhong, tienDichVu, tienCoc);

            MessageBox.Show(k.ThongBao);
            if (k.ThanhCong)
            {
                Tai(); // Load lại bảng phòng, phòng vừa thanh toán xong sẽ biến mất
            }
        }
    }
}