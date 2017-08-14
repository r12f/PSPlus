#
# GenericCollectionFactories.psm1
#

function New-GenericList([string] $typeName)
{
    $collectionTypeName = "System.Collections.Generic.List[$typeName]"
    return New-Object $collectionTypeName
}

function New-GenericQueue([string] $typeName)
{
    $collectionTypeName = "System.Collections.Generic.Queue[$typeName]"
    return New-Object $collectionTypeName
}

function New-GenericStack([string] $typeName)
{
    $collectionTypeName = "System.Collections.Generic.Stack[$typeName]"
    return New-Object $collectionTypeName
}

function New-GenericSet([string] $typeName)
{
    $collectionTypeName = "System.Collections.Generic.HashSet[$typeName]"
    return New-Object $collectionTypeName
}

function New-GenericDictionary([string] $keyTypeName, [string] $valueTypeName)
{
    $collectionTypeName = "System.Collections.Generic.Dictionary[$keyTypeName, $valueTypeName]"
    return New-Object $collectionTypeName
}