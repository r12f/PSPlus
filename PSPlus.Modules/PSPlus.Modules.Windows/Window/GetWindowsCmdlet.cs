using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PSPlus.Core.Windows.Window;

namespace PSPlus.Modules.Windows.Window
{
    [Cmdlet(VerbsCommon.Get, "Windows")]
    [OutputType(typeof(WindowControl))]
    public class GetWindowsCmdlet : Cmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public IntPtr Parent { get; set; }

        [Parameter]
        public int RecursiveDepth { get; set; }

        public GetWindowsCmdlet()
        {
            RecursiveDepth = 1;
        }

        protected override void ProcessRecord()
        {
            WindowControl root = null;
            if (Parent == IntPtr.Zero)
            {
                root = WindowControl.GetDesktopWindow();
            }
            else
            {
                root = new WindowControl(Parent);
            }

            Stack<KeyValuePair<WindowControl, int>> windows = new Stack<KeyValuePair<WindowControl, int>>();
            windows.Push(new KeyValuePair<WindowControl, int>(root, 0));

            while (windows.Count > 0)
            {
                var windowWithDepth = windows.Pop();

                var window = windowWithDepth.Key;
                var depth = windowWithDepth.Value;

                // Skip the root
                if (depth > 0)
                {
                    WriteObject(window);
                }

                if (RecursiveDepth != 0 && depth >= RecursiveDepth)
                {
                    continue;
                }

                foreach (var childWindow in window.GetChildren().Reverse())
                {
                    windows.Push(new KeyValuePair<WindowControl, int>(childWindow, depth + 1));
                }
            }
        }
    }
}
