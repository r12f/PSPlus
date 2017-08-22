using PSPlus.Core.Win32;
using System.Management.Automation;

namespace PSPlus.Modules.Win32
{
    [Cmdlet(VerbsCommon.Get, "ForegroundWindow")]
    [OutputType(typeof(WindowControl))]
    public class GetForegroundWindowCmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(WindowControl.GetForegroundWindow());
        }
    }
}
