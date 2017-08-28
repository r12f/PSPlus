using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Windows.Environments;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Windows.Environments
{
    [Cmdlet(VerbsData.Update, "EnvironmentVariablesInSystem")]
    [OutputType(typeof(string))]
    public class UpdateEnvironmentVariablesInSystemCmdlet : CmdletBase
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter Async
        {
            get { return _async; }
            set { _async = value; }
        }
        private bool _async = false;

        protected override void ProcessRecordInEH()
        {
            EnvironmentVariableUtils.Refresh(_async);
        }
    }
}
