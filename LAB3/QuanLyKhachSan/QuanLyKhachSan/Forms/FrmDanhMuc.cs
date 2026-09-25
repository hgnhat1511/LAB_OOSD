using System;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDanhMuc : Form
    {
        readonly DanhMucService s = new DanhMucService();

        public FrmDanhMuc()
        {
            InitializeComponent();

            // Gọi hàm tải dữ liệu ngay khi khởi tạo Form
            Tai();

            // Gán sự kiện cho các nút Thêm
            btnThemKhu.Click += btnThemKhu_Click;
            btnThemNV.Click += btnThemNV_Click;
            btnThemLoaiTN.Click += btnThemLoaiTN_Click;
            btnThemDV.Click += btnThemDV_Click;
            btnThemQD.Click += btnThemQD_Click;
        }

        void Tai()
        {
            // Đổ dữ liệu vào các DataGridView
            dgvKhu.DataSource = s.LayKhuVuc();
            dgvNV.DataSource = s.LayNhanVien();
            dgvLoaiTN.DataSource = s.LayLoaiTienNghi();
            dgvDV.DataSource = s.LayDichVu();
            dgvQD.DataSource = s.LayQuyDinhDenBu();

            // Cấu hình ComboBox Loại tiện nghi trong tab Đền bù
            cboQDLoai.DataSource = s.LayLoaiTienNghi();
            cboQDLoai.DisplayMember = "TenLoaiTN";
            cboQDLoai.ValueMember = "MaLoaiTN";
        }

        // Hàm hỗ trợ: Hiển thị thông báo và tải lại danh sách nếu thêm thành công
        void H(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao);
            if (k.ThanhCong) Tai();
        }

        // Xử lý sự kiện cho các nút Thêm
        private void btnThemKhu_Click(object sender, EventArgs e)
        {
            H(s.ThemKhu(txtKhuMa.Text.Trim(), txtKhuTen.Text.Trim()));
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            H(s.ThemNhanVien(txtNVMa.Text.Trim(), txtNVTen.Text.Trim(), txtNVVaiTro.Text.Trim(), txtNVSDT.Text.Trim()));
        }

        private void btnThemLoaiTN_Click(object sender, EventArgs e)
        {
            H(s.ThemLoaiTN(txtLoaiMa.Text.Trim(), txtLoaiTen.Text.Trim()));
        }

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            H(s.ThemDichVu(txtDVMa.Text.Trim(), txtDVTen.Text.Trim(), txtDVDVT.Text.Trim(), numDVGia.Value));
        }

        private void btnThemQD_Click(object sender, EventArgs e)
        {
            H(s.ThemQuyDinh(txtQDMa.Text.Trim(), cboQDLoai.SelectedValue == null ? "" : cboQDLoai.SelectedValue.ToString(), txtQDMucDo.Text.Trim(), numQDTien.Value));
        }

        // =================================================================
        // GIỮ NGUYÊN 2 HÀM TRỐNG NÀY - KHÔNG XÓA ĐỂ TRÁNH LỖI GIAO DIỆN
        // =================================================================
        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
        }

        private void tabKhuVuc_Click(object sender, EventArgs e)
        {
        }
    }
}