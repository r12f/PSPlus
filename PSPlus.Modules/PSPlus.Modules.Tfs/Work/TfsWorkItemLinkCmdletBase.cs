using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.WIQLUtils;

namespace PSPlus.Modules.Tfs.Work
{
    public class TfsWorkItemLinkCmdletBase : TfsCmdletBase
    {
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Work item Id.")]
        [Alias("wid")]
        public List<int> WorkItemId { get; set; }

        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, HelpMessage = "Work items.")]
        [Alias("w")]
        public List<WorkItem> WorkItem { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Work item relation type.")]
        [Alias("wrt")]
        public object WorkItemRelationType { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Related work item Id.")]
        [Alias("rwid", "rw")]
        public int RelatedWorkItemId { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Hyperlink.")]
        public string Hyperlink { get; set; }

        protected IEnumerable<WorkItem> QueryFromWorkItems(WorkItemStore workItemStore)
        {
            if (WorkItemId != null && WorkItemId.Count > 0)
            {
                WIQLQueryBuilder queryBuilder = new WIQLQueryBuilder();
                queryBuilder.Ids = WorkItemId;

                string wiqlQuery = queryBuilder.Build();
                WriteVerbose(string.Format("Query workitems with WIQL query: {0}.", wiqlQuery));

                var workItemCollection = workItemStore.Query(wiqlQuery);
                foreach (WorkItem workItem in workItemCollection)
                {
                    yield return workItem;
                }
            }

            if (WorkItem != null)
            {
                WriteVerbose(string.Format("Get workitems from command line argument: Count = {0}.", WorkItem.Count));

                foreach (WorkItem workItem in WorkItem)
                {
                    yield return workItem;
                }
            }
        }
    }
}
