using System.Management.Automation;
using PSPlus.Core.Windows.Window;

namespace PSPlus.Modules.Windows.Window
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
