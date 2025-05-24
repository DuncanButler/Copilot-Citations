# Run example citation tracking code
# Simple script to run the Python citation example

# Ensure we're in the right directory
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptPath

# Run the example
python citation_example.py

Write-Host
Write-Host "Run completed. Press Enter to exit..." -ForegroundColor Green
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
