using PSPlus.Diagnostics.EventTracing;
using System.Management.Automation;

namespace PSPlus.Modules.Diagnostics
{
    [Cmdlet(VerbsCommon.New, "EventSourceDefinition")]
    [OutputType(typeof(EventSourceDefinition))]
    public class NewEventSourceDefinitionCmdlet : Cmdlet
    {
        #region Parameters
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string ProviderName { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public ulong Keywords { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public int TraceLevel { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public int[] Ids { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public int[] ProcessIds { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public string[] ProcessNames { get; set; }
        #endregion

        protected override void ProcessRecord()
        {
            var eventSourceDefinition = EventSourceDefinition.Parse(ProviderName, Keywords, TraceLevel, Ids, ProcessIds, ProcessNames);
            WriteObject(eventSourceDefinition);
        }
    }
}
