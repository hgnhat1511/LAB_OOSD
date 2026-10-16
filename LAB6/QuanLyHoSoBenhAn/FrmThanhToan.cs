
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmThanhToan : Form
    {
        private ComboBox cmbLoai, cmbBenhNhan, cmbPhieuKham, cmbHinhThuc;
        private TextBox txtSoTien;
        private DataGridView dgv;

        public FrmThanhToan()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaiDanhMuc();
            TaiDanhSach();
        }

        private void TaoGiaoDien()
        {
            Controls.Clear();
            Text = "Thanh toán";
            Size = new Size(1100, 660);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "QUẢN LÝ THANH TOÁN";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.SetBounds(330, 10, 450, 35);
            title.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(title);

            Nhan("Loại thanh toán", 20, 65, 115);
            cmbLoai = TaoCombo(140, 65, 280);
            cmbLoai.Items.Add("Phí đăng ký hằng năm");
            cmbLoai.Items.Add("Viện phí");
            cmbLoai.SelectedIndex = 0;
            cmbLoai.SelectedIndexChanged += DoiLoai;

            Nhan("Bệnh nhân", 20, 105, 100);
            cmbBenhNhan = TaoCombo(140, 105, 400);

            Nhan("Phiếu khám", 560, 105, 100);
            cmbPhieuKham = TaoCombo(670, 105, 380);

            Nhan("Số tiền", 20, 145, 100);
            txtSoTien = TaoText(140, 145, 280);

            Nhan("Hình thức", 560, 145, 100);
            cmbHinhThuc = TaoCombo(670, 145, 220);
            cmbHinhThuc.Items.Add("Tiền mặt");
            cmbHinhThuc.Items.Add("Chuyển khoản");
            cmbHinhThuc.SelectedIndex = 0;

            Nut("Ghi nhận", 20, 195, GhiNhan);
            Nut("Làm mới", 130, 195, LamMoi);

            dgv = new DataGridView();
            dgv.SetBounds(20, 245, 1040, 350);
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            Controls.Add(dgv);
        }

        private void TaiDanhMuc()
        {
            try
            {
                using (SqlConnection cn = Database.GetConnection())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT MaBenhNhan,MaBenhNhan+N' - '+HoTen AS HienThi " +
                        "FROM BENH_NHAN ORDER BY HoTen", cn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbBenhNhan.DataSource = dt;
                        cmbBenhNhan.DisplayMember = "HienThi";
                        cmbBenhNhan.ValueMember = "MaBenhNhan";
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT PK.MaPhieuKham,PK.MaPhieuKham+N' - '+BN.HoTen AS HienThi " +
                        "FROM PHIEU_KHAM PK " +
                        "JOIN HO_SO_BENH_AN HS ON PK.MaHoSo=HS.MaHoSo " +
                        "JOIN BENH_NHAN BN ON HS.MaBenhNhan=BN.MaBenhNhan " +
                        "ORDER BY PK.NgayGioKham DESC", cn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbPhieuKham.DataSource = dt;
                        cmbPhieuKham.DisplayMember = "HienThi";
                        cmbPhieuKham.ValueMember = "MaPhieuKham";
                    }
                }

                DoiLoai(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục:\n" + ex.Message);
            }
        }

        private void DoiLoai(object sender, EventArgs e)
        {
            bool phiDangKy =
                Convert.ToString(cmbLoai.SelectedItem) == "Phí đăng ký hằng năm";

            if (cmbBenhNhan != null) cmbBenhNhan.Enabled = phiDangKy;
            if (cmbPhieuKham != null) cmbPhieuKham.Enabled = !phiDangKy;
        }

        private void GhiNhan(object sender, EventArgs e)
        {
            decimal soTien;
            if (!decimal.TryParse(txtSoTien.Text.Trim(), out soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền phải lớn hơn 0.");
                return;
            }

            if (cmbHinhThuc.SelectedItem == null)
            {
                MessageBox.Show("Chọn hình thức thanh toán.");
                return;
            }

            if (Convert.ToString(cmbLoai.SelectedItem) == "Phí đăng ký hằng năm")
                ThanhToanDangKy(soTien);
            else
                ThanhToanVienPhi(soTien);
        }

        private void ThanhToanDangKy(decimal soTien)
        {
            if (cmbBenhNhan.SelectedValue == null)
            {
                MessageBox.Show("Chọn bệnh nhân.");
                return;
            }

            string maBN = Convert.ToString(cmbBenhNhan.SelectedValue);

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                {
                    cn.Open();
                    using (SqlTransaction tran = cn.BeginTransaction())
                    {
                        try
                        {
                            string maDK = null;
                            string trangThai = null;

                            using (SqlCommand cmd = new SqlCommand(
                                "SELECT MaPhieuDangKy,TrangThaiPhi FROM PHIEU_DANG_KY " +
                                "WHERE MaBenhNhan=@bn AND NamDangKy=@nam", cn, tran))
                            {
                                cmd.Parameters.Add("@bn", SqlDbType.VarChar, 30).Value = maBN;
                                cmd.Parameters.Add("@nam", SqlDbType.Int).Value = DateTime.Today.Year;

                                using (SqlDataReader rd = cmd.ExecuteReader())
                                {
                                    if (rd.Read())
                                    {
                                        maDK = Convert.ToString(rd["MaPhieuDangKy"]);
                                        trangThai = Convert.ToString(rd["TrangThaiPhi"]);
                                    }
                                }
                            }

                            if (trangThai == "Đã thanh toán")
                            {
                                tran.Rollback();
                                MessageBox.Show("Phí đăng ký năm nay đã thanh toán.");
                                return;
                            }

                            if (maDK == null)
                            {
                                maDK = "DK" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

                                using (SqlCommand cmd = new SqlCommand(
                                    "INSERT INTO PHIEU_DANG_KY " +
                                    "(MaPhieuDangKy,MaBenhNhan,NgayDangKy,NamDangKy,SoTienPhaiNop,TrangThaiPhi) " +
                                    "VALUES(@ma,@bn,@ngay,@nam,@tien,N'Đã thanh toán')", cn, tran))
                                {
                                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value = maDK;
                                    cmd.Parameters.Add("@bn", SqlDbType.VarChar, 30).Value = maBN;
                                    cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = DateTime.Today;
                                    cmd.Parameters.Add("@nam", SqlDbType.Int).Value = DateTime.Today.Year;
                                    cmd.Parameters.Add("@tien", SqlDbType.Decimal).Value = soTien;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                using (SqlCommand cmd = new SqlCommand(
                                    "UPDATE PHIEU_DANG_KY SET SoTienPhaiNop=@tien," +
                                    "TrangThaiPhi=N'Đã thanh toán' WHERE MaPhieuDangKy=@ma", cn, tran))
                                {
                                    cmd.Parameters.Add("@tien", SqlDbType.Decimal).Value = soTien;
                                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value = maDK;
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            using (SqlCommand cmd = new SqlCommand(
                                "INSERT INTO THANH_TOAN " +
                                "(MaThanhToan,MaPhieuDangKy,MaPhieuKham,LoaiThanhToan," +
                                "SoTien,NgayThanhToan,HinhThuc,TrangThai) " +
                                "VALUES(@ma,@dk,NULL,N'Phí đăng ký hằng năm',@tien,@ngay,@ht,N'Đã thanh toán')",
                                cn, tran))
                            {
                                cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value =
                                    "TT" + Guid.NewGuid().ToString("N").Substring(0, 24);
                                cmd.Parameters.Add("@dk", SqlDbType.VarChar, 40).Value = maDK;
                                cmd.Parameters.Add("@tien", SqlDbType.Decimal).Value = soTien;
                                cmd.Parameters.Add("@ngay", SqlDbType.DateTime).Value = DateTime.Now;
                                cmd.Parameters.Add("@ht", SqlDbType.NVarChar, 50).Value =
                                    Convert.ToString(cmbHinhThuc.SelectedItem);
                                cmd.ExecuteNonQuery();
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            try { tran.Rollback(); } catch { }
                            throw;
                        }
                    }
                }

                MessageBox.Show("Đã ghi nhận phí đăng ký.");
                txtSoTien.Clear();
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán phí đăng ký:\n" + ex.Message);
            }
        }

        private void ThanhToanVienPhi(decimal soTien)
        {
            if (cmbPhieuKham.SelectedValue == null)
            {
                MessageBox.Show("Chọn phiếu khám.");
                return;
            }

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO THANH_TOAN " +
                    "(MaThanhToan,MaPhieuDangKy,MaPhieuKham,LoaiThanhToan," +
                    "SoTien,NgayThanhToan,HinhThuc,TrangThai) " +
                    "VALUES(@ma,NULL,@pk,N'Viện phí',@tien,@ngay,@ht,N'Đã thanh toán')", cn))
                {
                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value =
                        "TT" + Guid.NewGuid().ToString("N").Substring(0, 24);
                    cmd.Parameters.Add("@pk", SqlDbType.VarChar, 40).Value =
                        Convert.ToString(cmbPhieuKham.SelectedValue);
                    cmd.Parameters.Add("@tien", SqlDbType.Decimal).Value = soTien;
                    cmd.Parameters.Add("@ngay", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@ht", SqlDbType.NVarChar, 50).Value =
                        Convert.ToString(cmbHinhThuc.SelectedItem);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Đã ghi nhận viện phí.");
                txtSoTien.Clear();
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán viện phí:\n" + ex.Message);
            }
        }

        private void TaiDanhSach()
        {
            string sql =
                "SELECT TT.MaThanhToan,TT.LoaiThanhToan," +
                "COALESCE(BN1.HoTen,BN2.HoTen) AS BenhNhan," +
                "TT.SoTien,TT.NgayThanhToan,TT.HinhThuc,TT.TrangThai " +
                "FROM THANH_TOAN TT " +
                "LEFT JOIN PHIEU_DANG_KY DK ON TT.MaPhieuDangKy=DK.MaPhieuDangKy " +
                "LEFT JOIN BENH_NHAN BN1 ON DK.MaBenhNhan=BN1.MaBenhNhan " +
                "LEFT JOIN PHIEU_KHAM PK ON TT.MaPhieuKham=PK.MaPhieuKham " +
                "LEFT JOIN HO_SO_BENH_AN HS ON PK.MaHoSo=HS.MaHoSo " +
                "LEFT JOIN BENH_NHAN BN2 ON HS.MaBenhNhan=BN2.MaBenhNhan " +
                "ORDER BY TT.NgayThanhToan DESC";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thanh toán:\n" + ex.Message);
            }
        }

        private void LamMoi(object sender, EventArgs e)
        {
            txtSoTien.Clear();
            TaiDanhMuc();
            TaiDanhSach();
        }

        private void Nhan(string s, int x, int y, int w)
        { Label l = new Label(); l.Text = s; l.SetBounds(x, y, w, 25); Controls.Add(l); }

        private TextBox TaoText(int x, int y, int w)
        { TextBox t = new TextBox(); t.SetBounds(x, y, w, 25); Controls.Add(t); return t; }

        private ComboBox TaoCombo(int x, int y, int w)
        { ComboBox c = new ComboBox(); c.DropDownStyle = ComboBoxStyle.DropDownList; c.SetBounds(x, y, w, 26); Controls.Add(c); return c; }

        private void Nut(string s, int x, int y, EventHandler ev)
        { Button b = new Button(); b.Text = s; b.SetBounds(x, y, 100, 30); b.Click += ev; Controls.Add(b); }
    }
}
