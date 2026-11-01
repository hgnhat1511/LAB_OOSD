using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace QL_CH_NuocGiaiKhat
{
    public class Db
    {
        private static Db instance;
        public static Db Instance
        {
            get { if (instance == null) instance = new Db(); return Db.instance; }
            private set { Db.instance = value; }
        }

        private Db() { }

        // Đảm bảo chuỗi kết nối khớp với máy của bạn
        private string connectionString = @"Data Source=HOANGNHAT;Initial Catalog=QL_CH_NuocGiaiKhat;Integrated Security=True";

        // Hàm hỗ trợ tự động tìm và gán Parameters từ câu query một cách chính xác
        private void AddParameters(SqlCommand command, string query, object[] parameter)
        {
            if (parameter != null)
            {
                // Dùng Regex để tìm TẤT CẢ các từ bắt đầu bằng chữ @, ví dụ: @MaKH, @HoTen 
                // Cách này chính xác 100% dù bạn viết SQL có khoảng trắng hay dính liền dấu phẩy
                MatchCollection matches = Regex.Matches(query, @"@\w+");

                int i = 0;
                foreach (Match match in matches)
                {
                    if (i < parameter.Length)
                    {
                        command.Parameters.AddWithValue(match.Value, parameter[i]);
                        i++;
                    }
                }
            }
        }

        // Hàm dùng cho các câu lệnh SELECT (Trực tiếp trả về DataTable)
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    // Gọi hàm truyền tham số đã tối ưu
                    AddParameters(command, query, parameter);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL (ExecuteQuery): \n" + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }

        // Hàm dùng cho các câu lệnh INSERT, UPDATE, DELETE (Trả về số dòng thành công)
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    // Gọi hàm truyền tham số đã tối ưu
                    AddParameters(command, query, parameter);

                    data = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi CSDL (ExecuteNonQuery): \n" + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }
    }
}