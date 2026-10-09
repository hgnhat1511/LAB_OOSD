
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich
{
    public partial class FrmDangKyTour : Form
    {
        private TextBox txtMaDangKy;
        private TextBox txtMaKH;
        private TextBox txtHoTen;
        private ComboBox cboTour;
        private DateTimePicker dtpNgayDi;
        private NumericUpDown nudSoNguoi;
        private NumericUpDown nudDonGia;
        private Label lblTongTien;
        private DataGridView dgvDangKy;
        private DataTable bangDangKy;

        public FrmDangKyTour()
        {
            InitializeComponent();
            TaoGiaoDien();
            TaoBangDuLieu();
        }

        private void TaoGiaoDien()
        {
            Text = "Đăng ký tour du lịch";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1000, 680);
            MinimumSize = new Size(900, 600);
            Font = new Font("Arial", 10);
            BackColor = Color.WhiteSmoke;

            Controls.Clear();

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "ĐĂNG KÝ TOUR DU LỊCH";
            lblTieuDe.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 50;
            Controls.Add(lblTieuDe);

            GroupBox gbThongTin = new GroupBox();
            gbThongTin.Text = "Thông tin đăng ký";
            gbThongTin.Location = new Point(20, 60);
            gbThongTin.Size = new Size(940, 285);
            Controls.Add(gbThongTin);

            TaoNhan(gbThongTin, "Mã đăng ký:", 20, 32);
            txtMaDangKy = TaoTextBox(gbThongTin, 140, 28, 280);

            TaoNhan(gbThongTin, "Mã khách hàng:", 460, 32);
            txtMaKH = TaoTextBox(gbThongTin, 590, 28, 300);

            TaoNhan(gbThongTin, "Họ tên khách:", 20, 82);
            txtHoTen = TaoTextBox(gbThongTin, 140, 78, 280);

            TaoNhan(gbThongTin, "Tour đăng ký:", 460, 82);
            cboTour = new ComboBox();
            cboTour.Location = new Point(590, 78);
            cboTour.Size = new Size(300, 25);
            cboTour.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTour.Items.AddRange(new object[]
            {
                "TOUR01 - Đà Lạt",
                "TOUR02 - Nha Trang",
                "TOUR03 - Phú Quốc"
            });
            if (cboTour.Items.Count > 0)
                cboTour.SelectedIndex = 0;
            cboTour.SelectedIndexChanged += TinhTongTien;
            gbThongTin.Controls.Add(cboTour);

            TaoNhan(gbThongTin, "Ngày khởi hành:", 20, 132);
            dtpNgayDi = new DateTimePicker();
            dtpNgayDi.Location = new Point(140, 128);
            dtpNgayDi.Size = new Size(280, 25);
            dtpNgayDi.Format = DateTimePickerFormat.Short;
            dtpNgayDi.MinDate = DateTime.Today;
            gbThongTin.Controls.Add(dtpNgayDi);

            TaoNhan(gbThongTin, "Số người:", 460, 132);
            nudSoNguoi = new NumericUpDown();
            nudSoNguoi.Location = new Point(590, 128);
            nudSoNguoi.Size = new Size(120, 25);
            nudSoNguoi.Minimum = 1;
            nudSoNguoi.Maximum = 1000;
            nudSoNguoi.Value = 1;
            nudSoNguoi.ValueChanged += TinhTongTien;
            gbThongTin.Controls.Add(nudSoNguoi);

            TaoNhan(gbThongTin, "Đơn giá/người:", 20, 182);
            nudDonGia = new NumericUpDown();
            nudDonGia.Location = new Point(140, 178);
            nudDonGia.Size = new Size(280, 25);
            nudDonGia.Maximum = 1000000000;
            nudDonGia.Increment = 100000;
            nudDonGia.ValueChanged += TinhTongTien;
            gbThongTin.Controls.Add(nudDonGia);

            Button btnTinhTien = new Button();
            btnTinhTien.Text = "Tính tổng tiền";
            btnTinhTien.Location = new Point(460, 175);
            btnTinhTien.Size = new Size(150, 35);
            btnTinhTien.Click += TinhTongTien;
            gbThongTin.Controls.Add(btnTinhTien);

            lblTongTien = new Label();
            lblTongTien.Text = "TỔNG TIỀN: 0 VNĐ";
            lblTongTien.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTongTien.ForeColor = Color.DarkRed;
            lblTongTien.Location = new Point(20, 235);
            lblTongTien.Size = new Size(500, 30);
            gbThongTin.Controls.Add(lblTongTien);

            dgvDangKy = new DataGridView();
            dgvDangKy.Location = new Point(20, 360);
            dgvDangKy.Size = new Size(940, 220);
            dgvDangKy.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                             | AnchorStyles.Left | AnchorStyles.Right;
            dgvDangKy.AllowUserToAddRows = false;
            dgvDangKy.AllowUserToDeleteRows = false;
            dgvDangKy.ReadOnly = true;
            dgvDangKy.MultiSelect = false;
            dgvDangKy.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvDangKy.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvDangKy.BackgroundColor = Color.White;
            dgvDangKy.RowHeadersVisible = false;
            dgvDangKy.SelectionChanged += DgvDangKy_SelectionChanged;
            Controls.Add(dgvDangKy);

            TaoNut("Đăng ký", 160, 600, BtnDangKy_Click);
            TaoNut("Xóa đăng ký", 300, 600, BtnXoa_Click);
            TaoNut("Làm mới", 440, 600, BtnLamMoi_Click);
            TaoNut("Đóng", 580, 600, (s, e) => Close());
        }

        private void TaoNhan(Control cha, string noiDung, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = noiDung;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(125, 25);
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

        private void TaoNut(string noiDung, int x, int y,
            EventHandler suKien)
        {
            Button btn = new Button();
            btn.Text = noiDung;
            btn.Location = new Point(x, y);
            btn.Size = new Size(120, 38);
            btn.Font = new Font("Arial", 10);
            btn.Click += suKien;
            Controls.Add(btn);
        }

        private void TaoBangDuLieu()
        {
            bangDangKy = new DataTable();
            bangDangKy.Columns.Add("MaDangKy", typeof(string));
            bangDangKy.Columns.Add("MaKH", typeof(string));
            bangDangKy.Columns.Add("HoTen", typeof(string));
            bangDangKy.Columns.Add("Tour", typeof(string));
            bangDangKy.Columns.Add("NgayDi", typeof(DateTime));
            bangDangKy.Columns.Add("SoNguoi", typeof(int));
            bangDangKy.Columns.Add("TongTien", typeof(decimal));

            bangDangKy.PrimaryKey = new DataColumn[]
            {
                bangDangKy.Columns["MaDangKy"]
            };

            dgvDangKy.DataSource = bangDangKy;
            dgvDangKy.Columns["MaDangKy"].HeaderText = "Mã đăng ký";
            dgvDangKy.Columns["MaKH"].HeaderText = "Mã khách hàng";
            dgvDangKy.Columns["HoTen"].HeaderText = "Họ tên khách";
            dgvDangKy.Columns["Tour"].HeaderText = "Tour";
            dgvDangKy.Columns["NgayDi"].HeaderText = "Ngày khởi hành";
            dgvDangKy.Columns["SoNguoi"].HeaderText = "Số người";
            dgvDangKy.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvDangKy.Columns["NgayDi"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvDangKy.Columns["TongTien"].DefaultCellStyle.Format = "N0";
        }

        private void TinhTongTien(object sender, EventArgs e)
        {
            if (nudSoNguoi == null || nudDonGia == null || lblTongTien == null)
                return;

            decimal tongTien = nudSoNguoi.Value * nudDonGia.Value;
            lblTongTien.Text = "TỔNG TIỀN: " + tongTien.ToString("N0") + " VNĐ";
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDangKy.Text) ||
                string.IsNullOrWhiteSpace(txtMaKH.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã đăng ký, mã khách hàng và họ tên.");
                return;
            }

            string ma = txtMaDangKy.Text.Trim();

            if (bangDangKy.Rows.Find(ma) != null)
            {
                MessageBox.Show("Mã đăng ký đã tồn tại.");
                return;
            }

            decimal tongTien = nudSoNguoi.Value * nudDonGia.Value;

            bangDangKy.Rows.Add(
                ma,
                txtMaKH.Text.Trim(),
                txtHoTen.Text.Trim(),
                cboTour.Text,
                dtpNgayDi.Value.Date,
                (int)nudSoNguoi.Value,
                tongTien
            );

            MessageBox.Show("Đăng ký tour thành công.");
            XoaTrang();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDangKy.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đăng ký cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa đăng ký này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                string ma = dgvDangKy.CurrentRow.Cells["MaDangKy"]
                    .Value.ToString();

                DataRow dong = bangDangKy.Rows.Find(ma);
                if (dong != null)
                    bangDangKy.Rows.Remove(dong);

                XoaTrang();
                MessageBox.Show("Đã xóa đăng ký.");
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
        }

        private void DgvDangKy_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDangKy.CurrentRow == null ||
                dgvDangKy.CurrentRow.IsNewRow ||
                dgvDangKy.CurrentRow.Cells["MaDangKy"].Value == null)
                return;

            txtMaDangKy.Text =
                dgvDangKy.CurrentRow.Cells["MaDangKy"].Value.ToString();
            txtMaKH.Text =
                dgvDangKy.CurrentRow.Cells["MaKH"].Value.ToString();
            txtHoTen.Text =
                dgvDangKy.CurrentRow.Cells["HoTen"].Value.ToString();

            string tenTour =
                dgvDangKy.CurrentRow.Cells["Tour"].Value.ToString();

            if (cboTour.Items.Contains(tenTour))
                cboTour.SelectedItem = tenTour;

            dtpNgayDi.Value = Convert.ToDateTime(
                dgvDangKy.CurrentRow.Cells["NgayDi"].Value);

            nudSoNguoi.Value = Convert.ToDecimal(
                dgvDangKy.CurrentRow.Cells["SoNguoi"].Value);

            nudDonGia.Value = nudSoNguoi.Value == 0 ? 0 :
                Convert.ToDecimal(
                    dgvDangKy.CurrentRow.Cells["TongTien"].Value)
                / nudSoNguoi.Value;

            TinhTongTien(sender, e);
        }

        private void XoaTrang()
        {
            txtMaDangKy.Clear();
            txtMaKH.Clear();
            txtHoTen.Clear();
            if (cboTour.Items.Count > 0)
                cboTour.SelectedIndex = 0;

            if (dtpNgayDi.MinDate <= DateTime.Today)
                dtpNgayDi.Value = DateTime.Today;

            nudSoNguoi.Value = 1;
            nudDonGia.Value = 0;
            dgvDangKy.ClearSelection();
            txtMaDangKy.Focus();
            TinhTongTien(null, EventArgs.Empty);
        }
    }
}
