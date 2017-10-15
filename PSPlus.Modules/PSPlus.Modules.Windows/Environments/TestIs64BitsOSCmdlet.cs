using System;
using System.Management.Automation;
using PSPlus.Core.Powershell.Cmdlets;

namespace PSPlus.Modules.Windows.Environments
{
    [Cmdlet(VerbsDiagnostic.Test, "Is64BitsOS")]
    [OutputType(typeof(bool))]
    public class TestIs64BitsOSCmdlet : CmdletBase
    {
        protected override void ProcessRecordInEH()
        {
            WriteObject(Environment.Is64BitOperatingSystem);
        }
    }
}
