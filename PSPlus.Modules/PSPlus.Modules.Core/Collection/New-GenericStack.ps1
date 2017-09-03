function New-GenericStack
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, ValueFromPipeline = $true, Mandatory = $true)]
        [string] $InputObject
    )

    $collectionTypeName = "System.Collections.Generic.Stack[$InputObject]"
    return New-Object $collectionTypeName
}
