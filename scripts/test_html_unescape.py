import urllib.parse
import html
import re
import sys

sys.stdout.reconfigure(encoding='utf-8')

for pth in [
    r'LAB6\images\activity.drawio.svg',
    r'LAB6\images\Class =_Diagram.drawio.svg',
    r'LAB6\images\erd.drawio.svg',
    r'LAB6\images\phẩn.drawio.svg',
    r'LAB6\images\sequence.drawio.svg',
    r'D:\NAM4\PP_Ptr PM HDT\anh\classdiagram.drawio.svg',
    r'D:\NAM4\PP_Ptr PM HDT\anh\class_ppt.drawio.svg',
]:
    with open(pth, 'r', encoding='utf-8') as f:
        c = f.read()
    m = re.search(r'content="([^"]+)"', c)
    if m:
        raw = urllib.parse.unquote(m.group(1))
        xml = html.unescape(raw)
        print(f'{pth} -> Valid mxfile: {"<mxfile" in xml}, len: {len(xml)}')
