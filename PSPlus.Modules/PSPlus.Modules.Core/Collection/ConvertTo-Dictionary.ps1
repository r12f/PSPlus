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
