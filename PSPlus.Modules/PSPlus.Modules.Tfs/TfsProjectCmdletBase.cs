using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Tfs;
using System;
using System.Management.Automation;

namespace PSPlus.Modules.Tfs
{
    public class TfsProjectCmdletBase : CmdletBase
    {
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Team project.")]
        public Project Project { get; set; }

        protected Project EnsureProject()
        {
            Project project = Project;
            if (project == null)
            {
                project = CmdletContext.Project;
            }

            if (project == null)
            {
                throw new ArgumentException("Project is not specified. Please use Connect-TfsTreamProject to connect to your project, or use -Project option to specify one.");
            }

            return project;
        }
    }
}
