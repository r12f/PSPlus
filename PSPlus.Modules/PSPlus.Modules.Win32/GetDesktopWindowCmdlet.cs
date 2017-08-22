using PSPlus.Core.Win32;
using System.Management.Automation;

namespace PSPlus.Modules.Win32
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
