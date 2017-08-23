using PSPlus.Core;
using System;
using System.Management.Automation;
using System.Text;

namespace PSPlus.Modules.Crypto
{
    [Cmdlet(VerbsData.ConvertTo, "Base64")]
    [OutputType(typeof(string))]
    public class ConvertToBase64Cmdlet : CmdletBaseWithInputObject<string>
    {
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public string EncodingName { get; set; }

        protected override void ProcessRecord()
        {
            string encodingName = EncodingName;
            if (string.IsNullOrWhiteSpace(encodingName))
            {
                encodingName = "unicode";
            }

            Encoding encoding = Encoding.GetEncoding(encodingName);
            byte[] buffer = encoding.GetBytes(InputObject);
            string encodedString = Convert.ToBase64String(buffer);
            WriteObject(encodedString);
        }
    }
}
