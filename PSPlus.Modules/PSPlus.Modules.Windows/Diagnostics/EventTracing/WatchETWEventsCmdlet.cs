using System;
using System.Collections.Generic;
using System.Management.Automation;
using PSPlus.Windows.Diagnostics.EventTracing;

namespace PSPlus.Modules.Windows.Diagnostics.EventTracing
{
    [Cmdlet(VerbsCommon.Watch, "EtwEvents")]
    public class WatchEtwEventsCmdlet : Cmdlet
    {
        #region Parameters
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public EventWatcherOptions[] WatcherOptions { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public ScriptBlock AfterWatchStarted { get; set; }
        #endregion

        #region Member variables
        private List<EventWatcherOptions> _allWatcherOptions = new List<EventWatcherOptions>();
        #endregion

        protected override void ProcessRecord()
        {
            if (WatcherOptions.Length == 0)
            {
                throw new PSArgumentException("No watcher options is specified.");
            }

            _allWatcherOptions.AddRange(WatcherOptions);
        }

        protected override void EndProcessing()
        {
            using (EventWatcherManager watcherManager = new EventWatcherManager(AfterWatchStarted))
            {
                foreach (var watcherOptions in _allWatcherOptions)
                {
                    watcherManager.AddWatcher(watcherOptions);
                }

                Console.CancelKeyPress += (sender, e) =>
                {
                    watcherManager.RequestStop();
                };
                watcherManager.RunAllWatchersAndProcessCallbacks();
            }
        }
    }
}
