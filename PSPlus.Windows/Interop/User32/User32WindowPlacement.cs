using System.Runtime.InteropServices;

namespace PSPlus.Core.Windows.Interop.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct User32WindowPlacement
    {
        public uint Length;
        public uint Flags;
        public uint ShowCmd;
        public User32Point MinPosition;
        public User32Point MaxPosition;
        public User32Rect NormalPosition;
    }
}
