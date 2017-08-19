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

function ConvertTo-Set
{
    [CmdletBinding()]
    param(
        [Parameter(ValueFromPipeline = $true)]
        $Object,

        [Parameter(Mandatory = $false)]
        [switch] $GenericValue
    )
    
    begin
    {
        $set = $null
    }

    process
    {
        if ($set -eq $null)
        {
            $valueType = $null;
            if ($GenericValue)
            {
                $valueType = "System.Object"
            }
            else
            {
                $valueType = $Object.GetType().FullName
            }
            $set = New-GenericSet $valueType
        }

        if (!$set.Contains($Object))
        {
            $set.Add($Object)
        }
    }

    end
    {
        $set
    }
}

function New-GenericDictionary([string] $keyTypeName, [string] $valueTypeName)
{
    $collectionTypeName = "System.Collections.Generic.Dictionary[$keyTypeName, $valueTypeName]"
    return New-Object $collectionTypeName
}

function ConvertTo-Dictionary
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [ScriptBlock] $ToKey,

        [Parameter(Position = 1, Mandatory = $true)]
        [ScriptBlock] $ToValue,

        [Parameter(ValueFromPipeline = $true)]
        $Object,

        [Parameter(Mandatory = $false)]
        [switch] $GenericValue
    )
    
    begin
    {
        $dictionary = $null
    }

    process
    {
        $key = ForEach-Object $ToKey -InputObject $Object
        $value = ForEach-Object $ToValue -InputObject $Object

        if ($dictionary -eq $null)
        {
            $keyType = $key.GetType().FullName

            $valueType = $null;
            if ($GenericValue)
            {
                $valueType = "System.Object"
            }
            else
            {
                $valueType = $value.GetType().FullName
            }
            $dictionary = New-GenericDictionary $keyType $valueType
        }

        $dictionary.Add($key, $value)
    }

    end
    {
        $dictionary
    }
}
