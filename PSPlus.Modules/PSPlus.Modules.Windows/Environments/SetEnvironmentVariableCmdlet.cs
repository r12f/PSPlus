using System;
using System.Management.Automation;
using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Windows.Environments;

namespace PSPlus.Modules.Windows.Environments
{
    [Cmdlet(VerbsCommon.Set, "EnvironmentVariable")]
    public class SetEnvironmentVariableCmdlet : CmdletBase
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Key { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Value { get; set; }

        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true, Mandatory = false)]
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

            EnvironmentVariableUtils.Set(Key, Value, target);

            if (target != EnvironmentVariableTarget.Process)
            {
                Environment.SetEnvironmentVariable(Key, Value);
            }
        }
    }
}
