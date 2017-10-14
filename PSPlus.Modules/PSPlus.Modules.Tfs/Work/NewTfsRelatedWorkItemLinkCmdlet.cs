using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.WIQLUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PSPlus.Modules.Tfs.Work
{
    // We output impacted work items instead of RelatedLink, because RelatedLink only contains the "to" end, but no "from" end.
    // So outputing work item should be more helpful in most of the cases.
    [Cmdlet(VerbsCommon.Add, "TfsWorkItemLink")]
    [OutputType(typeof(WorkItem))]
    public class AddTfsWorkItemLinkCmdlet : TfsCmdletBase
    {
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Work item Id.")]
        [Alias("wid")]
        public List<int> WorkItemId { get; set; }

        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, HelpMessage = "Work items.")]
        [Alias("w")]
        public List<WorkItem> WorkItem { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Related work item type.")]
        [Alias("rwt")]
        public object WorkItemRelationType { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Related work item Id.")]
        [Alias("rwid", "rw")]
        public int RelatedWorkItemId { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true, HelpMessage = "Hyperlink.")]
        public string URL { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true, HelpMessage = "Hyperlink.")]
        public string ExternalURL { get; set; }

        protected override void ProcessRecordInEH()
        {
            WorkItemStore workItemStore = EnsureWorkItemStore();
            WorkItemLinkTypeEnd linkTypeEnd = EnsureWorkItemLinkTypeEnd(workItemStore);

            WorkItem[] workItems = QueryFromWorkItems(workItemStore).ToArray();
            if (workItems.Length == 0)
            {
                return;
            }

            WorkItem toWorkItem = workItemStore.GetWorkItem(RelatedWorkItemId);
            if (toWorkItem == null)
            {
                throw new ArgumentException(string.Format("Invalid to work item id: {0}.", RelatedWorkItemId));
            }

            foreach (var workItem in workItems)
            {
                workItem.Links.Add(new RelatedLink(linkTypeEnd, toWorkItem.Id));
                WriteObject(workItem);
            }

            workItemStore.BatchSave(workItems, SaveFlags.MergeLinks);
        }

        private WorkItemLinkTypeEnd EnsureWorkItemLinkTypeEnd(WorkItemStore workItemStore)
        {
            WorkItemLinkTypeEnd linkTypeEnd = null;
            if (WorkItemRelationType == null)
            {
                throw new ArgumentException("Work item link type must be specified!");
            }
            else if (WorkItemRelationType is string)
            {
                string linkTypeEndName = WorkItemRelationType as string;

                WorkItemLinkTypeEndCollection linkTypeCollection = workItemStore.WorkItemLinkTypes.LinkTypeEnds;
                if (!linkTypeCollection.Contains(linkTypeEndName))
                {
                    throw new InvalidOperationException(string.Format("Work item link type \"{0}\" doesn't exist, cannot create links to connect to parent work item.", linkTypeEndName));
                }

                linkTypeEnd = workItemStore.WorkItemLinkTypes.LinkTypeEnds[linkTypeEndName];
            }
            else if (WorkItemRelationType is WorkItemLinkTypeEnd)
            {
                linkTypeEnd = WorkItemRelationType as WorkItemLinkTypeEnd;
            }

            return linkTypeEnd;
        }

        private IEnumerable<WorkItem> QueryFromWorkItems(WorkItemStore workItemStore)
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
