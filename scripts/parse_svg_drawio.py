import urllib.parse
import re
import sys

sys.stdout.reconfigure(encoding='utf-8')

with open(r'LAB6\images\activity.drawio.svg', 'r', encoding='utf-8') as f:
    c = f.read()

m = re.search(r'content="([^"]+)"', c)
if m:
    xml = urllib.parse.unquote(m.group(1))
    print("Content starts with:")
    print(xml[:300])
