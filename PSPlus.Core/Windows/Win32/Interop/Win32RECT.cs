using System.Runtime.InteropServices;

namespace PSPlus.Core.Windows.Win32.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Win32Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
