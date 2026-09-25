# LAB 3: HỆ THỐNG QUẢN LÝ KHÁCH SẠN

**1. Thông tin sinh viên**
- Họ tên: Nguyễn Văn Hoàng Nhật
- MSSV: 1250080125
- Lớp: CNPM_2

**2. Môi trường / Version**
- Ngôn ngữ: C# (WinForms), .NET Framework 4.7.2
- CSDL: SQL Server (LocalDB)
- IDE: Visual Studio 2022

**3. Nội dung đã thực hiện**
- Xây dựng kiến trúc 3 lớp (UI, Services, Data).
- Thiết kế CSDL SQL Server (Phong, TienNghi, PhieuDatPhong, DichVu, HoaDon, KhachHang...).
- Thiết kế hệ thống WinForms và hoàn thiện các module: Danh mục, Phòng - Tiện nghi, Đặt - Nhận phòng, Dịch vụ, Trả phòng & Thống kê.

**4. Kết quả**
- Hệ thống hoạt động trơn tru.
- Các quy tắc nghiệp vụ (trùng lịch, cộng dồn dịch vụ, tính tiền) được kiểm thử thành công.
- (Chi tiết giao diện và CSDL xem trong file Word đính kèm).

**5. Lỗi gặp phải & Cách khắc phục**
- Lỗi: Khởi tạo bảng Khách hàng bị trống dữ liệu không hiển thị lên DataGridView.
- Khắc phục: Bổ sung Script `INSERT INTO` dữ liệu mẫu giả định cho bảng KhachHang.

**6. Hướng dẫn chạy chương trình**
- Mở file `QuanLyKhachSan.sln` bằng Visual Studio 2022.
- Mở SQL Server chạy file script để khởi tạo CSDL `QuanLyKhachSan`.
- Nhấn F5 để chạy chương trình.