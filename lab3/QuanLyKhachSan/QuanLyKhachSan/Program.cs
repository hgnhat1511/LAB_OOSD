using System;
using System.Windows.Forms;
using QuanLyKhachSan.Forms; // Thêm dòng này để gọi được thư mục Forms

namespace QuanLyKhachSan
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Sửa dòng dưới đây để chạy FrmMain
            Application.Run(new FrmMain());
        }
    }
}