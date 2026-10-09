
# LAB 5 - QUẢN LÝ CÔNG TY DU LỊCH

## A. MỤC TIÊU

- Phân tích và mô tả nghiệp vụ công ty du lịch Văn Hóa Việt, bao gồm: quản lý tour và hành trình, khách lẻ đăng ký theo chuyến, khách đăng ký theo đoàn, phân công hướng dẫn viên, tính lương, kết thúc tour và khảo sát khách hàng.
- Trả lời đầy đủ 6 câu hỏi của đề bài bằng bộ mô hình UML theo trình tự pha phân tích và pha thiết kế.
- Thiết kế cơ sở dữ liệu SQL Server nhất quán với mô hình lớp.
- Hiện thực hệ thống bằng C# WinForms trên .NET Framework 4.7.2 theo kiến trúc ba lớp: UI → Service → Data.
- Kiểm thử hệ thống bằng các kịch bản có dữ liệu cụ thể; truy vết yêu cầu → UML → Form/Service → bảng CSDL → test case.

## B. CÔNG NGHỆ SỬ DỤNG

- **Ngôn ngữ:** C#
- **Giao diện:** Windows Forms (WinForms)
- **Nền tảng:** .NET Framework 4.7.2
- **Cơ sở dữ liệu:** Microsoft SQL Server
- **Môi trường phát triển:** Visual Studio
- **Quản lý mã nguồn:** Git và GitHub

## C. CHỨC NĂNG HỆ THỐNG

Các chức năng chính của hệ thống dự kiến bao gồm:

1. Quản lý tour du lịch.
2. Quản lý khách hàng.
3. Đăng ký tour theo đoàn hoặc theo chuyến.
4. Phân công hướng dẫn viên.
5. Quản lý thanh toán.
6. Quản lý hành trình, điểm dừng chân và phương tiện.
7. Quản lý nhân viên và tính lương.
8. Kết thúc tour và khảo sát ý kiến khách hàng.

## D. KIẾN TRÚC HỆ THỐNG

Hệ thống được định hướng theo kiến trúc ba lớp:

| Lớp | Trách nhiệm |
|---|---|
| UI | Hiển thị giao diện WinForms, tiếp nhận thao tác của người dùng. |
| Service | Xử lý nghiệp vụ, kiểm tra dữ liệu và điều phối các thao tác. |
| Data | Kết nối SQL Server, truy vấn và cập nhật dữ liệu. |

Luồng xử lý tổng quát:

`Người dùng → UI → Service → Data → SQL Server`

Kết quả xử lý được trả ngược về giao diện để hiển thị cho người dùng.

## E. CƠ SỞ DỮ LIỆU

Cơ sở dữ liệu dự kiến sử dụng tên `QuanLyCongTyDuLich`.

Các nhóm dữ liệu chính:

- Tour và hành trình.
- Khách hàng, đăng ký đoàn và thành viên đoàn.
- Chuyến đi và đăng ký chuyến.
- Nhân viên và phân công hướng dẫn viên.
- Thanh toán.
- Điểm dừng chân, phương tiện và điểm tham quan.
- Khảo sát sau chuyến đi.

Thiết kế bảng, khóa chính, khóa ngoại và các ràng buộc dữ liệu cần bảo đảm tính nhất quán với mô hình lớp và quy tắc nghiệp vụ.

## F. KIỂM THỬ VÀ TRUY VẾT YÊU CẦU

Việc kiểm thử được thực hiện dựa trên các kịch bản nghiệp vụ có dữ liệu cụ thể.

Quy trình truy vết:

`Yêu cầu nghiệp vụ → Mô hình UML → Form/Service → Bảng CSDL → Test case`

Mỗi kịch bản kiểm thử cần xác định dữ liệu đầu vào, thao tác thực hiện, kết quả mong đợi và kết quả thực tế.

Các trường hợp cần kiểm thử bao gồm:

- Thêm, sửa, xóa và tra cứu thông tin.
- Kiểm tra dữ liệu đầu vào không hợp lệ.
- Kiểm tra ràng buộc khóa chính và khóa ngoại.
- Kiểm tra đăng ký tour, thanh toán và phân công hướng dẫn viên.
- Kiểm tra tính nhất quán dữ liệu giữa các lớp và cơ sở dữ liệu.

## G. CÀI ĐẶT VÀ CHẠY CHƯƠNG TRÌNH

### Yêu cầu môi trường

- Visual Studio hỗ trợ .NET Framework 4.7.2.
- Microsoft SQL Server.
- Cơ sở dữ liệu `QuanLyCongTyDuLich`.

### Các bước thực hiện

1. Clone repository về máy:

   ```bash
   git clone https://github.com/hgnhat1511/LAB_OOSD.git
   ```

2. Mở solution của Lab 5 trong Visual Studio.
3. Tạo cơ sở dữ liệu bằng script SQL của dự án.
4. Cấu hình chuỗi kết nối SQL Server phù hợp với môi trường đang sử dụng.
5. Build solution để kiểm tra lỗi.
6. Chạy chương trình bằng Visual Studio.

**Lưu ý:** Tên solution, tên file script SQL và cấu hình kết nối cần được cập nhật theo các file thực tế trong repository.

## H. TRẠNG THÁI THỰC HIỆN

- [x] Xây dựng giao diện chính và các form chức năng cơ bản.
- [x] Khởi tạo cơ sở dữ liệu SQL Server.
- [x] Xây dựng lớp kết nối cơ sở dữ liệu.
- [ ] Hoàn thiện kết nối từng form với SQL Server.
- [ ] Hoàn thiện các lớp Service và Data theo kiến trúc ba lớp.
- [ ] Hoàn thiện các mô hình UML theo yêu cầu đề bài.
- [ ] Kiểm thử đầy đủ các kịch bản nghiệp vụ và truy vết yêu cầu.

*Trạng thái cần được cập nhật theo tiến độ thực tế của dự án.*
