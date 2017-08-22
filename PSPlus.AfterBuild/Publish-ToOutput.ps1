#
# Publish-ToOutput.ps1
#
param(
    [Parameter(Position = 0, Mandatory = $true)]
    [string] $ConfigurationName
)

$projectRootFolderPath = Resolve-Path $PSScriptRoot\..
$moduleFolderRootPath = Join-Path $projectRootFolderPath -ChildPath PSPlus.Modules
$outputFolderPath = Join-Path $projectRootFolderPath -ChildPath Output\Binaries\$ConfigurationName
$publishFolderPath = Join-Path $projectRootFolderPath -ChildPath Output\Publish

# Kill all processes which may load our dlls
Write-Host "Stopping PowerShellToolsProcessHost.exe and vstest.executionengine*.exe ..."
Get-Process | ? { ($_.ProcessName -eq 'PowerShellToolsProcessHost') -or ($_.ProcessName.StartsWith('vstest.executionengine')) } | Stop-Process -Force

# Remove everything under publish folder
Write-Host "Removing folder $publishFolderPath ..."
Remove-Item -Recurse -Force $publishFolderPath

# Copy all binaries to publish folder
Write-Host "Copying $outputFolderPath to $publishFolderPath ..."
Copy-Item -Recurse $outputFolderPath $publishFolderPath

# Copy the root module to publish folder
Write-Host "Copy the root module to $publishFolderPath ..."
Copy-Item $projectRootFolderPath\PSPlus.psd1 $publishFolderPath

# Copy all module scripts to publish folder
Write-Host "Copy all module scripts under module root $moduleFolderRootPath ..."
Get-ChildItem $moduleFolderRootPath | ForEach-Object {
    $moduleFolderPath = $_.FullName
    Write-Host "Copy module scripts under folder $moduleFolderPath to $publishFolderPath ..."
    Copy-Item "$moduleFolderPath\*" $publishFolderPath -Filter *.psd1
    Copy-Item "$moduleFolderPath\*" $publishFolderPath -Filter *.psm1
}