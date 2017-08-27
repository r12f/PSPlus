using System;
using System.Runtime.InteropServices;

namespace PSPlus.Core.Windows.Core.Interop
{
    public static unsafe class CoreAPIs
    {
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr processHandle, [Out] bool* IsWow64Process);

        [DllImport("ntdll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int NtQueryInformationProcess([In] IntPtr processHandle, [In] int processInformationClass, [Out] IntPtr processInformation, [In] ulong processInformationLength, [Out] ulong* returnLength);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory([In] IntPtr processHandle, [In] IntPtr baseAddress, [Out] IntPtr buffer, [In] int size, [Out] IntPtr numberOfBytesRead);
    }
}
