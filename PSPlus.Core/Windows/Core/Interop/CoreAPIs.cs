using System;
using System.Runtime.InteropServices;

namespace PSPlus.Core.Windows.Core.Interop
{
    public static unsafe class CoreAPIs
    {
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr processHandle, [Out] bool* IsWow64Process);
    }
}
