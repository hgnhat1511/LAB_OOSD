import docx
from docx.shared import Inches, Pt, RGBColor
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.enum.table import WD_TABLE_ALIGNMENT
from docx.oxml import parse_xml
from docx.oxml.ns import nsdecls
import os
import sys

sys.stdout.reconfigure(encoding='utf-8')

def set_cell_background(cell, fill_hex):
    shading_elm = parse_xml(f'<w:shd {nsdecls("w")} w:fill="{fill_hex}"/>')
    cell._tc.get_or_add_tcPr().append(shading_elm)

def set_cell_margins(cell, top=80, bottom=80, left=120, right=120):
    tcPr = cell._tc.get_or_add_tcPr()
    tcMar = parse_xml(f'<w:tcMar {nsdecls("w")}><w:top w:w="{top}" w:type="dxa"/><w:bottom w:w="{bottom}" w:type="dxa"/><w:left w:w="{left}" w:type="dxa"/><w:right w:w="{right}" w:type="dxa"/></w:tcMar>')
    tcPr.append(tcMar)

def create_lab0():
    doc = docx.Document()
    
    # Page setup
    for section in doc.sections:
        section.top_margin = Inches(0.8)
        section.bottom_margin = Inches(0.8)
        section.left_margin = Inches(1.0)
        section.right_margin = Inches(1.0)
        
    # Title
    h1 = doc.add_heading(level=1)
    h1.paragraph_format.space_before = Pt(12)
    h1.paragraph_format.space_after = Pt(8)
    r = h1.add_run("LAB 0: CÀI ĐẶT VÀ KIỂM TRA MÔI TRƯỜNG PHÁT TRIỂN")
    r.font.name = "Times New Roman"
    r.font.size = Pt(16)
    r.font.bold = True
    r.font.color.rgb = RGBColor(0, 32, 96)
    
    # 1. Thông tin sinh viên & mục tiêu
    h2_1 = doc.add_heading(level=2)
    h2_1.paragraph_format.space_before = Pt(8)
    h2_1.paragraph_format.space_after = Pt(4)
    r = h2_1.add_run("1. Thông tin sinh viên và mục tiêu thực hành")
    r.font.name = "Times New Roman"
    r.font.size = Pt(13)
    r.font.bold = True
    
    p = doc.add_paragraph()
    p.paragraph_format.space_after = Pt(4)
    r = p.add_run("• Họ và tên: ")
    r.bold = True
    p.add_run("Nguyễn Văn Hoàng Nhật    ")
    r2 = p.add_run("• MSSV: ")
    r2.bold = True
    p.add_run("1250080125    ")
    r3 = p.add_run("• Lớp: ")
    r3.bold = True
    p.add_run("12_DH_CNPM2\n")
    p.add_run("• Mục tiêu: Chuẩn bị và kiểm tra đầy đủ toàn bộ nền tảng công nghệ, công cụ phát triển phần mềm hướng đối tượng (OOSD), hệ thống quản lý mã nguồn Git, môi trường biên dịch Java 21, công cụ quản lý dự án Maven, nền tảng container Docker và công cụ mô hình hóa PlantUML.")
    
    # 2. Báo cáo cấu hình môi trường
    h2_2 = doc.add_heading(level=2)
    h2_2.paragraph_format.space_before = Pt(8)
    h2_2.paragraph_format.space_after = Pt(4)
    r = h2_2.add_run("2. Báo cáo cấu hình môi trường phát triển")
    r.font.name = "Times New Roman"
    r.font.size = Pt(13)
    r.font.bold = True
    
    tbl_env = doc.add_table(rows=7, cols=3)
    tbl_env.alignment = WD_TABLE_ALIGNMENT.CENTER
    env_headers = ["Thành phần", "Phiên bản / Cấu hình chi tiết", "Kết quả kiểm tra"]
    for i, h in enumerate(env_headers):
        c = tbl_env.rows[0].cells[i]
        set_cell_background(c, "002060")
        set_cell_margins(c)
        cp = c.paragraphs[0]
        cp.alignment = WD_ALIGN_PARAGRAPH.CENTER
        cr = cp.add_run(h)
        cr.bold = True
        cr.font.color.rgb = RGBColor(255, 255, 255)
        cr.font.size = Pt(10)
        
    env_rows = [
        ("Hệ điều hành", "Windows 11 Home/Pro x64, kiến trúc amd64", "Đạt chuẩn"),
        ("Java Development Kit (JDK)", "Oracle JDK 21.0.12.1 LTS (build 21.0.12.1+1-LTS-4)", "Đạt chuẩn (java & javac)"),
        ("Apache Maven", "Apache Maven 3.9.11, Java version: 21.0.12.1", "Đạt chuẩn (mvn -version)"),
        ("Git Version Control", "Git version 2.48.1.windows.1", "Đạt chuẩn, config user.name"),
        ("Docker Platform", "Docker Engine v29.7.2, Docker Compose v5.5.0", "Đạt chuẩn"),
        ("Mô hình hóa UML", "PlantUML Integration, Draw.io Diagram Tools", "Đạt chuẩn")
    ]
    for row_idx, (c1, c2, c3) in enumerate(env_rows, start=1):
        r = tbl_env.rows[row_idx]
        bg = "F9FBFD" if row_idx % 2 == 1 else "FFFFFF"
        for col_idx, text in enumerate([c1, c2, c3]):
            cell = r.cells[col_idx]
            set_cell_background(cell, bg)
            set_cell_margins(cell)
            cp = cell.paragraphs[0]
            if col_idx in (0, 2):
                cp.alignment = WD_ALIGN_PARAGRAPH.CENTER
            else:
                cp.alignment = WD_ALIGN_PARAGRAPH.LEFT
            cr = cp.add_run(text)
            cr.font.name = "Times New Roman"
            cr.font.size = Pt(9.5)
            if col_idx == 0:
                cr.bold = True
            elif col_idx == 2:
                cr.bold = True
                cr.font.color.rgb = RGBColor(0, 128, 0)
                
    # 3. Nhật ký thực hành
    h2_3 = doc.add_heading(level=2)
    h2_3.paragraph_format.space_before = Pt(8)
    h2_3.paragraph_format.space_after = Pt(4)
    r = h2_3.add_run("3. Nhật ký thực hành (Lab Journal)")
    r.font.name = "Times New Roman"
    r.font.size = Pt(13)
    r.font.bold = True
    
    tbl_journal = doc.add_table(rows=6, cols=3)
    tbl_journal.alignment = WD_TABLE_ALIGNMENT.CENTER
    j_headers = ["Thời gian", "Thao tác thực hiện", "Kết quả ghi nhận"]
    for i, h in enumerate(j_headers):
        c = tbl_journal.rows[0].cells[i]
        set_cell_background(c, "002060")
        set_cell_margins(c)
        cp = c.paragraphs[0]
        cp.alignment = WD_ALIGN_PARAGRAPH.CENTER
        cr = cp.add_run(h)
        cr.bold = True
        cr.font.color.rgb = RGBColor(255, 255, 255)
        cr.font.size = Pt(10)
        
    j_data = [
        ("15:10", "Kiểm tra hệ điều hành máy trạm thực hành", "Windows 11 x64, hoạt động mượt mà"),
        ("15:15", "Cài đặt JDK 21 LTS và chạy lệnh java/javac -version", "Cả 2 lệnh trỏ chính xác về JDK 21.0.12.1"),
        ("15:20", "Kiểm tra lệnh mvn -version", "Maven chạy thành công, nhận đúng Java Home"),
        ("15:33", "Kiểm tra git --version và thiết lập git config --global", "Git 2.48.1 ghi nhận đúng thông tin học tập của sinh viên"),
        ("16:08", "Cài đặt Docker Desktop và kiểm tra lệnh docker/docker compose", "Docker 29.7.2 và Docker Compose v5.5.0 hoạt động ổn định")
    ]
    for row_idx, (c1, c2, c3) in enumerate(j_data, start=1):
        r = tbl_journal.rows[row_idx]
        bg = "F9FBFD" if row_idx % 2 == 1 else "FFFFFF"
        for col_idx, text in enumerate([c1, c2, c3]):
            cell = r.cells[col_idx]
            set_cell_background(cell, bg)
            set_cell_margins(cell)
            cp = cell.paragraphs[0]
            if col_idx == 0:
                cp.alignment = WD_ALIGN_PARAGRAPH.CENTER
            else:
                cp.alignment = WD_ALIGN_PARAGRAPH.LEFT
            cr = cp.add_run(text)
            cr.font.name = "Times New Roman"
            cr.font.size = Pt(9.5)
            if col_idx == 0:
                cr.bold = True
                
    # 4. Minh chứng kiểm tra môi trường chi tiết
    h2_4 = doc.add_heading(level=2)
    h2_4.paragraph_format.space_before = Pt(8)
    h2_4.paragraph_format.space_after = Pt(4)
    r = h2_4.add_run("4. Minh chứng kiểm tra môi trường chi tiết")
    r.font.name = "Times New Roman"
    r.font.size = Pt(13)
    r.font.bold = True
    
    screenshots = [
        ("Hình 0.1: Kiểm tra cấu hình hệ điều hành Windows 11", r"LAB0\evidence\screenshots\ktra_cauhinhmay.png"),
        ("Hình 0.2: Kiểm tra phiên bản Java Development Kit (JDK 21)", r"LAB0\evidence\screenshots\JDK_version.png"),
        ("Hình 0.3: Kiểm tra phiên bản Apache Maven", r"LAB0\evidence\screenshots\MVN_version.png"),
        ("Hình 0.4: Kiểm tra phiên bản Git Version Control", r"LAB0\evidence\screenshots\Git_version.png"),
        ("Hình 0.5: Kiểm tra cấu hình danh tính sinh viên và commit Git", r"LAB0\evidence\screenshots\commit_git.png"),
        ("Hình 0.6: Quá trình cài đặt Docker Desktop", r"LAB0\evidence\screenshots\CaiDat_Docker.png"),
        ("Hình 0.7: Kiểm tra phiên bản Docker Engine và Docker Compose", r"LAB0\evidence\screenshots\Docker_version.png"),
        ("Hình 0.8: Biên dịch và chạy thử nghiệm dự án mẫu với Maven", r"LAB0\evidence\screenshots\maven_builtest.png"),
        ("Hình 0.9: Kiểm tra hoạt động của công cụ mô hình hóa PlantUML", r"LAB0\evidence\screenshots\plantUML.png")
    ]
    
    for title, img_path in screenshots:
        if os.path.exists(img_path):
            p_img = doc.add_paragraph()
            p_img.alignment = WD_ALIGN_PARAGRAPH.CENTER
            p_img.paragraph_format.space_before = Pt(6)
            p_img.paragraph_format.space_after = Pt(2)
            run = p_img.add_run()
            run.add_picture(img_path, width=Inches(5.8))
            
            p_cap = doc.add_paragraph()
            p_cap.alignment = WD_ALIGN_PARAGRAPH.CENTER
            p_cap.paragraph_format.space_before = Pt(2)
            p_cap.paragraph_format.space_after = Pt(8)
            rcap = p_cap.add_run(title)
            rcap.font.name = "Times New Roman"
            rcap.font.size = Pt(10)
            rcap.font.italic = True
            rcap.font.bold = True
            
    doc.save(r'scripts\LAB0_formatted.docx')
    print('LAB0_formatted.docx created successfully!')

if __name__ == '__main__':
    create_lab0()

