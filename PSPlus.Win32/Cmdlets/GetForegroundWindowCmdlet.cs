using System.Management.Automation;

namespace PSPlus.Win32.Cmdlets
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
