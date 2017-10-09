using System;
using System.IO;
using System.Management.Automation;
using PSPlus.Core.Powershell.Cmdlets;

namespace PSPlus.Modules.Core.IO
{
    [Cmdlet(VerbsCommon.Rename, "FileExtension")]
    [OutputType(typeof(string))]
    public class RenameFileExtensionCmdlet : CmdletBaseWithInputObject<string>
    {
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Extension;

        protected override void ProcessRecordInEH()
        {
            if (string.IsNullOrWhiteSpace(InputObject))
            {
                throw new ArgumentException("InputObject cannot be null or empty.");
            }

            string newPath = Path.ChangeExtension(InputObject, Extension);
            WriteObject(newPath);
        }
    }
}
