function Restore-DesktopLayout
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [string] $FilePath
    )

    $layoutRules = Get-Content $FilePath | ConvertFrom-Json
    Restore-DesktopLayoutFromLayoutRules $layoutRules
}
