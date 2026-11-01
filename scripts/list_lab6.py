import os
import sys

sys.stdout.reconfigure(encoding='utf-8')
for f in os.listdir('LAB6/images'):
    print(f, f.encode('unicode_escape').decode())

