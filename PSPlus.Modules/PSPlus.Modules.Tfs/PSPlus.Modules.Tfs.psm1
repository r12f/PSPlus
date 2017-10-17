#
# PSPlus.Modules.Tfs.psm1
#

$functionRootFolder = Join-Path -Path $PSScriptRoot -ChildPath "Functions\PSPlus.Modules.Tfs" -Resolve -ErrorAction SilentlyContinue
if ($functionRootFolder -ne $null) {
    Get-ChildItem -Path $functionRootFolder -Filter '*.ps1' | ForEach-Object {
        . $_.FullName | Out-Null
    }
}
