using PSPlus.Core.Crypto;
using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Core.Text;
using System;
using System.Management.Automation;
using System.Text;

namespace PSPlus.Modules.Core.Crypto
{
    [Cmdlet(VerbsData.ConvertTo, "Base64")]
    [OutputType(typeof(string))]
    public class ConvertToBase64Cmdlet : CmdletBaseWithInputObject<string>
    {
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public string EncodingName { get; set; }

        protected override void ProcessRecordInEH()
        {
            string encodingName = EncodingName;
            if (string.IsNullOrWhiteSpace(encodingName))
            {
                encodingName = EncodingFactory.EncodingNames.UTF8;
            }

            Encoding encoding = EncodingFactory.Get(encodingName);
            if (encoding == null)
            {
                throw new ArgumentException(string.Format("Unsupported encoding: {0}", encodingName));
            }

            string encodedString = Base64.EncodeString(InputObject, encoding);
            WriteObject(encodedString);
        }
    }
}
