using System;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace PSPlus.Windows.Diagnostics.EventTracing
{
    public sealed class EventWatcher : IDisposable
    {
        private EventWatcherManager _watcherManager;
        private EventWatcherOptions _watcherOptions;
        private TraceEventSession _session;

        public Task WatcherTask { get; private set; }

        public EventWatcher(EventWatcherManager watcherManager, EventWatcherOptions watcherOptions)
        {
            _watcherManager = watcherManager;
            _watcherOptions = watcherOptions;
        }

        public void Run()
        {
            _session = new TraceEventSession(_watcherOptions.SessionName);

            _session.Source.Dynamic.All += delegate (TraceEvent etwEvent)
            {
                _watcherManager.QueueCallbackTask(_watcherOptions, etwEvent);
            };

            _session.EnableEventSources(_watcherOptions.EventSources);

            WatcherTask = Task.Run(() =>
            {
                _session.Source.Process();
            });
        }

        public void RequestStop()
        {
            _session.Stop();
        }

        public void Dispose()
        {
            _session.Dispose();
            _session = null;
        }
    }
}
