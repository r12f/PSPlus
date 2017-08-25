#
# PSPlus.Modules.Runtime.Functional.psm1
#
# These functions cannot be implemented by C#, because we cannot properly set the $_ variable in the ScriptBlock.
# The API to change the $_ is supported in Powershell 4, but it makes the "closure" goes away, even after calling GetNewClosure.

# Test-Any
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
            Stop-UpstreamCommands $PSCmdlet
        }
    }

    end
    {
        $true
    }
}
