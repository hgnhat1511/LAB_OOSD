
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich
{
    public partial class FrmTour : Form
    {
        private TextBox txtMaTour;
        private TextBox txtTenTour;
        private NumericUpDown nudSoNgay;
        private NumericUpDown nudSoDem;
        private NumericUpDown nudDonGia;
        private DataGridView dgvTour;

        private DataTable bangTour;

        public FrmTour()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaoBangDuLieu();
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý tour du lịch";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 650);
            MinimumSize = new Size(850, 550);
            Font = new Font("Arial", 10);
            BackColor = Color.WhiteSmoke;

            Controls.Clear();

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "QUẢN LÝ TOUR DU LỊCH";
            lblTieuDe.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 55;
            Controls.Add(lblTieuDe);

            GroupBox gbThongTin = new GroupBox();
            gbThongTin.Text = "Thông tin tour";
            gbThongTin.Location = new Point(20, 65);
            gbThongTin.Size = new Size(940, 175);
            Controls.Add(gbThongTin);

            TaoNhan(gbThongTin, "Mã tour:", 20, 32);
            txtMaTour = TaoTextBox(gbThongTin, 120, 28, 280);

            TaoNhan(gbThongTin, "Tên tour:", 450, 32);
            txtTenTour = TaoTextBox(gbThongTin, 550, 28, 350);

            TaoNhan(gbThongTin, "Số ngày:", 20, 82);
            nudSoNgay = TaoSo(gbThongTin, 120, 78, 100, 1, 100);

            TaoNhan(gbThongTin, "Số đêm:", 250, 82);
            nudSoDem = TaoSo(gbThongTin, 330, 78, 100, 0, 100);

            TaoNhan(gbThongTin, "Đơn giá (VNĐ):", 450, 82);
            nudDonGia = TaoSo(gbThongTin, 570, 78, 200, 0, 1000000000);
            nudDonGia.Increment = 100000;

            Label lblGhiChu = new Label();
            lblGhiChu.Text = "Nhập đầy đủ thông tin tour trước khi thêm hoặc cập nhật.";
            lblGhiChu.AutoSize = true;
            lblGhiChu.ForeColor = Color.DimGray;
            lblGhiChu.Location = new Point(20, 130);
            gbThongTin.Controls.Add(lblGhiChu);

            dgvTour = new DataGridView();
            dgvTour.Location = new Point(20, 255);
            dgvTour.Size = new Size(940, 270);
            dgvTour.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                           | AnchorStyles.Left | AnchorStyles.Right;
            dgvTour.AllowUserToAddRows = false;
            dgvTour.AllowUserToDeleteRows = false;
            dgvTour.ReadOnly = true;
            dgvTour.MultiSelect = false;
            dgvTour.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTour.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvTour.BackgroundColor = Color.White;
            dgvTour.RowHeadersVisible = false;
            dgvTour.SelectionChanged += DgvTour_SelectionChanged;
            Controls.Add(dgvTour);

            Button btnThem = TaoNut("Thêm", 160, 545);
            btnThem.Click += BtnThem_Click;
            Controls.Add(btnThem);

            Button btnSua = TaoNut("Sửa", 300, 545);
            btnSua.Click += BtnSua_Click;
            Controls.Add(btnSua);

            Button btnXoa = TaoNut("Xóa", 440, 545);
            btnXoa.Click += BtnXoa_Click;
            Controls.Add(btnXoa);

            Button btnLamMoi = TaoNut("Làm mới", 580, 545);
            btnLamMoi.Click += BtnLamMoi_Click;
            Controls.Add(btnLamMoi);

            Button btnDong = TaoNut("Đóng", 720, 545);
            btnDong.Click += (s, e) => Close();
            Controls.Add(btnDong);
        }

        private void TaoNhan(Control cha, string noiDung, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = noiDung;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(115, 25);
            cha.Controls.Add(lbl);
        }

        private TextBox TaoTextBox(Control cha, int x, int y, int rong)
        {
            TextBox txt = new TextBox();
            txt.Location = new Point(x, y);
            txt.Size = new Size(rong, 25);
            cha.Controls.Add(txt);
            return txt;
        }

        private NumericUpDown TaoSo(Control cha, int x, int y,
            int rong, decimal min, decimal max)
        {
            NumericUpDown nud = new NumericUpDown();
            nud.Location = new Point(x, y);
            nud.Size = new Size(rong, 25);
            nud.Minimum = min;
            nud.Maximum = max;
            cha.Controls.Add(nud);
            return nud;
        }

        private Button TaoNut(string noiDung, int x, int y)
        {
            Button btn = new Button();
            btn.Text = noiDung;
            btn.Location = new Point(x, y);
            btn.Size = new Size(110, 38);
            btn.Font = new Font("Arial", 10);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private void TaoBangDuLieu()
        {
            bangTour = new DataTable();

            bangTour.Columns.Add("MaTour", typeof(string));
            bangTour.Columns.Add("TenTour", typeof(string));
            bangTour.Columns.Add("SoNgay", typeof(int));
            bangTour.Columns.Add("SoDem", typeof(int));
            bangTour.Columns.Add("DonGia", typeof(decimal));
            bangTour.PrimaryKey = new DataColumn[]
            {
                bangTour.Columns["MaTour"]
             };

            dgvTour.DataSource = bangTour;

            dgvTour.Columns["MaTour"].HeaderText = "Mã tour";
            dgvTour.Columns["TenTour"].HeaderText = "Tên tour";
            dgvTour.Columns["SoNgay"].HeaderText = "Số ngày";
            dgvTour.Columns["SoDem"].HeaderText = "Số đêm";
            dgvTour.Columns["DonGia"].HeaderText = "Đơn giá (VNĐ)";

            dgvTour.Columns["DonGia"].DefaultCellStyle.Format = "N0";
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaTour.Text))
            {
                MessageBox.Show("Vui lòng nhập mã tour.");
                txtMaTour.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenTour.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tour.");
                txtTenTour.Focus();
                return false;
            }

            if (nudSoDem.Value > nudSoNgay.Value)
            {
                MessageBox.Show("Số đêm không được lớn hơn số ngày.");
                return false;
            }

            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            foreach (DataRow row in bangTour.Rows)
            {
                if (string.Equals(
                    row["MaTour"].ToString().Trim(),
                    txtMaTour.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã tour đã tồn tại.");
                    txtMaTour.Focus();
                    return;
                }
            }

            bangTour.Rows.Add(
                txtMaTour.Text.Trim(),
                txtTenTour.Text.Trim(),
                (int)nudSoNgay.Value,
                (int)nudSoDem.Value,
                nudDonGia.Value
            );

            MessageBox.Show("Thêm tour thành công.");
            XoaTrang();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvTour.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tour cần sửa.");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            string maTourCu =
                dgvTour.CurrentRow.Cells["MaTour"].Value.ToString();

            foreach (DataRow row in bangTour.Rows)
            {
                if (!string.Equals(
                    row["MaTour"].ToString(),
                    maTourCu,
                    StringComparison.OrdinalIgnoreCase)
                    && string.Equals(
                    row["MaTour"].ToString(),
                    txtMaTour.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã tour mới đã được sử dụng.");
                    return;
                }
            }

            DataRow dong = bangTour.Rows.Find(maTourCu);

            // Bảng chưa có khóa chính nên tìm dòng theo mã tour.
            if (dong == null)
            {
                foreach (DataRow row in bangTour.Rows)
                {
                    if (row["MaTour"].ToString() == maTourCu)
                    {
                        dong = row;
                        break;
                    }
                }
            }

            if (dong != null)
            {
                dong["MaTour"] = txtMaTour.Text.Trim();
                dong["TenTour"] = txtTenTour.Text.Trim();
                dong["SoNgay"] = (int)nudSoNgay.Value;
                dong["SoDem"] = (int)nudSoDem.Value;
                dong["DonGia"] = nudDonGia.Value;

                MessageBox.Show("Cập nhật tour thành công.");
                XoaTrang();
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTour.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tour cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa tour này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                int viTri = dgvTour.CurrentRow.Index;
                if (viTri >= 0 && viTri < bangTour.Rows.Count)
                    bangTour.Rows.RemoveAt(viTri);

                XoaTrang();
                MessageBox.Show("Đã xóa tour.");
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
        }

        private void DgvTour_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTour.CurrentRow == null ||
                dgvTour.CurrentRow.IsNewRow)
                return;

            if (dgvTour.CurrentRow.Cells["MaTour"].Value == null)
                return;

            txtMaTour.Text =
                dgvTour.CurrentRow.Cells["MaTour"].Value.ToString();

            txtTenTour.Text =
                dgvTour.CurrentRow.Cells["TenTour"].Value.ToString();

            nudSoNgay.Value = Convert.ToDecimal(
                dgvTour.CurrentRow.Cells["SoNgay"].Value);

            nudSoDem.Value = Convert.ToDecimal(
                dgvTour.CurrentRow.Cells["SoDem"].Value);

            nudDonGia.Value = Convert.ToDecimal(
                dgvTour.CurrentRow.Cells["DonGia"].Value);
        }

        private void XoaTrang()
        {
            txtMaTour.Clear();
            txtTenTour.Clear();
            nudSoNgay.Value = 1;
            nudSoDem.Value = 0;
            nudDonGia.Value = 0;

            dgvTour.ClearSelection();
            txtMaTour.Focus();
        }
    }
}
