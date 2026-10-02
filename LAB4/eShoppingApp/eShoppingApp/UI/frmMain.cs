using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using eShoppingApp.Data;

namespace eShoppingApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Gọi class kết nối Database mà mình đã tạo
                var db = new DatabaseConnection();

                // 2. Viết câu lệnh SQL để lấy dữ liệu từ bảng SANPHAM
                string sql = "SELECT MaSP AS [Mã SP], TenSP AS [Tên Sản Phẩm], GiaBan AS [Giá Bán] FROM SANPHAM";

                // 3. Thực thi lệnh và đổ dữ liệu vào cái bảng DataGridView
                dataGridView1.DataSource = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                // Báo lỗi nếu có trục trặc
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}