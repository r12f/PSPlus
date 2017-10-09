using System;
using System.Collections;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace PSPlus.Modules.Tfs.Work.WorkItem
{
    [Cmdlet(VerbsCommon.New, "TfsWorkItem")]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem))]
    public class NewTfsWorkItemCmdlet : TfsCmdletBase
    {
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Team project.")]
        public Project Project { get; set; }

        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = true, HelpMessage = "Work item type.")]
        [Alias("t", "Type")]
        public string WorkItemType { get; set; }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = true, HelpMessage = "Work item title.")]
        public string Title { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Assigned to.")]
        public string AssignedTo { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Area path.")]
        public string AreaPath { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Iteration path.")]
        public string IterationPath { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false, HelpMessage = "Properties.")]
        public Hashtable Properties { get; set; }

        protected override void ProcessRecordInEH()
        {
            WorkItemType workItemType = EnsureWorkItemType();

            var workItem = workItemType.NewWorkItem();
            workItem.Title = Title;

            if (!string.IsNullOrEmpty(AssignedTo))
            {
                workItem.Fields[CoreField.AssignedTo].Value = AssignedTo;
            }

            if (!string.IsNullOrEmpty(AreaPath))
            {
                workItem.AreaPath = AreaPath;
            }

            if (!string.IsNullOrEmpty(IterationPath))
            {
                workItem.IterationPath = IterationPath;
            }

            if (Properties != null)
            {
                foreach (DictionaryEntry property in Properties)
                {
                    string propertyKey = property.Key as string;
                    workItem.Fields[propertyKey].Value = property.Value;
                }
            }

            workItem.Save();

            WriteObject(workItem);
        }

        private WorkItemType EnsureWorkItemType()
        {
            foreach (WorkItemType workItemType in Project.WorkItemTypes)
            {
                if (string.Compare(workItemType.Name, WorkItemType, true) == 0)
                {
                    return workItemType;
                }
            }

            throw new ArgumentException(string.Format("Invalid work item type: {0}.", WorkItemType));
        }
    }
}
