using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PSPlus.Tfs.TfsExtensions
{
    public static class TfsTeamProjectCollectionExtensions
    {
        public static WorkItemStore GetWorkItemStore(this TfsTeamProjectCollection collection)
        {
            return collection.GetService<WorkItemStore>();
        }

        public static ProjectCollection GetProjectCollection(this TfsTeamProjectCollection collection)
        {
            return collection.GetService<ProjectCollection>();
        }

        public static IEnumerable<Project> GetProjects(this TfsTeamProjectCollection collection, string namePattern)
        {
            WildcardPattern parsedNamePattern = new WildcardPattern(namePattern, WildcardOptions.Compiled | WildcardOptions.CultureInvariant | WildcardOptions.IgnoreCase);

            var workItemStore = new WorkItemStore(collection);
            if (workItemStore == null)
            {
                throw new InvalidOperationException("Access denied when getting work item store.");
            }

            return workItemStore.GetProjects(namePattern);
        }
    }
}
