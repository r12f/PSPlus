using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Tfs;
using PSPlus.Tfs.TfsExtensions;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Tfs.TeamProject
{
    [Cmdlet(VerbsCommon.Get, "TfsTeamProject")]
    [OutputType(typeof(Project))]
    public class GetTfsTeamProjectCmdlet : CmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true, HelpMessage = "Project name. Support wildcard pattern matching.")]
        public string Name { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Team Project Collection.")]
        public TfsTeamProjectCollection Collection { get; set; }

        protected override void ProcessRecordInEH()
        {
            if (Collection == null && CmdletContext.Collection == null)
            {
                throw new ArgumentException("Collection is not specified. Please use Connect-TfsTreamProjectCollection to connect to your collection, or use -Collection option to specify one.");
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }

            WorkItemStore workItemStore = null;
            if (Collection == null)
            {
                workItemStore = CmdletContext.WorkItemStore;
            }
            else
            {
                workItemStore = new WorkItemStore(Collection);
            }

            foreach (var project in workItemStore.GetProjects(Name))
            {
                WriteObject(project);
            }
        }
    }
}
