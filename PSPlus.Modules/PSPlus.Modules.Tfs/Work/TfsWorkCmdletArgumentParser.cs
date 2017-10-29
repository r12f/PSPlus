using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.TfsUtils;

namespace PSPlus.Modules.Tfs.Work
{
    public static class TfsWorkCmdletArgumentParser
    {
        public static TeamFoundationIdentity ParseUserIdentity(string argumentName, TfsTeamProjectCollection collection, object argumentUser)
        {
            TeamFoundationIdentity userIdentity = null;
            if (argumentUser != null)
            {
                if (argumentUser is PSObject)
                {
                    argumentUser = (argumentUser as PSObject).BaseObject;
                }

                if (argumentUser is string)
                {
                    string userEmail = argumentUser as string;
                    userIdentity = collection.GetUserIdentityByEmail(userEmail);

                    if (userIdentity == null)
                    {
                        throw new PSArgumentException(string.Format("Field \"{0}\": No user is found through email: {1}.", argumentName, userEmail));
                    }
                }
                else if (argumentUser is TeamFoundationIdentity)
                {
                    userIdentity = argumentUser as TeamFoundationIdentity;
                }
                else
                {
                    throw new PSArgumentException("Field \"Assigned To\": the argument type is not supported. We only support user email and TeamFoundationIdentity.");
                }
            }

            return userIdentity;
        }

        public static IEnumerable<TeamFoundationIdentity> ParseUserIdentities(string argumentName, TfsTeamProjectCollection collection, List<object> argumentUsers)
        {
            if (argumentUsers == null)
            {
                yield break;
            }

            foreach (object argumentUser in argumentUsers)
            {
                if (argumentUser == null)
                {
                    throw new PSArgumentException(string.Format("Field \"{0}\": Argument cannot be null."));
                }

                yield return ParseUserIdentity(argumentName, collection, argumentUser);
            }
        }

        public static string ParseUserDisplayPartForWorkItemIdentityField(string argumentName, TfsTeamProjectCollection collection, Field workItemField, object argumentUser)
        {
            TeamFoundationIdentity userIdentity = ParseUserIdentity(argumentName, collection, argumentUser);
            if (userIdentity == null)
            {
                return null;
            }

            foreach (IdentityFieldValue allowedValue in workItemField.IdentityFieldAllowedValues)
            {
                if (allowedValue.Sid == userIdentity.Descriptor.Identifier)
                {
                    return allowedValue.DisplayPart;
                }
            }

            return null;
        }
    }
}
