using System;
using System.Runtime.InteropServices;

namespace PSPlus.Core.Windows.Interop.Ntdll
{
    public static unsafe class NtdllAPIs
    {
        [DllImport("ntdll.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int NtQueryInformationProcess([In] IntPtr processHandle, [In] int processInformationClass, [Out] IntPtr processInformation, [In] ulong processInformationLength, [Out] ulong* returnLength);
    }
}
