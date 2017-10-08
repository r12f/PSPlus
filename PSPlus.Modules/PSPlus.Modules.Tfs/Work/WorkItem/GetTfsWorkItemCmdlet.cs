using Microsoft.TeamFoundation.Client;
using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Tfs;
using PSPlus.Tfs.TfsExtensions;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PSPlus.Modules.Tfs.Work.WorkItem
{
    [Cmdlet(VerbsCommon.Get, "TfsWorkItem")]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem))]
    public class GetTfsWorkItemCmdlet : CmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, HelpMessage = "WorkItem Id.")]
        public int Id { get; set; } = 0;

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Work item type.")]
        public string Type { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Conditions in where clause in WIQL query.")]
        public string Filter { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "TFS Collection.")]
        public TfsTeamProjectCollection Collection { get; set; }

        protected override void ProcessRecordInEH()
        {
            if (Collection == null)
            {
                Collection = CmdletContext.Collection;
            }

            if (Collection == null)
            {
                throw new ArgumentException("Collection is not specified. Please use Connect-TfsTreamProjectCollection to connect to your collection, or use -Collection option to specify one.");
            }

            foreach (var workItem in QueryWorkItems())
            {
                WriteObject(workItem);
            }
        }

        private IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem> QueryWorkItems()
        {
            var workItemStore = Collection.GetWorkItemStore();
            if (Id > 0)
            {
                yield return workItemStore.GetWorkItem(Id);
                yield break;
            }

            if (!string.IsNullOrWhiteSpace(Filter))
            {
                var workItemCollection = workItemStore.Query(Filter);
                foreach (Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem workItem in workItemCollection)
                {
                    yield return workItem;
                }

                yield break;
            }
        }
    }
}
