# 🛒 Hệ Thống e-SHOPPING - LAB 4 (Bài Tập Lý Thuyết Tuần 5)

Dự án ứng dụng Desktop quản lý bán hàng trực tuyến, được thiết kế và hiện thực theo phương pháp hướng đối tượng (OOSD), áp dụng chuẩn kiến trúc phân tầng UI → Service/Adapter → Data.

## 🎯 Mục Tiêu Dự Án
* **Phân tích nghiệp vụ:** Phân biệt rõ các chức năng nội bộ của hệ thống e-SHOPPING với 3 hệ thống/dịch vụ bên ngoài: Hệ thống quản lý sản phẩm, Dịch vụ thanh toán trực tuyến và Dịch vụ Email.
* **Mô hình hóa UML:** Xây dựng trọn bộ mô hình UML theo trình tự từ pha phân tích đến pha thiết kế bao gồm: Use Case, Lớp phân tích, Trạng thái, Tuần tự, Lớp thiết kế, Thiết kế theo chức năng, Hoạt động.
* **Thiết kế & Hiện thực:** Thiết kế CSDL SQL Server bám sát và nhất quán với mô hình lớp. Hiện thực prototype bằng ngôn ngữ **C# WinForms (.NET Framework 4.7.2)**.
* **Kiểm thử & Truy vết:** Thực hiện kiểm thử phần mềm theo kịch bản và thiết lập ma trận truy vết toàn diện: *Yêu cầu → UML → Form/Service → Bảng CSDL → Test case*.



## 🛠 Công Nghệ & Cấu Trúc Thư Mục
* **Công cụ phát triển:** Visual Studio 2022.
* **Cơ sở dữ liệu:** SQL Server LocalDB (`(localdb)\MSSQLLocalDB`).
* **Kiến trúc hệ thống:** 
  * `UI`: Chứa các Form giao diện tương tác với người dùng.
  * `Services`: Chứa logic xử lý nghiệp vụ nội bộ và các Adapter kết nối hệ thống ngoài.
  * `Data`: Lớp thao tác trực tiếp với cơ sở dữ liệu bằng ADO.NET.

## 📥 Cách Nộp Bài (Submission Guidelines)
* Đưa toàn bộ nội dung bài tập lý thuyết và source code lên GitHub.
* Phân loại/Tag nhánh nộp bài: **lab4**.