import docx
import sys

sys.stdout.reconfigure(encoding='utf-8')

doc = docx.Document(r'BAO_CAO_TONG_HOP_LAB_OOSD_1250080125_NguyenVanHoangNhat.docx')
print('Total paragraphs:', len(doc.paragraphs))
print('Total tables:', len(doc.tables))
print('Total inline shapes (images):', len(doc.inline_shapes))

print('\n--- ALL HEADING 1 ENTRIES ---')
for i, p in enumerate(doc.paragraphs):
    if p.style.name == 'Heading 1':
        print(f'p[{i}]: {p.text.strip()}')

print('\n--- SAMPLE HEADING 2 ENTRIES ---')
for i, p in enumerate(doc.paragraphs):
    if p.style.name == 'Heading 2':
        print(f'p[{i}]: {p.text.strip()[:60]}')
