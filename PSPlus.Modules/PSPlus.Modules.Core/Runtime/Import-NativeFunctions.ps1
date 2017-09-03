function Import-NativeFunctions
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = $true, Mandatory = $true)]
        [string] $ClassName,

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = $true, Mandatory = $true)]
        [PSPlus.Core.Powershell.NativeFunctionSignature[]] $Functions
    )

    $functionsToInject = @($Functions | % { "
        [DllImport(`"$($_.DllName)`")]
        public static extern $($_.Signature);
    " }) -join ""

    return Add-Type -MemberDefinition $functionsToInject -Name $ClassName -Namespace PSPlus.DynamicTypes -PassThru 
}
