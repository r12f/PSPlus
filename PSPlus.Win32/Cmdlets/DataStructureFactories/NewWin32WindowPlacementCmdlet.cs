using PSPlus.Win32.Interop;
using System.Management.Automation;

namespace PSPlus.Win32.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "Win32WindowPlacement")]
    [OutputType(typeof(Win32WindowPlacement))]
    public class NewWin32WindowPlacementCmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(new Win32WindowPlacement());
        }
    }
}
