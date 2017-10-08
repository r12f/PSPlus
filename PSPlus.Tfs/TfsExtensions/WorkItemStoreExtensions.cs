using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Collections.Generic;
using System.Management.Automation;

namespace PSPlus.Tfs.TfsExtensions
{
    public static class WorkItemStoreExtensions
    {
        public static IEnumerable<Project> GetProjects(this WorkItemStore workItemStore, string namePattern)
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
    }
}
