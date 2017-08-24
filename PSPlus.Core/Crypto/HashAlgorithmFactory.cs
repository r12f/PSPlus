using System.Security.Cryptography;

namespace PSPlus.Core.Crypto
{
    public class HashAlgorithmFactory
    {
        public static class HashAlgorithmNames
        {
            public const string MD5 = "MD5";
            public const string SHA1 = "SHA1";
            public const string SHA256 = "SHA256";
            public const string SHA384 = "SHA384";
            public const string SHA512 = "SHA512";
            public const string RIPEMD160 = "RIPEMD160";
        }

        public static HashAlgorithm Create(string algorithmName)
        {
            switch (algorithmName.ToUpperInvariant())
            {
                case HashAlgorithmNames.MD5:
                    return MD5.Create();
                case HashAlgorithmNames.SHA1:
                    return SHA1.Create();
                case HashAlgorithmNames.SHA256:
                    return SHA256.Create();
                case HashAlgorithmNames.SHA384:
                    return SHA384.Create();
                case HashAlgorithmNames.SHA512:
                    return SHA512.Create();
                case HashAlgorithmNames.RIPEMD160:
                    return RIPEMD160.Create();
            }

            return null;
        }
    }
}
