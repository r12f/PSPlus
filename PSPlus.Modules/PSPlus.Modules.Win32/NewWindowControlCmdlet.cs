using PSPlus.Core.Windows.Win32;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Win32
{
    [Cmdlet(VerbsCommon.New, "WindowControl")]
    [OutputType(typeof(WindowControl))]
    public class NewWindowControlCmdlet : Cmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public IntPtr Hwnd { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(new WindowControl(Hwnd));
        }
    }
}
