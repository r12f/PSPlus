function New-GenericQueue
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, ValueFromPipeline = $true, Mandatory = $true)]
        [string] $InputObject
    )

    $collectionTypeName = "System.Collections.Generic.Queue[$InputObject]"
    return New-Object $collectionTypeName
}
