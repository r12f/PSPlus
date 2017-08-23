using System;
using System.Text;

namespace PSPlus.Core.Crypto
{
    public static class Base64
    {
        public static string EncodeString(string s, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(s);
            return Convert.ToBase64String(buffer);
        }

        public static string DecodeString(string s, Encoding encoding)
        {
            byte[] buffer = Convert.FromBase64String(s);
            return encoding.GetString(buffer);
        }
    }
}
