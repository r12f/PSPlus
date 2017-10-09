using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace PSPlus.Modules.Tfs.Work.WorkItem
{
    [Cmdlet(VerbsCommon.Get, "TfsWorkItem")]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem))]
    public class GetTfsWorkItemCmdlet : TfsCmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, HelpMessage = "Work item Id.")]
        public int Id { get; set; } = 0;

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Work item type.")]
        [Alias("t")]
        public string Type { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Conditions in where clause in WIQL query.")]
        [Alias("f")]
        public string Filter { get; set; }

        protected override void ProcessRecordInEH()
        {
            WorkItemStore workItemStore = EnsureWorkItemStore();
            foreach (var workItem in QueryWorkItems(workItemStore))
            {
                WriteObject(workItem);
            }
        }

        private IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem> QueryWorkItems(WorkItemStore workItemStore)
        {
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
