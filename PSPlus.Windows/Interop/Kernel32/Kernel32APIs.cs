using System;
using System.Runtime.InteropServices;

namespace PSPlus.Windows.Interop.Kernel32
{
    public static unsafe class Kernel32APIs
    {
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr processHandle, [Out] bool* IsWow64Process);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory([In] IntPtr processHandle, [In] IntPtr baseAddress, [Out] IntPtr buffer, [In] int size, [Out] IntPtr numberOfBytesRead);
    }
}
