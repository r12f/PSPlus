using System;
using System.Diagnostics;
using System.Management.Automation;
using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Core.Windows.Diagnostics;

namespace PSPlus.Modules.Windows.Diagnostics.ProcessControl
{
    [Cmdlet(VerbsCommon.Get, "ProcessCommandLine")]
    [OutputType(typeof(bool))]
    public class GetProcessCommandLine : CmdletBaseWithOptionalInputObject<Process>
    {
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public int Id { get; set; } = 0;

        [Parameter(Mandatory = false)]
        public SwitchParameter OutputProcessNameWhenError
        {
            get { return _outputProcessNameWhenError; }
            set { _outputProcessNameWhenError = value; }
        }
        private bool _outputProcessNameWhenError = false;

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

            try
            {
                string commandLine = process.GetCommandLine();
                WriteObject(commandLine);
            }
            catch
            {
                if (_outputProcessNameWhenError)
                {
                    WriteObject(process.ProcessName);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
