function Test-Any
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
        if ($result)
        {
            $true
            Stop-UpstreamCommands $PSCmdlet
        }
    }

    end
    {
        $false
    }
}
