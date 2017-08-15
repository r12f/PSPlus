#
# PostBuild.ps1
#
Write-Host "Stopping PowerShellToolsProcessHost.exe and vstest.executionengine.x86.exe ..."
Get-Process | ? { ($_.ProcessName -eq 'PowerShellToolsProcessHost') -or ($_.ProcessName -eq 'vstest.executionengine.x86') } | Stop-Process -Force