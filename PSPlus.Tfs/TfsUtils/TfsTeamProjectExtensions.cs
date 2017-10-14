using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PSPlus.Tfs.TfsUtils
{
    public static class TfsTeamProjectExtensions
    {
        public static IEnumerable<WorkItemType> GetWorkItemTypes(this Project project, string namePattern)
        {
            if (string.IsNullOrEmpty(namePattern))
            {
                throw new ArgumentException("namePattern cannot be null or empty.");
            }

            WildcardPattern workItemTypeNamePattern = new WildcardPattern(namePattern, WildcardOptions.Compiled | WildcardOptions.CultureInvariant | WildcardOptions.IgnoreCase);
            foreach (WorkItemType workItemType in project.WorkItemTypes)
            {
                if (workItemTypeNamePattern.IsMatch(workItemType.Name))
                {
                    yield return workItemType;
                }
            }
        }
    }
}
