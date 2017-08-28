using System;
using System.Runtime.InteropServices;

namespace PSPlus.Core.Windows.Interop.Ntdll
{
    public static unsafe class NtdllAPIs
    {
        [DllImport("ntdll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int NtQueryInformationProcess([In] IntPtr processHandle, [In] int processInformationClass, [Out] IntPtr processInformation, [In] ulong processInformationLength, [Out] ulong* returnLength);

        [DllImport("ntdll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int NtQueryInformationThread([In] IntPtr threadHandle, [In] int threadInformationClass, [Out] IntPtr threadInformation, [In] ulong threadInformationLength, [Out] ulong* returnLength);
    }
}
