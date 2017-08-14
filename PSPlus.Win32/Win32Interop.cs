using System;
using System.Runtime.InteropServices;

namespace PSPlus.Win32
{
    public static class Win32Interop
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int show);
    }
}
