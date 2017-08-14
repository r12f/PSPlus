using System;

namespace PSPlus.Win32
{
    public class WindowControl
    {
        public IntPtr Hwnd { get; set; }

        public WindowControl(ulong hwnd)
        {
            if (hwnd == 0)
            {
                throw new ArgumentNullException("hwnd");
            }

            Hwnd = (IntPtr)hwnd;
        }

        public bool ShowWindow(int show)
        {
            return Win32APIs.ShowWindow(Hwnd, show);
        }
    }
}
