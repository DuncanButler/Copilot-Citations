# Cleanup script for Copilot-Citations project
# Removes old files that have been moved to new locations

# Run this after confirming all files have been properly reorganized

Write-Host "Cleaning up old files that have been moved to new locations..." -ForegroundColor Yellow

# Files that have been moved to other directories and can be removed
$filesToRemove = @(
    "citation_example.py",
    "citation_tracker.py",
    "citation-git-hook.ps1",
    "CitationExample.cs",
    "CitationMSBuildTask.cs",
    "CitationTracker.cs",
    "citations.json",
    "vscode_citation_extension.py",
    "VisualStudioExtensionConcept.cs"
)

# Check and remove each file if it exists at the root level
foreach ($file in $filesToRemove) {
    $path = Join-Path -Path (Get-Location) -ChildPath $file
    if (Test-Path $path) {
        Write-Host "Removing: $file" -ForegroundColor Cyan
        Remove-Item $path -Force
    }
}

Write-Host "Cleanup complete!" -ForegroundColor Green
Write-Host "All moved files have been organized into their proper directories:" -ForegroundColor Green
Write-Host "- /python: Python implementation files" -ForegroundColor Green
Write-Host "- /csharp: C# implementation files" -ForegroundColor Green
Write-Host "- /tools: Git hooks and utility scripts" -ForegroundColor Green
Write-Host "- /shared: Example files and common resources" -ForegroundColor Green
