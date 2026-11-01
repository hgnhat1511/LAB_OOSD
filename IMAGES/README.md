# THƯ MỤC IMAGES – TOÀN BỘ SƠ ĐỒ & FILE NGUỒN DRAW.IO CÁC BÀI LAB OOSD

> **Học phần:** Phương pháp phát triển phần mềm hướng đối tượng (OOSD)  
> **Sinh viên:** Nguyễn Văn Hoàng Nhật  
> **MSSV:** 1250080125  
> **Lớp:** 12_DH_CNPM2  
> **Repository:** https://github.com/hgnhat1511/LAB_OOSD  

---

## 🎯 TUÂN THỦ QUY ĐỊNH BẮT BUỘC VỀ FILE HÌNH VẼ
Tất cả các sơ đồ, hình vẽ trong phần phân tích và thiết kế hệ thống được tạo bằng công cụ chuyên dụng:
- **Draw.io (diagrams.net):** Toàn bộ các file `.drawio` và `.drawio.svg` (giữ nguyên mô hình vector XML, không phải ảnh chụp màn hình raster) có thể mở, xem và chỉnh sửa trực tiếp trên [app.diagrams.net](https://app.diagrams.net/) hoặc plugin Draw.io trên VS Code / Visual Studio.
- **PlantUML:** Các file mã nguồn `.puml` định nghĩa chi tiết các biểu đồ tuần tự và lớp thiết kế.

---

## 📁 DANH MỤC CÁC FILE DRAW.IO THEO TỪNG BÀI LAB

### 1. `IMAGES/LAB0/` – Môi trường & Minh chứng thực hành
- Lưu trữ 10 hình ảnh minh chứng kiểm tra cấu hình hệ thống: Windows 11, JDK 21, Maven 3.9, Git 2.48, Docker Desktop, PlantUML plugin.

### 2. `IMAGES/LAB1/` – Phân tích yêu cầu & Mô hình hóa ban đầu
- 📄 [`LAB1_Bai1_UseCase_Model.drawio`](./LAB1/LAB1_Bai1_UseCase_Model.drawio): Sơ đồ Use Case Model hệ thống thư viện trực tuyến (13 ca sử dụng, quan hệ include/extend).
- 📄 [`LAB1_Bai2_UseCase_TongQuat.drawio`](./LAB1/LAB1_Bai2_UseCase_TongQuat.drawio): Sơ đồ Use Case tổng quát Cửa hàng nước giải khát (Actor Khách quen, Khách vãng lai, Nhân viên, Nhà cung ứng).
- 📄 [`LAB1_Bai2_Class_Diagram.drawio`](./LAB1/LAB1_Bai2_Class_Diagram.drawio): Sơ đồ lớp phân tích đầy đủ (NhanHieu, NuocGiaiKhat, NhaCungUng, DonDatHang, ChiTietDDH, PhieuGiaoHang, ChiTietPGH, KhachHang, HoaDon, ChiTietHD, PhieuHen, PhieuGhiNo).
- 📄 [`LAB1_Bai2_Activity_Diagram.drawio`](./LAB1/LAB1_Bai2_Activity_Diagram.drawio): Biểu đồ hoạt động quy trình bán hàng, kiểm kho, hẹn giao và ghi nợ.
- 📄 [`LAB1_Bai2_Sequence_Diagram.drawio`](./LAB1/LAB1_Bai2_Sequence_Diagram.drawio): Biểu đồ tuần tự nghiệp vụ Lập phiếu ghi nợ cho khách quen.

### 3. `IMAGES/LAB2/` – Hệ thống Quản lý Thư viện
- 📄 [`LAB2_UseCase_TongQuat.drawio`](./LAB2/LAB2_UseCase_TongQuat.drawio): Sơ đồ Use Case tổng quát toàn hệ thống thư viện.
- 📄 [`LAB2_Class_Diagram.drawio`](./LAB2/LAB2_Class_Diagram.drawio): Sơ đồ lớp (Class Diagram) quản lý sách, độc giả, phiếu mượn/trả.
- 📄 [`LAB2_ERD_QuanLyThuVien.drawio`](./LAB2/LAB2_ERD_QuanLyThuVien.drawio): Sơ đồ thực thể liên kết (ERD) cơ sở dữ liệu thư viện.
- 📄 [`LAB2_Activity_MuonTraSach.drawio`](./LAB2/LAB2_Activity_MuonTraSach.drawio): Biểu đồ hoạt động luồng Mượn - Trả sách và xử lý vi phạm.
- 📄 [`LAB2_Sequence_LapPhieuMuon.drawio`](./LAB2/LAB2_Sequence_LapPhieuMuon.drawio): Biểu đồ tuần tự nghiệp vụ Lập phiếu mượn sách.

### 4. `IMAGES/LAB3/` – Hệ thống Quản lý Khách sạn
- 📄 [`LAB3_UseCase_QuanLyKhachSan.drawio`](./LAB3/LAB3_UseCase_QuanLyKhachSan.drawio): Sơ đồ Use Case hệ thống Quản lý Khách sạn (Lễ tân, Quản lý, Khách thuê).
- 📄 [`LAB3_ERD_QuanLyKhachSan.drawio`](./LAB3/LAB3_ERD_QuanLyKhachSan.drawio): Sơ đồ cơ sở dữ liệu quan hệ ERD (Phong, LoaiPhong, TienNghi, KhachHang, PhieuDatPhong, DichVu, HoaDon).

### 5. `IMAGES/LAB4/` – Hệ thống e-SHOPPING
- 📄 [`LAB4_UseCase_TongQuat.drawio`](./LAB4/LAB4_UseCase_TongQuat.drawio): Sơ đồ Use Case tổng quát (15 ca sử dụng, phân quyền Customer & Admin).
- 📄 [`LAB4_Activity_Checkout.drawio`](./LAB4/LAB4_Activity_Checkout.drawio): Biểu đồ hoạt động luồng kiểm tra tồn kho, áp dụng mã miễn phí vận chuyển ($0 nếu >= $50, $5 nếu < $50) và thanh toán thẻ.
- 📄 [`LAB4_Analysis_Class_Diagram.drawio`](./LAB4/LAB4_Analysis_Class_Diagram.drawio): Sơ đồ lớp phân tích (SanPham, NhomSanPham, GioHang, KhachHang, DonHang, ChiTietDonHang, ThanhToan, GiaoHang).
- 📄 [`LAB4_DangKy_DangNhap.drawio`](./LAB4/LAB4_DangKy_DangNhap.drawio): Sơ đồ phân tích luồng Đăng ký & Đăng nhập hệ thống e-Shopping.

### 6. `IMAGES/LAB5/` – Quản lý Công ty Du lịch (Văn Hóa Việt TP.HCM)
- 📄 [`LAB5_UseCase_QuanLyDuLich.drawio`](./LAB5/LAB5_UseCase_QuanLyDuLich.drawio): Sơ đồ Use Case toàn hệ thống du lịch.
- 📄 [`LAB5_Activity_DangKyTour.drawio`](./LAB5/LAB5_Activity_DangKyTour.drawio): Biểu đồ hoạt động quy trình đăng ký tour và thanh toán đặt cọc.

### 7. `IMAGES/LAB6/` – Quản lý Hồ sơ Bệnh án của Bệnh nhân
- 📄 [`LAB6_Activity_Diagram.drawio`](./LAB6/LAB6_Activity_Diagram.drawio): Biểu đồ hoạt động quy trình khám chữa bệnh và lưu trữ bệnh án.
- 📄 [`LAB6_Class_Diagram.drawio`](./LAB6/LAB6_Class_Diagram.drawio): Biểu đồ lớp phân tích và thiết kế chi tiết.
- 📄 [`LAB6_ERD_Diagram.drawio`](./LAB6/LAB6_ERD_Diagram.drawio): Sơ đồ cơ sở dữ liệu ERD 13 bảng.
- 📄 [`LAB6_UseCase_Diagram.drawio`](./LAB6/LAB6_UseCase_Diagram.drawio): Sơ đồ Use Case tổng quát.
- 📄 [`LAB6_PhanRa_ChucNang.drawio`](./LAB6/LAB6_PhanRa_ChucNang.drawio): Sơ đồ phân rã chức năng nghiệp vụ.
- 📄 [`LAB6_Sequence_Diagram.drawio`](./LAB6/LAB6_Sequence_Diagram.drawio): Biểu đồ tuần tự khám bệnh, kê đơn và lưu kết quả.

### 8. `IMAGES/LAB7/` – Quản lý Nhân sự (Công ty Sơn Đông Á)
- 📄 [`LAB7_UseCase_QuanLyNhanSu.drawio`](./LAB7/LAB7_UseCase_QuanLyNhanSu.drawio): Sơ đồ Use Case quản lý nhân sự (CBNS, Giám đốc, Kế toán, Ban chấm công).
- 📄 [`LAB7_State_NhanVien.drawio`](./LAB7/LAB7_State_NhanVien.drawio): Biểu đồ chuyển trạng thái của nhân viên (Ứng viên -> Thử việc -> Chính thức -> Thôi việc).
- 📄 Toàn bộ các file mã nguồn PlantUML (`.puml`): `SD_Design_KhenThuong.puml`, `SD_Design_PhieuChi.puml`, `SD_Design_LapHopDong.puml`, `SD_Design_PhuTroi.puml`, `SD_Design_LapBangLuong.puml`, `SD_Design_XoaBangLuong.puml`.

### 9. `IMAGES/LAB8/` – Quản lý Cửa hàng Nước Giải Khát
- 📄 [`LAB8_Hinh2_1_UseCase_TongQuat.drawio`](./LAB8/LAB8_Hinh2_1_UseCase_TongQuat.drawio): Sơ đồ Use Case tổng quát theo đặc tả StarUML.
- 📄 [`LAB8_Hinh2_6_State_HoaDon.drawio`](./LAB8/LAB8_Hinh2_6_State_HoaDon.drawio): Biểu đồ chuyển trạng thái Hóa đơn.
- 📄 [`LAB8_Hinh3_3_ERD.drawio`](./LAB8/LAB8_Hinh3_3_ERD.drawio): Sơ đồ thực thể liên kết ERD đầy đủ các bảng dữ liệu.

---

## 🛠️ HƯỚNG DẪN MỞ VÀ CHỈNH SỬA FILE DRAW.IO
1. Truy cập trực tuyến: [https://app.diagrams.net/](https://app.diagrams.net/)
2. Chọn **Open Existing Diagram** -> Chọn file `.drawio` từ máy hoặc chọn trực tiếp từ GitHub Repository.
3. Hoặc cài đặt extension **Draw.io Integration** trên Visual Studio Code để xem và chỉnh sửa trực tiếp ngay trong mã nguồn.
