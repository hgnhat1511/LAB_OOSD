
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmBenhNhan : Form
    {
        private TextBox txtTen, txtDiaChi, txtSDT, txtAnh, txtTim;
        private DateTimePicker dtpNgaySinh;
        private DataGridView dgv;
        private string maChon = null;

        public FrmBenhNhan()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaiDanhSach();
        }

        private void TaoGiaoDien()
        {
            Controls.Clear();
            Text = "Quản lý bệnh nhân";
            Size = new Size(1050, 680);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "QUẢN LÝ BỆNH NHÂN";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.SetBounds(300, 15, 450, 35);
            title.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(title);

            Nhan("Họ tên", 25, 70);
            txtTen = ONhap(125, 70, 300);

            Nhan("Địa chỉ", 25, 110);
            txtDiaChi = ONhap(125, 110, 300);

            Nhan("Ngày sinh", 490, 70);
            dtpNgaySinh = new DateTimePicker();
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.SetBounds(590, 70, 220, 25);
            Controls.Add(dtpNgaySinh);

            Nhan("Điện thoại", 490, 110);
            txtSDT = ONhap(590, 110, 220);

            Nhan("Ảnh 3x4", 25, 150);
            txtAnh = ONhap(125, 150, 300);
            Nut("Chọn ảnh", 435, 147, ChonAnh);

            Nut("Thêm", 25, 195, Them);
            Nut("Sửa", 135, 195, Sua);
            Nut("Xóa", 245, 195, Xoa);
            Nut("Làm mới", 355, 195, LamMoi);

            txtTim = ONhap(490, 198, 280);
            Nut("Tìm", 780, 195, Tim);

            dgv = new DataGridView();
            dgv.SetBounds(20, 245, 990, 380);
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.CellClick += ChonDong;
            Controls.Add(dgv);
        }

        private void TaiDanhSach(string q = "")
        {
            string sql = "SELECT MaBenhNhan,HoTen,DiaChi,NgaySinh," +
                         "SoDienThoai,Anh3x4 FROM BENH_NHAN";

            if (q.Trim() != "")
                sql += " WHERE HoTen LIKE @q OR SoDienThoai LIKE @q";

            sql += " ORDER BY HoTen";

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

                Cot("MaBenhNhan", "Mã bệnh nhân");
                Cot("HoTen", "Họ tên");
                Cot("DiaChi", "Địa chỉ");
                Cot("NgaySinh", "Ngày sinh");
                Cot("SoDienThoai", "Điện thoại");
                Cot("Anh3x4", "Ảnh");

                if (dgv.Columns.Contains("NgaySinh"))
                    dgv.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải bệnh nhân:\n" + ex.Message);
            }
        }

        private void Them(object sender, EventArgs e)
        {
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Nhập họ tên bệnh nhân.");
                return;
            }

            string ma = "BN" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string sql = "INSERT INTO BENH_NHAN " +
                "(MaBenhNhan,HoTen,DiaChi,NgaySinh,SoDienThoai,Anh3x4) " +
                "VALUES(@ma,@ten,@dc,@ns,@sdt,@anh)";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    GanThamSo(cmd);
                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 30).Value = ma;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Thêm bệnh nhân thành công.");
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm bệnh nhân:\n" + ex.Message);
            }
        }

        private void Sua(object sender, EventArgs e)
        {
            if (maChon == null)
            {
                MessageBox.Show("Chọn bệnh nhân cần sửa.");
                return;
            }

            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Nhập họ tên.");
                return;
            }

            string sql = "UPDATE BENH_NHAN SET HoTen=@ten,DiaChi=@dc," +
                "NgaySinh=@ns,SoDienThoai=@sdt,Anh3x4=@anh WHERE MaBenhNhan=@ma";

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    GanThamSo(cmd);
                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 30).Value = maChon;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Cập nhật thành công.");
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật:\n" + ex.Message);
            }
        }

        private void Xoa(object sender, EventArgs e)
        {
            if (maChon == null)
            {
                MessageBox.Show("Chọn bệnh nhân cần xóa.");
                return;
            }

            if (MessageBox.Show("Xóa bệnh nhân đã chọn?", "Xác nhận",
                MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection cn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM BENH_NHAN WHERE MaBenhNhan=@ma", cn))
                {
                    cmd.Parameters.Add("@ma", SqlDbType.VarChar, 30).Value = maChon;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Xóa thành công.");
                LamMoi(null, null);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa bệnh nhân đã có dữ liệu liên quan.\n" + ex.Message);
            }
        }

        private void Tim(object sender, EventArgs e)
        {
            TaiDanhSach(txtTim.Text);
        }

        private void ChonDong(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow r = dgv.Rows[e.RowIndex];
            maChon = Convert.ToString(r.Cells["MaBenhNhan"].Value);
            txtTen.Text = Convert.ToString(r.Cells["HoTen"].Value);
            txtDiaChi.Text = Convert.ToString(r.Cells["DiaChi"].Value);
            txtSDT.Text = Convert.ToString(r.Cells["SoDienThoai"].Value);
            txtAnh.Text = Convert.ToString(r.Cells["Anh3x4"].Value);

            if (r.Cells["NgaySinh"].Value != DBNull.Value)
                dtpNgaySinh.Value = Convert.ToDateTime(r.Cells["NgaySinh"].Value);
        }

        private void ChonAnh(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Ảnh|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtAnh.Text = dlg.FileName;
            }
        }

        private void LamMoi(object sender, EventArgs e)
        {
            maChon = null;
            txtTen.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtAnh.Clear();
            txtTim.Clear();
            dgv.ClearSelection();
        }

        private void GanThamSo(SqlCommand cmd)
        {
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 100).Value = txtTen.Text.Trim();
            cmd.Parameters.Add("@dc", SqlDbType.NVarChar, 255).Value = txtDiaChi.Text.Trim();
            cmd.Parameters.Add("@ns", SqlDbType.Date).Value = dtpNgaySinh.Value.Date;
            cmd.Parameters.Add("@sdt", SqlDbType.VarChar, 15).Value = txtSDT.Text.Trim();
            cmd.Parameters.Add("@anh", SqlDbType.NVarChar, 500).Value = txtAnh.Text.Trim();
        }

        private void Nhan(string s, int x, int y)
        {
            Label l = new Label();
            l.Text = s;
            l.SetBounds(x, y, 95, 25);
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

        private void Cot(string ten, string hienThi)
        {
            if (dgv.Columns.Contains(ten))
                dgv.Columns[ten].HeaderText = hienThi;
        }
    }
}
