#
# PSPlus.Modules.Core.Collection.psm1
#
# These cmdlets are hard to be implmeneted in C#, because we cannot change the type system of existing assembly.
# So to create new types in C#, we have to dynamically create an assembly. And powershell already take care of everything for us.

function New-GenericList
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, ValueFromPipeline = $true, Mandatory = $true)]
        [string] $InputObject
    )

    $collectionTypeName = "System.Collections.Generic.List[$InputObject]"
    return New-Object $collectionTypeName
}

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

function ConvertTo-Set
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, ValueFromPipeline = $true, Mandatory = $true)]
        [object] $InputObject,

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
                $valueType = $InputObject.GetType().FullName
            }
            $set = New-GenericSet $valueType
        }

        if (!$set.Contains($InputObject))
        {
            $set.Add($InputObject)
        }
    }

    end
    {
        $set
    }
}

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

function ConvertTo-Dictionary
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [ScriptBlock] $ToKey,

        [Parameter(Position = 1, Mandatory = $true)]
        [ScriptBlock] $ToValue,

        [Parameter(ValueFromPipeline = $true)]
        [object] $InputObject,

        [Parameter(Mandatory = $false)]
        [switch] $GenericValue
    )
    
    begin
    {
        $dictionary = $null
    }

    process
    {
        $key = ForEach-Object $ToKey -InputObject $InputObject
        $value = ForEach-Object $ToValue -InputObject $InputObject

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
