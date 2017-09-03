function Test-All
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [ScriptBlock] $Predicate,

        [Parameter(ValueFromPipeline = $true)]
        $InputObject
    )

    process
    {
        $result = ForEach-Object $Predicate -InputObject $InputObject
        if (-not $result)
        {
            $false
            Stop-UpstreamCommands $PSCmdlet
        }
    }

    end
    {
        $true
    }
}
