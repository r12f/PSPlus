using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.WIQLUtils;
using System.Collections.Generic;
using System.Management.Automation;

namespace PSPlus.Modules.Tfs.Work
{
    [Cmdlet(VerbsCommon.Get, "TfsWorkItem")]
    [OutputType(typeof(WorkItem))]
    public class GetTfsWorkItemCmdlet : TfsCmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, HelpMessage = "Work item Id.")]
        public List<int> Id { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Work item type.")]
        [Alias("t")]
        public List<string> Type { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Work item state.")]
        [Alias("s")]
        public List<string> State { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Assigned to.")]
        [Alias("at")]
        public List<string> AssginedTo { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Work item title.")]
        public string Title { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Area path.")]
        [Alias("ap", "Area")]
        public string AreaPath { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Under area path.")]
        [Alias("uap")]
        public string UnderAreaPath { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Iteration path.")]
        [Alias("ip", "Iteration")]
        public string IterationPath { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Under iteration path.")]
        [Alias("uip")]
        public string UnderIterationPath { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Conditions in where clause in WIQL query.")]
        [Alias("f")]
        public string Filter { get; set; }

        protected override void ProcessRecordInEH()
        {
            WorkItemStore workItemStore = EnsureWorkItemStore();
            foreach (var workItem in QueryWorkItems(workItemStore))
            {
                WriteObject(workItem);
            }
        }

        private IEnumerable<WorkItem> QueryWorkItems(WorkItemStore workItemStore)
        {
            WIQLQueryBuilder queryBuilder = new WIQLQueryBuilder();
            queryBuilder.Ids = Id;
            queryBuilder.WorkItemTypes = Type;
            queryBuilder.States = State;
            queryBuilder.AssignedTo = AssginedTo;
            queryBuilder.Title = Title;
            queryBuilder.AreaPath = AreaPath;
            queryBuilder.UnderAreaPath = UnderAreaPath;
            queryBuilder.IterationPath = IterationPath;
            queryBuilder.UnderIterationPath = UnderIterationPath;
            queryBuilder.ExtraFilters = Filter;

            string wiqlQuery = queryBuilder.Build();
            WriteVerbose(string.Format("Query workitems with WIQL query: {0}.", wiqlQuery));

            var workItemCollection = workItemStore.Query(wiqlQuery);
            foreach (WorkItem workItem in workItemCollection)
            {
                yield return workItem;
            }
        }
    }
}
