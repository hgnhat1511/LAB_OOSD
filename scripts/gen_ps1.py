import os
import sys

sys.stdout.reconfigure(encoding='utf-8')

ps1_content = '''$w = $null
try {
    $w = New-Object -ComObject Word.Application
    $w.Visible = $false
    $w.DisplayAlerts = 0
    Write-Host "Opening master document..."
    $docPath = Join-Path (Get-Location).Path "BAO_CAO_TONG_HOP_LAB_OOSD_1250080125_NguyenVanHoangNhat.docx"
    $doc = $w.Documents.Open($docPath, [ref]$false, [ref]$false)
    
    Write-Host "Checking TablesOfContents count:" $doc.TablesOfContents.Count
    if ($doc.TablesOfContents.Count -ge 1) {
        Write-Host "Updating existing TOC..."
        $doc.TablesOfContents.Item(1).Update()
    } else {
        Write-Host "Adding native TOC..."
        # Heading MUC LUC TONG HOP is around paragraph 18-20
        $found = $false
        for ($i = 1; $i -le [Math]::Min(30, $doc.Paragraphs.Count); $i++) {
            $txt = $doc.Paragraphs.Item($i).Range.Text
            if ($txt -match "M.*C L.*C") {
                $targetRange = $doc.Paragraphs.Item($i).Range
                $targetRange.Collapse(0)
                $doc.TablesOfContents.Add($targetRange, $true, 1, 3)
                $found = $true
                break
            }
        }
        if (-not $found) {
            $doc.TablesOfContents.Add($doc.Paragraphs.Item(20).Range, $true, 1, 3)
        }
    }
    
    $pageCount = $doc.ComputeStatistics(2)
    Write-Host "Total pages: $pageCount"
    
    $doc.Save()
    $doc.Close([ref]$false)
    Write-Host "Done successfully!"
} catch {
    Write-Host "Error: $($_.Exception.Message)"
} finally {
    if ($w -ne $null) {
        $w.Quit([ref]$false)
        [System.Runtime.Interopservices.Marshal]::ReleaseComObject($w) | Out-Null
    }
    Stop-Process -Name WINWORD -Force -ErrorAction SilentlyContinue
}
'''

with open(r'scripts\apply_word_toc.ps1', 'w', encoding='utf-8-sig') as f:
    f.write(ps1_content)
print('Wrote apply_word_toc.ps1 with UTF-8 BOM')

