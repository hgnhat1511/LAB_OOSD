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
r = h1.add_run("LAB 2: HỆ THỐNG QUẢN LÝ THƯ VIỆN")
r.font.name = "Times New Roman"
r.font.size = Pt(16)
r.font.bold = True
r.font.color.rgb = RGBColor(0, 32, 96)

p = doc.add_paragraph()
p.paragraph_format.space_after = Pt(8)
r = p.add_run("Hệ thống Quản lý Thư viện được xây dựng nhằm tin học hóa quy trình quản lý độc giả, sách, danh mục và mượn trả sách, áp dụng kiến trúc phần mềm hướng đối tượng kết hợp với hệ quản trị cơ sở dữ liệu SQL Server. Dưới đây là các mô hình phân tích, thiết kế UML và giao diện thực tế của hệ thống.")
r.font.name = "Times New Roman"
r.font.size = Pt(11)

doc.save(r'scripts\lab2_header.docx')

composer = Composer(doc)
doc_lab2 = docx.Document(r'LAB2\BaoCaoBaiLAB2_CNPM2_NguyenVanHoangNhat.docx')
composer.append(doc_lab2)
composer.save(r'scripts\LAB2_formatted.docx')
print('LAB2_formatted.docx created successfully!')
