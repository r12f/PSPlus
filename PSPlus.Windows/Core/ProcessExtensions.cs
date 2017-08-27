using PSPlus.Core.Windows.Core.Interop;
using System.Diagnostics;

namespace PSPlus.Core.Windows.Core
{
    public static class ProcessExtensions
    {
        public static bool IsWow64Process(this Process process)
        {
            bool isWow64Process = false;
            unsafe
            {
                CoreAPIs.IsWow64Process(process.Handle, &isWow64Process);
            }
            return isWow64Process;
        }
    }
}
