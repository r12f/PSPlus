﻿using System.Runtime.InteropServices;

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
        public long Ldr;
        public long ProcessParameters;
        public fixed byte Reserved4[104];

        // PVOID Reserved5[52];
        public fixed long Reserved5[52];
        public long PostProcessInitRoutine;
        public fixed byte Reserved6[128];

        // PVOID Reserved7[1];
        public fixed long Reserved7[1];

        public ulong SessionId;
    }
}
