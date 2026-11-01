# BÁO CÁO TỔNG HỢP BÀI TẬP THỰC HÀNH (LAB 0 – LAB 8)
## HỌC PHẦN: PHƯƠNG PHÁP PHÁT TRIỂN PHẦN MỀM HƯỚNG ĐỐI TƯỢNG (OOSD)

---

### 👨‍🎓 THÔNG TIN SINH VIÊN
- **Họ và tên:** NGUYỄN VĂN HOÀNG NHẬT
- **Mã số sinh viên (MSSV):** 1250080125
- **Lớp học phần:** 12_DH_CNPM2 (CNPM2)
- **Trường:** Đại học Tài nguyên và Môi trường TP. Hồ Chí Minh
- **Khoa:** Công nghệ Thông tin
- **Giảng viên hướng dẫn:** Bộ môn Kỹ thuật Phần mềm
- **Thời gian nộp bài:** Chủ nhật, ngày 01/11/2026

---

### 🌐 THÔNG TIN REPOSITORY VÀ HÌNH VẼ
- **Đường link Repository GitHub:** [https://github.com/hgnhat1511/LAB_OOSD](https://github.com/hgnhat1511/LAB_OOSD)
- **Thư mục IMAGES lưu trữ hình vẽ & sơ đồ gốc:** [https://github.com/hgnhat1511/LAB_OOSD/tree/main/IMAGES](https://github.com/hgnhat1511/LAB_OOSD/tree/main/IMAGES)
- **Quyền truy cập:** **PUBLIC (Công khai)** – Luôn duy trì ở chế độ công khai để giảng viên truy cập kiểm tra và đối chiếu các hình vẽ.

---

### 📑 FILE WORD TỔNG HỢP NỘP BÀI
- **File chính thức:** [`BAO_CAO_TONG_HOP_LAB_OOSD_1250080125_NguyenVanHoangNhat.docx`](./BAO_CAO_TONG_HOP_LAB_OOSD_1250080125_NguyenVanHoangNhat.docx)
- **Bản sao lưu:** [`TONG_HOP_BAI_LAB_OOSD.docx`](./TONG_HOP_BAI_LAB_OOSD.docx)
- **Quy mô báo cáo:** 223 trang, 1147 đoạn văn, 106 bảng dữ liệu, 119 hình vẽ và sơ đồ.
- **Mục lục:** Mục lục tự động chuẩn xác thể hiện chi tiết từng LAB và số trang tương ứng.
- **Đường link GitHub:** Được đặt nổi bật ở ngay đầu file Word (Trang 2) kèm theo bảng tổng hợp.

---

### 📂 DANH MỤC TỔNG HỢP CÁC BÀI LAB

| STT | Bài LAB | Tên đề tài / Nội dung trọng tâm | Công cụ thiết kế & Công nghệ | Thư mục mã nguồn | Thư mục hình vẽ |
| :---: | :--- | :--- | :--- | :--- | :--- |
| 1 | **LAB 0** | Cài đặt & kiểm tra môi trường phát triển | JDK 21, Maven 3.9, Git, Docker, PlantUML | [`LAB0/`](./LAB0/) | [`IMAGES/LAB0/`](./IMAGES/LAB0/) |
| 2 | **LAB 1** | Phân tích yêu cầu & Mô hình hóa ban đầu (Thư viện online & Cửa hàng NGK) | Use Case Model, Analysis Class, Sequence | [`LAB1/`](./LAB1/) | [`IMAGES/LAB1/`](./IMAGES/LAB1/) |
| 3 | **LAB 2** | Hệ thống Quản lý Thư viện | C# WinForms, SQL Server, ERD, Sequence | [`LAB2/`](./LAB2/) | [`IMAGES/LAB2/`](./IMAGES/LAB2/) |
| 4 | **LAB 3** | Hệ thống Quản lý Khách sạn | Kiến trúc 3 tầng (UI - Services - Data), SQL Server | [`LAB3/`](./LAB3/) | [`IMAGES/LAB3/`](./IMAGES/LAB3/) |
| 5 | **LAB 4** | Phân tích và Thiết kế hệ thống e-Shopping | 15 Use Cases, Checkout, Activity, Sequence, ERD 3NF | [`LAB4/`](./LAB4/) | [`IMAGES/LAB4/`](./IMAGES/LAB4/) |
| 6 | **LAB 5** | Phân tích & Thiết kế HT Quản lý Công ty Du lịch (Văn Hóa Việt TP.HCM) | PlantUML, 6 Sequence Diagrams, ERD, WinForms | [`LAB5/`](./LAB5/) | [`IMAGES/LAB5/`](./IMAGES/LAB5/) |
| 7 | **LAB 6** | Quản lý Hồ sơ Bệnh án của Bệnh nhân | Draw.io (SVG/XML), ERD 13 bảng, WinForms | [`LAB6/`](./LAB6/) | [`IMAGES/LAB6/`](./IMAGES/LAB6/) |
| 8 | **LAB 7** | Quản lý Nhân sự – Công ty sơn Đông Á | 4 Giai đoạn, PlantUML (.puml), State Machine | [`LAB7/`](./LAB7/) | [`IMAGES/LAB7/`](./IMAGES/LAB7/) |
| 9 | **LAB 8** | Quản lý Cửa hàng Nước Giải Khát | StarUML, 4 Giai đoạn PT-TK, ERD, WinForms | [`LAB8/`](./LAB8/) | [`IMAGES/LAB8/`](./IMAGES/LAB8/) |

---

### 🎨 QUY ĐỊNH VỀ HÌNH VẼ PHÂN TÍCH VÀ THIẾT KẾ TRÊN GITHUB
Toàn bộ sơ đồ, hình vẽ trong phần phân tích và thiết kế hệ thống được lưu trữ trong thư mục [`IMAGES/`](./IMAGES/):
- Giữ nguyên định dạng gốc và vector có chứa siêu dữ liệu mô hình chuyên dụng:
  + **Draw.io:** Các file `.drawio.svg` chứa mã nguồn XML mô hình hóa đầy đủ (có thể mở chỉnh sửa trực tiếp trên diagrams.net).
  + **PlantUML:** Các file mã nguồn `.puml` và sơ đồ kết xuất.
  + **StarUML / Visio:** Các sơ đồ phân rã chi tiết, biểu đồ trạng thái, biểu đồ hoạt động và biểu đồ tuần tự.
- Giảng viên có thể truy cập trực tiếp thư mục [`IMAGES/`](./IMAGES/) trên GitHub để kiểm tra đối chiếu tính xác thực của từng bài thực hành.
