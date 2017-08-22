using Microsoft.Diagnostics.Tracing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;

namespace PSPlus.Diagnostics.EventTracing
{
    public sealed class EventWatcherManager : IEventWatcherManager, IDisposable
    {
        private class CallbackTask
        {
            public EventWatcherOptions WatcherOptions { get; set; }
            public TraceEvent EtwEvent { get; set; }
        }

        private Dictionary<string, EventWatcher> _watchers = new Dictionary<string, EventWatcher>();
        private ScriptBlock _afterWatchStarted;

        private ConcurrentQueue<CallbackTask> _callbackTaskQueue = new ConcurrentQueue<CallbackTask>();
        private AutoResetEvent _callbackTaskQueuedEvent = new AutoResetEvent(false);

        private ManualResetEvent _quitEvent = new ManualResetEvent(false);

        public EventWatcherManager(ScriptBlock afterWatchStarted)
        {
            _afterWatchStarted = afterWatchStarted;
        }

        public void AddWatcher(EventWatcherOptions watcherOptions)
        {
            if (string.IsNullOrWhiteSpace(watcherOptions.SessionName))
            {
                throw new ArgumentException("SessionName cannot be empty.");
            }

            if (_watchers.ContainsKey(watcherOptions.SessionName))
            {
                throw new ArgumentException(string.Format("Session {0} already exists! Please choose another session name.", watcherOptions.SessionName));
            }

            _watchers.Add(watcherOptions.SessionName, new EventWatcher(this, watcherOptions));
        }

        public void RunAllWatchersAndProcessCallbacks()
        {
            StartRunningAllWatchers();
            ProcessWatcherCallbacks();
            StopAllWatchersAndWaitForCompletion();
        }

        private void StartRunningAllWatchers()
        {
            foreach (var watcher in _watchers.Values)
            {
                watcher.Run();
            }

            if (_afterWatchStarted != null)
            {
                _afterWatchStarted.Invoke();
            }
        }

        private void ProcessWatcherCallbacks()
        {
            WaitHandle[] waitHandles = new WaitHandle[] { _quitEvent, _callbackTaskQueuedEvent };

            for (;;)
            {
                int handleIndex = WaitHandle.WaitAny(waitHandles);
                if (handleIndex == 0)
                {
                    break;
                }

                CallbackTask callbackTask;
                while (_callbackTaskQueue.TryDequeue(out callbackTask))
                {
                    object[] parameters = new object[] { callbackTask.EtwEvent, this as IEventWatcherManager, callbackTask.WatcherOptions };
                    callbackTask.WatcherOptions.OnEvent.Invoke(parameters);

                    // If we are requested to quit after handling one event, quit.
                    if (_quitEvent.WaitOne(0))
                    {
                        return;
                    }
                }
            }
        }

        private void StopAllWatchersAndWaitForCompletion()
        {
            foreach (var watcher in _watchers.Values)
            {
                watcher.RequestStop();
            }

            Task[] allWatcherTasks = _watchers.Values.Select(x => x.WatcherTask).ToArray();
            Task.WaitAll(allWatcherTasks);
        }

        public void QueueCallbackTask(EventWatcherOptions watcherOptions, TraceEvent etwEvent)
        {
            _callbackTaskQueue.Enqueue(new CallbackTask() { WatcherOptions = watcherOptions, EtwEvent = etwEvent });
            _callbackTaskQueuedEvent.Set();
        }

        public void RequestStop()
        {
            _quitEvent.Set();
        }

        public void Dispose()
        {
            foreach (var watcher in _watchers.Values)
            {
                watcher.Dispose();
            }

            _watchers.Clear();
        }
    }
}
