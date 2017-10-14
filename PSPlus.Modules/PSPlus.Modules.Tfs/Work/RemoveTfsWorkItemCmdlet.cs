using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.WIQLUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PSPlus.Modules.Tfs.Work
{
    [Cmdlet(VerbsCommon.Remove, "TfsWorkItem", DefaultParameterSetName = null)]
    [OutputType(typeof(RemoveTfsWorkItemResult))]
    public class RemoveTfsWorkItemCmdlet : TfsCmdletBase
    {
        public class RemoveTfsWorkItemResult
        {
            public WorkItem WorkItem { get; set; }
            public WorkItemOperationError Error { get; set; }
        }

        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, ParameterSetName = "QueryByWIQL", HelpMessage = "Work item Id.")]
        public List<int> Id { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = "QueryByWIQL", HelpMessage = "Work item type.")]
        [Alias("t")]
        public List<string> Type { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = "QueryByWIQL", HelpMessage = "Work item state.")]
        [Alias("s")]
        public List<string> State { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = "QueryByWIQL", HelpMessage = "Assigned to.")]
        [Alias("at")]
        public List<string> AssginedTo { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = "QueryByWIQL", HelpMessage = "Work item title.")]
        public string Title { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = "QueryByWIQL", HelpMessage = "Area path.")]
        [Alias("ap", "Area")]
        public string AreaPath { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = "QueryByWIQL", HelpMessage = "Under area path.")]
        [Alias("uap")]
        public string UnderAreaPath { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = "QueryByWIQL", HelpMessage = "Iteration path.")]
        [Alias("ip", "Iteration")]
        public string IterationPath { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = "QueryByWIQL", HelpMessage = "Under iteration path.")]
        [Alias("uip")]
        public string UnderIterationPath { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = "QueryByWIQL", HelpMessage = "Conditions in where clause in WIQL query.")]
        [Alias("f")]
        public string Filter { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Work items.")]
        [Alias("w")]
        public List<WorkItem> WorkItem { get; set; }

        protected override void ProcessRecordInEH()
        {
            if (ParameterSetName != "QueryByWIQL" && (WorkItem == null || WorkItem.Count == 0))
            {
                throw new ArgumentException("No filter has been specified. Please specify at least 1 filter for removing the work items.");
            }

            WorkItemStore workItemStore = EnsureWorkItemStore();

            List<WorkItem> deletingWorkItems = QueryWorkItems(workItemStore).ToList();
            List<int> idOfDeletingWorkItems = deletingWorkItems.Select(x => x.Id).ToList();
            WriteVerbose(string.Format("Removing workitems with id: {0}.", string.Join(", ", idOfDeletingWorkItems)));

            ICollection<WorkItemOperationError> errors = workItemStore.DestroyWorkItems(idOfDeletingWorkItems);

            var errorMap = errors.ToDictionary(x => x.Id, x => x);
            List<RemoveTfsWorkItemResult> results = deletingWorkItems.Select((x) =>
            {
                WorkItemOperationError error = null;
                errorMap.TryGetValue(x.Id, out error);

                return new RemoveTfsWorkItemResult() { WorkItem = x, Error = error };
            }).ToList();

            foreach (var result in results)
            {
                WriteObject(result);
            }
        }

        private IEnumerable<WorkItem> QueryWorkItems(WorkItemStore workItemStore)
        {
            if (ParameterSetName == "QueryByWIQL")
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

            if (WorkItem != null)
            {
                WriteVerbose(string.Format("Get workitems from command line argument: Count = {0}.", WorkItem.Count));

                foreach (WorkItem workItem in WorkItem)
                {
                    yield return workItem;
                }
            }
        }
    }
}
