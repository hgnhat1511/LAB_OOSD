
using System.Data.SqlClient;

namespace QuanLyHoSoBenhAn
{
    public static class Database
    {
        private static string connectionString =
            @"Data Source=HOANGNHAT;Initial Catalog=QuanLyHoSoBenhAn;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
