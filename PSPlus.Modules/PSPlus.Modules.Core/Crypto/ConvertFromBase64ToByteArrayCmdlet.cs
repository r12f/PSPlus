using System;
using System.Management.Automation;
using PSPlus.Core.Powershell.Cmdlets;

namespace PSPlus.Modules.Core.Crypto
{
    [Cmdlet(VerbsData.ConvertFrom, "Base64ToByteArray")]
    [OutputType(typeof(string))]
    public class ConvertFromBase64ByteArrayCmdlet : CmdletBaseWithInputObject<string>
    {
        protected override void ProcessRecordInEH()
        {
            byte[] buffer = Convert.FromBase64String(InputObject);
            WriteObject(buffer);
        }
    }
}
