using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.TfsUtils;

namespace PSPlus.Modules.Tfs.Work
{
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemArea")]
    [OutputType(typeof(NodeInfo))]
    public class GetTfsWorkItemAreaCmdlet : TfsProjectCmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, HelpMessage = "Area path.")]
        [Alias("a", "Area")]
        public string AreaPath { get; set; }

        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Recursive.")]
        [Alias("r")]
        public SwitchParameter Recursive { get; set; }

        protected override void ProcessRecordInEH()
        {
            Project project = EnsureProject();
            TfsTeamProjectCollection tpc = project.GetTeamProjectCollection();
            ICommonStructureService css = tpc.GetCommonStructureService();
        }
    }
}
