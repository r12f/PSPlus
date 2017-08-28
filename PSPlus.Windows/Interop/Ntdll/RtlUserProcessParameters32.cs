using System.Runtime.InteropServices;

namespace PSPlus.Windows.Core.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RtlUserProcessParameters32
    {
        public fixed byte Reserved1[16];
        public fixed int Reserved2[10];
        public UnicodeString ImagePathName;
        public UnicodeString CommandLine;
    }
}
