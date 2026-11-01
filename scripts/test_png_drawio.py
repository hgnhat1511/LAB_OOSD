import struct
import urllib.parse
import html
import os
import sys

sys.stdout.reconfigure(encoding='utf-8')

def extract_xml_from_png(path):
    with open(path, 'rb') as f:
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

pngs = [
    r'D:\NAM4\PP_Ptr PM HDT\anh\Untitled Diagram.drawio.png',
    r'D:\NAM4\PP_Ptr PM HDT\anh\usecase.drawio.png',
    os.path.join(r'C:\Users\Hoang Nhat\Downloads', 'Sơ đồ_ Đăng ký & Đăng nhập.drawio.png')
]

for p in pngs:
    xml = extract_xml_from_png(p)
    print(p, '-> Valid mxfile:', '<mxfile' in xml if xml else False, 'len:', len(xml) if xml else 0)
