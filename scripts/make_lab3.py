import docx
from docx.shared import Inches, Pt, RGBColor
from docxcompose.composer import Composer
import sys

sys.stdout.reconfigure(encoding='utf-8')

doc = docx.Document()
for section in doc.sections:
    section.top_margin = Inches(0.8)
    section.bottom_margin = Inches(0.8)
    section.left_margin = Inches(1.0)
    section.right_margin = Inches(1.0)

h1 = doc.add_heading(level=1)
h1.paragraph_format.space_before = Pt(12)
h1.paragraph_format.space_after = Pt(8)
r = h1.add_run("LAB 3: HỆ THỐNG QUẢN LÝ KHÁCH SẠN")
r.font.name = "Times New Roman"
r.font.size = Pt(16)
r.font.bold = True
r.font.color.rgb = RGBColor(0, 32, 96)

h2_1 = doc.add_heading(level=2)
h2_1.paragraph_format.space_before = Pt(8)
h2_1.paragraph_format.space_after = Pt(4)
r = h2_1.add_run("1. Tổng quan hệ thống và môi trường thực hiện")
r.font.name = "Times New Roman"
r.font.size = Pt(13)
r.font.bold = True

p1 = doc.add_paragraph()
p1.paragraph_format.space_after = Pt(4)
p1.add_run("• Học phần: Phương pháp phát triển phần mềm hướng đối tượng (OOSD)\n")
p1.add_run("• Sinh viên thực hiện: Nguyễn Văn Hoàng Nhật - MSSV: 1250080125 - Lớp: 12_DH_CNPM2\n")
p1.add_run("• Môi trường phát triển: C# (WinForms), .NET Framework 4.7.2, IDE Visual Studio 2022, CSDL SQL Server (LocalDB).\n")
p1.add_run("• Kiến trúc phần mềm: Mô hình 3 lớp phân tách rõ ràng (UI, Services, Data access layer).")

h2_2 = doc.add_heading(level=2)
h2_2.paragraph_format.space_before = Pt(8)
h2_2.paragraph_format.space_after = Pt(4)
r = h2_2.add_run("2. Thiết kế cơ sở dữ liệu và các module chức năng")
r.font.name = "Times New Roman"
r.font.size = Pt(13)
r.font.bold = True

p2 = doc.add_paragraph()
p2.paragraph_format.space_after = Pt(8)
p2.add_run("Hệ thống quản lý khách sạn được xây dựng với đầy đủ các bảng dữ liệu: Phong, TienNghi, PhieuDatPhong, DichVu, HoaDon, KhachHang, ChiTietDichVu, PhieuNhanPhong... Hoàn thiện các module giao diện người dùng:\n")
p2.add_run("  + Quản lý Danh mục (FrmDanhMuc) & Quản lý Phòng - Tiện nghi (FrmPhongTienNghi).\n")
p2.add_run("  + Nghiệp vụ Đặt phòng & Nhận phòng (FrmDatPhong).\n")
p2.add_run("  + Quản lý Dịch vụ gia tăng (FrmDichVu).\n")
p2.add_run("  + Nghiệp vụ Trả phòng & Lập hóa đơn thanh toán (FrmTraPhong).\n")
p2.add_run("  + Thống kê doanh thu và báo cáo hoạt động (FrmThongKe).\n")
p2.add_run("Dưới đây là chi tiết kết quả thực hiện các bài tập hướng dẫn, bài tập trên lớp và bài tập về nhà:")

doc.save(r'scripts\lab3_header.docx')

composer = Composer(doc)
doc_lab3 = docx.Document(r'LAB3\LAB3_NguyenVanHoangNhat.docx')
composer.append(doc_lab3)
composer.save(r'scripts\LAB3_formatted.docx')
print('LAB3_formatted.docx created successfully!')
