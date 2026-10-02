using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace eShoppingApp.Data
{
    public class DatabaseConnection
    {
        // Lấy chuỗi kết nối từ file App.config
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["eShoppingConn"].ConnectionString;

        // Hàm này dùng cho lệnh SELECT (lấy dữ liệu ra dưới dạng bảng)
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        // Hàm này dùng cho lệnh INSERT, UPDATE, DELETE (thêm, sửa, xóa)
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
    }
}