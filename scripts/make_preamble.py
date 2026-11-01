import docx
from docx.shared import Inches, Pt, RGBColor
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.enum.table import WD_TABLE_ALIGNMENT, WD_ALIGN_VERTICAL
from docx.oxml import OxmlElement, parse_xml
from docx.oxml.ns import nsdecls, qn
import sys

sys.stdout.reconfigure(encoding='utf-8')

def set_cell_background(cell, fill_hex):
    shading_elm = parse_xml(f'<w:shd {nsdecls("w")} w:fill="{fill_hex}"/>')
    cell._tc.get_or_add_tcPr().append(shading_elm)

def set_cell_margins(cell, top=100, bottom=100, left=150, right=150):
    tcPr = cell._tc.get_or_add_tcPr()
    tcMar = parse_xml(f'<w:tcMar {nsdecls("w")}><w:top w:w="{top}" w:type="dxa"/><w:bottom w:w="{bottom}" w:type="dxa"/><w:left w:w="{left}" w:type="dxa"/><w:right w:w="{right}" w:type="dxa"/></w:tcMar>')
    tcPr.append(tcMar)

def create_preamble():
    doc = docx.Document()
    
    # Set page margins to standard A4 (2cm all around)
    for section in doc.sections:
        section.top_margin = Inches(0.8)
        section.bottom_margin = Inches(0.8)
        section.left_margin = Inches(1.0)
        section.right_margin = Inches(1.0)
    
    # ==================== TRANG BÌA ====================
    p_inst = doc.add_paragraph()
    p_inst.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_inst.paragraph_format.space_before = Pt(0)
    p_inst.paragraph_format.space_after = Pt(4)
    run = p_inst.add_run("BỘ GIÁO DỤC VÀ ĐÀO TẠO\nTRƯỜNG ĐẠI HỌC TÀI NGUYÊN VÀ MÔI TRƯỜNG TP. HỒ CHÍ MINH")
    run.font.name = "Times New Roman"
    run.font.size = Pt(13)
    run.font.bold = True
    run.font.color.rgb = RGBColor(0, 32, 96)
    
    p_fac = doc.add_paragraph()
    p_fac.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_fac.paragraph_format.space_before = Pt(0)
    p_fac.paragraph_format.space_after = Pt(20)
    run = p_fac.add_run("KHOA CÔNG NGHỆ THÔNG TIN\n— — — 🙠 🕮 🙢 — — —")
    run.font.name = "Times New Roman"
    run.font.size = Pt(13)
    run.font.bold = True
    run.font.color.rgb = RGBColor(0, 32, 96)
    
    p_sp1 = doc.add_paragraph()
    p_sp1.paragraph_format.space_before = Pt(40)
    
    p_title = doc.add_paragraph()
    p_title.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_title.paragraph_format.space_before = Pt(20)
    p_title.paragraph_format.space_after = Pt(12)
    run = p_title.add_run("BÁO CÁO TỔNG HỢP\nBÀI TẬP THỰC HÀNH TOÀN KHÓA (LAB 0 – LAB 8)")
    run.font.name = "Times New Roman"
    run.font.size = Pt(22)
    run.font.bold = True
    run.font.color.rgb = RGBColor(192, 0, 0)
    
    p_sub = doc.add_paragraph()
    p_sub.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_sub.paragraph_format.space_before = Pt(0)
    p_sub.paragraph_format.space_after = Pt(40)
    run = p_sub.add_run("Học phần: PHƯƠNG PHÁP PHÁT TRIỂN PHẦN MỀM HƯỚNG ĐỐI TƯỢNG (OOSD)\n(Object-Oriented Software Development Methodology)")
    run.font.name = "Times New Roman"
    run.font.size = Pt(14)
    run.font.italic = True
    run.font.color.rgb = RGBColor(0, 32, 96)
    
    p_sp2 = doc.add_paragraph()
    p_sp2.paragraph_format.space_before = Pt(50)
    
    # Bảng thông tin sinh viên
    tbl_info = doc.add_table(rows=5, cols=2)
    tbl_info.alignment = WD_TABLE_ALIGNMENT.CENTER
    info_data = [
        ("Giảng viên hướng dẫn:", "Bộ môn Kỹ thuật Phần mềm"),
        ("Sinh viên thực hiện:", "NGUYỄN VĂN HOÀNG NHẬT"),
        ("Mã số sinh viên (MSSV):", "1250080125"),
        ("Lớp học phần:", "12_DH_CNPM2 (CNPM2)"),
        ("Ngày hoàn thành:", "Chủ nhật, ngày 01/11/2026")
    ]
    for row_idx, (k, v) in enumerate(info_data):
        row = tbl_info.rows[row_idx]
        cell_k, cell_v = row.cells[0], row.cells[1]
        
        pk = cell_k.paragraphs[0]
        pk.paragraph_format.space_before = Pt(4)
        pk.paragraph_format.space_after = Pt(4)
        rk = pk.add_run(k)
        rk.font.name = "Times New Roman"
        rk.font.size = Pt(13)
        rk.font.bold = True
        
        pv = cell_v.paragraphs[0]
        pv.paragraph_format.space_before = Pt(4)
        pv.paragraph_format.space_after = Pt(4)
        rv = pv.add_run(v)
        rv.font.name = "Times New Roman"
        rv.font.size = Pt(13)
        if "NGUYỄN VĂN" in v or "1250080125" in v:
            rv.font.bold = True
            rv.font.color.rgb = RGBColor(0, 32, 96)
    
    p_sp3 = doc.add_paragraph()
    p_sp3.paragraph_format.space_before = Pt(60)
    
    p_bot = doc.add_paragraph()
    p_bot.alignment = WD_ALIGN_PARAGRAPH.CENTER
    p_bot.paragraph_format.space_after = Pt(0)
    run = p_bot.add_run("TP. HỒ CHÍ MINH – THÁNG 11/2026")
    run.font.name = "Times New Roman"
    run.font.size = Pt(12)
    run.font.bold = True
    
    # Ngắt trang sang Trang 2
    doc.add_page_break()
    
    # ==================== TRANG 2: THÔNG TIN BÀI NỘP & LINK GITHUB ====================
    p_h1 = doc.add_heading(level=1)
    p_h1.paragraph_format.space_before = Pt(10)
    p_h1.paragraph_format.space_after = Pt(12)
    r_h1 = p_h1.add_run("THÔNG TIN BÀI NỘP VÀ ĐƯỜNG LINK GITHUB REPOSITORY")
    r_h1.font.name = "Times New Roman"
    r_h1.font.size = Pt(16)
    r_h1.font.bold = True
    r_h1.font.color.rgb = RGBColor(0, 32, 96)
    
    # Khung nổi bật thông tin GitHub
    callout_tbl = doc.add_table(rows=1, cols=1)
    callout_tbl.alignment = WD_TABLE_ALIGNMENT.CENTER
    c_cell = callout_tbl.cell(0, 0)
    set_cell_background(c_cell, "F2F4F7")
    set_cell_margins(c_cell, top=140, bottom=140, left=200, right=200)
    
    cp = c_cell.paragraphs[0]
    cp.paragraph_format.space_before = Pt(4)
    cp.paragraph_format.space_after = Pt(6)
    r_box_title = cp.add_run("📌 ĐƯỜNG LINK GITHUB REPOSITORY MÔN HỌC (CHẾ ĐỘ PUBLIC)")
    r_box_title.font.name = "Times New Roman"
    r_box_title.font.size = Pt(13)
    r_box_title.font.bold = True
    r_box_title.font.color.rgb = RGBColor(192, 0, 0)
    
    cp2 = c_cell.add_paragraph()
    cp2.paragraph_format.space_before = Pt(2)
    cp2.paragraph_format.space_after = Pt(4)
    r_url_label = cp2.add_run("🔗 GitHub Repository môn OOSD: ")
    r_url_label.font.name = "Times New Roman"
    r_url_label.font.size = Pt(12)
    r_url_label.font.bold = True
    r_url = cp2.add_run("https://github.com/hgnhat1511/LAB_OOSD")
    r_url.font.name = "Times New Roman"
    r_url.font.size = Pt(12)
    r_url.font.bold = True
    r_url.font.underline = True
    r_url.font.color.rgb = RGBColor(0, 102, 204)
    
    cp3 = c_cell.add_paragraph()
    cp3.paragraph_format.space_before = Pt(2)
    cp3.paragraph_format.space_after = Pt(4)
    r_img_label = cp3.add_run("📁 Thư mục IMAGES lưu trữ hình vẽ & sơ đồ gốc: ")
    r_img_label.font.name = "Times New Roman"
    r_img_label.font.size = Pt(12)
    r_img_label.font.bold = True
    r_img_url = cp3.add_run("https://github.com/hgnhat1511/LAB_OOSD/tree/main/IMAGES")
    r_img_url.font.name = "Times New Roman"
    r_img_url.font.size = Pt(12)
    r_img_url.font.bold = True
    r_img_url.font.underline = True
    r_img_url.font.color.rgb = RGBColor(0, 102, 204)
    
    cp4 = c_cell.add_paragraph()
    cp4.paragraph_format.space_before = Pt(2)
    cp4.paragraph_format.space_after = Pt(4)
    r_st = cp4.add_run("🔴 Trạng thái quyền truy cập: PUBLIC (Công khai 100%) – Giảng viên có thể truy cập, xem và tải trực tiếp bất kỳ lúc nào.")
    r_st.font.name = "Times New Roman"
    r_st.font.size = Pt(11.5)
    r_st.font.italic = True
    r_st.font.bold = True
    r_st.font.color.rgb = RGBColor(0, 128, 0)
    
    p_note = doc.add_paragraph()
    p_note.paragraph_format.space_before = Pt(12)
    p_note.paragraph_format.space_after = Pt(8)
    r_note = p_note.add_run("1. Tuân thủ đầy đủ quy định nộp bài của Giảng viên:")
    r_note.font.name = "Times New Roman"
    r_note.font.size = Pt(12.5)
    r_note.font.bold = True
    
    bullets = [
        "Tổng hợp đầy đủ tất cả các bài LAB trong học phần từ LAB 0 đến LAB 8 đúng thứ tự và toàn vẹn nội dung.",
        "Toàn bộ sơ đồ phân tích và thiết kế được thực hiện bằng các công cụ chuyên dụng: Draw.io (diagrams.net), PlantUML, StarUML, Visio / UML Modeling Tools theo đúng quy định bắt buộc.",
        "Tạo thư mục chính xác là `IMAGES` trên GitHub chứa toàn bộ file nguồn thiết kế gốc (giữ nguyên không xuất ra dạng hình ảnh đơn thuần: Draw.io SVG/XML, PlantUML `.puml`, v.v.).",
        "Repository luôn duy trì trạng thái Public để phục vụ công tác kiểm tra, đối chiếu của Giảng viên.",
        "File Word có Mục lục tự động, phân chia từng bài LAB rõ ràng và đánh số trang chuẩn xác."
    ]
    for b in bullets:
        bp = doc.add_paragraph(style='List Bullet')
        bp.paragraph_format.space_before = Pt(1)
        bp.paragraph_format.space_after = Pt(2)
        br = bp.add_run(b)
        br.font.name = "Times New Roman"
        br.font.size = Pt(11)
        
    p_tbl_lbl = doc.add_paragraph()
    p_tbl_lbl.paragraph_format.space_before = Pt(12)
    p_tbl_lbl.paragraph_format.space_after = Pt(6)
    r_tl = p_tbl_lbl.add_run("2. Bảng tổng hợp các bài LAB trong học phần:")
    r_tl.font.name = "Times New Roman"
    r_tl.font.size = Pt(12.5)
    r_tl.font.bold = True
    
    tbl_labs = doc.add_table(rows=10, cols=4)
    tbl_labs.alignment = WD_TABLE_ALIGNMENT.CENTER
    headers = ["Bài LAB", "Tên đề tài / Nội dung trọng tâm", "Công cụ mô hình hóa / Môi trường", "Trạng thái"]
    for c_idx, h in enumerate(headers):
        cell = tbl_labs.rows[0].cells[c_idx]
        set_cell_background(cell, "002060")
        set_cell_margins(cell, top=80, bottom=80, left=100, right=100)
        p = cell.paragraphs[0]
        p.alignment = WD_ALIGN_PARAGRAPH.CENTER
        r = p.add_run(h)
        r.font.name = "Times New Roman"
        r.font.size = Pt(10.5)
        r.font.bold = True
        r.font.color.rgb = RGBColor(255, 255, 255)
        
    labs_summary = [
        ("LAB 0", "Cài đặt & kiểm tra môi trường phát triển", "JDK 21, Maven 3.9, Git, Docker, PlantUML", "Hoàn thành"),
        ("LAB 1", "Phân tích yêu cầu: Thư viện online & Cửa hàng nước giải khát", "Use Case Model, Analysis Class, Sequence", "Hoàn thành"),
        ("LAB 2", "Hệ thống Quản lý Thư viện", "C# WinForms, SQL Server, ERD, Sequence Diagrams", "Hoàn thành"),
        ("LAB 3", "Hệ thống Quản lý Khách sạn", "Kiến trúc 3 tầng (UI-Services-Data), SQL Server", "Hoàn thành"),
        ("LAB 4", "Phân tích và Thiết kế hệ thống e-Shopping", "15 Use Cases, Checkout, Activity, Sequence, ERD 3NF", "Hoàn thành"),
        ("LAB 5", "Phân tích & Thiết kế HT Quản lý Công ty Du lịch", "PlantUML, 6 Sequence Diagrams, Activity, WinForms", "Hoàn thành"),
        ("LAB 6", "Quản lý Hồ sơ Bệnh án của Bệnh nhân", "Draw.io SVG/XML, ERD 13 bảng, WinForms C#", "Hoàn thành"),
        ("LAB 7", "Quản lý Nhân sự – Công ty sơn Đông Á", "4 Giai đoạn, PlantUML (.puml), State Machine", "Hoàn thành"),
        ("LAB 8", "Quản lý Cửa hàng Nước Giải Khát", "StarUML, 4 Giai đoạn PT-TK, ERD, WinForms C#", "Hoàn thành")
    ]
    for row_idx, data in enumerate(labs_summary, start=1):
        row = tbl_labs.rows[row_idx]
        bg = "F9FBFD" if row_idx % 2 == 1 else "FFFFFF"
        for col_idx, text in enumerate(data):
            cell = row.cells[col_idx]
            set_cell_background(cell, bg)
            set_cell_margins(cell, top=60, bottom=60, left=100, right=100)
            p = cell.paragraphs[0]
            if col_idx in (0, 3):
                p.alignment = WD_ALIGN_PARAGRAPH.CENTER
            else:
                p.alignment = WD_ALIGN_PARAGRAPH.LEFT
            r = p.add_run(text)
            r.font.name = "Times New Roman"
            r.font.size = Pt(10)
            if col_idx == 0:
                r.font.bold = True
            elif col_idx == 3:
                r.font.bold = True
                r.font.color.rgb = RGBColor(0, 128, 0)
                
    doc.add_page_break()
    
    # ==================== TRANG 3: MỤC LỤC TỔNG HỢP ====================
    p_toc_title = doc.add_heading(level=1)
    p_toc_title.paragraph_format.space_before = Pt(10)
    p_toc_title.paragraph_format.space_after = Pt(14)
    r_tt = p_toc_title.add_run("MỤC LỤC TỔNG HỢP")
    r_tt.font.name = "Times New Roman"
    r_tt.font.size = Pt(16)
    r_tt.font.bold = True
    r_tt.font.color.rgb = RGBColor(0, 32, 96)
    
    # Insert Table of Contents field code
    p_fld = doc.add_paragraph()
    p_fld.paragraph_format.space_before = Pt(0)
    p_fld.paragraph_format.space_after = Pt(6)
    
    # XML element for TOC field
    fldSimple = parse_xml(r'<w:fldSimple %s w:instr="TOC \o &quot;1-3&quot; \h \z \u"/>' % nsdecls('w'))
    p_fld._p.append(fldSimple)
    
    doc.save(r'scripts\preamble.docx')
    print('Preamble saved to scripts\preamble.docx')

if __name__ == '__main__':
    create_preamble()

