using PSPlus.Core.Powershell;
using PSPlus.Core.Powershell.Cmdlets;
using System.Management.Automation;

namespace PSPlus.Modules.Core.Runtime
{
    [Cmdlet(VerbsCommon.New, "NativeFunctionSignature")]
    [OutputType(typeof(NativeFunctionSignature))]
    public class NewNativeFunctionSignatureCmdlet : CmdletBase
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string DllName { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Signature { get; set; }

        protected override void ProcessRecordInEH()
        {
            WriteObject(new NativeFunctionSignature(DllName, Signature));
        }
    }
}
