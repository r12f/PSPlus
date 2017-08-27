using Microsoft.Diagnostics.Tracing.Session;
using System.Collections.Generic;

namespace PSPlus.Windows.Diagnostics.EventTracing
{
    public static class TraceEventSessionExtensions
    {
        public static void EnableEventSources(this TraceEventSession session, IEnumerable<EventSourceDefinition> eventSources)
        {
            foreach (var eventSource in eventSources)
            {
                TraceEventProviderOptions providerOptions = new TraceEventProviderOptions()
                {
                    EventIDsToEnable = eventSource.Ids,
                    ProcessIDFilter = eventSource.ProcessIds,
                    ProcessNameFilter = eventSource.ProcessNames,
                };

                // If this API fails, it will throw an exception.
                // https://github.com/Microsoft/perfview/blob/master/src/TraceEvent/TraceEventSession.cs
                session.EnableProvider(eventSource.ProviderGuid, eventSource.TraceLevel, eventSource.Keywords, providerOptions);
            }
        }
    }
}
