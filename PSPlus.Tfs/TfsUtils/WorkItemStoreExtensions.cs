using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace PSPlus.Tfs.TfsUtils
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
