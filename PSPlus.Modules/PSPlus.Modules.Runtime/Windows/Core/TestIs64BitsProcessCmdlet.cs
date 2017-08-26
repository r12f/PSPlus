﻿using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Core.Windows.Core;
using System;
using System.Diagnostics;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Core
{
    [Cmdlet(VerbsDiagnostic.Test, "Is64BitsProcess")]
    [OutputType(typeof(bool))]
    public class TestIs64BitsProcessCmdlet : CmdletBaseWithOptionalInputObject<Process>
    {
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public int Id { get; set; } = 0;

        protected override void ProcessRecordInEH()
        {
            Process process = InputObject;
            if (process == null)
            {
                if (Id == 0)
                {
                    throw new ArgumentException("Both InputObject and Id is not specified.");
                }

                // If the process doesn't exist, it will throw an exception, so we don't need to do a null check afterwards.
                process = Process.GetProcessById(Id);
            }

            if (!Environment.Is64BitOperatingSystem)
            {
                WriteObject(false);
                return;
            }

            bool isWow64Process = process.IsWow64Process();
            WriteObject(!isWow64Process);
        }
    }
}
