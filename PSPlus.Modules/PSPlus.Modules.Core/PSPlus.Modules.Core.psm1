#
# PSPlus.Modules.Core.psm1
#

$functionRootFolder = Join-Path -Path $PSScriptRoot -ChildPath "Functions\PSPlus.Modules.Core" -Resolve
Get-ChildItem -Path $functionRootFolder -Filter '*.ps1' | ForEach-Object {
    . $_.FullName | Out-Null
}