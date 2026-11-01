import docx
from docx.shared import Inches, Pt, RGBColor
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.oxml import parse_xml
from docx.oxml.ns import nsdecls, qn
import sys
import os

sys.stdout.reconfigure(encoding='utf-8')

def build_lab3():
    source_path = r'LAB3\Lab3.docx'
    print(f"Loading {source_path}...")
    doc = docx.Document(source_path)
    
    # Update first paragraph to be standard Heading 1: LAB 3: HỆ THỐNG QUẢN LÝ KHÁCH SẠN
    p0 = doc.paragraphs[0]
    p0.text = "LAB 3: HỆ THỐNG QUẢN LÝ KHÁCH SẠN"
    p0.style = 'Heading 1'
    p0.paragraph_format.space_before = Pt(12)
    p0.paragraph_format.space_after = Pt(8)
    for r in p0.runs:
        r.font.name = "Times New Roman"
        r.font.size = Pt(16)
        r.font.bold = True
        r.font.color.rgb = RGBColor(0, 32, 96)
        
    # Insert student information right after Heading 1
    p_info = doc.paragraphs[1].insert_paragraph_before(
        "• Học phần: Phương pháp phát triển phần mềm hướng đối tượng (OOSD)\n"
        "• Sinh viên thực hiện: Nguyễn Văn Hoàng Nhật  -  MSSV: 1250080125  -  Lớp: 12_DH_CNPM2\n"
        "• Đề tài: Hệ thống Quản lý Khách sạn (WinForms C#, .NET Framework 4.7.2, SQL Server LocalDB)\n"
        "• Mô hình kiến trúc: 3 lớp phân tách rõ ràng (UI Forms - Business Services - Data Layer Db.cs)"
    )
    p_info.style = 'Normal'
    p_info.paragraph_format.space_before = Pt(4)
    p_info.paragraph_format.space_after = Pt(8)
    for r in p_info.runs:
        r.font.name = "Times New Roman"
        r.font.size = Pt(13)
        
    out_path = r'scripts\LAB3_formatted.docx'
    doc.save(out_path)
    print(f"Saved {out_path} with {len(doc.paragraphs)} paragraphs and {len(doc.tables)} tables.")

if __name__ == '__main__':
    build_lab3()
