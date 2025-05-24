# Citation Git Hook for C# Projects
# This script automatically updates the citation document before each commit
# Place this file in .git/hooks/pre-commit and make it executable

param(
    [string]$ProjectPath = (Get-Location)
)

Write-Host "Citation Tracker: Pre-commit hook running..." -ForegroundColor Cyan

# Check if this is a .NET project
$hasCsProjFiles = Test-Path "$ProjectPath\*.csproj"
if (-not $hasCsProjFiles) {
    Write-Host "No C# project detected, skipping citation update." -ForegroundColor Yellow
    exit 0
}

# Check if the Citation Tracker library is available
$citationDllPath = "$ProjectPath\packages\CitationTracker*\lib\netstandard2.0\CitationTracker.dll"
$citationDll = Get-ChildItem -Path $citationDllPath -ErrorAction SilentlyContinue | Select-Object -First 1

if (-not $citationDll) {
    Write-Host "Citation Tracker DLL not found. Is the package installed?" -ForegroundColor Yellow
    exit 0
}

# Load the Citation Tracker assembly
try {
    Add-Type -Path $citationDll.FullName
    Write-Host "Citation Tracker loaded successfully." -ForegroundColor Green
}
catch {
    Write-Host "Failed to load Citation Tracker: $_" -ForegroundColor Red
    exit 0  # Don't block commits on failure
}

# Update the citations document
try {
    $tracker = New-Object CitationTracking.CitationTracker -ArgumentList $ProjectPath
    $citationsPath = $tracker.ExportCitationsMarkdown()
    Write-Host "Citations document updated: $citationsPath" -ForegroundColor Green
    
    # Add the CITATIONS.md file to the git staging area if it exists
    if (Test-Path $citationsPath) {
        git add $citationsPath
        Write-Host "Added updated CITATIONS.md to commit." -ForegroundColor Green
    }
}
catch {
    Write-Host "Warning: Failed to update citations: $_" -ForegroundColor Yellow
    # Continue with commit even if there's an error
}

exit 0  # Success
