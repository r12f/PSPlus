using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs;
using PSPlus.Tfs.TfsUtils;

namespace PSPlus.Modules.Tfs.Connection
{
    [Cmdlet(VerbsCommunications.Connect, "TfsTeamProject")]
    [OutputType(typeof(Project))]
    public class ConnectTfsTeamProjectCmdlet : TfsCmdletBase
    {
        [Parameter(Mandatory = false, HelpMessage = "Team project name.")]
        [Alias("p", "Project")]
        public string ProjectName { get; set; }

        protected override void ProcessRecordInEH()
        {
            WorkItemStore workItemStore = EnsureWorkItemStore();

            List<Project> projects = workItemStore.GetProjects(ProjectName).ToList();
            if (projects.Count == 0)
            {
                throw new InvalidOperationException(string.Format("Cannot find project named {0}.", ProjectName));
            }
            else if (projects.Count > 1)
            {
                throw new InvalidOperationException(string.Format("More than 1 projects found with name {0}.", ProjectName));
            }

            CmdletContext.Project = projects[0];

            WriteObject(CmdletContext.Project);
        }
    }
}
