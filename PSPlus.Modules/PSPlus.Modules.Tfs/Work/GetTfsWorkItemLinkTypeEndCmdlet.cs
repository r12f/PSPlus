using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.TfsUtils;
using PSPlus.Tfs.WIQLUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PSPlus.Modules.Tfs.Work
{
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemLinkTypeEnd")]
    [OutputType(typeof(WorkItemLinkTypeEnd))]
    public class GetTfsWorkItemLinkTypeEndCmdlet : TfsCmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, HelpMessage = "Work item link type name.")]
        [Alias("t")]
        public string Type { get; set; }

        protected override void ProcessRecordInEH()
        {
            if (string.IsNullOrWhiteSpace(Type))
            {
                Type = "*";
            }

            WorkItemStore workItemStore = EnsureWorkItemStore();
            foreach (WorkItemLinkTypeEnd workItemLinkTypeEnd in workItemStore.MatchWorkItemLinkTypeEnds(Type))
            {
                WriteObject(workItemLinkTypeEnd);
            }
        }
    }
}
