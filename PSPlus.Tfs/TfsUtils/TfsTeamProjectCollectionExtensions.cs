using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

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

        public static IEnumerable<Project> MatchProjects(this TfsTeamProjectCollection collection, string namePattern)
        {
            var workItemStore = new WorkItemStore(collection);
            if (workItemStore == null)
            {
                throw new InvalidOperationException("Access denied when getting work item store.");
            }

            return workItemStore.MatchProjects(namePattern);
        }

        public static IIdentityManagementService GetIdentityManagementService(this TfsTeamProjectCollection collection)
        {
            return (IIdentityManagementService)collection.GetService(typeof(IIdentityManagementService));
        }

        public static TeamFoundationIdentity GetUserIdentityByEmail(this TfsTeamProjectCollection collection, string mailAddress)
        {
            IIdentityManagementService ims = collection.GetIdentityManagementService();
            return ims.ReadIdentity(IdentitySearchFactor.MailAddress, mailAddress, MembershipQuery.Direct, ReadIdentityOptions.ExtendedProperties);
        }
    }
}
