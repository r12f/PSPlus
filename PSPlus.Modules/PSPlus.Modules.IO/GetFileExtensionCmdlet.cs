﻿using PSPlus.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace PSPlus.Modules.IO
{
    [Cmdlet(VerbsCommon.Get, "FileExtension")]
    [OutputType(typeof(string))]
    public class GetFileExtensionCmdlet : CmdletBaseWithInputObject
    {
        protected override void ProcessRecord()
        {
            if (string.IsNullOrWhiteSpace(InputObject))
            {
                throw new ArgumentException("InputObject cannot be null or empty.");
            }

            string extension = Path.GetExtension(InputObject);
            WriteObject(extension);
        }
    }
}