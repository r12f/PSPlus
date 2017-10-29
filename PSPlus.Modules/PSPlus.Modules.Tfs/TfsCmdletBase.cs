using System.Management.Automation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Tfs;
using PSPlus.Tfs.TfsUtils;

namespace PSPlus.Modules.Tfs
{
    public class TfsCmdletBase : CmdletBase
    {
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Team project collection.")]
        [Alias("c", "TPC")]
        public TfsTeamProjectCollection Collection { get; set; }

        protected TfsTeamProjectCollection EnsureCollection()
        {
            TfsTeamProjectCollection collection = Collection;
            if (collection == null)
            {
                collection = CmdletContext.Collection;
            }

            if (collection == null)
            {
                throw new PSArgumentException("Collection is not specified. Please use Connect-TfsTreamProjectCollection to connect to your collection, or use -Collection option to specify one.");
            }

            return collection;
        }

        protected WorkItemStore EnsureWorkItemStore()
        {
            WorkItemStore workItemStore = null;

            TfsTeamProjectCollection collection = Collection;
            if (Collection == null)
            {
                workItemStore = CmdletContext.WorkItemStore;
            }
            else
            {
                workItemStore = Collection.GetWorkItemStore();
            }

            if (workItemStore == null)
            {
                throw new PSArgumentException("Collection is not specified. Please use Connect-TfsTreamProjectCollection to connect to your collection, or use -Collection option to specify one.");
            }

            return workItemStore;
        }
    }
}
