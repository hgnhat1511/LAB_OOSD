from docx import Document
from docxcompose.composer import Composer
import os
import sys

sys.stdout.reconfigure(encoding='utf-8')

test_out = r'scripts\test_composed.docx'
master = Document(r'LAB1\NguyenVanHoangNhat_CNPM2_Buoi2_Bai1.docx')
composer = Composer(master)
doc2 = Document(r'LAB1\NguyenVanHoangNhat_CNPM2_Buoi2_Bai2.docx')
composer.append(doc2)
composer.save(test_out)

print(f'Composed successfully! Size: {os.path.getsize(test_out)} bytes')

