import os
import shutil
import zipfile
import docx
import sys

sys.stdout.reconfigure(encoding='utf-8')

images_root = r'D:\NAM4\PP_Ptr PM HDT\BTN\LAB_OOSD\IMAGES'
os.makedirs(images_root, exist_ok=True)

# 1. LAB0
lab0_dir = os.path.join(images_root, 'LAB0')
os.makedirs(lab0_dir, exist_ok=True)
src_lab0 = r'D:\NAM4\PP_Ptr PM HDT\BTN\LAB_OOSD\LAB0\evidence\screenshots'
if os.path.exists(src_lab0):
    for f in os.listdir(src_lab0):
        shutil.copy2(os.path.join(src_lab0, f), os.path.join(lab0_dir, f))
print('LAB0 images copied:', len(os.listdir(lab0_dir)))

# Function to extract images from a docx file into a folder
def extract_docx_media(docx_path, out_dir, prefix=''):
    os.makedirs(out_dir, exist_ok=True)
    extracted = []
    with zipfile.ZipFile(docx_path, 'r') as z:
        media_files = [f for f in z.namelist() if f.startswith('word/media/')]
        media_files.sort(key=lambda x: int(''.join(filter(str.isdigit, x)) or 0))
        for i, mf in enumerate(media_files):
            ext = os.path.splitext(mf)[1]
            out_name = f'{prefix}_{i+1:02d}{ext}' if prefix else f'image_{i+1:02d}{ext}'
            target_path = os.path.join(out_dir, out_name)
            with open(target_path, 'wb') as f_out:
                f_out.write(z.read(mf))
            extracted.append(target_path)
    return extracted

# 2. LAB1
lab1_dir = os.path.join(images_root, 'LAB1')
extract_docx_media(r'LAB1\NguyenVanHoangNhat_CNPM2_Buoi2_Bai1.docx', lab1_dir, prefix='LAB1_Bai1')
extract_docx_media(r'LAB1\NguyenVanHoangNhat_CNPM2_Buoi2_Bai2.docx', lab1_dir, prefix='LAB1_Bai2')
print('LAB1 images extracted:', len(os.listdir(lab1_dir)))

# 3. LAB2
lab2_dir = os.path.join(images_root, 'LAB2')
extract_docx_media(r'LAB2\BaoCaoBaiLAB2_CNPM2_NguyenVanHoangNhat.docx', lab2_dir, prefix='LAB2')
print('LAB2 images extracted:', len(os.listdir(lab2_dir)))

# 4. LAB3
lab3_dir = os.path.join(images_root, 'LAB3')
extract_docx_media(r'LAB3\LAB3_NguyenVanHoangNhat.docx', lab3_dir, prefix='LAB3')
print('LAB3 images extracted:', len(os.listdir(lab3_dir)))

# 5. LAB4
lab4_dir = os.path.join(images_root, 'LAB4')
extract_docx_media(r'LAB4\Lab4.docx', lab4_dir, prefix='LAB4')
print('LAB4 images extracted:', len(os.listdir(lab4_dir)))

# 6. LAB5
lab5_dir = os.path.join(images_root, 'LAB5')
extract_docx_media(r'LAB5\Bao_cao_Lab5_Quan_ly_cong_ty_du_lich.docx', lab5_dir, prefix='LAB5')
print('LAB5 images extracted:', len(os.listdir(lab5_dir)))

# 7. LAB6
lab6_dir = os.path.join(images_root, 'LAB6')
os.makedirs(lab6_dir, exist_ok=True)
src_lab6 = r'LAB6\images'
if os.path.exists(src_lab6):
    for f in os.listdir(src_lab6):
        src_f = os.path.join(src_lab6, f)
        dst_f = os.path.join(lab6_dir, f)
        shutil.copy2(src_f, dst_f)
extract_docx_media(r'LAB6\Bao_cao_Lab6.docx', lab6_dir, prefix='LAB6_Doc')
print('LAB6 files in IMAGES:', len(os.listdir(lab6_dir)))

