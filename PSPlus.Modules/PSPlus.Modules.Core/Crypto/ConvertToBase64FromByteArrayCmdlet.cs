using System;
using System.Management.Automation;
using PSPlus.Core.Powershell.Cmdlets;

namespace PSPlus.Modules.Core.Crypto
{
    [Cmdlet(VerbsData.ConvertTo, "Base64FromByteArray")]
    [OutputType(typeof(string))]
    public class ConvertToBase64FromByteArrayCmdlet : CmdletBaseWithInputObject<byte[]>
    {
        protected override void ProcessRecordInEH()
        {
            string base64String = Convert.ToBase64String(InputObject);
            WriteObject(base64String);
        }
    }
}
