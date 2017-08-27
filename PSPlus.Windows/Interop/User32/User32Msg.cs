using System;
using System.Runtime.InteropServices;

namespace PSPlus.Core.Windows.Interop.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct User32Msg
    {
        public IntPtr Hwnd;
        public uint Message;
        public IntPtr WParam;
        public IntPtr LParam;
        public uint Time;
        public User32Point Pt;
    }
}
