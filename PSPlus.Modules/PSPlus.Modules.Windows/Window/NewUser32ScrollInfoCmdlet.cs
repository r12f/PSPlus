using PSPlus.Core.Windows.Interop.User32;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Window
{
    [Cmdlet(VerbsCommon.New, "User32ScrollInfo")]
    [OutputType(typeof(User32ScrollInfo))]
    public class NewUser32ScrollInfoCmdlet : Cmdlet
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
            var scrollInfo = new User32ScrollInfo()
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
