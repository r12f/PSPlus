using System.Runtime.InteropServices;

namespace PSPlus.Core.Win32.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Win32WindowPlacement
    {
        public uint Length;
        public uint Flags;
        public uint ShowCmd;
        public Win32Point MinPosition;
        public Win32Point MaxPosition;
        public Win32Rect NormalPosition;
    }
}