# 8. LAB7
lab7_dir = os.path.join(images_root, 'LAB7')
os.makedirs(lab7_dir, exist_ok=True)
src_lab7 = r'LAB7\img'
if os.path.exists(src_lab7):
    for f in os.listdir(src_lab7):
        shutil.copy2(os.path.join(src_lab7, f), os.path.join(lab7_dir, f))

# Extract PUML from LAB7 docx
doc7 = docx.Document(r'LAB7\QuanLyNhanSu_DongA_Lab7.docx')
for t in doc7.tables:
    for r in t.rows:
        for c in r.cells:
            text = c.text.strip()
            if '@startuml' in text:
                first_line = [l.strip() for l in text.splitlines() if l.strip()][0]
                name = first_line.replace('@startuml', '').strip()
                if not name:
                    name = 'Diagram'
                puml_file = os.path.join(lab7_dir, f'{name}.puml')
                with open(puml_file, 'w', encoding='utf-8') as pf:
                    pf.write(text)
                print(f'Exported PUML: {puml_file}')

extract_docx_media(r'LAB7\QuanLyNhanSu_DongA_Lab7.docx', lab7_dir, prefix='LAB7_Doc')
print('LAB7 files in IMAGES:', len(os.listdir(lab7_dir)))

# 9. LAB8
lab8_dir = os.path.join(images_root, 'LAB8')
extract_docx_media(r'LAB8\Lab8_QuanLyCuaHangNuocGiaiKhat.docx', lab8_dir, prefix='LAB8')
print('LAB8 images extracted:', len(os.listdir(lab8_dir)))

# Also copy top-level DrawIO source files to IMAGES root and include subject diagrams
anh_dir = r'D:\NAM4\PP_Ptr PM HDT\anh'
if os.path.exists(anh_dir):
    for f in os.listdir(anh_dir):
        if f.endswith(('.drawio.svg', '.drawio.png', '.png', '.svg')):
            try:
                shutil.copy2(os.path.join(anh_dir, f), os.path.join(images_root, f))
            except Exception as e:
                print('Error copying anh file:', f, e)

