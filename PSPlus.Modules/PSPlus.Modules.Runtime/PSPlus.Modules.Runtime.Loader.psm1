#
# PSPlus.Modules.Runtime.Loader.psm1
#
# These cmdlets are hard to be implmeneted in C#, because we cannot change the type system of existing assembly.
# So to create new types in C#, we have to dynamically create an assembly. And powershell already take care of everything for us.

class NativeFunctionSignature
{
    [String] $DllName
    [String] $Signature

    NativeFunctionSignature($dllName, $signature)
    {
        $this.DllName = $dllName;
        $this.Signature = $signature;
    }
}

function New-NativeFunctionSignature
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = $true, Mandatory = $true)]
        [string] $DllName,

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = $true, Mandatory = $true)]
        [string] $Signature
    )

    return [NativeFunctionSignature]::new($DllName, $Signature)
}

function Import-NativeFunctions
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = $true, Mandatory = $true)]
        [string] $ClassName,

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = $true, Mandatory = $true)]
        [NativeFunctionSignature[]] $Functions
    )

    $functionsToInject = @($Functions | % { "
        [DllImport(`"$($_.DllName)`")]
        public static extern $($_.Signature);
    " }) -join ""

    return Add-Type -MemberDefinition $functionsToInject -Name $ClassName -Namespace PSPlus.DynamicTypes -PassThru 
}
