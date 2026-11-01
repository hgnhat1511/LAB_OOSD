import urllib.parse
import re
import sys

sys.stdout.reconfigure(encoding='utf-8')

for pth in [
    r'D:\NAM4\PP_Ptr PM HDT\anh\classdiagram.drawio.svg',
    r'D:\NAM4\PP_Ptr PM HDT\anh\class_ppt.drawio.svg',
    r'LAB6\images\activity.drawio.svg',
    r'LAB6\images\Class =_Diagram.drawio.svg',
    r'LAB6\images\erd.drawio.svg',
    r'LAB6\images\ph?n.drawio.svg',
    r'LAB6\images\sequence.drawio.svg',
]:
    try:
        with open(pth, 'r', encoding='utf-8', errors='ignore') as f:
            c = f.read()
        m = re.search(r'content="([^"]+)"', c)
        if m:
            xml = urllib.parse.unquote(m.group(1))
            # search for some words
            words = set(re.findall(r'[A-Za-z0-9_]{4,}', xml))
            sample = [w for w in words if not w.startswith(('mx', 'cell', 'edge', 'style', 'geom', 'point', 'vertex', 'arrow', 'color', 'shadow', 'align', 'font', 'stroke'))][:10]
            print(f'{pth} -> {sample}')
    except Exception as e:
        print(f'{pth} -> {e}')
