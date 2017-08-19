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
            Win32WindowPlacement placement = new Win32WindowPlacement();

            unsafe
            {
                placement.Length = (uint)sizeof(Win32WindowPlacement);
            }

            WriteObject(placement);
        }
    }
}
