
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmToaThuoc : Form
    {
        private ComboBox cmbPhieuKham;
        private TextBox txtGhiChu, txtTenThuoc, txtLieuDung;
        private TextBox txtCachDung, txtSoLuong, txtThoiGian;
        private DataGridView dgvThuoc, dgvToa;
        private DataTable dsThuoc;

        public FrmToaThuoc()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaoBangThuoc();
            TaiPhieuKham();
            TaiDanhSachToa();
        }

        private void TaoGiaoDien()
        {
            Controls.Clear();
            Text = "Lập toa thuốc";
            Size = new Size(1150, 720);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "LẬP TOA THUỐC";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.SetBounds(350, 10, 450, 35);
            title.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(title);

            Nhan("Phiếu khám", 20, 60, 100);
            cmbPhieuKham = TaoCombo(125, 60, 400);
            Nhan("Ghi chú", 550, 60, 80);
            txtGhiChu = TaoText(630, 60, 450);

            Nhan("Tên thuốc", 20, 105, 100);
            txtTenThuoc = TaoText(125, 105, 240);
            Nhan("Liều dùng", 380, 105, 90);
            txtLieuDung = TaoText(470, 105, 180);
            Nhan("Cách dùng", 665, 105, 90);
            txtCachDung = TaoText(755, 105, 325);

            Nhan("Số lượng", 20, 145, 100);
            txtSoLuong = TaoText(125, 145, 100);
            Nhan("Thời gian dùng", 250, 145, 120);
            txtThoiGian = TaoText(370, 145, 180);

            Nut("Thêm thuốc", 570, 142, 105, ThemThuoc);
            Nut("Bỏ thuốc", 685, 142, 95, BoThuoc);
            Nut("Lưu toa", 790, 142, 95, LuuToa);
            Nut("Làm mới", 895, 142, 95, LamMoi);

            Nhan("Danh sách thuốc trong toa", 20, 185, 250);
            dgvThuoc = TaoGrid(20, 215, 520, 180);

            Nhan("Các toa thuốc đã lưu", 565, 185, 250);
            dgvToa = TaoGrid(565, 215, 550, 420);
        }

        private void TaoBangThuoc()
        {
            dsThuoc = new DataTable();
            dsThuoc.Columns.Add("TenThuoc", typeof(string));
            dsThuoc.Columns.Add("LieuDung", typeof(string));
            dsThuoc.Columns.Add("CachDung", typeof(string));
            dsThuoc.Columns.Add("SoLuong", typeof(int));
            dsThuoc.Columns.Add("ThoiGianDung", typeof(string));
            dgvThuoc.DataSource = dsThuoc;
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
                    cmbPhieuKham.DataSource = dt;
                    cmbPhieuKham.DisplayMember = "HienThi";
                    cmbPhieuKham.ValueMember = "MaPhieuKham";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phiếu khám:\n" + ex.Message);
            }
        }

        private void ThemThuoc(object sender, EventArgs e)
        {
            int sl;
            if (txtTenThuoc.Text.Trim() == "")
            {
                MessageBox.Show("Nhập tên thuốc.");
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương.");
                return;
            }

            dsThuoc.Rows.Add(txtTenThuoc.Text.Trim(),
                txtLieuDung.Text.Trim(), txtCachDung.Text.Trim(),
                sl, txtThoiGian.Text.Trim());

            txtTenThuoc.Clear();
            txtLieuDung.Clear();
            txtCachDung.Clear();
            txtSoLuong.Clear();
            txtThoiGian.Clear();
            txtTenThuoc.Focus();
        }

        private void BoThuoc(object sender, EventArgs e)
        {
            if (dgvThuoc.CurrentRow == null) return;
            int i = dgvThuoc.CurrentRow.Index;

            if (i >= 0 && i < dsThuoc.Rows.Count)
                dsThuoc.Rows.RemoveAt(i);
        }

        private void LuuToa(object sender, EventArgs e)
        {
            if (cmbPhieuKham.SelectedValue == null)
            {
                MessageBox.Show("Chọn phiếu khám.");
                return;
            }

            if (dsThuoc.Rows.Count == 0)
            {
                MessageBox.Show("Thêm ít nhất một thuốc.");
                return;
            }

            string maToa = "TT" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                {
                    cn.Open();
                    using (SqlTransaction tran = cn.BeginTransaction())
                    {
                        try
                        {
                            string sqlToa =
                                "INSERT INTO TOA_THUOC(MaToaThuoc,MaPhieuKham,NgayLap,GhiChu) " +
                                "VALUES(@ma,@pk,@ngay,@gc)";

                            using (SqlCommand cmd = new SqlCommand(sqlToa, cn, tran))
                            {
                                cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value = maToa;
                                cmd.Parameters.Add("@pk", SqlDbType.VarChar, 40).Value =
                                    Convert.ToString(cmbPhieuKham.SelectedValue);
                                cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = DateTime.Today;
                                cmd.Parameters.Add("@gc", SqlDbType.NVarChar, 500).Value = txtGhiChu.Text.Trim();
                                cmd.ExecuteNonQuery();
                            }

                            foreach (DataRow r in dsThuoc.Rows)
                            {
                                string sqlCT =
                                    "INSERT INTO CHI_TIET_TOA_THUOC " +
                                    "(MaChiTiet,MaToaThuoc,TenThuoc,LieuDung,CachDung,SoLuong,ThoiGianDung) " +
                                    "VALUES(@ma,@toa,@ten,@lieu,@cach,@sl,@tg)";

                                using (SqlCommand cmd = new SqlCommand(sqlCT, cn, tran))
                                {
                                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value =
                                        "CT" + Guid.NewGuid().ToString("N").Substring(0, 20);
                                    cmd.Parameters.Add("@toa", SqlDbType.VarChar, 40).Value = maToa;
                                    cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 150).Value = Convert.ToString(r["TenThuoc"]);
                                    cmd.Parameters.Add("@lieu", SqlDbType.NVarChar, 100).Value = Convert.ToString(r["LieuDung"]);
                                    cmd.Parameters.Add("@cach", SqlDbType.NVarChar, 255).Value = Convert.ToString(r["CachDung"]);
                                    cmd.Parameters.Add("@sl", SqlDbType.Int).Value = Convert.ToInt32(r["SoLuong"]);
                                    cmd.Parameters.Add("@tg", SqlDbType.NVarChar, 100).Value = Convert.ToString(r["ThoiGianDung"]);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show("Lưu toa thuốc thành công.");
                LamMoi(null, null);
                TaiDanhSachToa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu toa thuốc. Phiếu khám có thể đã có toa.\n" + ex.Message);
            }
        }

        private void TaiDanhSachToa()
        {
            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT TT.MaToaThuoc,TT.MaPhieuKham,BN.HoTen,TT.NgayLap,TT.GhiChu " +
                    "FROM TOA_THUOC TT " +
                    "JOIN PHIEU_KHAM PK ON TT.MaPhieuKham=PK.MaPhieuKham " +
                    "JOIN HO_SO_BENH_AN HS ON PK.MaHoSo=HS.MaHoSo " +
                    "JOIN BENH_NHAN BN ON HS.MaBenhNhan=BN.MaBenhNhan " +
                    "ORDER BY TT.NgayLap DESC", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvToa.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải toa thuốc:\n" + ex.Message);
            }
        }

        private void LamMoi(object sender, EventArgs e)
        {
            txtGhiChu.Clear();
            txtTenThuoc.Clear();
            txtLieuDung.Clear();
            txtCachDung.Clear();
            txtSoLuong.Clear();
            txtThoiGian.Clear();
            dsThuoc.Rows.Clear();
            TaiPhieuKham();
        }

        private void Nhan(string s, int x, int y, int w)
        {
            Label l = new Label(); l.Text = s;
            l.SetBounds(x, y, w, 25); Controls.Add(l);
        }

        private TextBox TaoText(int x, int y, int w)
        {
            TextBox t = new TextBox(); t.SetBounds(x, y, w, 25);
            Controls.Add(t); return t;
        }

        private ComboBox TaoCombo(int x, int y, int w)
        {
            ComboBox c = new ComboBox();
            c.DropDownStyle = ComboBoxStyle.DropDownList;
            c.SetBounds(x, y, w, 26); Controls.Add(c); return c;
        }

        private void Nut(string s, int x, int y, int w, EventHandler ev)
        {
            Button b = new Button(); b.Text = s;
            b.SetBounds(x, y, w, 30); b.Click += ev; Controls.Add(b);
        }

        private DataGridView TaoGrid(int x, int y, int w, int h)
        {
            DataGridView g = new DataGridView();
            g.SetBounds(x, y, w, h);
            g.ReadOnly = true; g.AllowUserToAddRows = false;
            g.MultiSelect = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.RowHeadersVisible = false; Controls.Add(g);
            return g;
        }
    }
}
