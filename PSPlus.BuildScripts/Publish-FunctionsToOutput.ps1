#
# Publish-FunctionsToOutput.ps1
#
param(
    [Parameter(Position = 0, Mandatory = $true)]
    [string] $SourceFolder,

    [Parameter(Position = 1, Mandatory = $true)]
    [string] $OutputFolder
)

$sourceModuleName = (Get-Item $SourceFolder -ErrorAction Stop).Name
$targetFunctionFolder = Join-Path $OutputFolder -ChildPath "Functions\$sourceModuleName"

# Remove everything under target function folder
if (Test-Path $targetFunctionFolder) {
    Write-Host "Target function folder $targetFunctionFolder already exists, removing ..."
    Remove-Item -Recurse -Force $targetFunctionFolder -ErrorAction Stop
}

# Create target function folder, if doesn't exist.
if (-not ((Get-Item $targetFunctionFolder -ErrorAction SilentlyContinue) -is [System.IO.DirectoryInfo])) {
    Write-Host "Creating function folder $targetFunctionFolder ..."
    New-Item -ItemType directory -Path $targetFunctionFolder -ErrorAction Stop | Out-Null
}

# Copy all functions to publish folder
Write-Host "Copy all functions under module root $SourceFolder to $targetFunctionFolder ..."
Get-ChildItem $SourceFolder -Recurse -Include *.ps1 | ForEach-Object {
    Copy-Item $_ $targetFunctionFolder -ErrorAction Stop
}