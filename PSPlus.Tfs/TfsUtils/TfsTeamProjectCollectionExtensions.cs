using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PSPlus.Tfs.TfsUtils
{
    public static class TfsTeamProjectCollectionExtensions
    {
        public static WorkItemStore GetWorkItemStore(this TfsTeamProjectCollection collection)
        {
            return new WorkItemStore(collection);
        }

        public static ProjectCollection GetProjectCollection(this TfsTeamProjectCollection collection)
        {
            return collection.GetWorkItemStore().Projects;
        }

        public static IEnumerable<Project> GetProjects(this TfsTeamProjectCollection collection, string namePattern)
        {
            WildcardPattern parsedNamePattern = new WildcardPattern(namePattern, WildcardOptions.Compiled | WildcardOptions.CultureInvariant | WildcardOptions.IgnoreCase);

            var workItemStore = new WorkItemStore(collection);
            if (workItemStore == null)
            {
                throw new InvalidOperationException("Access denied when getting work item store.");
            }

            return workItemStore.MatchProjects(namePattern);
        }
    }
}
