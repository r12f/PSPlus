#
# PostBuild.ps1
#
Write-Host "Stopping PowerShellToolsProcessHost.exe and vstest.executionengine*.exe ..."
Get-Process | ? { ($_.ProcessName -eq 'PowerShellToolsProcessHost') -or ($_.ProcessName.StartsWith('vstest.executionengine')) } | Stop-Process -Force