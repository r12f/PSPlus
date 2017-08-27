using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PSPlus.Windows.Core.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PEB64
    {
        public fixed byte Reserved1[2];
        public byte BeingDebugged;
        public fixed byte Reserved2[1];

        // PVOID Reserved3[2];
        public fixed long Reserved3[2];
        public IntPtr Ldr;
        public IntPtr ProcessParameters;
        public fixed byte Reserved4[104];

        // PVOID Reserved5[52];
        public fixed long Reserved5[52];
        public IntPtr PostProcessInitRoutine;
        public fixed byte Reserved6[128];

        // PVOID Reserved7[1];
        public fixed long Reserved7[1];

        public ulong SessionId;
    }
}
