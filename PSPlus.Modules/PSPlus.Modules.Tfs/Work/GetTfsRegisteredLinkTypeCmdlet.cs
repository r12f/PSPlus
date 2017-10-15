using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.TfsUtils;

namespace PSPlus.Modules.Tfs.Work
{
    [Cmdlet(VerbsCommon.Get, "TfsRegisteredLinkType")]
    [OutputType(typeof(RegisteredLinkType))]
    public class GetTfsRegisteredLinkTypeCmdlet : TfsCmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, HelpMessage = "Registered link type name.")]
        [Alias("t")]
        public string Type { get; set; }

        protected override void ProcessRecordInEH()
        {
            if (string.IsNullOrWhiteSpace(Type))
            {
                Type = "*";
            }

            WorkItemStore workItemStore = EnsureWorkItemStore();
            foreach (RegisteredLinkType registeredLinkTypeEnd in workItemStore.MatchRegisteredLinkType(Type))
            {
                WriteObject(registeredLinkTypeEnd);
            }
        }
    }
}
