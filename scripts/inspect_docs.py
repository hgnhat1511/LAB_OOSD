import docx
import sys
import os
import zipfile

sys.stdout.reconfigure(encoding='utf-8')

def find_diagram_paragraphs(doc_path):
    doc = docx.Document(doc_path)
    res = []
    for i, p in enumerate(doc.paragraphs):
        has_img = any('<w:drawing' in r._r.xml for r in p.runs)
        text = p.text.strip()
        if has_img:
            prev_t = doc.paragraphs[i-1].text.strip() if i > 0 else ''
            next_t = doc.paragraphs[i+1].text.strip() if i+1 < len(doc.paragraphs) else ''
            res.append((i, text, prev_t, next_t))
    return res

docs = [
    ('LAB0', r'LAB0\Lab00_1250080125_NguyenVanHoangNhat.docx'),
    ('LAB1_1', r'LAB1\NguyenVanHoangNhat_CNPM2_Buoi2_Bai1.docx'),
    ('LAB1_2', r'LAB1\NguyenVanHoangNhat_CNPM2_Buoi2_Bai2.docx'),
    ('LAB2', r'LAB2\BaoCaoBaiLAB2_CNPM2_NguyenVanHoangNhat.docx'),
    ('LAB3', r'LAB3\LAB3_NguyenVanHoangNhat.docx'),
    ('LAB4', r'LAB4\Lab4.docx'),
    ('LAB5', r'LAB5\Bao_cao_Lab5_Quan_ly_cong_ty_du_lich.docx'),
    ('LAB6', r'LAB6\Bao_cao_Lab6.docx'),
    ('LAB7', r'LAB7\QuanLyNhanSu_DongA_Lab7.docx'),
    ('LAB8', r'LAB8\Lab8_QuanLyCuaHangNuocGiaiKhat.docx'),
]

for lab, pth in docs:
    imgs = find_diagram_paragraphs(pth)
    print(f'=== {lab} ({len(imgs)} images) ===')
    for idx, t, prv, nxt in imgs:
        label = t or prv or nxt
        print(f'  p[{idx}]: {label[:80]}')
