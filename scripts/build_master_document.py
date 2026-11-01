import docx
from docxcompose.composer import Composer
import os
import sys

sys.stdout.reconfigure(encoding='utf-8')

print("Starting master document assembly...")

preamble_path = r'scripts\preamble.docx'
labs = [
    ('LAB 0', r'scripts\LAB0_formatted.docx'),
    ('LAB 1', r'scripts\LAB1_formatted.docx'),
    ('LAB 2', r'scripts\LAB2_formatted.docx'),
    ('LAB 3', r'scripts\LAB3_formatted.docx'),
    ('LAB 4', r'scripts\LAB4_formatted.docx'),
    ('LAB 5', r'scripts\LAB5_formatted.docx'),
    ('LAB 6', r'scripts\LAB6_formatted.docx'),
    ('LAB 7', r'scripts\LAB7_formatted.docx'),
    ('LAB 8', r'scripts\LAB8_formatted.docx'),
]

master_doc = docx.Document(preamble_path)
composer = Composer(master_doc)

for lab_name, lab_path in labs:
    print(f"Appending {lab_name} from {lab_path}...")
    if not os.path.exists(lab_path):
        print(f"ERROR: {lab_path} does not exist!")
        continue
    sub_doc = docx.Document(lab_path)
    
    # Ensure there's a page break before each lab
    master_doc.add_page_break()
    composer.append(sub_doc)
    print(f"Successfully appended {lab_name}!")

output_filename = r'BAO_CAO_TONG_HOP_LAB_OOSD_1250080125_NguyenVanHoangNhat.docx'
composer.save(output_filename)

# Also save a standard copy
standard_name = r'TONG_HOP_BAI_LAB_OOSD.docx'
composer.save(standard_name)

size_mb = os.path.getsize(output_filename) / (1024 * 1024)
print(f"Master document created successfully: {output_filename} ({size_mb:.2f} MB)")