# Write a comprehensive README in IMAGES
images_readme = os.path.join(images_root, 'README.md')
with open(images_readme, 'w', encoding='utf-8') as f:
    f.write('''# THƯ MỤC IMAGES – TOÀN BỘ SƠ ĐỒ PHÂN TÍCH VÀ THIẾT KẾ CÁC BÀI LAB OOSD

> **Học phần:** Phương pháp phát triển phần mềm hướng đối tượng (OOSD)  
> **Sinh viên:** Nguyễn Văn Hoàng Nhật  
> **MSSV:** 1250080125  
> **Lớp:** 12_DH_CNPM2  
> **Repository:** https://github.com/hgnhat1511/LAB_OOSD  

---

## 📁 DANH MỤC LƯU TRỮ HÌNH VẼ THEO TỪNG BÀI LAB

Thư mục này lưu trữ toàn bộ các hình vẽ phân tích, thiết kế, kiến trúc và minh chứng thực hành của tất cả các bài LAB (từ LAB 0 đến LAB 8) theo đúng quy định của môn học:
- **Công cụ thực hiện:** Draw.io (diagrams.net), PlantUML, StarUML, Visio / UML Modeling Tools.
- **Định dạng lưu trữ:** Giữ nguyên định dạng gốc và vector có chứa siêu dữ liệu mô hình (Draw.io SVG/XML, PlantUML `.puml`, v.v.) đảm bảo có thể mở xem và chỉnh sửa trực tiếp.

### 1. `LAB0/` - Cài đặt & kiểm tra môi trường
- Minh chứng cấu hình hệ thống máy tính Windows 11 x64.
- Cài đặt & kiểm tra Java Development Kit: JDK 21.
- Cài đặt & kiểm tra Apache Maven 3.9.
- Cài đặt & kiểm tra Git Version Control và cấu hình danh tính sinh viên.
- Cài đặt & kiểm tra Docker Desktop & Docker Compose.
- Cấu hình IDE và plugin PlantUML để mô hình hóa phần mềm.
- Kết quả chạy lệnh build & test dự án mẫu.

### 2. `LAB1/` - Phân tích yêu cầu & Mô hình hóa ban đầu
- **Bài 1 - Hệ thống Thư viện trực tuyến:**
  + Mô hình Use Case (Use Case Model).
  + Đặc tả chi tiết các Use Case chính (Tìm kiếm tài liệu, Đọc trực tuyến, Tải tài liệu, Mượn sách, Quản lý tài khoản).
- **Bài 2 - Hệ thống Cửa hàng nước giải khát:**
  + Sơ đồ Use Case tổng quát.
  + Sơ đồ lớp phân tích (Analysis Class Diagram).
  + Sơ đồ Use Case phân rã luồng bán hàng & đặt hàng.
  + Sơ đồ hoạt động (Activity Diagram).
  + Sơ đồ tuần tự (Sequence Diagram).

### 3. `LAB2/` - Hệ thống Quản lý Thư viện
- Hình 1: Sơ đồ Use Case tổng quát.
- Hình 2: Sơ đồ Lớp (Class Diagram) hệ thống thư viện.
- Hình 3: Sơ đồ Thực thể liên kết (ERD).
- Hình 4: Phác thảo thiết kế giao diện Form.
- Hình 5: Biểu đồ hoạt động (Activity Diagram) – Quy trình Mượn - Trả sách.
- Hình 6 - 8: Quy trình nghiệp vụ: Thêm mới sách, Nhận trả sách & Lập phiếu phạt, Cấp thẻ thư viện.
- Hình 9 - 12: Biểu đồ tuần tự (Sequence Diagram) – Cấp thẻ thư viện, Lập phiếu mượn, Nhận trả sách, Thêm mới đầu sách.
- Hình 13 - 14: Giao diện thực tế Form Main, Form Danh Mục.

### 4. `LAB3/` - Hệ thống Quản lý Khách sạn
- Kiến trúc hệ thống 3 lớp (UI, Services, Data).
- Thiết kế cơ sở dữ liệu và giao diện WinForms:
  + Danh mục phòng, Loại phòng, Khách hàng, Dịch vụ.
  + Quy trình đặt phòng, nhận phòng, lập hóa đơn và trả phòng.
  + Thống kê doanh thu và báo cáo tình trạng phòng.

### 5. `LAB4/` - Hệ thống e-SHOPPING
- Sơ đồ Use Case tổng quát (15 ca sử dụng phân rã chi tiết).
- Sơ đồ Use Case phân rã phân hệ Xử lý đơn hàng (Checkout).
- Sơ đồ lớp phân tích (Analysis Class Diagram) với các lớp thực thể SanPham, NhomSanPham, DonHang, GioHang, KhachHang, ThanhToan, GiaoHang.
- Sơ đồ hoạt động (Activity Diagram) mô tả luồng kiểm tra tồn kho, áp dụng mã giảm giá và tính phí vận chuyển.
- Sơ đồ tuần tự (Sequence Diagram) thanh toán và xử lý đơn hàng.
- Sơ đồ thực thể liên kết (ERD) chuẩn hóa mức 3NF.
- Sơ đồ kiến trúc thành phần (Component Diagram).
- Ma trận truy vết yêu cầu (Traceability Matrix).

### 6. `LAB5/` - Hệ thống Quản lý Công ty Du lịch (Văn Hóa Việt TP.HCM)
- Use Case Diagram tổng quát toàn hệ thống.
- Use Case Diagram phân rã quy trình từ đăng ký đến kết thúc tour.
- Sơ đồ cơ sở dữ liệu quan hệ (ERD).
- Biểu đồ hoạt động: Quy trình đăng ký tour, Quy trình tổ chức & kết thúc tour.
- 6 Biểu đồ tuần tự (Sequence Diagram):
  + Lập phiếu đăng ký tour du lịch theo đoàn.
  + Đăng ký tour theo chuyến.
  + Phân công hướng dẫn viên.
  + Thanh toán tour (đặt cọc, thanh toán đợt sau).
  + Gửi khảo sát và lưu phản hồi của khách hàng.
  + Thu phần chi phí còn lại của tour đoàn.
- Thiết kế giao diện WinForms đầy đủ các chức năng quản lý.

### 7. `LAB6/` - Hệ thống Quản lý Hồ sơ Bệnh án của Bệnh nhân
- **File gốc Draw.io (định dạng SVG/XML chứa mô hình có thể chỉnh sửa trực tiếp):**
  + `activity.drawio.svg`: Biểu đồ hoạt động quy trình khám chữa bệnh.
  + `Class =_Diagram.drawio.svg`: Biểu đồ lớp phân tích và thiết kế.
  + `erd.drawio.svg`: Sơ đồ thực thể liên kết ERD 13 bảng.
  + `lab6_usecase.svg`: Sơ đồ Use Case tổng quát.
  + `phẩn.drawio.svg` / `phanra.png`: Sơ đồ phân rã chức năng.
  + `sequence.drawio.svg`: Biểu đồ tuần tự nghiệp vụ khám và kê đơn.
- Giao diện ứng dụng WinForms: FormMain, Bệnh nhân, Phiếu khám, Xét nghiệm, Lịch khám định kỳ, Hồ sơ bệnh án, Toa thuốc, Thanh toán.

### 8. `LAB7/` - Hệ thống Quản lý Nhân sự (Công ty Sơn Đông Á)
- Sơ đồ Use Case tổng quát và phân rã thử việc - hợp đồng.
- Biểu đồ lớp phân tích (Class Diagram).
- Biểu đồ trạng thái (State Machine Diagram): Trạng thái Nhân viên, Quyết định khen thưởng, Quyết định kỷ luật.
- Biểu đồ hoạt động (Activity Diagram) luồng thử việc và ký hợp đồng.
- Biểu đồ lớp thiết kế (Design Class Diagram - Lớp thực thể và Lớp Form).
- Sơ đồ cơ sở dữ liệu quan hệ (ERD).
- **Mã nguồn và sơ đồ PlantUML:**
  + `SD_Design_KhenThuong.puml`
  + `SD_Design_PhieuChi.puml`
  + `SD_Design_LapHopDong.puml`
  + `SD_Design_PhuTroi.puml`
  + `SD_Design_LapBangLuong.puml`
  + `SD_Design_XoaBangLuong.puml`
- Giao diện thực tế Form Nhân viên, Phòng ban, Hợp đồng, Bảng lương, Khen thưởng, Kỷ luật.

### 9. `LAB8/` - Hệ thống Quản lý Cửa hàng Nước Giải Khát
- Hình 2.1: Use Case Diagram tổng quát toàn hệ thống.
- Hình 2.2: Use Case Diagram phân rã chi tiết luồng bán hàng và hẹn giao.
- Hình 2.3: Class Diagram phân tích phần nhập hàng từ nhà cung ứng.
- Hình 2.4: Class Diagram phân tích phần bán hàng, hẹn giao, ghi nợ.
- Hình 2.5: Class Diagram tổng quát toàn hệ thống.
- Hình 2.6 - 2.8: State Diagram cho Hóa đơn, Đơn đặt hàng, Phiếu ghi nợ.
- Hình 2.9: Activity Diagram quy trình bán hàng, hẹn giao đến giao đủ (3 phân đoạn).
- Hình 2.10: Sequence Diagram phân tích nghiệp vụ Lập phiếu ghi nợ cho khách quen.
- Hình 3.1 - 3.2: Class Diagram thiết kế phần nhập hàng và bán hàng/hẹn giao/ghi nợ.
- Hình 3.3: Sơ đồ thực thể liên kết ERD.
- Hình 3.4 - 3.5: Sequence Diagram thiết kế Lập hóa đơn/hẹn giao và Lập phiếu ghi nợ.
- Giao diện phần mềm WinForms thực tế (Bán hàng, Đơn đặt hàng, Phiếu giao hàng, Phiếu hẹn, Phiếu ghi nợ, Khách hàng).
''')
print('IMAGES/README.md written successfully!')
