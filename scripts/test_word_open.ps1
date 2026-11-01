$w = $null
try {
    $w = New-Object -ComObject Word.Application
    $w.Visible = $false
    $w.DisplayAlerts = 0
    Write-Host "Word application started successfully!"
    $docPath = Join-Path (Get-Location).Path "scripts\preamble.docx"
    $doc = $w.Documents.Open($docPath, [ref]$false, [ref]$true)
    Write-Host "Preamble opened, fields count:" $doc.Fields.Count
    $doc.Close([ref]$false)
} catch {
    Write-Host "Error:" $_.Exception.Message
} finally {
    if ($w -ne $null) {
        $w.Quit([ref]$false)
        [System.Runtime.Interopservices.Marshal]::ReleaseComObject($w) | Out-Null
    }
}
