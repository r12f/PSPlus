function Save-DesktopLayout
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [string] $FilePath,

        [Parameter(Mandatory = $false)]
        [string[]] $IncludeProcessNames = @(),

        [Parameter(Mandatory = $false)]
        [string[]] $ExcludeProcessNames = @(),

        [Parameter(Mandatory = $false)]
        [string[]] $IncludeClassNames = @(),

        [Parameter(Mandatory = $false)]
        [string[]] $ExcludeClassNames = @()
    )

    $layoutRules = New-DesktopLayoutRulesFromCurrentLayout -IncludeProcessNames $IncludeProcessNames -ExcludeProcessNames $ExcludeProcessNames -IncludeClassNames $IncludeClassNames -ExcludeClassNames $ExcludeClassNames
    ConvertTo-Json -InputObject  @($layoutRules) | Out-File -FilePath $FilePath
}
