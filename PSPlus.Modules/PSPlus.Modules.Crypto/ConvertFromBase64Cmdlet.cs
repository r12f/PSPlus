using PSPlus.Core;
using System;
using System.Management.Automation;
using System.Text;

namespace PSPlus.Modules.Crypto
{
    [Cmdlet(VerbsData.ConvertFrom, "Base64")]
    [OutputType(typeof(string))]
    public class ConvertFromBase64Cmdlet : CmdletBaseWithInputObject<string>
    {
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public string EncodingName { get; set; }

        protected override void ProcessRecord()
        {
            byte[] buffer = Convert.FromBase64String(InputObject);

            string encodingName = EncodingName;
            if (string.IsNullOrWhiteSpace(encodingName))
            {
                encodingName = "unicode";
            }

            Encoding encoding = Encoding.GetEncoding(encodingName);
            string decodedString = encoding.GetString(buffer);
            WriteObject(decodedString);
        }
    }
}
