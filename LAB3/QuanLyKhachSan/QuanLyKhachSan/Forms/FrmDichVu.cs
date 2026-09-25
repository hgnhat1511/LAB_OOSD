using System;
using System.Data;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDichVu : Form
    {
        readonly DichVuService s = new DichVuService();
        readonly DanhMucService dm = new DanhMucService();

        public FrmDichVu()
        {
            InitializeComponent();

            // Khởi tạo dữ liệu
            ThietLapBanDau();

            // GÁN SỰ KIỆN TỰ ĐỘNG
            btnThemDV.Click += btnThemDV_Click;
            dgvPhong.SelectionChanged += dgvPhong_SelectionChanged;
        }

        private void ThietLapBanDau()
        {
            // Tải danh sách nhân viên vào ComboBox
            cboNV.DataSource = dm.LayNhanVien();
            cboNV.DisplayMember = "HoTen";
            cboNV.ValueMember = "MaNV";

            // Thiết lập số lượng mặc định là 1
            numSoLuong.Minimum = 1;
            numSoLuong.Value = 1;

            Tai();
        }

        void Tai()
        {
            // Load phòng đang có khách ở và menu dịch vụ
            dgvPhong.DataSource = s.LayPhongDangO();
            dgvDichVu.DataSource = s.LayDanhSachDichVu();
        }

        void TaiChiTietDichVu()
        {
            if (dgvPhong.CurrentRow == null) return;

            string soPhieuDat = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhieuDat"].Value);
            string soPhong = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhong"].Value);

            // Tìm xem phòng này đã có phiếu sử dụng dịch vụ nào chưa
            DataTable dtPhieu = s.LayPhieuSDDV(soPhieuDat, soPhong);
            if (dtPhieu.Rows.Count > 0)
            {
                // Nếu có rồi thì lấy mã phiếu nạp lên txtSoPhieu và load chi tiết
                string soPhieuSD = dtPhieu.Rows[0]["SoPhieuSDDV"].ToString();
                txtSoPhieu.Text = soPhieuSD;
                dgvChiTiet.DataSource = s.LayChiTietSDDV(soPhieuSD);
            }
            else
            {
                // Nếu chưa có thì xóa rỗng bảng chi tiết
                dgvChiTiet.DataSource = null;
            }
        }

        private void dgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            TaiChiTietDichVu();
        }

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            if (dgvPhong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một phòng đang ở bên bảng danh sách phòng.");
                return;
            }

            if (dgvDichVu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ từ Menu.");
                return;
            }

            string soPhieuDat = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhieuDat"].Value);
            string soPhong = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhong"].Value);

            string maDV = Convert.ToString(dgvDichVu.CurrentRow.Cells["MaDV"].Value);
            decimal donGia = Convert.ToDecimal(dgvDichVu.CurrentRow.Cells["DonGia"].Value);

            string maNV = cboNV.SelectedValue == null ? "" : cboNV.SelectedValue.ToString();
            string soPhieuSD = txtSoPhieu.Text.Trim();

            // Gọi hàm thêm dịch vụ vào CSDL
            var k = s.ThemDichVu(soPhieuSD, soPhieuDat, soPhong, dtNgay.Value, maNV, maDV, (int)numSoLuong.Value, donGia);

            MessageBox.Show(k.ThongBao);

            if (k.ThanhCong)
            {
                TaiChiTietDichVu(); // Refresh lại bảng chi tiết
            }
        }
    }
}