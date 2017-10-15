using System;
using Microsoft.Win32;
using PSPlus.Core.Windows.Interop.User32;

namespace PSPlus.Windows.Environments
{
    // Implemented based on https://msdn.microsoft.com/en-us/library/96xafkes(v=vs.110).aspx
    public static class EnvironmentVariableUtils
    {
        private static readonly string s_userEnvironmentKeyName = @"Environment";
        private static readonly string s_machineEnvironmentKeyName = @"System\CurrentControlSet\Control\Session Manager\Environment";

        public static void Set(string valueName, string value, EnvironmentVariableTarget target)
        {
            switch (target)
            {
                case EnvironmentVariableTarget.Process:
                    Environment.SetEnvironmentVariable(valueName, value);
                    break;
                case EnvironmentVariableTarget.User:
                    RegistryKey userEnvironmentKey = Registry.CurrentUser.OpenSubKey(s_userEnvironmentKeyName, true);
                    SetEnvironmentVariableUnderRegistryKey(userEnvironmentKey, valueName, value);
                    break;
                case EnvironmentVariableTarget.Machine:
                    RegistryKey machineEnvironmentKey = Registry.LocalMachine.OpenSubKey(s_machineEnvironmentKeyName, true);
                    SetEnvironmentVariableUnderRegistryKey(machineEnvironmentKey, valueName, value);
                    break;
            }
        }

        private static void SetEnvironmentVariableUnderRegistryKey(RegistryKey environmentKey, string valueName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                environmentKey.DeleteValue(valueName);
            }
            else
            {
                environmentKey.SetValue(valueName, value);
            }
        }

        public static string Get(string valueName, EnvironmentVariableTarget target)
        {
            string value = null;
            switch (target)
            {
                case EnvironmentVariableTarget.Process:
                    value = Environment.GetEnvironmentVariable(valueName);
                    break;
                case EnvironmentVariableTarget.User:
                    RegistryKey userEnvironmentKey = Registry.CurrentUser.OpenSubKey(s_userEnvironmentKeyName);
                    value = GetEnvironmentVariableUnderRegistryKey(userEnvironmentKey, valueName);
                    break;
                case EnvironmentVariableTarget.Machine:
                    RegistryKey machineEnvironmentKey = Registry.LocalMachine.OpenSubKey(s_machineEnvironmentKeyName);
                    value = GetEnvironmentVariableUnderRegistryKey(machineEnvironmentKey, valueName);
                    break;
            }

            return value;
        }

        private static string GetEnvironmentVariableUnderRegistryKey(RegistryKey environmentKey, string valueName)
        {
            return environmentKey.GetValue(valueName) as string;
        }

        public static void Refresh(bool async)
        {
            if (async)
            {
                User32APIs.PostMessageW(User32Consts.HWND_BROADCAST, User32MsgIds.WM_SETTINGCHANGE, (IntPtr)0, (IntPtr)0);
            }
            else
            {
                User32APIs.SendMessageW(User32Consts.HWND_BROADCAST, User32MsgIds.WM_SETTINGCHANGE, (IntPtr)0, (IntPtr)0);
            }
        }
    }
}
