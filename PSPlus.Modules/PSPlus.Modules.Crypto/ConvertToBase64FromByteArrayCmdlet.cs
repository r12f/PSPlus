using PSPlus.Core;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Crypto
{
    [Cmdlet(VerbsData.ConvertTo, "Base64FromByteArray")]
    [OutputType(typeof(string))]
    public class ConvertToBase64FromByteArrayCmdlet : CmdletBaseWithInputObject<byte[]>
    {
        protected override void ProcessRecord()
        {
            string base64String = Convert.ToBase64String(InputObject);
            WriteObject(base64String);
        }
    }
}
