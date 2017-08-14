using System.Runtime.InteropServices;

namespace PSPlus.Win32.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Win32ScrollInfo
    {
        public uint Size;
        public uint Mask;
        public int Min;
        public int Max;
        public uint Page;
        public int Pos;
        public int TrackPos;
    }
}
