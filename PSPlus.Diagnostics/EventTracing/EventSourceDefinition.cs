using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;
using System;
using System.Collections.Generic;

namespace PSPlus.Diagnostics.EventTracing
{
    public sealed class EventSourceDefinition
    {
        public string ProviderName { get; set; }
        public Guid ProviderGuid { get; set; }
        public ulong Keywords { get; set; }
        public TraceEventLevel TraceLevel { get; set; }
        public List<int> Ids { get; set; } 
        public List<int> ProcessIds { get; set; }
        public List<string> ProcessNames { get; set; }

        public static EventSourceDefinition Parse(string providerName, ulong matchAnyKeywords, int traceLevel, IEnumerable<int> ids, IEnumerable<int> processIds, IEnumerable<string> processNames)
        {
            Guid providerGuid = TraceEventProviders.GetProviderGuidByName(providerName);
            if (providerGuid == Guid.Empty)
            {
                providerGuid = TraceEventProviders.GetEventSourceGuidFromName(providerName);
                if (providerGuid == Guid.Empty)
                {
                    throw new ArgumentException(string.Format("Provider name doesn't exist: {0}", providerName));
                }
            }

            var definition = new EventSourceDefinition()
            {
                ProviderName = providerName,
                ProviderGuid = providerGuid,
                Keywords = matchAnyKeywords,
                TraceLevel = (TraceEventLevel)traceLevel,
            };

            if (ids != null)
            {
                var idList = new List<int>(ids);
                definition.Ids = idList.Count > 0 ? idList : null;
            }

            if (processIds != null)
            {
                var processIdList = new List<int>(processIds);
                definition.ProcessIds = processIdList.Count > 0 ? processIdList : null;
            }

            if (processNames != null)
            {
                var processNameList = new List<string>(processNames);
                definition.ProcessNames = processNameList.Count > 0 ? processNameList : null;
            }

            return definition;
        }
    }
}
