using PSPlus.Core.Win32.Interop;
using System.Management.Automation;

namespace PSPlus.Modules.Win32
{
    [Cmdlet(VerbsCommon.New, "Win32Rect")]
    [OutputType(typeof(Win32Rect))]
    public class NewWin32RectCmdlet : Cmdlet
    {
        [Parameter(Position = 0)]
        public int Left { get; set; }

        [Parameter(Position = 1)]
        public int Top { get; set; }

        [Parameter(Position = 2)]
        public int Right { get; set; }

        [Parameter(Position = 3)]
        public int Bottom { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(new Win32Rect() { Left = Left, Top = Top, Right = Right, Bottom = Bottom });
        }
    }
}
