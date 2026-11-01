import docx
from docx.shared import Inches, Pt, RGBColor
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.oxml import parse_xml
from docx.oxml.ns import nsdecls, qn
from docxcompose.composer import Composer
import os
import sys

sys.stdout.reconfigure(encoding='utf-8')

def apply_font_to_run(run, font_name="Times New Roman", size_pt=13, is_heading1=False):
    run.font.name = font_name
    run.font.size = Pt(16 if is_heading1 else size_pt)
    if is_heading1:
        run.font.bold = True
        run.font.color.rgb = RGBColor(0, 32, 96)
    
    # Set OpenXML rFonts
    rPr = run._r.get_or_add_rPr()
    rFonts = rPr.find(qn('w:rFonts'))
    if rFonts is None:
        rFonts = parse_xml(f'<w:rFonts {nsdecls("w")} w:ascii="{font_name}" w:hAnsi="{font_name}" w:cs="{font_name}"/>')
        rPr.append(rFonts)
    else:
        rFonts.set(qn('w:ascii'), font_name)
        rFonts.set(qn('w:hAnsi'), font_name)
        rFonts.set(qn('w:cs'), font_name)

def build_and_format_master():
    print("Step 1: Merging all LAB documents into master document...")
    
    preamble_path = r'scripts\preamble.docx'
    labs = [
        ('LAB 0', r'scripts\LAB0_formatted.docx'),
        ('LAB 1', r'scripts\LAB1_formatted.docx'),
        ('LAB 2', r'scripts\LAB2_formatted.docx'),
        ('LAB 3', r'scripts\LAB3_formatted.docx'),
        ('LAB 4', r'scripts\LAB4_formatted.docx'),
        ('LAB 5', r'scripts\LAB5_formatted.docx'),
        ('LAB 6', r'scripts\LAB6_formatted.docx'),
        ('LAB 7', r'scripts\LAB7_formatted.docx'),
        ('LAB 8', r'scripts\LAB8_formatted.docx'),
    ]
    
    master_doc = docx.Document(preamble_path)
    composer = Composer(master_doc)
    
    for lab_name, lab_path in labs:
        print(f"  Appending {lab_name} from {lab_path}...")
        if not os.path.exists(lab_path):
            raise FileNotFoundError(f"Missing formatted lab file: {lab_path}")
        sub_doc = docx.Document(lab_path)
        master_doc.add_page_break()
        composer.append(sub_doc)
        print(f"  Successfully appended {lab_name}!")
        
    temp_output = r'scripts\master_unformatted.docx'
    composer.save(temp_output)
    print(f"Merged document saved to {temp_output}")
    
    print("Step 2: Applying uniform typography (Times New Roman, Heading 1: 16pt, all other text & tables: 13pt)...")
    doc = docx.Document(temp_output)
    
    # Configure document styles default font
    for style_name in ['Normal', 'Heading 1', 'Heading 2', 'Heading 3', 'Heading 4', 'List Bullet', 'List Paragraph']:
        try:
            st = doc.styles[style_name]
            st.font.name = "Times New Roman"
            if style_name == 'Heading 1':
                st.font.size = Pt(16)
                st.font.bold = True
            else:
                st.font.size = Pt(13)
        except Exception:
            pass

    # Process all paragraphs
    for i, p in enumerate(doc.paragraphs):
        # Preserve cover page styling on page 1
        if i == 3 and "BÁO CÁO TỔNG HỢP" in p.text:
            for r in p.runs:
                apply_font_to_run(r, "Times New Roman", 20, is_heading1=False)
                r.font.bold = True
                r.font.color.rgb = RGBColor(192, 0, 0)
            continue
        if i == 4 and "Học phần" in p.text:
            for r in p.runs:
                apply_font_to_run(r, "Times New Roman", 13, is_heading1=False)
                r.font.italic = True
                r.font.color.rgb = RGBColor(0, 32, 96)
            continue

        is_h1 = (p.style.name == 'Heading 1') or p.text.strip().startswith('LAB ')
        
        # Check if TOC paragraph: toc 1, toc 2, etc. Leave text to Word update, but set font
        if p.style.name.startswith('toc'):
            for r in p.runs:
                apply_font_to_run(r, "Times New Roman", 13, is_heading1=False)
            continue
            
        for r in p.runs:
            bold_flag = r.font.bold
            italic_flag = r.font.italic
            color_val = r.font.color.rgb if r.font.color else None
            
            apply_font_to_run(r, "Times New Roman", 13, is_heading1=is_h1)
            
            # If not heading 1, preserve existing bold/italic/color
            if not is_h1:
                if bold_flag is not None:
                    r.font.bold = bold_flag
                if italic_flag is not None:
                    r.font.italic = italic_flag
                if color_val is not None:
                    r.font.color.rgb = color_val

    print("Step 3: Applying font formatting to all tables...")
    for t_idx, table in enumerate(doc.tables):
        for row in table.rows:
            for cell in row.cells:
                for p in cell.paragraphs:
                    for r in p.runs:
                        bold_flag = r.font.bold
                        italic_flag = r.font.italic
                        color_val = r.font.color.rgb if r.font.color else None
                        
                        apply_font_to_run(r, "Times New Roman", 13, is_heading1=False)
                        
                        if bold_flag is not None:
                            r.font.bold = bold_flag
                        if italic_flag is not None:
                            r.font.italic = italic_flag
                        if color_val is not None:
                            r.font.color.rgb = color_val

    output_filename = r'BAO_CAO_TONG_HOP_LAB_OOSD_1250080125_NguyenVanHoangNhat.docx'
    doc.save(output_filename)
    
    standard_name = r'TONG_HOP_BAI_LAB_OOSD.docx'
    doc.save(standard_name)
    
    size_mb = os.path.getsize(output_filename) / (1024 * 1024)
    print(f"Step 4: Master document successfully created and formatted: {output_filename} ({size_mb:.2f} MB)")

if __name__ == '__main__':
    build_and_format_master()
