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
        $Object
    )

    process
    {
        $result = ForEach-Object $predicate -InputObject $Object
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
        $Object
    )

    process
    {
        $result = ForEach-Object $Predicate -InputObject $Object
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
