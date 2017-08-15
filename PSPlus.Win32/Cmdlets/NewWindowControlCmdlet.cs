using System;
using System.Management.Automation;

namespace PSPlus.Win32.Cmdlets
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
