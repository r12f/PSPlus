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
        $object
    )

    process
    {
        $result = ForEach-Object $predicate -InputObject $object
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
        [Parameter(Mandatory = $true)]
        [ScriptBlock] $predicate,

        [Parameter(ValueFromPipeline = $true)]
        $object
    )

    process
    {
        $result = ForEach-Object $predicate -InputObject $object
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
