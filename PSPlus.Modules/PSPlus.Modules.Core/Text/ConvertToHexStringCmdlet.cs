using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Core.Text;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace PSPlus.Modules.Core.Text
{
    [Cmdlet(VerbsData.ConvertTo, "HexString")]
    [OutputType(typeof(string))]
    public class ConvertToHexStringCmdlet : CmdletBaseWithInputObject<PSObject>
    {
        [ValidateSet(
            EncodingFactory.EncodingNames.ASCII,
            EncodingFactory.EncodingNames.Unicode,
            EncodingFactory.EncodingNames.LittleEndianUnicode,
            EncodingFactory.EncodingNames.BigEndianUnicode,
            EncodingFactory.EncodingNames.UTF7,
            EncodingFactory.EncodingNames.UTF8,
            EncodingFactory.EncodingNames.UTF32
        )]
        public string Encoding { get; set; } = EncodingFactory.EncodingNames.UTF8;

        protected override void ProcessRecordInEH()
        {
            object baseObject = InputObject.BaseObject;
            byte[] baseObjectBytes = ConvertObjectToBytes(baseObject);
            string hexString = baseObjectBytes.ToHex();
            WriteObject(hexString);
        }

        private byte[] ConvertObjectToBytes(object baseObject)
        {
            // Byte
            if (baseObject is byte)
            {
                return new byte[] { (byte)baseObject };
            }
            else if (baseObject is byte[])
            {
                return baseObject as byte[];
            }

            // Int16
            else if (baseObject is Int16)
            {
                return BitConverter.GetBytes((Int16)baseObject);
            }
            else if (baseObject is Int16[])
            {
                List<byte> bytes = new List<byte>();
                foreach (var value in baseObject as Int16[])
                {
                    byte[] valueBytes = BitConverter.GetBytes(value);
                    bytes.AddRange(valueBytes);
                }
                return bytes.ToArray();
            }

            // UInt16
            else if (baseObject is UInt16)
            {
                return BitConverter.GetBytes((UInt16)baseObject);
            }
            else if (baseObject is UInt16[])
            {
                List<byte> bytes = new List<byte>();
                foreach (var value in baseObject as UInt16[])
                {
                    byte[] valueBytes = BitConverter.GetBytes(value);
                    bytes.AddRange(valueBytes);
                }
                return bytes.ToArray();
            }

            // Int32
            else if (baseObject is Int32)
            {
                return BitConverter.GetBytes((Int32)baseObject);
            }
            else if (baseObject is Int32[])
            {
                List<byte> bytes = new List<byte>();
                foreach (var value in baseObject as Int32[])
                {
                    byte[] valueBytes = BitConverter.GetBytes(value);
                    bytes.AddRange(valueBytes);
                }
                return bytes.ToArray();
            }

            // UInt32
            else if (baseObject is UInt32)
            {
                return BitConverter.GetBytes((UInt32)baseObject);
            }
            else if (baseObject is UInt32[])
            {
                List<byte> bytes = new List<byte>();
                foreach (var value in baseObject as UInt32[])
                {
                    byte[] valueBytes = BitConverter.GetBytes(value);
                    bytes.AddRange(valueBytes);
                }
                return bytes.ToArray();
            }

            // Int64
            else if (baseObject is Int64)
            {
                return BitConverter.GetBytes((Int64)baseObject);
            }
            else if (baseObject is Int64[])
            {
                List<byte> bytes = new List<byte>();
                foreach (var value in baseObject as Int64[])
                {
                    byte[] valueBytes = BitConverter.GetBytes(value);
                    bytes.AddRange(valueBytes);
                }
                return bytes.ToArray();
            }

            // UInt64
            else if (baseObject is UInt64)
            {
                return BitConverter.GetBytes((UInt64)baseObject);
            }
            else if (baseObject is UInt64[])
            {
                List<byte> bytes = new List<byte>();
                foreach (var value in baseObject as UInt64[])
                {
                    byte[] valueBytes = BitConverter.GetBytes(value);
                    bytes.AddRange(valueBytes);
                }
                return bytes.ToArray();
            }

            // String
            else if (baseObject is string)
            {
                Encoding encoding = EncodingFactory.Get(Encoding);
                if (encoding == null)
                {
                    throw new ArgumentException(string.Format("Unsupported encoding type: {0}.", Encoding));
                }

                return encoding.GetBytes(baseObject as string);
            }

            throw new ArgumentException(string.Format("Unsupported object type: {0}", baseObject.GetType().FullName));
        }
    }
}
