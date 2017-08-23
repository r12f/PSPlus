using PSPlus.Core;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Crypto
{
    [Cmdlet(VerbsData.ConvertFrom, "Base64ToByteArray")]
    [OutputType(typeof(string))]
    public class ConvertFromBase64ByteArrayCmdlet : CmdletBaseWithInputObject<string>
    {
        protected override void ProcessRecord()
        {
            byte[] buffer = Convert.FromBase64String(InputObject);
            WriteObject(buffer);
        }
    }
}
