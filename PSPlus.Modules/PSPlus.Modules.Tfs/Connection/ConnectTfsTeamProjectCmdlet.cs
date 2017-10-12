using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs;
using PSPlus.Tfs.TfsUtils;

namespace PSPlus.Modules.Tfs.Connection
{
    [Cmdlet(VerbsCommunications.Connect, "TfsTeamProject")]
    [OutputType(typeof(Project))]
    public class ConnectTfsTeamProjectCmdlet : TfsCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Team project name.")]
        [Alias("p", "Project")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Return the project object.")]
        public SwitchParameter PassThru { get; set; }

        protected override void ProcessRecordInEH()
        {
            TfsTeamProjectCollection collection = EnsureCollection();
            WorkItemStore workItemStore = EnsureWorkItemStore();

            List<Project> projects = workItemStore.GetProjects(Name).ToList();
            if (projects.Count == 0)
            {
                throw new InvalidOperationException(string.Format("Cannot find project named {0}.", Name));
            }
            else if (projects.Count > 1)
            {
                throw new InvalidOperationException(string.Format("More than 1 projects found with name {0}.", Name));
            }

            CmdletContext.Collection = collection;
            CmdletContext.WorkItemStore = workItemStore;
            CmdletContext.Project = projects[0];

            if (PassThru.IsPresent)
            {
                WriteObject(CmdletContext.Project);
            }
        }
    }
}
