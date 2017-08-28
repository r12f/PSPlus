using System.Runtime.InteropServices;

namespace PSPlus.Windows.Core.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RtlUserProcessParameters64
    {
        public fixed byte Reserved1[16];
        public fixed long Reserved2[10];
        public UnicodeString64 ImagePathName;
        public UnicodeString64 CommandLine;
    }
}
