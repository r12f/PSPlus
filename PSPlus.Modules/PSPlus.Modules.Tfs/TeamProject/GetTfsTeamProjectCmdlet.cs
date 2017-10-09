using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.TfsExtensions;

namespace PSPlus.Modules.Tfs.TeamProject
{
    [Cmdlet(VerbsCommon.Get, "TfsTeamProject")]
    [OutputType(typeof(Project))]
    public class GetTfsTeamProjectCmdlet : TfsCmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true, HelpMessage = "Project name. Support wildcard pattern matching.")]
        [Alias("n")]
        public string Name { get; set; }

        protected override void ProcessRecordInEH()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }

            WorkItemStore workItemStore = EnsureWorkItemStore();
            foreach (var project in workItemStore.GetProjects(Name))
            {
                WriteObject(project);
            }
        }
    }
}
