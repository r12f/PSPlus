using PSPlus.Diagnostics.EventTracing;
using System.Collections.Generic;
using System.Management.Automation;

namespace PSPlus.Modules.Diagnostics
{
    [Cmdlet(VerbsCommon.New, "EventWatcherOptions")]
    [OutputType(typeof(EventWatcherOptions))]
    public class NewEventWatcherOptionsCmdlet : Cmdlet
    {
        #region Parameters
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string SessionName { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public string ProviderName { get; set; }

        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public ulong Keywords { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public int TraceLevel { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public int[] Ids { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public int[] ProcessIds { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public string[] ProcessNames { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public EventSourceDefinition[] EventSources { get; set; }

        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public ScriptBlock OnEvent { get; set; }
        #endregion

        protected override void ProcessRecord()
        {
            var watcherOptions = new EventWatcherOptions(SessionName, GetAllEventSourceDefinitionsFromParameters(), OnEvent);
            WriteObject(watcherOptions);
        }

        private IEnumerable<EventSourceDefinition> GetAllEventSourceDefinitionsFromParameters()
        {
            if (!string.IsNullOrWhiteSpace(ProviderName))
            {
                yield return EventSourceDefinition.Parse(ProviderName, Keywords, TraceLevel, Ids, ProcessIds, ProcessNames);
            }

            if (EventSources != null)
            {
                foreach (var eventSource in EventSources)
                {
                    yield return eventSource;
                }
            }
        }
    }
}
