
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmHoSoBenhAn : Form
    {
        private ComboBox cmbBenhNhan;
        private TextBox txtTim;
        private DataGridView dgv;

        public FrmHoSoBenhAn()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaiBenhNhan();
            TaiDanhSach();
        }

        private void TaoGiaoDien()
        {
            Controls.Clear();
            Text = "Hồ sơ bệnh án";
            Size = new Size(1000, 620);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "QUẢN LÝ HỒ SƠ BỆNH ÁN";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.SetBounds(250, 15, 500, 35);
            title.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(title);

            Nhan("Bệnh nhân", 25, 75);
            cmbBenhNhan = new ComboBox();
            cmbBenhNhan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBenhNhan.SetBounds(130, 75, 400, 26);
            Controls.Add(cmbBenhNhan);
            Nut("Tạo hồ sơ", 550, 72, TaoHoSo);

            Nhan("Từ khóa", 25, 120);
            txtTim = ONhap(130, 120, 400);
            Nut("Tra cứu", 550, 117, TimKiem);
            Nut("Làm mới", 660, 117, LamMoi);

            dgv = new DataGridView();
            dgv.SetBounds(20, 170, 940, 390);
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            Controls.Add(dgv);
        }

        private void TaiBenhNhan()
        {
            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT MaBenhNhan, MaBenhNhan + N' - ' + HoTen AS HienThi " +
                    "FROM BENH_NHAN ORDER BY HoTen", cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbBenhNhan.DataSource = dt;
                    cmbBenhNhan.DisplayMember = "HienThi";
                    cmbBenhNhan.ValueMember = "MaBenhNhan";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải bệnh nhân:\n" + ex.Message);
            }
        }

        private void TaiDanhSach(string q = "")
        {
            string sql =
                "SELECT HS.MaHoSo, HS.MaBenhNhan, BN.HoTen, HS.NgayLap, HS.TrangThai " +
                "FROM HO_SO_BENH_AN HS JOIN BENH_NHAN BN " +
                "ON HS.MaBenhNhan=BN.MaBenhNhan ";

            if (q.Trim() != "")
                sql += "WHERE HS.MaHoSo LIKE @q OR BN.HoTen LIKE @q ";

            sql += "ORDER BY HS.NgayLap DESC";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    if (q.Trim() != "")
                        cmd.Parameters.Add("@q", SqlDbType.NVarChar, 255)
                            .Value = "%" + q.Trim() + "%";

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }

                Cot("MaHoSo", "Mã hồ sơ");
                Cot("MaBenhNhan", "Mã bệnh nhân");
                Cot("HoTen", "Họ tên");
                Cot("NgayLap", "Ngày lập");
                Cot("TrangThai", "Trạng thái");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hồ sơ:\n" + ex.Message);
            }
        }

        private void TaoHoSo(object sender, EventArgs e)
        {
            if (cmbBenhNhan.SelectedValue == null)
            {
                MessageBox.Show("Chưa có bệnh nhân.");
                return;
            }

            string maBN = Convert.ToString(cmbBenhNhan.SelectedValue);

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                {
                    cn.Open();

                    string sqlPhi =
                        "SELECT COUNT(*) FROM PHIEU_DANG_KY " +
                        "WHERE MaBenhNhan=@bn AND NamDangKy=@nam " +
                        "AND TrangThaiPhi=N'Đã thanh toán'";

                    using (SqlCommand cmd = new SqlCommand(sqlPhi, cn))
                    {
                        cmd.Parameters.Add("@bn", SqlDbType.VarChar, 30).Value = maBN;
                        cmd.Parameters.Add("@nam", SqlDbType.Int).Value = DateTime.Today.Year;

                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            MessageBox.Show("Bệnh nhân chưa đóng phí đăng ký năm hiện tại.");
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM HO_SO_BENH_AN WHERE MaBenhNhan=@bn", cn))
                    {
                        cmd.Parameters.Add("@bn", SqlDbType.VarChar, 30).Value = maBN;

                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Bệnh nhân đã có hồ sơ bệnh án.");
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO HO_SO_BENH_AN(MaHoSo,MaBenhNhan,NgayLap,TrangThai) " +
                        "VALUES(@ma,@bn,@ngay,N'Đang hoạt động')", cn))
                    {
                        cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value =
                            "HS" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        cmd.Parameters.Add("@bn", SqlDbType.VarChar, 30).Value = maBN;
                        cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = DateTime.Today;
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Tạo hồ sơ thành công.");
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo hồ sơ:\n" + ex.Message);
            }
        }

        private void TimKiem(object sender, EventArgs e)
        {
            TaiDanhSach(txtTim.Text);
        }

        private void LamMoi(object sender, EventArgs e)
        {
            txtTim.Clear();
            TaiBenhNhan();
            TaiDanhSach();
        }

        private void Cot(string ten, string hienThi)
        {
            if (dgv.Columns.Contains(ten))
                dgv.Columns[ten].HeaderText = hienThi;
        }

        private void Nhan(string s, int x, int y)
        {
            Label l = new Label();
            l.Text = s;
            l.SetBounds(x, y, 100, 25);
            Controls.Add(l);
        }

        private TextBox ONhap(int x, int y, int w)
        {
            TextBox t = new TextBox();
            t.SetBounds(x, y, w, 25);
            Controls.Add(t);
            return t;
        }

        private void Nut(string s, int x, int y, EventHandler ev)
        {
            Button b = new Button();
            b.Text = s;
            b.SetBounds(x, y, 100, 30);
            b.Click += ev;
            Controls.Add(b);
        }
    }
}
