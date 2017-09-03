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
if (Test-Path $publishFolderPath -IsValid) {
    Write-Host "Removing folder $publishFolderPath ..."
    Remove-Item -Recurse -Force $publishFolderPath

    if (-not $?) {
        exit 1
    }
}

# Copy all binaries to publish folder
Write-Host "Copying $outputFolderPath to $publishFolderPath ..."
Copy-Item -Recurse $outputFolderPath $publishFolderPath
if (-not $?) {
    exit 1
}

# Copy the root module to publish folder
Write-Host "Copy the root module to $publishFolderPath ..."
Copy-Item $projectRootFolderPath\PSPlus.psd1 $publishFolderPath
if (-not $?) {
    exit 1
}

# Copy all module scripts to publish folder
Write-Host "Copy all module scripts under module root $moduleFolderRootPath ..."
Get-ChildItem $moduleFolderRootPath | ForEach-Object {
    $moduleName = $_.Name
    $moduleFolderPath = $_.FullName
    Write-Host "Copy module scripts under folder $moduleFolderPath to $publishFolderPath ..."
    Get-ChildItem $moduleFolderPath -Recurse -Include *.psd1,*.psm1 | ForEach-Object {
        Copy-Item $_ $publishFolderPath
        if (-not $?) {
            exit 1
        }
    }

    $moduleFunctionPublishFolderPath = Join-Path $publishFolderPath -ChildPath "Functions\$moduleName"
    if (-not ((Get-Item $moduleFunctionPublishFolderpath -ErrorAction SilentlyContinue) -is [System.IO.DirectoryInfo])) {
        Write-Host "Creating module function folder $moduleFunctionPublishFolderPath ..."
        New-Item -ItemType directory -Path $moduleFunctionPublishFolderPath | Out-Null
        if (-not $?) {
            exit 1
        }
    }

    Write-Host "Copy module functions under folder $moduleFolderPath to $moduleFunctionPublishFolderPath ..."
    Get-ChildItem $moduleFolderPath -Recurse -Include *.ps1 | ForEach-Object {
        Copy-Item $_ $moduleFunctionPublishFolderPath
        if (-not $?) {
            exit 1
        }
    }
}

