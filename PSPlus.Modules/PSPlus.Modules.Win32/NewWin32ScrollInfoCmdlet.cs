using PSPlus.Core.Windows.Win32.Interop;
using System.Management.Automation;

namespace PSPlus.Modules.Win32
{
    [Cmdlet(VerbsCommon.New, "Win32ScrollInfo")]
    [OutputType(typeof(Win32ScrollInfo))]
    public class NewWin32ScrollInfoCmdlet : Cmdlet
    {
        [Parameter(Position = 0)]
        public uint Size { get; set; }

        [Parameter(Position = 1)]
        public uint Mask { get; set; }

        [Parameter(Position = 2)]
        public int Min { get; set; }

        [Parameter(Position = 3)]
        public int Max { get; set; }

        [Parameter(Position = 4)]
        public uint Page { get; set; }

        [Parameter(Position = 5)]
        public int Pos { get; set; }

        [Parameter(Position = 6)]
        public int TrackPos { get; set; }

        protected override void ProcessRecord()
        {
            var scrollInfo = new Win32ScrollInfo()
            {
                Size = Size,
                Mask = Mask,
                Min = Min,
                Max = Max,
                Page = Page,
                Pos = Pos,
                TrackPos = TrackPos,
            };

            WriteObject(scrollInfo);
        }
    }
}
