using System.Runtime.InteropServices;

namespace PSPlus.Core.Windows.Win32.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Win32Point
    {
        public int X;
        public int Y;
    }
}
