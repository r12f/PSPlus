using System;
using System.IO;
using System.Management.Automation;
using PSPlus.Core.Powershell.Cmdlets;

namespace PSPlus.Modules.Core.IO
{
    [Cmdlet(VerbsCommon.Get, "FileExtension")]
    [OutputType(typeof(string))]
    public class GetFileExtensionCmdlet : CmdletBaseWithInputObject<string>
    {
        protected override void ProcessRecordInEH()
        {
            if (string.IsNullOrWhiteSpace(InputObject))
            {
                throw new PSArgumentException("InputObject cannot be null or empty.");
            }

            string extension = Path.GetExtension(InputObject);
            WriteObject(extension);
        }
    }
}
