using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PSPlus.Windows.Diagnostics.EventTracing
{
    public class EventWatcherOptions
    {
        public string SessionName { get; set; }
        public List<EventSourceDefinition> EventSources { get; set; }
        public ScriptBlock OnEvent { get; set; }

        public EventWatcherOptions(string sessionName, IEnumerable<EventSourceDefinition> eventSources, ScriptBlock onEvent)
        {
            SessionName = sessionName;
            EventSources = new List<EventSourceDefinition>(eventSources);
            OnEvent = onEvent;

            if (string.IsNullOrWhiteSpace(SessionName))
            {
                throw new ArgumentException("SessionName cannot be empty.");
            }

            if (EventSources.Count == 0)
            {
                throw new ArgumentException("No event source is specified.");
            }

            if (OnEvent == null)
            {
                throw new ArgumentNullException("OnEvent callback cannot be null.");
            }
        }
    }
}
