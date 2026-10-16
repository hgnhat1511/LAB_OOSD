
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmPhieuKham : Form
    {
        private ComboBox cmbHoSo, cmbBacSi, cmbKhoa, cmbLoai;
        private DateTimePicker dtpNgay;
        private TextBox txtTrieuChung, txtNhietDo, txtHuyetAp;
        private DataGridView dgv;
        private string maChon;

        public FrmPhieuKham()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaiDanhMuc();
            TaiDanhSach();
        }

        private void TaoGiaoDien()
        {
            Controls.Clear();
            Text = "Phiếu khám";
            Size = new Size(1150, 730);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "QUẢN LÝ PHIẾU KHÁM";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.SetBounds(350, 10, 450, 35);
            title.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(title);

            Nhan("Hồ sơ", 20, 60, 100);
            cmbHoSo = TaoCombo(130, 60, 350);
            Nhan("Bác sĩ", 510, 60, 90);
            cmbBacSi = TaoCombo(600, 60, 420);
            Nhan("Khoa", 20, 100, 100);
            cmbKhoa = TaoCombo(130, 100, 350);
            Nhan("Ngày khám", 510, 100, 90);
            dtpNgay = new DateTimePicker();
            dtpNgay.SetBounds(600, 100, 200, 26);
            Controls.Add(dtpNgay);

            Nhan("Loại khám", 20, 140, 100);
            cmbLoai = TaoCombo(130, 140, 350);
            cmbLoai.Items.Add("Thông thường");
            cmbLoai.Items.Add("Định kỳ");
            cmbLoai.SelectedIndex = 0;

            Nhan("Nhiệt độ", 510, 140, 90);
            txtNhietDo = TaoText(600, 140, 100);
            Nhan("Huyết áp", 720, 140, 80);
            txtHuyetAp = TaoText(800, 140, 220);

            Nhan("Triệu chứng", 20, 180, 100);
            txtTrieuChung = TaoText(130, 180, 890, 45);
            txtTrieuChung.Multiline = true;

            Nut("Lập phiếu", 20, 235, Them);
            Nut("Sửa phiếu", 130, 235, Sua);
            Nut("Xóa phiếu", 240, 235, Xoa);
            Nut("Làm mới", 350, 235, LamMoi);

            dgv = new DataGridView();
            dgv.SetBounds(20, 280, 1080, 400);
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.CellClick += ChonDong;
            Controls.Add(dgv);
        }

        private void TaiDanhMuc()
        {
            try
            {
                using (SqlConnection cn = Database.GetConnection())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT HS.MaHoSo,HS.MaHoSo+N' - '+BN.HoTen AS HienThi " +
                        "FROM HO_SO_BENH_AN HS JOIN BENH_NHAN BN " +
                        "ON HS.MaBenhNhan=BN.MaBenhNhan ORDER BY BN.HoTen", cn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbHoSo.DataSource = dt;
                        cmbHoSo.DisplayMember = "HienThi";
                        cmbHoSo.ValueMember = "MaHoSo";
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT MaBacSi,MaBacSi+N' - '+HoTen AS HienThi " +
                        "FROM BAC_SI ORDER BY HoTen", cn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbBacSi.DataSource = dt;
                        cmbBacSi.DisplayMember = "HienThi";
                        cmbBacSi.ValueMember = "MaBacSi";
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT MaKhoa,TenKhoa FROM KHOA ORDER BY TenKhoa", cn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbKhoa.DataSource = dt;
                        cmbKhoa.DisplayMember = "TenKhoa";
                        cmbKhoa.ValueMember = "MaKhoa";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục. Kiểm tra dữ liệu KHOA và BAC_SI.\n" + ex.Message);
            }
        }

        private void TaiDanhSach()
        {
            string sql =
                "SELECT PK.MaPhieuKham,PK.MaHoSo,BN.HoTen," +
                "PK.MaBacSi,BS.HoTen AS TenBacSi,PK.MaKhoa,K.TenKhoa," +
                "PK.NgayGioKham,PK.LoaiKham,PK.TrieuChung,PK.NhietDo,PK.HuyetAp " +
                "FROM PHIEU_KHAM PK " +
                "JOIN HO_SO_BENH_AN HS ON PK.MaHoSo=HS.MaHoSo " +
                "JOIN BENH_NHAN BN ON HS.MaBenhNhan=BN.MaBenhNhan " +
                "JOIN BAC_SI BS ON PK.MaBacSi=BS.MaBacSi " +
                "JOIN KHOA K ON PK.MaKhoa=K.MaKhoa ORDER BY PK.NgayGioKham DESC";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }

                Cot("MaPhieuKham", "Mã phiếu");
                Cot("MaHoSo", "Mã hồ sơ");
                Cot("HoTen", "Bệnh nhân");
                Cot("TenBacSi", "Bác sĩ");
                Cot("TenKhoa", "Khoa");
                Cot("NgayGioKham", "Ngày khám");
                Cot("LoaiKham", "Loại khám");
                Cot("TrieuChung", "Triệu chứng");
                Cot("NhietDo", "Nhiệt độ");
                Cot("HuyetAp", "Huyết áp");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phiếu khám:\n" + ex.Message);
            }
        }

        private bool KiemTra()
        {
            if (cmbHoSo.SelectedValue == null ||
                cmbBacSi.SelectedValue == null ||
                cmbKhoa.SelectedValue == null)
            {
                MessageBox.Show("Cần có hồ sơ, bác sĩ và khoa.");
                return false;
            }
            return true;
        }

        private object NhietDo()
        {
            if (txtNhietDo.Text.Trim() == "")
                return DBNull.Value;

            decimal n;
            if (!decimal.TryParse(txtNhietDo.Text.Trim(), out n) || n < 30 || n > 45)
                throw new Exception("Nhiệt độ không hợp lệ.");

            return n;
        }

        private void Them(object sender, EventArgs e)
        {
            if (!KiemTra()) return;

            string sql = "INSERT INTO PHIEU_KHAM " +
                "(MaPhieuKham,MaHoSo,MaBacSi,MaKhoa,NgayGioKham,TrieuChung,NhietDo,HuyetAp,LoaiKham) " +
                "VALUES(@ma,@hs,@bs,@k,@ngay,@tc,@nd,@ha,@loai)";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    GanThamSo(cmd);
                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value =
                        "PK" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Lập phiếu khám thành công.");
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lập phiếu khám:\n" + ex.Message);
            }
        }

        private void Sua(object sender, EventArgs e)
        {
            if (maChon == null) { MessageBox.Show("Chọn phiếu khám."); return; }
            if (!KiemTra()) return;

            string sql = "UPDATE PHIEU_KHAM SET MaHoSo=@hs,MaBacSi=@bs,MaKhoa=@k," +
                "NgayGioKham=@ngay,TrieuChung=@tc,NhietDo=@nd,HuyetAp=@ha," +
                "LoaiKham=@loai WHERE MaPhieuKham=@ma";

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

                MessageBox.Show("Cập nhật phiếu khám thành công.");
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật phiếu khám:\n" + ex.Message);
            }
        }

        private void Xoa(object sender, EventArgs e)
        {
            if (maChon == null) { MessageBox.Show("Chọn phiếu khám."); return; }
            if (MessageBox.Show("Xóa phiếu khám?", "Xác nhận",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM PHIEU_KHAM WHERE MaPhieuKham=@ma", cn))
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
                MessageBox.Show("Phiếu khám có thể đã liên kết với toa thuốc, xét nghiệm hoặc thanh toán.\n" + ex.Message);
            }
        }

        private void GanThamSo(SqlCommand cmd)
        {
            cmd.Parameters.Add("@hs", SqlDbType.VarChar, 40).Value = Convert.ToString(cmbHoSo.SelectedValue);
            cmd.Parameters.Add("@bs", SqlDbType.VarChar, 20).Value = Convert.ToString(cmbBacSi.SelectedValue);
            cmd.Parameters.Add("@k", SqlDbType.VarChar, 20).Value = Convert.ToString(cmbKhoa.SelectedValue);
            cmd.Parameters.Add("@ngay", SqlDbType.DateTime).Value = dtpNgay.Value;
            cmd.Parameters.Add("@tc", SqlDbType.NVarChar, 1000).Value = txtTrieuChung.Text.Trim();
            cmd.Parameters.Add("@nd", SqlDbType.Decimal).Value = NhietDo();
            cmd.Parameters.Add("@ha", SqlDbType.NVarChar, 30).Value = txtHuyetAp.Text.Trim();
            cmd.Parameters.Add("@loai", SqlDbType.NVarChar, 30).Value = Convert.ToString(cmbLoai.SelectedItem);
        }

        private void ChonDong(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgv.Rows[e.RowIndex];

            maChon = Convert.ToString(r.Cells["MaPhieuKham"].Value);
            cmbHoSo.SelectedValue = r.Cells["MaHoSo"].Value;
            cmbBacSi.SelectedValue = r.Cells["MaBacSi"].Value;
            cmbKhoa.SelectedValue = r.Cells["MaKhoa"].Value;
            txtTrieuChung.Text = Convert.ToString(r.Cells["TrieuChung"].Value);
            txtNhietDo.Text = r.Cells["NhietDo"].Value == DBNull.Value
                ? "" : Convert.ToString(r.Cells["NhietDo"].Value);
            txtHuyetAp.Text = Convert.ToString(r.Cells["HuyetAp"].Value);
            cmbLoai.SelectedItem = Convert.ToString(r.Cells["LoaiKham"].Value);
            if (r.Cells["NgayGioKham"].Value != DBNull.Value)
                dtpNgay.Value = Convert.ToDateTime(r.Cells["NgayGioKham"].Value);
        }

        private void LamMoi(object sender, EventArgs e)
        {
            maChon = null;
            txtTrieuChung.Clear();
            txtNhietDo.Clear();
            txtHuyetAp.Clear();
            cmbLoai.SelectedIndex = 0;
            TaiDanhMuc();
        }

        private void Nhan(string s, int x, int y, int w)
        { Label l = new Label(); l.Text = s; l.SetBounds(x, y, w, 25); Controls.Add(l); }

        private ComboBox TaoCombo(int x, int y, int w)
        { ComboBox c = new ComboBox(); c.DropDownStyle = ComboBoxStyle.DropDownList; c.SetBounds(x, y, w, 26); Controls.Add(c); return c; }

        private TextBox TaoText(int x, int y, int w, int h = 25)
        { TextBox t = new TextBox(); t.SetBounds(x, y, w, h); Controls.Add(t); return t; }

        private void Nut(string s, int x, int y, EventHandler ev)
        { Button b = new Button(); b.Text = s; b.SetBounds(x, y, 100, 30); b.Click += ev; Controls.Add(b); }

        private void Cot(string c, string t)
        { if (dgv.Columns.Contains(c)) dgv.Columns[c].HeaderText = t; }
    }
}
