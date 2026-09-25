using System;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Forms
{
    public partial class FrmDocGia : Form
    {
        private readonly DocGiaService service = new DocGiaService();

        public FrmDocGia() { InitializeComponent(); }

        private void FrmDocGia_Load(object sender, EventArgs e)
        {
            if (cboPhai.Items.Count > 0) cboPhai.SelectedIndex = 0;
            TaiDuLieu();
        }

        private void TaiDuLieu()
        {
            dgvDocGia.DataSource = service.LayTatCa();
            if (dgvDocGia.Columns.Count > 0) dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ShowResult(KetQuaXuLy kq)
        {
            MessageBox.Show(kq.ThongBao, kq.ThanhCong ? "Thông báo" : "Lỗi", MessageBoxButtons.OK, kq.ThanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (kq.ThanhCong) TaiDuLieu();
        }

        private void btnThem_Click(object sender, EventArgs e) { ShowResult(service.LuuDocGia(txtMa.Text, txtTen.Text, dtpNgaySinh.Value, cboPhai.SelectedItem?.ToString(), txtSDT.Text, false)); }

        private void btnCapNhat_Click(object sender, EventArgs e) { ShowResult(service.LuuDocGia(txtMa.Text, txtTen.Text, dtpNgaySinh.Value, cboPhai.SelectedItem?.ToString(), txtSDT.Text, true)); }

        private void btnXoa_Click(object sender, EventArgs e) { if (MessageBox.Show("Xóa độc giả này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) ShowResult(service.XoaDocGia(txtMa.Text.Trim())); }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMa.Clear(); txtTen.Clear(); txtSDT.Clear(); txtTim.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            if (cboPhai.Items.Count > 0) cboPhai.SelectedIndex = 0;
            txtMa.ReadOnly = false; btnThem.Enabled = true; btnCapNhat.Enabled = false; btnXoa.Enabled = false;
            TaiDuLieu();
        }

        private void btnTim_Click(object sender, EventArgs e) { dgvDocGia.DataSource = service.TimKiem(txtTim.Text.Trim()); }

        private void dgvDocGia_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null || dgvDocGia.CurrentRow.DataBoundItem == null) return;
            DataRowView r = (DataRowView)dgvDocGia.CurrentRow.DataBoundItem;

            txtMa.Text = Convert.ToString(r["MaDocGia"]);
            txtTen.Text = Convert.ToString(r["HoTen"]);
            if (r["NgaySinh"] != DBNull.Value) dtpNgaySinh.Value = Convert.ToDateTime(r["NgaySinh"]);

            // Lấy dữ liệu Phai từ CSDL gán vào cboPhai
            cboPhai.SelectedItem = Convert.ToString(r["Phai"]);

            txtSDT.Text = Convert.ToString(r["SoDienThoai"]);

            txtMa.ReadOnly = true; btnThem.Enabled = false; btnCapNhat.Enabled = true; btnXoa.Enabled = true;
        }
    }
}