using System;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Forms
{
    public partial class FrmSach : Form
    {
        private readonly SachService service = new SachService();

        public FrmSach() { InitializeComponent(); }

        private void FrmSach_Load(object sender, EventArgs e)
        {
            cboTheLoai.DataSource = service.LayDSTheLoai();
            cboTheLoai.DisplayMember = "TenTheLoai";
            cboTheLoai.ValueMember = "MaTheLoai";

            cboNXB.DataSource = service.LayDSNhaXuatBan();
            cboNXB.DisplayMember = "MaNhaXuatBan";
            cboNXB.ValueMember = "MaNhaXuatBan";

            TaiDuLieu();
        }

        private void TaiDuLieu()
        {
            dgvSach.DataSource = service.LayTatCa();
            if (dgvSach.Columns.Count > 0) dgvSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ShowResult(KetQuaXuLy kq)
        {
            MessageBox.Show(kq.ThongBao, kq.ThanhCong ? "Thông báo" : "Lỗi", MessageBoxButtons.OK, kq.ThanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (kq.ThanhCong) TaiDuLieu();
        }

        private void btnThem_Click(object sender, EventArgs e) { ShowResult(service.LuuSach(txtMa.Text, txtTen.Text, (int)numNam.Value, (int)numSoLuong.Value, cboTheLoai.SelectedValue?.ToString(), cboNXB.SelectedValue?.ToString(), false)); }

        private void btnCapNhat_Click(object sender, EventArgs e) { ShowResult(service.LuuSach(txtMa.Text, txtTen.Text, (int)numNam.Value, (int)numSoLuong.Value, cboTheLoai.SelectedValue?.ToString(), cboNXB.SelectedValue?.ToString(), true)); }

        private void btnXoa_Click(object sender, EventArgs e) { if (MessageBox.Show("Xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) ShowResult(service.XoaSach(txtMa.Text.Trim())); }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMa.Clear(); txtTen.Clear(); txtTim.Clear(); numNam.Value = DateTime.Now.Year; numSoLuong.Value = 0;
            txtMa.ReadOnly = false; btnThem.Enabled = true; btnCapNhat.Enabled = false; btnXoa.Enabled = false;
            TaiDuLieu();
        }

        private void btnTim_Click(object sender, EventArgs e) { dgvSach.DataSource = service.TimKiem(txtTim.Text.Trim()); }

        private void dgvSach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSach.CurrentRow == null || dgvSach.CurrentRow.DataBoundItem == null) return;
            DataRowView r = (DataRowView)dgvSach.CurrentRow.DataBoundItem;

            txtMa.Text = Convert.ToString(r["MaDauSach"]);
            txtTen.Text = Convert.ToString(r["TenSach"]);
            if (r["NamXuatBan"] != DBNull.Value) numNam.Value = Convert.ToDecimal(r["NamXuatBan"]);
            if (r["SoLuongHienCo"] != DBNull.Value) numSoLuong.Value = Convert.ToDecimal(r["SoLuongHienCo"]);
            cboTheLoai.SelectedValue = Convert.ToString(r["MaTheLoai"]);
            cboNXB.SelectedValue = Convert.ToString(r["MaNhaXuatBan"]);

            txtMa.ReadOnly = true; btnThem.Enabled = false; btnCapNhat.Enabled = true; btnXoa.Enabled = true;
        }
    }
}