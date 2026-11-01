import os
import re
import urllib.parse
import html
import struct
import sys

sys.stdout.reconfigure(encoding='utf-8')

def extract_xml_from_svg(svg_path):
    with open(svg_path, 'r', encoding='utf-8', errors='ignore') as f:
        c = f.read()
    m = re.search(r'content="([^"]+)"', c)
    if m:
        raw = urllib.parse.unquote(m.group(1))
        return html.unescape(raw)
    return None

def extract_xml_from_png(png_path):
    with open(png_path, 'rb') as f:
        data = f.read()
    pos = 8
    while pos < len(data):
        length, chunk_type = struct.unpack('>I4s', data[pos:pos+8])
        chunk_data = data[pos+8:pos+8+length]
        if chunk_type == b'tEXt' and chunk_data.startswith(b'mxfile\x00'):
            raw = chunk_data[7:]
            decoded = urllib.parse.unquote(raw.decode('utf-8', errors='ignore'))
            return html.unescape(decoded)
        pos += 8 + length + 4
    return None

# 1. LAB6
lab6_dir = r'IMAGES\LAB6'
svg_files = [
    ('activity.drawio.svg', 'LAB6_Activity_Diagram.drawio'),
    ('Class =_Diagram.drawio.svg', 'LAB6_Class_Diagram.drawio'),
    ('erd.drawio.svg', 'LAB6_ERD_Diagram.drawio'),
    ('lab6_usecase.svg', 'LAB6_UseCase_Diagram.drawio'),
    ('phẩn.drawio.svg', 'LAB6_PhanRa_ChucNang.drawio'),
    ('sequence.drawio.svg', 'LAB6_Sequence_Diagram.drawio'),
]

for src_name, dst_name in svg_files:
    src_p = os.path.join(r'LAB6\images', src_name)
    if os.path.exists(src_p):
        xml = extract_xml_from_svg(src_p)
        if xml:
            dst_p = os.path.join(lab6_dir, dst_name)
            with open(dst_p, 'w', encoding='utf-8') as f:
                f.write(xml)
            print(f'Saved: {dst_p} ({len(xml)} bytes)')

# 2. anh folder & Downloads
extra_files = [
    (r'D:\NAM4\PP_Ptr PM HDT\anh\classdiagram.drawio.svg', r'IMAGES\classdiagram.drawio', 'svg'),
    (r'D:\NAM4\PP_Ptr PM HDT\anh\class_ppt.drawio.svg', r'IMAGES\class_ppt.drawio', 'svg'),
    (r'D:\NAM4\PP_Ptr PM HDT\anh\Untitled Diagram.drawio.png', r'IMAGES\Untitled_Diagram.drawio', 'png'),
    (r'D:\NAM4\PP_Ptr PM HDT\anh\usecase.drawio.png', r'IMAGES\usecase.drawio', 'png'),
    (os.path.join(r'C:\Users\Hoang Nhat\Downloads', 'Sơ đồ_ Đăng ký & Đăng nhập.drawio.png'), r'IMAGES\LAB4\LAB4_DangKy_DangNhap.drawio', 'png'),
]

for src_p, dst_p, ftype in extra_files:
    if os.path.exists(src_p):
        xml = extract_xml_from_svg(src_p) if ftype == 'svg' else extract_xml_from_png(src_p)
        if xml:
            os.makedirs(os.path.dirname(dst_p), exist_ok=True)
            with open(dst_p, 'w', encoding='utf-8') as f:
                f.write(xml)
            print(f'Saved: {dst_p} ({len(xml)} bytes)')
