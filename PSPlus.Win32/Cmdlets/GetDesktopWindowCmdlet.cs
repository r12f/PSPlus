using System.Management.Automation;

namespace PSPlus.Win32.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "DesktopWindow")]
    [OutputType(typeof(WindowControl))]
    public class GetDesktopWindowCmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(WindowControl.GetDesktopWindow());
        }
    }
}
