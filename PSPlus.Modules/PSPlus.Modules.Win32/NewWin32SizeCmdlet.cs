using PSPlus.Core.Windows.Win32.Interop;
using System.Management.Automation;

namespace PSPlus.Modules.Win32
{
    [Cmdlet(VerbsCommon.New, "Win32Size")]
    [OutputType(typeof(Win32Size))]
    public class NewWin32SizeCmdlet : Cmdlet
    {
        [Parameter(Position = 0)]
        public int Cx { get; set; }

        [Parameter(Position = 1)]
        public int Cy { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(new Win32Size() { Cx = Cx, Cy = Cy });
        }
    }
}
