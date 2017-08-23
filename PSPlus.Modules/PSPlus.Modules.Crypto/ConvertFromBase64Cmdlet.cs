using PSPlus.Core;
using PSPlus.Core.Crypto;
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
            string encodingName = EncodingName;
            if (string.IsNullOrWhiteSpace(encodingName))
            {
                encodingName = "utf-8";
            }

            Encoding encoding = Encoding.GetEncoding(encodingName);
            string decodedString = Base64.DecodeString(InputObject, encoding);
            WriteObject(decodedString);
        }
    }
}
