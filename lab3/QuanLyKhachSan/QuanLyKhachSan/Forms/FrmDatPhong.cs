using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDatPhong : Form
    {
        readonly DatPhongService s = new DatPhongService();
        readonly DanhMucService dm = new DanhMucService();

        // Danh sách tạm để chứa các phòng đang được chọn trước khi lập phiếu
        BindingList<PhongDatItem> chon = new BindingList<PhongDatItem>();

        public FrmDatPhong()
        {
            InitializeComponent();

            ThietLapBanDau();

            // TỰ ĐỘNG GÁN SỰ KIỆN TẠI ĐÂY
            btnThemKhach.Click += btnThemKhach_Click;
            btnThemPhong.Click += btnThemPhong_Click;
            btnBoPhong.Click += btnBoPhong_Click;
            btnLapPhieu.Click += btnLapPhieu_Click;
            btnThemNguoi.Click += btnThemNguoi_Click;
            btnNhanPhong.Click += btnNhanPhong_Click;
            btnNoShow.Click += btnNoShow_Click;

            // Sự kiện khi click chọn 1 dòng trong bảng dgvPhieu
            dgvPhieu.SelectionChanged += dgvPhieu_SelectionChanged;
        }

        private void ThietLapBanDau()
        {
            // Tải dữ liệu ComboBox Khách hàng
            cboKhach.DataSource = s.LayKhach();
            cboKhach.DisplayMember = "HoTen";
            cboKhach.ValueMember = "MaKhach";

            // Tải dữ liệu ComboBox Nhân viên (Lấy từ DanhMucService)
            cboNV.DataSource = dm.LayNhanVien();
            cboNV.DisplayMember = "HoTen";
            cboNV.ValueMember = "MaNV";

            // Nạp dữ liệu Kênh đặt
            cboKenh.Items.AddRange(new object[] { "Điện thoại", "Website", "Trực tiếp" });
            if (cboKenh.Items.Count > 0) cboKenh.SelectedIndex = 0;

            // Gán bảng chọn phòng tạm thời
            dgvChon.DataSource = chon;

            Tai();
        }

        void Tai()
        {
            dgvKhach.DataSource = s.LayKhach();
            dgvPhong.DataSource = s.LayPhong();
            dgvPhieu.DataSource = s.LayPhieuDat();
        }

        string V(ComboBox c)
        {
            return c.SelectedValue == null ? "" : c.SelectedValue.ToString();
        }

        void H(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao);
            if (k.ThanhCong) Tai();
        }

        // ==========================================
        // CÁC HÀM XỬ LÝ SỰ KIỆN NÚT BẤM
        // ==========================================
        private void btnThemKhach_Click(object sender, EventArgs e)
        {
            H(s.ThemKhach(txtMaKH.Text.Trim(), txtTenKH.Text.Trim(), txtCMND.Text.Trim(), txtQT.Text.Trim(), txtSDT.Text.Trim()));
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            if (dgvPhong.CurrentRow == null) return;
            string p = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhong"].Value);

            // Kiểm tra trùng phòng trong danh sách chọn
            foreach (var x in chon)
            {
                if (x.SoPhong == p)
                {
                    MessageBox.Show("Phòng đã có trong phiếu.");
                    return;
                }
            }

            int n = (int)numSoNguoi.Value;
            decimal g = Convert.ToDecimal(dgvPhong.CurrentRow.Cells["DonGiaNgay"].Value);
            chon.Add(new PhongDatItem { SoPhong = p, SoNguoi = n, DonGiaNgay = g });
        }

        private void btnBoPhong_Click(object sender, EventArgs e)
        {
            if (dgvChon.CurrentRow != null && dgvChon.CurrentRow.Index >= 0 && dgvChon.CurrentRow.Index < chon.Count)
                chon.RemoveAt(dgvChon.CurrentRow.Index);
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            H(s.TaoDatPhong(txtSoPhieu.Text.Trim(), V(cboKhach), V(cboNV), dtLap.Value, dtNhan.Value, dtTra.Value, numCoc.Value, cboKenh.Text, new List<PhongDatItem>(chon)));
            if (chon.Count > 0) chon.Clear(); // Lập xong thì xóa danh sách tạm
        }

        private void dgvPhieu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhieu.CurrentRow == null) return;

            // Lấy Số Phiếu Đặt từ dòng đang chọn
            string so = Convert.ToString(dgvPhieu.CurrentRow.Cells["SoPhieuDat"].Value);
            txtPhieuChon.Text = so;

            // Tải chi tiết phòng và người lưu trú của phiếu đó
            dgvCT.DataSource = s.LayChiTiet(so);
            dgvNguoi.DataSource = s.LayNguoiLuuTru(so);
        }

        private void btnThemNguoi_Click(object sender, EventArgs e)
        {
            H(s.ThemNguoiLuuTru(txtPhieuChon.Text.Trim(), txtNguoiPhong.Text.Trim(), txtNguoiTen.Text.Trim(), txtNguoiCMND.Text.Trim(), txtNguoiQT.Text.Trim()));
        }

        private void btnNhanPhong_Click(object sender, EventArgs e)
        {
            H(s.NhanPhong(txtPhieuChon.Text.Trim(), DateTime.Now));
        }

        private void btnNoShow_Click(object sender, EventArgs e)
        {
            H(s.DanhDauNoShow(txtPhieuChon.Text.Trim()));
        }
    }
}