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
