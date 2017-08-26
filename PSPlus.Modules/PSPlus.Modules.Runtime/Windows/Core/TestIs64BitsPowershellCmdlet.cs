using PSPlus.Core.Powershell.Cmdlets;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Core
{
    [Cmdlet(VerbsDiagnostic.Test, "Is64BitsPowershell")]
    [OutputType(typeof(bool))]
    public class TestIs64BitsPowershellCmdlet : CmdletBase
    {
        protected override void ProcessRecordInEH()
        {
            WriteObject(Environment.Is64BitProcess);
        }
    }
}
