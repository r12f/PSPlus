using System;
using System.Runtime.InteropServices;

namespace PSPlus.Windows.Core.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UnicodeString64
    {
        public ushort Length;
        public ushort MaximumLength;
        public long Buffer;
    }
}
