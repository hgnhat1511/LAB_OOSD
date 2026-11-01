import docx
from docx.shared import Inches, Pt, RGBColor
import sys
import os

sys.stdout.reconfigure(encoding='utf-8')

def demote_subheadings(doc):
    for i, p in enumerate(doc.paragraphs):
        if i == 0:
            continue
        if p.style.name == 'Heading 3':
            p.style = 'Heading 4'
        elif p.style.name == 'Heading 2':
            p.style = 'Heading 3'
        elif p.style.name == 'Heading 1':
            p.style = 'Heading 2'

def format_lab4():
    doc = docx.Document(r'LAB4\Lab4.docx')
    p0 = doc.paragraphs[0]
    p0.text = "LAB 4: PHÂN TÍCH VÀ THIẾT KẾ HỆ THỐNG e-SHOPPING"
    p0.style = 'Heading 1'
    doc.save(r'scripts\LAB4_formatted.docx')
    print('LAB4_formatted.docx ready')

def format_lab5():
    doc = docx.Document(r'LAB5\Bao_cao_Lab5_Quan_ly_cong_ty_du_lich.docx')
    p0 = doc.paragraphs[0]
    p0.text = "LAB 5: HỆ THỐNG QUẢN LÝ CÔNG TY DU LỊCH"
    p0.style = 'Heading 1'
    demote_subheadings(doc)
    doc.save(r'scripts\LAB5_formatted.docx')
    print('LAB5_formatted.docx ready')

def format_lab6():
    doc = docx.Document(r'LAB6\Bao_cao_Lab6.docx')
    p0 = doc.paragraphs[0]
    p0.text = "LAB 6: QUẢN LÝ HỒ SƠ BỆNH ÁN CỦA BỆNH NHÂN"
    p0.style = 'Heading 1'
    demote_subheadings(doc)
    doc.save(r'scripts\LAB6_formatted.docx')
    print('LAB6_formatted.docx ready')

def format_lab7():
    doc = docx.Document(r'LAB7\QuanLyNhanSu_DongA_Lab7.docx')
    p0 = doc.paragraphs[0]
    p0.text = "LAB 7: HỆ THỐNG QUẢN LÝ NHÂN SỰ – CÔNG TY SƠN ĐÔNG Á"
    p0.style = 'Heading 1'
    demote_subheadings(doc)
    doc.save(r'scripts\LAB7_formatted.docx')
    print('LAB7_formatted.docx ready')

def format_lab8():
    doc = docx.Document(r'LAB8\Lab8_QuanLyCuaHangNuocGiaiKhat.docx')
    p0 = doc.paragraphs[0]
    p0.text = "LAB 8: HỆ THỐNG QUẢN LÝ CỬA HÀNG NƯỚC GIẢI KHÁT"
    p0.style = 'Heading 1'
    demote_subheadings(doc)
    doc.save(r'scripts\LAB8_formatted.docx')
    print('LAB8_formatted.docx ready')

if __name__ == '__main__':
    format_lab4()
    format_lab5()
    format_lab6()
    format_lab7()
    format_lab8()
