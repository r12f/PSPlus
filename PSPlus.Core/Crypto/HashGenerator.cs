using System.Security.Cryptography;
using System.Text;

namespace PSPlus.Core.Crypto
{
    public class HashGenerator
    {
        public static byte[] ComputeStringHash(string data, HashAlgorithm algorithm, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(data);
            return algorithm.ComputeHash(buffer);
        }
    }
}
