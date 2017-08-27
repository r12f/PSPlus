using PSPlus.Core.Windows.Interop.User32;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Window
{
    [Cmdlet(VerbsCommon.New, "User32Point")]
    [OutputType(typeof(User32Point))]
    public class NewUser32PointCmdlet : Cmdlet
    {
        [Parameter(Position = 0)]
        public int X { get; set; }

        [Parameter(Position = 1)]
        public int Y { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(new User32Point() { X = X, Y = Y });
        }
    }
}
