using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSPlus.Core.Text
{
    public class EncodingFactory
    {
        public static class EncodingNames
        {
            public const string ASCII = "ASCII";
            public const string Unicode = "UNICODE";
            public const string LittleEndianUnicode = "UNICODELE";
            public const string BigEndianUnicode = "UNICODEBE";
            public const string UTF7 = "UTF7";
            public const string UTF8 = "UTF8";
            public const string UTF32 = "UTF32";
        }

        public static Encoding Get(string encodingName)
        {
            Encoding encoding = null;
            switch (encodingName.ToUpperInvariant())
            {
                case EncodingNames.ASCII:
                    encoding = Encoding.ASCII;
                    break;
                case EncodingNames.Unicode:
                case EncodingNames.LittleEndianUnicode:
                    encoding = Encoding.Unicode;
                    break;
                case EncodingNames.BigEndianUnicode:
                    encoding = Encoding.BigEndianUnicode;
                    break;
                case EncodingNames.UTF7:
                    encoding = Encoding.UTF7;
                    break;
                case EncodingNames.UTF8:
                    encoding = Encoding.UTF8;
                    break;
                case EncodingNames.UTF32:
                    encoding = Encoding.UTF32;
                    break;
            }

            return encoding;
        }
    }
}
