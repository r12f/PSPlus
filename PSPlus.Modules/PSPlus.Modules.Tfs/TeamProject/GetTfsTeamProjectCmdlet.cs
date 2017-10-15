using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.TfsUtils;

namespace PSPlus.Modules.Tfs.TeamProject
{
    [Cmdlet(VerbsCommon.Get, "TfsTeamProject")]
    [OutputType(typeof(Project))]
    public class GetTfsTeamProjectCmdlet : TfsCmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, HelpMessage = "Project name. Support wildcard pattern matching.")]
        [Alias("n")]
        public string Name { get; set; }

        protected override void ProcessRecordInEH()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = "*";
            }

            WorkItemStore workItemStore = EnsureWorkItemStore();
            foreach (var project in workItemStore.MatchProjects(Name))
            {
                WriteObject(project);
            }
        }
    }
}
