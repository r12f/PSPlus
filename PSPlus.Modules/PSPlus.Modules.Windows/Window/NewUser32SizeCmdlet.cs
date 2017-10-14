using PSPlus.Core.Windows.Interop.User32;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Window
{
    [Cmdlet(VerbsCommon.New, "User32Size")]
    [OutputType(typeof(User32Size))]
    public class NewUser32SizeCmdlet : Cmdlet
    {
        [Parameter(Position = 0)]
        public int Cx { get; set; }

        [Parameter(Position = 1)]
        public int Cy { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(new User32Size() { Cx = Cx, Cy = Cy });
        }
    }
}
