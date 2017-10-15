using System.Management.Automation;
using PSPlus.Core.Windows.Interop.User32;

namespace PSPlus.Modules.Windows.Window
{
    [Cmdlet(VerbsCommon.New, "User32WindowPlacement")]
    [OutputType(typeof(User32WindowPlacement))]
    public class NewUserWindowPlacementCmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            User32WindowPlacement placement = new User32WindowPlacement();

            unsafe
            {
                placement.Length = (uint)sizeof(User32WindowPlacement);
            }

            WriteObject(placement);
        }
    }
}
