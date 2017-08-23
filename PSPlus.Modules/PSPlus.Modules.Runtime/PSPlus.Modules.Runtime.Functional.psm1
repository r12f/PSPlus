#
# Functional.psm1
#

# Test-Any
function Test-Any
{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $true)]
        [ScriptBlock] $predicate,

        [Parameter(ValueFromPipeline = $true)]
        $InputObject
    )

    process
    {
        $result = ForEach-Object $predicate -InputObject $InputObject
        if ($result)
        {
            $true
            Stop-UpstreamCommands($PSCmdlet)
        }
    }

    end
    {
        $false
    }
}

# Test-All
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
            Stop-UpstreamCommands($PSCmdlet)
        }
    }

    end
    {
        $true
    }
}
