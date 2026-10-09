using Microsoft.Data.SqlClient;
using System;

namespace QuanLyCongTyDuLich
{
    public static class DatabaseConnection
    {
        private static readonly string connectionString =
            @"Server=HOANGNHAT;Database=QuanLyCongTyDuLich;User ID=sa;Password=123;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool KiemTraKetNoi()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Không thể kết nối database!\n" + ex.Message,
                    "Lỗi kết nối"
                );

                return false;
            }
        }
    }
}