#
# PSPlus.Modules.Windows.psm1
#

$functionRootFolder = Join-Path -Path $PSScriptRoot -ChildPath "Functions\PSPlus.Modules.Windows" -Resolve -ErrorAction SilentlyContinue
if ($functionRootFolder -ne $null) {
    Get-ChildItem -Path $functionRootFolder -Filter '*.ps1' | ForEach-Object {
        . $_.FullName | Out-Null
    }
}
