using PSPlus.Core.Windows.Win32;
using PSPlus.Core.Windows.Win32.Interop;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Win32
{
    [Cmdlet(VerbsCommon.Get, "WindowFromPoint")]
    [OutputType(typeof(WindowControl))]
    public class GetWindowFromPointCmdlet : Cmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        public Win32Point Point { get; set; }

        [Parameter(Position = 1)]
        public IntPtr Parent { get; set; }

        [Parameter(Position = 2)]
        public uint Flags { get; set; }

        private WindowControl _parentWindow = null;

        protected override void BeginProcessing()
        {
            if (Parent != IntPtr.Zero)
            {
                _parentWindow = new WindowControl(Parent);
            }

            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            WindowControl result = null;
            if (_parentWindow != null)
            {
                result = _parentWindow.ChildWindowFromPointEx(Point, Flags);
            }
            else
            {
                result = WindowControl.WindowFromPoint(Point);
            }

            WriteObject(result);
        }
    }
}
