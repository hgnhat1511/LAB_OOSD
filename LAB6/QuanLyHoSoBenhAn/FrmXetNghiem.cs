
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmXetNghiem : Form
    {
        private ComboBox cmbPhieu, cmbTrangThai;
        private TextBox txtTen, txtKetQua;
        private DateTimePicker dtpNgay;
        private DataGridView dgv;
        private string maChon;

        public FrmXetNghiem()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaiPhieuKham();
            TaiDanhSach();
        }

        private void TaoGiaoDien()
        {
            Controls.Clear();
            Text = "Quản lý xét nghiệm";
            Size = new Size(1100, 690);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "QUẢN LÝ XÉT NGHIỆM";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.SetBounds(320, 10, 450, 35);
            title.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(title);

            Nhan("Phiếu khám", 20, 60, 100);
            cmbPhieu = TaoCombo(125, 60, 400);

            Nhan("Ngày chỉ định", 550, 60, 110);
            dtpNgay = new DateTimePicker();
            dtpNgay.Format = DateTimePickerFormat.Short;
            dtpNgay.SetBounds(665, 60, 180, 26);
            Controls.Add(dtpNgay);

            Nhan("Tên xét nghiệm", 20, 100, 100);
            txtTen = TaoText(125, 100, 400);

            Nhan("Trạng thái", 550, 100, 110);
            cmbTrangThai = TaoCombo(665, 100, 180);
            cmbTrangThai.Items.Add("Chờ thực hiện");
            cmbTrangThai.Items.Add("Đã có kết quả");
            cmbTrangThai.Items.Add("Đã hủy");
            cmbTrangThai.SelectedIndex = 0;

            Nhan("Kết quả", 20, 140, 100);
            txtKetQua = TaoText(125, 140, 720, 50);
            txtKetQua.Multiline = true;

            Nut("Thêm", 20, 205, Them);
            Nut("Cập nhật", 130, 205, CapNhat);
            Nut("Xóa", 240, 205, Xoa);
            Nut("Làm mới", 350, 205, LamMoi);

            dgv = new DataGridView();
            dgv.SetBounds(20, 250, 1040, 360);
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.CellClick += ChonDong;
            Controls.Add(dgv);
        }

        private void TaiPhieuKham()
        {
            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT PK.MaPhieuKham,PK.MaPhieuKham+N' - '+BN.HoTen AS HienThi " +
                    "FROM PHIEU_KHAM PK " +
                    "JOIN HO_SO_BENH_AN HS ON PK.MaHoSo=HS.MaHoSo " +
                    "JOIN BENH_NHAN BN ON HS.MaBenhNhan=BN.MaBenhNhan " +
                    "ORDER BY PK.NgayGioKham DESC", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbPhieu.DataSource = dt;
                    cmbPhieu.DisplayMember = "HienThi";
                    cmbPhieu.ValueMember = "MaPhieuKham";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phiếu khám:\n" + ex.Message);
            }
        }

        private void TaiDanhSach()
        {
            string sql =
                "SELECT X.MaXetNghiem,X.MaPhieuKham,BN.HoTen,X.TenXetNghiem," +
                "X.NgayChiDinh,X.KetQua,X.NgayCapNhat,X.TrangThai " +
                "FROM XET_NGHIEM X " +
                "JOIN PHIEU_KHAM PK ON X.MaPhieuKham=PK.MaPhieuKham " +
                "JOIN HO_SO_BENH_AN HS ON PK.MaHoSo=HS.MaHoSo " +
                "JOIN BENH_NHAN BN ON HS.MaBenhNhan=BN.MaBenhNhan " +
                "ORDER BY X.NgayChiDinh DESC";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }

                Cot("MaXetNghiem", "Mã xét nghiệm");
                Cot("MaPhieuKham", "Mã phiếu khám");
                Cot("HoTen", "Bệnh nhân");
                Cot("TenXetNghiem", "Xét nghiệm");
                Cot("NgayChiDinh", "Ngày chỉ định");
                Cot("KetQua", "Kết quả");
                Cot("NgayCapNhat", "Ngày cập nhật");
                Cot("TrangThai", "Trạng thái");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải xét nghiệm:\n" + ex.Message);
            }
        }

        private bool KiemTra()
        {
            if (cmbPhieu.SelectedValue == null)
            {
                MessageBox.Show("Chọn phiếu khám.");
                return false;
            }
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Nhập tên xét nghiệm.");
                return false;
            }
            if (cmbTrangThai.SelectedItem == null)
            {
                MessageBox.Show("Chọn trạng thái.");
                return false;
            }
            if (Convert.ToString(cmbTrangThai.SelectedItem) == "Đã có kết quả"
                && txtKetQua.Text.Trim() == "")
            {
                MessageBox.Show("Nhập kết quả xét nghiệm.");
                return false;
            }
            return true;
        }

        private object NgayCapNhat()
        {
            return txtKetQua.Text.Trim() == ""
                ? (object)DBNull.Value : DateTime.Today;
        }

        private void Them(object sender, EventArgs e)
        {
            if (!KiemTra()) return;

            string sql =
                "INSERT INTO XET_NGHIEM " +
                "(MaXetNghiem,MaPhieuKham,TenXetNghiem,NgayChiDinh, " +
                "KetQua,NgayCapNhat,TrangThai) " +
                "VALUES(@ma,@pk,@ten,@ngay,@kq,@nc,@tt)";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    GanThamSo(cmd);
                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value =
                        "XN" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Thêm xét nghiệm thành công.");
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm xét nghiệm:\n" + ex.Message);
            }
        }

        private void CapNhat(object sender, EventArgs e)
        {
            if (maChon == null)
            {
                MessageBox.Show("Chọn xét nghiệm cần cập nhật.");
                return;
            }
            if (!KiemTra()) return;

            string sql =
                "UPDATE XET_NGHIEM SET MaPhieuKham=@pk,TenXetNghiem=@ten," +
                "NgayChiDinh=@ngay,KetQua=@kq,NgayCapNhat=@nc,TrangThai=@tt " +
                "WHERE MaXetNghiem=@ma";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    GanThamSo(cmd);
                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value = maChon;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Cập nhật xét nghiệm thành công.");
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật xét nghiệm:\n" + ex.Message);
            }
        }

        private void Xoa(object sender, EventArgs e)
        {
            if (maChon == null)
            {
                MessageBox.Show("Chọn xét nghiệm cần xóa.");
                return;
            }
            if (MessageBox.Show("Xóa xét nghiệm?", "Xác nhận",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM XET_NGHIEM WHERE MaXetNghiem=@ma", cn))
                {
                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value = maChon;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa xét nghiệm:\n" + ex.Message);
            }
        }

        private void GanThamSo(SqlCommand cmd)
        {
            cmd.Parameters.Add("@pk", SqlDbType.VarChar, 40).Value =
                Convert.ToString(cmbPhieu.SelectedValue);
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 150).Value = txtTen.Text.Trim();
            cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = dtpNgay.Value.Date;
            cmd.Parameters.Add("@kq", SqlDbType.NVarChar, 1000).Value =
                txtKetQua.Text.Trim() == "" ? (object)DBNull.Value : txtKetQua.Text.Trim();
            cmd.Parameters.Add("@nc", SqlDbType.Date).Value = NgayCapNhat();
            cmd.Parameters.Add("@tt", SqlDbType.NVarChar, 30).Value =
                Convert.ToString(cmbTrangThai.SelectedItem);
        }

        private void ChonDong(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgv.Rows[e.RowIndex];
            maChon = Convert.ToString(r.Cells["MaXetNghiem"].Value);
            cmbPhieu.SelectedValue = r.Cells["MaPhieuKham"].Value;
            txtTen.Text = Convert.ToString(r.Cells["TenXetNghiem"].Value);
            txtKetQua.Text = Convert.ToString(r.Cells["KetQua"].Value);
            cmbTrangThai.SelectedItem = Convert.ToString(r.Cells["TrangThai"].Value);

            if (r.Cells["NgayChiDinh"].Value != DBNull.Value)
                dtpNgay.Value = Convert.ToDateTime(r.Cells["NgayChiDinh"].Value);
        }

        private void LamMoi(object sender, EventArgs e)
        {
            maChon = null;
            txtTen.Clear();
            txtKetQua.Clear();
            cmbTrangThai.SelectedIndex = 0;
            TaiPhieuKham();
        }

        private void Cot(string c, string t)
        {
            if (dgv.Columns.Contains(c)) dgv.Columns[c].HeaderText = t;
        }

        private void Nhan(string s, int x, int y, int w)
        {
            Label l = new Label(); l.Text = s; l.SetBounds(x, y, w, 25); Controls.Add(l);
        }

        private TextBox TaoText(int x, int y, int w, int h = 25)
        {
            TextBox t = new TextBox(); t.SetBounds(x, y, w, h); Controls.Add(t); return t;
        }

        private ComboBox TaoCombo(int x, int y, int w)
        {
            ComboBox c = new ComboBox(); c.DropDownStyle = ComboBoxStyle.DropDownList;
            c.SetBounds(x, y, w, 26); Controls.Add(c); return c;
        }

        private void Nut(string s, int x, int y, EventHandler ev)
        {
            Button b = new Button(); b.Text = s; b.SetBounds(x, y, 100, 30);
            b.Click += ev; Controls.Add(b);
        }
    }
}
