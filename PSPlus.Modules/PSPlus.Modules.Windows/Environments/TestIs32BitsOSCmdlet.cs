using PSPlus.Core.Powershell.Cmdlets;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Environments
{
    [Cmdlet(VerbsDiagnostic.Test, "Is32BitsOS")]
    [OutputType(typeof(bool))]
    public class TestIs32BitsOSCmdlet : CmdletBase
    {
        protected override void ProcessRecordInEH()
        {
            WriteObject(!Environment.Is64BitOperatingSystem);
        }
    }
}
