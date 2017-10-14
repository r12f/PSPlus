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
    [Cmdlet(VerbsCommon.New, "TfsWorkItemLink")]
    [OutputType(typeof(WorkItem))]
    public class NewTfsWorkItemLinkCmdlet : TfsWorkItemLinkCmdletBase
    {
        protected override void ProcessRecordInEH()
        {
            WorkItemStore workItemStore = EnsureWorkItemStore();

            WorkItem[] workItems = QueryFromWorkItems(workItemStore).ToArray();
            if (workItems.Length == 0)
            {
                return;
            }

            AddWorkItemRelationLinkToWorkItemsIfNeeded(workItemStore, workItems);
            AddHyperlinkToWorkItemsIfNeeded(workItems);
            workItemStore.BatchSave(workItems, SaveFlags.MergeLinks);

            foreach (var workItem in workItems)
            {
                WriteObject(workItem);
            }
        }

        private void AddWorkItemRelationLinkToWorkItemsIfNeeded(WorkItemStore workItemStore, WorkItem[] workItems)
        {
            WorkItemLinkTypeEnd linkTypeEnd = CalculateWorkItemLinkTypeEnd(workItemStore);
            WorkItem relatedWorkItem = workItemStore.GetWorkItem(RelatedWorkItemId);
            if (relatedWorkItem == null)
            {
                throw new ArgumentException(string.Format("Invalid to work item id: {0}.", RelatedWorkItemId));
            }

            foreach (var workItem in workItems)
            {
                workItem.Links.Add(new RelatedLink(linkTypeEnd, relatedWorkItem.Id));
            }
        }

        protected WorkItemLinkTypeEnd CalculateWorkItemLinkTypeEnd(WorkItemStore workItemStore)
        {
            WorkItemLinkTypeEnd linkTypeEnd = null;
            if (WorkItemRelationType == null)
            {
                return null;
            }

            if (WorkItemRelationType is string)
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
            else
            {
                throw new ArgumentException("Work item link type only must be a string or WorkItemLinkTypeEnd object.");
            }

            return linkTypeEnd;
        }

        private void AddHyperlinkToWorkItemsIfNeeded(WorkItem[] workItems)
        {
            if (string.IsNullOrWhiteSpace(Hyperlink))
            {
                return;
            }

            foreach (var workItem in workItems)
            {
                workItem.Links.Add(new Hyperlink(Hyperlink));
            }
        }
    }
}
