using PSPlus.Core.Cmdlets;
using PSPlus.Core.Crypto;
using PSPlus.Core.Text;
using System;
using System.Management.Automation;
using System.Security.Cryptography;
using System.Text;

namespace PSPlus.Modules.Crypto
{
    [Cmdlet(VerbsCommon.Get, "StringHash")]
    [OutputType(typeof(HashResult))]
    public class GetStringHashCmdlet : CmdletBaseWithInputObject<string>
    {
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Algorithm { get; set; }

        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true, Mandatory = false)]
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

            HashAlgorithm algorithm = HashAlgorithmFactory.Create(Algorithm);
            if (algorithm == null)
            {
                throw new ArgumentException(string.Format("Unsupported algorithm: {0}", Algorithm));
            }

            byte[] hashBuffer = HashGenerator.ComputeStringHash(InputObject, algorithm, encoding);

            HashResult result = new HashResult()
            {
                Algorithm = Algorithm,
                Hash = hashBuffer.ToHex(),
                HashBuffer = hashBuffer,
            };

            WriteObject(result);
        }
    }
}
