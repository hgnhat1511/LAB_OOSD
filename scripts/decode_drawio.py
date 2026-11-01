import base64
import zlib
import urllib.parse
import re
import os
import struct
import sys

sys.stdout.reconfigure(encoding='utf-8')

def get_mxfile_from_png(path):
    with open(path, 'rb') as f:
        data = f.read()
    pos = 8
    while pos < len(data):
        length, chunk_type = struct.unpack('>I4s', data[pos:pos+8])
        chunk_data = data[pos+8:pos+8+length]
        if chunk_type == b'tEXt' and chunk_data.startswith(b'mxfile\x00'):
            raw = chunk_data[7:]
            return urllib.parse.unquote(raw.decode('latin1'))
        pos += 8 + length + 4
    return None

def get_mxfile_from_svg(path):
    with open(path, 'r', encoding='utf-8', errors='ignore') as f:
        c = f.read()
    m = re.search(r'content="([^"]+)"', c)
    if m:
        return urllib.parse.unquote(m.group(1))
    return None

def decode_diagram(diagram_str):
    try:
        compressed = base64.b64decode(diagram_str)
        decompressed = zlib.decompress(compressed, -15)
        return urllib.parse.unquote(decompressed.decode('utf-8'))
    except Exception as e:
        return f"Error: {e}"

def extract_from_mxfile(mxfile_str):
    m = re.search(r'<diagram[^>]*>(.*?)</diagram>', mxfile_str, re.DOTALL)
    if m:
        return decode_diagram(m.group(1).strip())
    return None

files = [
    ('Downloads_DangKy_DangNhap', os.path.join(r'C:\Users\Hoang Nhat\Downloads', 'Sơ đồ_ Đăng ký & Đăng nhập.drawio.png'), 'png'),
    ('anh_Untitled', r'D:\NAM4\PP_Ptr PM HDT\anh\Untitled Diagram.drawio.png', 'png'),
    ('anh_UseCase', r'D:\NAM4\PP_Ptr PM HDT\anh\usecase.drawio.png', 'png'),
    ('anh_Class', r'D:\NAM4\PP_Ptr PM HDT\anh\classdiagram.drawio.svg', 'svg'),
    ('anh_ClassPPT', r'D:\NAM4\PP_Ptr PM HDT\anh\class_ppt.drawio.svg', 'svg'),
    ('lab6_activity', r'LAB6\images\activity.drawio.svg', 'svg'),
    ('lab6_class', r'LAB6\images\Class =_Diagram.drawio.svg', 'svg'),
    ('lab6_erd', r'LAB6\images\erd.drawio.svg', 'svg'),
    ('lab6_usecase', r'LAB6\images\lab6_usecase.svg', 'svg'),
    ('lab6_sequence', r'LAB6\images\sequence.drawio.svg', 'svg'),
]

for tag, pth, ftype in files:
    if not os.path.exists(pth):
        print(f'{tag}: NOT FOUND')
        continue
    xml = get_mxfile_from_png(pth) if ftype == 'png' else get_mxfile_from_svg(pth)
    if xml:
        inner = extract_from_mxfile(xml)
        if inner and not inner.startswith('Error'):
            labels = re.findall(r'value="([^"]+)"', inner)
            clean_labels = [re.sub(r'<[^>]+>', ' ', l).strip() for l in labels if l.strip() and not l.startswith('&')]
            print(f'=== {tag} (labels: {len(clean_labels)}) ===')
            print(' ', clean_labels[:8])
        else:
            print(f'=== {tag} === {inner}')
