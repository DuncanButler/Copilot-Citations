# Final cleanup script for Copilot-Citations project
# Removes files that have been moved to new locations

Write-Host "Performing final cleanup of moved files..." -ForegroundColor Yellow

# Remove CITATIONS.md from root since it's now in shared/examples
if (Test-Path "CITATIONS.md") {
    Write-Host "Removing CITATIONS.md from root (already moved to shared/examples)" -ForegroundColor Cyan
    Remove-Item "CITATIONS.md" -Force
}

# Remove CitationTracker.nuspec from root since it's now in csharp directory
if (Test-Path "CitationTracker.nuspec") {
    Write-Host "Removing CitationTracker.nuspec from root (already moved to csharp directory)" -ForegroundColor Cyan
    Remove-Item "CitationTracker.nuspec" -Force
}

Write-Host "Final cleanup complete!" -ForegroundColor Green
Write-Host "Project structure has been fully reorganized." -ForegroundColor Green
