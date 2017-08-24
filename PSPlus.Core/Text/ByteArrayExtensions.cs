using System.Text;

namespace PSPlus.Core.Text
{
    public static class ByteArrayExtensions
    {
        public static string ToHex(this byte[] buffer)
        {
            StringBuilder hexString = new StringBuilder();
            for (int i = 0; i < buffer.Length; ++i)
            {
                hexString.Append(buffer[i].ToString("x2"));
            }
            return hexString.ToString();
        }
    }
}
