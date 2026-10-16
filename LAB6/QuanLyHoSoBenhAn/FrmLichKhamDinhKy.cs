
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmLichKhamDinhKy : Form
    {
        private ComboBox cmbHoSo, cmbTrangThai;
        private DateTimePicker dtpNgay, dtpGio;
        private TextBox txtGhiChu;
        private DataGridView dgv;
        private string maChon;

        public FrmLichKhamDinhKy()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaiHoSo();
            TaiDanhSach();
        }

        private void TaoGiaoDien()
        {
            Controls.Clear();
            Text = "Lịch khám định kỳ";
            Size = new Size(1100, 670);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "QUẢN LÝ LỊCH KHÁM ĐỊNH KỲ";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.SetBounds(300, 10, 500, 35);
            title.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(title);

            Nhan("Hồ sơ bệnh án", 20, 65, 105);
            cmbHoSo = TaoCombo(130, 65, 400);

            Nhan("Ngày hẹn", 560, 65, 90);
            dtpNgay = new DateTimePicker();
            dtpNgay.Format = DateTimePickerFormat.Short;
            dtpNgay.SetBounds(660, 65, 180, 26);
            Controls.Add(dtpNgay);

            Nhan("Giờ hẹn", 20, 105, 100);
            dtpGio = new DateTimePicker();
            dtpGio.Format = DateTimePickerFormat.Custom;
            dtpGio.CustomFormat = "HH:mm";
            dtpGio.ShowUpDown = true;
            dtpGio.SetBounds(130, 105, 120, 26);
            Controls.Add(dtpGio);

            Nhan("Trạng thái", 300, 105, 90);
            cmbTrangThai = TaoCombo(400, 105, 230);
            cmbTrangThai.Items.Add("Đã đặt");
            cmbTrangThai.Items.Add("Đã khám");
            cmbTrangThai.Items.Add("Đã hủy");
            cmbTrangThai.SelectedIndex = 0;

            Nhan("Ghi chú", 20, 145, 100);
            txtGhiChu = TaoText(130, 145, 760, 40);
            txtGhiChu.Multiline = true;

            Nut("Đặt lịch", 20, 200, Them);
            Nut("Sửa lịch", 130, 200, Sua);
            Nut("Hủy lịch", 240, 200, Huy);
            Nut("Làm mới", 350, 200, LamMoi);

            dgv = new DataGridView();
            dgv.SetBounds(20, 250, 1040, 350);
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.CellClick += ChonDong;
            Controls.Add(dgv);
        }

        private void TaiHoSo()
        {
            try
            {
                using (SqlConnection cn = Database.GetConnection())
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hồ sơ:\n" + ex.Message);
            }
        }

        private void TaiDanhSach()
        {
            string sql =
                "SELECT LK.MaLichKham,LK.MaHoSo,BN.HoTen,LK.NgayHen," +
                "LK.GioHen,LK.TrangThai,LK.GhiChu " +
                "FROM LICH_KHAM_DINH_KY LK " +
                "JOIN HO_SO_BENH_AN HS ON LK.MaHoSo=HS.MaHoSo " +
                "JOIN BENH_NHAN BN ON HS.MaBenhNhan=BN.MaBenhNhan " +
                "ORDER BY LK.NgayHen,LK.GioHen";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }

                Cot("MaLichKham", "Mã lịch");
                Cot("MaHoSo", "Mã hồ sơ");
                Cot("HoTen", "Bệnh nhân");
                Cot("NgayHen", "Ngày hẹn");
                Cot("GioHen", "Giờ hẹn");
                Cot("TrangThai", "Trạng thái");
                Cot("GhiChu", "Ghi chú");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch hẹn:\n" + ex.Message);
            }
        }

        private bool KiemTra()
        {
            if (cmbHoSo.SelectedValue == null)
            {
                MessageBox.Show("Chọn hồ sơ bệnh án.");
                return false;
            }

            if (cmbTrangThai.SelectedItem == null)
            {
                MessageBox.Show("Chọn trạng thái.");
                return false;
            }

            if (dtpNgay.Value.Date < DateTime.Today &&
                Convert.ToString(cmbTrangThai.SelectedItem) == "Đã đặt")
            {
                MessageBox.Show("Ngày hẹn không được ở quá khứ.");
                return false;
            }

            return true;
        }

        private void Them(object sender, EventArgs e)
        {
            if (!KiemTra()) return;

            string sql =
                "INSERT INTO LICH_KHAM_DINH_KY " +
                "(MaLichKham,MaHoSo,NgayHen,GioHen,TrangThai,GhiChu) " +
                "VALUES(@ma,@hs,@ngay,@gio,@tt,@gc)";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    GanThamSo(cmd);
                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value =
                        "LK" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Đặt lịch thành công.");
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đặt lịch:\n" + ex.Message);
            }
        }

        private void Sua(object sender, EventArgs e)
        {
            if (maChon == null)
            {
                MessageBox.Show("Chọn lịch cần sửa.");
                return;
            }
            if (!KiemTra()) return;

            string sql =
                "UPDATE LICH_KHAM_DINH_KY SET MaHoSo=@hs,NgayHen=@ngay," +
                "GioHen=@gio,TrangThai=@tt,GhiChu=@gc WHERE MaLichKham=@ma";

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

                MessageBox.Show("Cập nhật lịch thành công.");
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa lịch:\n" + ex.Message);
            }
        }

        private void Huy(object sender, EventArgs e)
        {
            if (maChon == null)
            {
                MessageBox.Show("Chọn lịch cần hủy.");
                return;
            }

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE LICH_KHAM_DINH_KY SET TrangThai=N'Đã hủy' " +
                    "WHERE MaLichKham=@ma", cn))
                {
                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 40).Value = maChon;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Đã hủy lịch.");
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hủy lịch:\n" + ex.Message);
            }
        }

        private void GanThamSo(SqlCommand cmd)
        {
            cmd.Parameters.Add("@hs", SqlDbType.VarChar, 40).Value =
                Convert.ToString(cmbHoSo.SelectedValue);
            cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = dtpNgay.Value.Date;
            cmd.Parameters.Add("@gio", SqlDbType.Time).Value = dtpGio.Value.TimeOfDay;
            cmd.Parameters.Add("@tt", SqlDbType.NVarChar, 30).Value =
                Convert.ToString(cmbTrangThai.SelectedItem);
            cmd.Parameters.Add("@gc", SqlDbType.NVarChar, 500).Value = txtGhiChu.Text.Trim();
        }

        private void ChonDong(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow r = dgv.Rows[e.RowIndex];
            maChon = Convert.ToString(r.Cells["MaLichKham"].Value);
            cmbHoSo.SelectedValue = r.Cells["MaHoSo"].Value;
            cmbTrangThai.SelectedItem = Convert.ToString(r.Cells["TrangThai"].Value);
            txtGhiChu.Text = Convert.ToString(r.Cells["GhiChu"].Value);

            if (r.Cells["NgayHen"].Value != DBNull.Value)
                dtpNgay.Value = Convert.ToDateTime(r.Cells["NgayHen"].Value);

            if (r.Cells["GioHen"].Value != DBNull.Value)
                dtpGio.Value = DateTime.Today.Add((TimeSpan)r.Cells["GioHen"].Value);
        }

        private void LamMoi(object sender, EventArgs e)
        {
            maChon = null;
            txtGhiChu.Clear();
            cmbTrangThai.SelectedIndex = 0;
            TaiHoSo();
            dgv.ClearSelection();
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
