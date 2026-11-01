$w = New-Object -ComObject Word.Application
$w.Visible = $false
try {
    $doc = $w.Documents.Add()
    $p = $doc.Paragraphs.Add()
    $p.Range.Text = "Hello Test Word COM"
    $testDocPath = Join-Path (Get-Location).Path "scripts\test_out.docx"
    $doc.SaveAs2($testDocPath)
    $doc.Close()
    Write-Host "Success, file saved to $testDocPath"
} finally {
    $w.Quit()
    [System.Runtime.Interopservices.Marshal]::ReleaseComObject($w) | Out-Null
}
