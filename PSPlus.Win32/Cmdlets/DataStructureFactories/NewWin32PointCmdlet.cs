using PSPlus.Win32.Interop;
using System.Management.Automation;

namespace PSPlus.Win32.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "Win32Point")]
    [OutputType(typeof(Win32Point))]
    public class NewWin32PointCmdlet : Cmdlet
    {
        [Parameter(Position = 0)]
        public int X { get; set; }

        [Parameter(Position = 1)]
        public int Y { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(new Win32Point() { X = X, Y = Y });
        }
    }
}
