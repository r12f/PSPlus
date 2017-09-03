using System;

namespace PSPlus.Core.Powershell
{
    public class NativeFunctionSignature
    {
        public string DllName { get; set; }
        public string Signature { get; set; }

        public NativeFunctionSignature(string dllName, string signature)
        {
            if (string.IsNullOrWhiteSpace(dllName))
            {
                throw new ArgumentException("DllName cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(signature))
            {
                throw new ArgumentException("Signature cannot be null or empty.");
            }

            DllName = dllName;
            Signature = signature;
        }
    }
}
