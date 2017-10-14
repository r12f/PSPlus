using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace PSPlus.Tfs.TfsUtils
{
    public static class WorkItemStoreExtensions
    {
        public static IEnumerable<Project> MatchProjects(this WorkItemStore workItemStore, string namePattern)
        {
            WildcardPattern parsedNamePattern = new WildcardPattern(namePattern, WildcardOptions.Compiled | WildcardOptions.CultureInvariant | WildcardOptions.IgnoreCase);

            ProjectCollection projectCollection = workItemStore.Projects;
            foreach (Project project in projectCollection)
            {
                if (parsedNamePattern.IsMatch(project.Name))
                {
                    yield return project;
                }
            }
        }

        public static IEnumerable<RegisteredLinkType> MatchRegisteredLinkType(this WorkItemStore workItemStore, string linkTypeNamePattern)
        {
            WildcardPattern parsedNamePattern = new WildcardPattern(linkTypeNamePattern, WildcardOptions.Compiled | WildcardOptions.CultureInvariant | WildcardOptions.IgnoreCase);

            RegisteredLinkTypeCollection linkTypeCollection = workItemStore.RegisteredLinkTypes;
            foreach (RegisteredLinkType linkType in linkTypeCollection)
            {
                if (parsedNamePattern.IsMatch(linkType.Name))
                {
                    yield return linkType;
                }
            }
        }

        public static IEnumerable<WorkItemLinkTypeEnd> MatchWorkItemLinkTypeEnds(this WorkItemStore workItemStore, string linkTypeNamePattern)
        {
            WildcardPattern parsedNamePattern = new WildcardPattern(linkTypeNamePattern, WildcardOptions.Compiled | WildcardOptions.CultureInvariant | WildcardOptions.IgnoreCase);

            WorkItemLinkTypeEndCollection linkTypeCollection = workItemStore.WorkItemLinkTypes.LinkTypeEnds;
            foreach (WorkItemLinkTypeEnd workItemLinkTypeEnd in linkTypeCollection)
            {
                if (parsedNamePattern.IsMatch(workItemLinkTypeEnd.Name))
                {
                    yield return workItemLinkTypeEnd;
                }
            }
        }
    }
}
