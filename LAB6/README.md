# LAB 6 – QUẢN LÝ HỒ SƠ BỆNH ÁN

## 1. Giới thiệu

Xây dựng ứng dụng quản lý hồ sơ bệnh án bằng C# Windows Forms và SQL Server. Hệ thống hỗ trợ quản lý thông tin bệnh nhân, đăng ký khám, hồ sơ bệnh án, khám chữa bệnh, xét nghiệm, toa thuốc, thanh toán và lịch khám sức khỏe định kỳ.

## 2. Mục tiêu và sản phẩm đầu ra

- Phân tích nghiệp vụ đăng ký bệnh nhân, lệ phí năm, hồ sơ bệnh án, khám bệnh, xét nghiệm, toa thuốc, lịch khám định kỳ và hoàn tất đợt điều trị.
- Xác định Class, ID, association, multiplicity, Actor, Use Case và đặc tả Use Case trọng tâm.
- Xây dựng Class Diagram, Use Case Diagram tổng quát, Use Case Diagram phân rã từ đăng ký khám đến xét nghiệm và nhận toa thuốc.
- Thiết kế Activity Diagram và Sequence Diagram cho nghiệp vụ lập toa thuốc.
- Thiết kế cơ sở dữ liệu SQL Server với khóa chính, khóa ngoại, ràng buộc UNIQUE và transaction cho nghiệp vụ lưu nhiều bảng.
- Xây dựng ứng dụng Windows Forms theo từng module và kiểm thử các quy tắc nghiệp vụ.

## 3. Công nghệ sử dụng

- Ngôn ngữ: C#.
- Giao diện: Windows Forms.
- Hệ quản trị cơ sở dữ liệu: Microsoft SQL Server.
- Truy cập dữ liệu: ADO.NET với `SqlConnection`, `SqlCommand` và `SqlDataAdapter`.
- Công cụ phát triển: Visual Studio.
- Quản lý mã nguồn: Git và GitHub.

## 4. Các chức năng chính

| Form | Chức năng |
|---|---|
| `FrmMain` | Màn hình chính, điều hướng các chức năng. |
| `FrmBenhNhan` | Thêm, sửa, xóa, tìm kiếm bệnh nhân và chọn ảnh 3 × 4. |
| `FrmHoSoBenhAn` | Tạo và tra cứu hồ sơ bệnh án. |
| `FrmPhieuKham` | Lập phiếu khám, ghi nhận triệu chứng, bác sĩ, khoa và các chỉ số sức khỏe. |
| `FrmToaThuoc` | Lập toa thuốc, ghi nhận thuốc, liều dùng, cách dùng và số lượng. |
| `FrmXetNghiem` | Chỉ định xét nghiệm và cập nhật kết quả. |
| `FrmThanhToan` | Ghi nhận phí đăng ký hằng năm và viện phí. |
| `FrmLichKhamDinhKy` | Đặt, sửa và hủy lịch khám định kỳ. |

## 5. Cơ sở dữ liệu

Tên database: `QuanLyHoSoBenhAn`.

Các bảng chính:

- `BENH_NHAN`: Thông tin bệnh nhân.
- `PHIEU_DANG_KY`: Thông tin đăng ký và phí đăng ký hằng năm.
- `HO_SO_BENH_AN`: Hồ sơ bệnh án và mã hồ sơ.
- `KHOA`, `BAC_SI`: Danh mục khoa và bác sĩ.
- `PHIEU_KHAM`: Thông tin từng lần khám.
- `TOA_THUOC`, `CHI_TIET_TOA_THUOC`: Toa thuốc và danh sách thuốc.
- `XET_NGHIEM`: Chỉ định và kết quả xét nghiệm.
- `THANH_TOAN`: Thông tin phí đăng ký và viện phí.
- `LICH_KHAM_DINH_KY`: Lịch hẹn khám sức khỏe định kỳ.
- `NHAN_VIEN`, `THEO_DOI_SAU_DIEU_TRI`: Nhân viên và dữ liệu theo dõi sau điều trị.

## 6. Hướng dẫn cài đặt và chạy chương trình

### Bước 1. Chuẩn bị môi trường

Cài đặt Visual Studio với workload phát triển ứng dụng .NET Desktop và SQL Server. Mở solution của dự án trong Visual Studio.

### Bước 2. Tạo cơ sở dữ liệu

Mở SQL Server Management Studio (SSMS), kết nối đến SQL Server và chạy script SQL tạo database, bảng, khóa ngoại cùng dữ liệu danh mục mẫu.

Database phải có tên `QuanLyHoSoBenhAn`.

### Bước 3. Cấu hình kết nối

Mở `Database.cs` và kiểm tra chuỗi kết nối:

```csharp
@"Data Source=HOANGNHAT;Initial Catalog=QuanLyHoSoBenhAn;Integrated Security=True;TrustServerCertificate=True;"
```

Thay `HOANGNHAT` nếu tên SQL Server trên máy khác. Chuỗi kết nối trên sử dụng Windows Authentication.

### Bước 4. Chạy ứng dụng

1. Build solution bằng `Ctrl + Shift + B`.
2. Kiểm tra và sửa các lỗi biên dịch nếu có.
3. Nhấn `F5` để chạy chương trình.
4. Mở các chức năng từ màn hình chính.

## 7. Quy trình kiểm thử nghiệp vụ

Thực hiện kiểm thử theo thứ tự:

1. Thêm một bệnh nhân mới.
2. Ghi nhận phí đăng ký hằng năm.
3. Tạo hồ sơ bệnh án cho bệnh nhân đã hoàn tất phí đăng ký.
4. Lập phiếu khám và kiểm tra thông tin bác sĩ, khoa, triệu chứng.
5. Tạo chỉ định xét nghiệm và cập nhật kết quả.
6. Lập toa thuốc cho phiếu khám và kiểm tra dữ liệu đã lưu.
7. Đặt lịch khám sức khỏe định kỳ cho bệnh nhân có hồ sơ.
8. Ghi nhận viện phí và kiểm tra lịch sử thanh toán.

Các trường hợp kiểm thử bổ sung gồm đăng ký trùng năm, tạo hồ sơ trùng, nhập thiếu dữ liệu bắt buộc, chọn sai hồ sơ, nhập số lượng thuốc không hợp lệ và xóa dữ liệu đang được bảng khác tham chiếu.

Kết quả Pass/Fail cần được ghi nhận sau khi chạy thử thực tế trên môi trường SQL Server.

## 8. Kết quả mong đợi

Ứng dụng cho phép quản lý thông tin bệnh nhân và các nghiệp vụ liên quan đến hồ sơ bệnh án trên cơ sở dữ liệu SQL Server. Các biểu mẫu sử dụng chung dữ liệu, liên kết với nhau bằng khóa chính và khóa ngoại nhằm bảo đảm tính nhất quán.

**Lưu ý:** Đây là sản phẩm phục vụ học tập và thực hành phân tích, thiết kế, lập trình hướng đối tượng; không phải phần mềm y tế dùng trực tiếp trong môi trường khám chữa bệnh thực tế.

## 9. Repository

GitHub: https://github.com/hgnhat1511/LAB_OOSD

Thư mục bài thực hành: `LAB6/`
