using System.Runtime.InteropServices;

namespace PSPlus.Core.Windows.Interop.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct User32Size
    {
        public int Cx;
        public int Cy;
    }
}
