using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace PSPlus.Modules.Tfs.Work
{
    [Cmdlet(VerbsCommon.Remove, "TfsWorkItemLink")]
    [OutputType(typeof(WorkItem))]
    public class RemoveTfsWorkItemLinkCmdlet : TfsWorkItemLinkCmdletBase
    {
        private WildcardPattern _linkTypeEndNamePattern = null;
        private WildcardPattern _hyperlinkPattern = null;

        protected override void ProcessRecordInEH()
        {
            PrepareForProcessing();

            WorkItemStore workItemStore = EnsureWorkItemStore();

            WorkItem[] workItems = QueryFromWorkItems(workItemStore).ToArray();
            if (workItems.Length == 0)
            {
                return;
            }

            foreach (var workItem in workItems)
            {
                // If any link needs to be removed, add it into pending removing list and remove it later.
                // We do this to avoid potential errors in removing during enumeration.
                List<Link> pendingRemovingLinks = new List<Link>();

                foreach (Link link in workItem.Links)
                {
                    bool needsToBeRemoved = false;

                    switch (link.BaseType)
                    {
                        case BaseLinkType.RelatedLink:
                            needsToBeRemoved = NeedsToBeRemovedDueToWorkItemRelationMatched(link as RelatedLink);
                            break;
                        case BaseLinkType.Hyperlink:
                            needsToBeRemoved = NeedsToBeRemovedDueToHyperlinkMatched(link as Hyperlink);
                            break;
                    }

                    if (needsToBeRemoved)
                    {
                        pendingRemovingLinks.Add(link);
                    }
                }

                foreach (var pendingRemovingLink in pendingRemovingLinks)
                {
                    workItem.Links.Remove(pendingRemovingLink);
                }
            }

            workItemStore.BatchSave(workItems, SaveFlags.MergeLinks);

            foreach (var workItem in workItems)
            {
                WriteObject(workItem);
            }
        }

        private void PrepareForProcessing()
        {
            string linkTypeEndName = CalculateWorkItemLinkTypeEndName();
            if (linkTypeEndName == null && RelatedWorkItemId <= 0 && string.IsNullOrWhiteSpace(Hyperlink))
            {
                throw new ArgumentException("Please specify at least one condition of link type, related work item id and hyperlink to determine which link to remove.");
            }

            if (linkTypeEndName != null)
            {
                _linkTypeEndNamePattern = new WildcardPattern(linkTypeEndName, WildcardOptions.Compiled | WildcardOptions.CultureInvariant | WildcardOptions.IgnoreCase);
            }

            if (Hyperlink != null)
            {
                _hyperlinkPattern = new WildcardPattern(Hyperlink, WildcardOptions.Compiled | WildcardOptions.CultureInvariant | WildcardOptions.IgnoreCase);
            }
        }

        private string CalculateWorkItemLinkTypeEndName()
        {
            if (WorkItemRelationType == null)
            {
                return null;
            }

            if (WorkItemRelationType is string)
            {
                return WorkItemRelationType as string;
            }
            else if (WorkItemRelationType is WorkItemLinkTypeEnd)
            {
                WorkItemLinkTypeEnd linkTypeEnd = WorkItemRelationType as WorkItemLinkTypeEnd;
                return linkTypeEnd.Name;
            }

            throw new ArgumentException("WorkItemRelationType must be either a string or a WorkItemLinkTypeEnd object.");
        }

        private bool NeedsToBeRemovedDueToWorkItemRelationMatched(RelatedLink relatedLink)
        {
            if (_linkTypeEndNamePattern == null && RelatedWorkItemId <= 0)
            {
                return false;
            }

            if (_linkTypeEndNamePattern != null)
            {
                if (!_linkTypeEndNamePattern.IsMatch(relatedLink.LinkTypeEnd.Name))
                {
                    return false;
                }
            }

            if (RelatedWorkItemId > 0)
            {
                if (relatedLink.RelatedWorkItemId != RelatedWorkItemId)
                {
                    return false;
                }
            }

            return true;
        }

        private bool NeedsToBeRemovedDueToHyperlinkMatched(Hyperlink hyperlink)
        {
            if (_hyperlinkPattern == null)
            {
                return false;
            }

            return _hyperlinkPattern.IsMatch(hyperlink.Location);
        }
    }
}
