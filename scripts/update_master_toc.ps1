$w = $null
try {
    $w = New-Object -ComObject Word.Application
    $w.Visible = $false
    $w.DisplayAlerts = 0
    Write-Host "Opening master document..."
    $docPath = Join-Path (Get-Location).Path "BAO_CAO_TONG_HOP_LAB_OOSD_1250080125_NguyenVanHoangNhat.docx"
    $doc = $w.Documents.Open($docPath, [ref]$false, [ref]$false)
    
    Write-Host "Updating Table of Contents and all Fields..."
    foreach ($toc in $doc.TablesOfContents) {
        $toc.Update()
    }
    foreach ($field in $doc.Fields) {
        $field.Update()
    }
    
    # Also update page numbers in headers/footers
    foreach ($sec in $doc.Sections) {
        foreach ($h in $sec.Headers) {
            foreach ($f in $h.Range.Fields) { $f.Update() }
        }
        foreach ($f in $sec.Footers) {
            foreach ($fld in $f.Range.Fields) { $fld.Update() }
        }
    }
    
    $pageCount = $doc.ComputeStatistics(2) # 2 = wdStatisticPages
    Write-Host "Total pages computed by Word: $pageCount"
    
    $doc.Save()
    $doc.Close([ref]$false)
    Write-Host "Master document updated and saved successfully!"
} catch {
    Write-Host "Error:" $_.Exception.Message
} finally {
    if ($w -ne $null) {
        $w.Quit([ref]$false)
        [System.Runtime.Interopservices.Marshal]::ReleaseComObject($w) | Out-Null
    }
}
