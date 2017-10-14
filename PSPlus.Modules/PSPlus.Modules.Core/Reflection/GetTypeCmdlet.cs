using PSPlus.Core.Powershell;
using PSPlus.Core.Powershell.Cmdlets;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Core.Reflection
{
    [Cmdlet(VerbsCommon.Get, "Type")]
    [OutputType(typeof(Type))]
    public class GetTypeCmdlet : CmdletBaseWithInputObject<PSObject>
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter All
        {
            get { return _all; }
            set { _all = value; }
        }
        private bool _all = false;

        protected override void ProcessRecordInEH()
        {
            object baseObject = InputObject.BaseObject;
            Type baseObjectType = baseObject as Type ?? baseObject.GetType();
            WriteObject(baseObjectType);

            if (!All)
            {
                PipelineControl.StopUpstreamCommands(this);
            }
        }
    }
}
