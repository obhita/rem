$path = Split-Path $MyInvocation.MyCommand.Path
$cloc = Join-Path $path cloc-1.52.exe
$cloc

Invoke-Expression ( "$cloc --no3 --ignored=ignored.txt --report-file=ClocReport.txt ." )