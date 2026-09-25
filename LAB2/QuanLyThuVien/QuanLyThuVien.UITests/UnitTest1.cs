using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using NUnit.Framework;
using System.IO;

namespace QuanLyThuVien.UITests
{
    public class MainFormTests
    {
        [Test]
        public void Test_ClickMoFormQuảnLySach_ThanhCong()
        {
            // 1. Trỏ đường dẫn tới file .exe của phần mềm bạn vừa code xong
            // (Đường dẫn này lùi lại 3 thư mục để trỏ từ thư mục Test sang thư mục App chính)
            string appPath = Path.GetFullPath(@"..\..\..\QuanLyThuVien\bin\Debug\QuanLyThuVien.exe");

            // 2. Khởi chạy ứng dụng và Tool Automation
            using (var app = Application.Launch(appPath))
            using (var automation = new UIA3Automation())
            {
                // 3. Lấy cửa sổ chính của phần mềm
                var mainWindow = app.GetMainWindow(automation);
                Assert.IsNotNull(mainWindow, "Không thể mở được phần mềm Quản lý thư viện.");

                // 4. Tìm cái nút có chữ "Quản lý đầu sách" trên màn hình
                var btnSach = mainWindow.FindFirstDescendant(cf => cf.ByName("Quản lý đầu sách"))?.AsButton();
                Assert.IsNotNull(btnSach, "Không tìm thấy nút Quản lý đầu sách trên giao diện.");

                // 5. Ra lệnh cho chuột tự động Click vào nút đó
                btnSach.Invoke();

                // 6. Đợi 1 giây và kiểm tra xem cửa sổ "Quản lý đầu sách" đã xuất hiện chưa
                System.Threading.Thread.Sleep(1000);
                var sachWindow = mainWindow.FindFirstDescendant(cf => cf.ByName("Quản lý đầu sách"));

                // 7. Chốt kết quả Test (Nếu form mở lên = Pass, không mở lên = Fail)
                Assert.IsNotNull(sachWindow, "Test Thất Bại: Form Quản lý đầu sách không được mở lên sau khi click.");

                // Đóng ứng dụng sau khi test xong
                app.Close();
            }
        }
    }
}