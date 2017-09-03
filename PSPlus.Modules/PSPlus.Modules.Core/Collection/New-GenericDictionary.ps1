function New-GenericDictionary
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = $true, Mandatory = $true)]
        [string] $KeyTypeName,

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = $true, Mandatory = $true)]
        [string] $ValueTypeName
    )

    $collectionTypeName = "System.Collections.Generic.Dictionary[$KeyTypeName, $ValueTypeName]"
    return New-Object $collectionTypeName
}
