using System.Runtime.InteropServices;

namespace PSPlus.Windows.Core.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UnicodeString32
    {
        public ushort Length;
        public ushort MaximumLength;
        public int Buffer;
    }
}
