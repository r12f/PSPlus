using System.Runtime.InteropServices;

namespace PSPlus.Core.Windows.Interop.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct User32Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
