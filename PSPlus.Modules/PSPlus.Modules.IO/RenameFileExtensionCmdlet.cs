using PSPlus.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace PSPlus.Modules.IO
{
    [Cmdlet(VerbsCommon.Rename, "FileExtension")]
    [OutputType(typeof(string))]
    public class RenameFileExtensionCmdlet : CmdletBaseWithInputObject
    {
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Extension;

        protected override void ProcessRecord()
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
