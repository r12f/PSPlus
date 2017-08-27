using PSPlus.Core.Windows.Win32.Interop;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Win32
{
    [Cmdlet(VerbsCommon.New, "Win32WindowPlacement")]
    [OutputType(typeof(Win32WindowPlacement))]
    public class NewWin32WindowPlacementCmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            Win32WindowPlacement placement = new Win32WindowPlacement();

            unsafe
            {
                placement.Length = (uint)sizeof(Win32WindowPlacement);
            }

            WriteObject(placement);
        }
    }
}
