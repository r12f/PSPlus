function New-GenericSet
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, ValueFromPipeline = $true, Mandatory = $true)]
        [string] $InputObject
    )

    $collectionTypeName = "System.Collections.Generic.HashSet[$InputObject]"
    return New-Object $collectionTypeName
}
