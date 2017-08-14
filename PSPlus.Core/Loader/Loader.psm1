#
# Loader.psm1
#

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

function New-NativeFunctionSignature([String] $dllName, [String] $signature)
{
    return [NativeFunctionSignature]::new($dllName, $signature)
}

function Import-NativeFunctions([String] $className, [NativeFunctionSignature[]] $functions)
{
    $functionsToInject = @($functions | % { "
        [DllImport(`"$($_.DllName)`")]
        public static extern $($_.Signature);
    " }) -join ""

    return Add-Type -MemberDefinition $functionsToInject -Name $className -Namespace PSPlus -PassThru 
}