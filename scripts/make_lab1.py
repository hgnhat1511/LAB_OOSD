import docx
from docx.shared import Inches, Pt, RGBColor
from docxcompose.composer import Composer
import sys

sys.stdout.reconfigure(encoding='utf-8')

# 1. Base doc with Heading 1 and Heading 2 for Bai 1
doc = docx.Document()
for section in doc.sections:
    section.top_margin = Inches(0.8)
    section.bottom_margin = Inches(0.8)
    section.left_margin = Inches(1.0)
    section.right_margin = Inches(1.0)

h1 = doc.add_heading(level=1)
h1.paragraph_format.space_before = Pt(12)
h1.paragraph_format.space_after = Pt(8)
r = h1.add_run("LAB 1: PHÂN TÍCH VÀ THIẾT KẾ HỆ THỐNG")
r.font.name = "Times New Roman"
r.font.size = Pt(16)
r.font.bold = True
r.font.color.rgb = RGBColor(0, 32, 96)

h2_1 = doc.add_heading(level=2)
h2_1.paragraph_format.space_before = Pt(8)
h2_1.paragraph_format.space_after = Pt(6)
r = h2_1.add_run("Phần 1 – Bài 1: Xây dựng hệ thống thư viện trực tuyến")
r.font.name = "Times New Roman"
r.font.size = Pt(14)
r.font.bold = True

doc.save(r'scripts\lab1_header.docx')

# Compose with Bai 1
composer = Composer(doc)
doc_bai1 = docx.Document(r'LAB1\NguyenVanHoangNhat_CNPM2_Buoi2_Bai1.docx')
composer.append(doc_bai1)

# Transition to Bai 2
doc_mid = docx.Document()
h2_2 = doc_mid.add_heading(level=2)
h2_2.paragraph_format.space_before = Pt(16)
h2_2.paragraph_format.space_after = Pt(6)
r = h2_2.add_run("Phần 2 – Bài 2: Hệ thống quản lý cửa hàng nước giải khát")
r.font.name = "Times New Roman"
r.font.size = Pt(14)
r.font.bold = True
doc_mid.save(r'scripts\lab1_mid.docx')

composer.append(doc_mid)
doc_bai2 = docx.Document(r'LAB1\NguyenVanHoangNhat_CNPM2_Buoi2_Bai2.docx')
composer.append(doc_bai2)

composer.save(r'scripts\LAB1_formatted.docx')
print('LAB1_formatted.docx created successfully!')
