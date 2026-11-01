import docx
from docx.shared import Pt, RGBColor
from docx.oxml import parse_xml
from docx.oxml.ns import nsdecls, qn
import sys
import os

sys.stdout.reconfigure(encoding='utf-8')

def set_run_properties(run, font_name="Times New Roman", size_pt=13, is_h1=False, preserve_bold=True, preserve_italic=True, preserve_color=True):
    val_hp = "32" if is_h1 else ("26" if size_pt == 13 else str(int(size_pt * 2)))
    
    rPr = run._r.get_or_add_rPr()
    
    # 1. Font family
    rFonts = rPr.find(qn('w:rFonts'))
    if rFonts is None:
        rFonts = parse_xml(f'<w:rFonts {nsdecls("w")} w:ascii="{font_name}" w:hAnsi="{font_name}" w:cs="{font_name}"/>')
        rPr.append(rFonts)
    else:
        rFonts.set(qn('w:ascii'), font_name)
        rFonts.set(qn('w:hAnsi'), font_name)
        rFonts.set(qn('w:cs'), font_name)
        
    # 2. Size in half-points (32 for 16pt, 26 for 13pt)
    sz = rPr.find(qn('w:sz'))
    if sz is None:
        sz = parse_xml(f'<w:sz {nsdecls("w")} w:val="{val_hp}"/>')
        rPr.append(sz)
    else:
        sz.set(qn('w:val'), val_hp)
        
    szCs = rPr.find(qn('w:szCs'))
    if szCs is None:
        szCs = parse_xml(f'<w:szCs {nsdecls("w")} w:val="{val_hp}"/>')
        rPr.append(szCs)
    else:
        szCs.set(qn('w:val'), val_hp)
        
    # 3. Bold for Heading 1
    if is_h1:
        run.font.bold = True
        run.font.color.rgb = RGBColor(0, 32, 96)

def apply_formatting(doc_path):
    print(f"Applying strict Times New Roman (H1=16pt, all other=13pt) to {doc_path}...")
    doc = docx.Document(doc_path)
    
    # Process paragraphs
    for i, p in enumerate(doc.paragraphs):
        # Skip cover page title
        if i == 3 and "BÁO CÁO TỔNG HỢP" in p.text:
            continue
        if i == 4 and "Học phần" in p.text:
            continue
            
        # Check if Heading 1
        is_h1 = (p.style.name == 'Heading 1') or (p.text.strip().startswith('LAB ') and not p.style.name.startswith('toc'))
        
        # Don't overwrite TOC text fields (Word manages them), but set styling
        if p.style.name.startswith('toc'):
            for r in p.runs:
                set_run_properties(r, "Times New Roman", 13, is_h1=False)
            continue
            
        for r in p.runs:
            set_run_properties(r, "Times New Roman", 13, is_h1=is_h1)
            
    # Process all tables
    for t in doc.tables:
        for row in t.rows:
            for cell in row.cells:
                for p in cell.paragraphs:
                    for r in p.runs:
                        set_run_properties(r, "Times New Roman", 13, is_h1=False)
                        
    doc.save(doc_path)
    print("Done applying formatting!")

if __name__ == '__main__':
    apply_formatting('BAO_CAO_TONG_HOP_LAB_OOSD_1250080125_NguyenVanHoangNhat.docx')
    apply_formatting('TONG_HOP_BAI_LAB_OOSD.docx')

