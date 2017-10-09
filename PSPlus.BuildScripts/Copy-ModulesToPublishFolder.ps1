#
# Copy-ModulesToPublishFolder.ps1
#

param(
    [Parameter(Position = 0, Mandatory = $true)]
    [string] $OutputFolder,

    [Parameter(Position = 1, Mandatory = $true)]
    [string] $PublishFolder
)

# Kill all processes which may load our dlls
Write-Host "Stopping PowerShellToolsProcessHost.exe and vstest.executionengine*.exe ..."
Get-Process | ? { ($_.ProcessName -eq 'PowerShellToolsProcessHost') -or ($_.ProcessName.StartsWith('vstest.executionengine')) } | Stop-Process -Force

# Remove everything under publish folder
if (Test-Path $PublishFolder) {
    Write-Host "Target publish folder $PublishFolder already exists, removing ..."
    Remove-Item -Recurse -Force $PublishFolder -ErrorAction Stop
}

# Copy output folder to publish folder
Write-Host "Copy all packages from $OutputFolder to $PublishFolder ..."
Copy-Item -Path $OutputFolder -Destination $PublishFolder -Recurse -ErrorAction Stop