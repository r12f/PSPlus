using PSPlus.Core.Windows.Window;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Window
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
