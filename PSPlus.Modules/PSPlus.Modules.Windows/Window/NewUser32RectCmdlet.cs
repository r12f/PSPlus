using System.Management.Automation;
using PSPlus.Core.Windows.Interop.User32;

namespace PSPlus.Modules.Windows.Window
{
    [Cmdlet(VerbsCommon.New, "User32Rect")]
    [OutputType(typeof(User32Rect))]
    public class NewUser32RectCmdlet : Cmdlet
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
            WriteObject(new User32Rect() { Left = Left, Top = Top, Right = Right, Bottom = Bottom });
        }
    }
}
