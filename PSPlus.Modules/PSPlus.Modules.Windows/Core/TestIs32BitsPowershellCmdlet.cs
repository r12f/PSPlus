using PSPlus.Core.Powershell.Cmdlets;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Core
{
    [Cmdlet(VerbsDiagnostic.Test, "Is32BitsPowershell")]
    [OutputType(typeof(bool))]
    public class TestIs32BitsPowershellCmdlet : CmdletBase
    {
        protected override void ProcessRecordInEH()
        {
            WriteObject(!Environment.Is64BitProcess);
        }
    }
}
