using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Windows.Environments;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Environments
{
    [Cmdlet(VerbsCommon.Get, "EnvironmentVariable")]
    [OutputType(typeof(string))]
    public class GetEnvironmentVariableCmdlet : CmdletBase
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Key { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = false)]
        [ValidateSet("User", "Machine")]
        public string Target { get; set; }

        protected override void ProcessRecordInEH()
        {
            EnvironmentVariableTarget target = EnvironmentVariableTarget.Process;

            switch (Target)
            {
                case "User":
                    target = EnvironmentVariableTarget.User;
                    break;
                case "Machine":
                    target = EnvironmentVariableTarget.Machine;
                    break;
            }
            
            string value = EnvironmentVariableUtils.Get(Key, target);
            WriteObject(value);
        }
    }
}
