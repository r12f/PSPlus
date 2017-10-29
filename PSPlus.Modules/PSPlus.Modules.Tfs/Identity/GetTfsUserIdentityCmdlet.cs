using System.Management.Automation;
using Microsoft.TeamFoundation.Framework.Client;
using PSPlus.Tfs.TfsUtils;

namespace PSPlus.Modules.Tfs.Account
{
    [Cmdlet(VerbsCommon.Get, "TfsUserIdentity")]
    [OutputType(typeof(TeamFoundationIdentity))]
    public class GetTfsUserIdentityCmdlet : TfsCmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true, HelpMessage = "Email address of the user.")]
        [Alias("e")]
        [ValidateNotNullOrEmpty()]
        public string Email { get; set; }

        protected override void ProcessRecordInEH()
        {
            var collection = EnsureCollection();

            var userId = collection.GetUserIdentityByEmail(Email);
            if (userId == null)
            {
                return;
            }

            WriteObject(userId);
        }
    }
}
