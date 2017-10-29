using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using PSPlus.Tfs.TfsUtils;

namespace PSPlus.Modules.Tfs.Work
{
    [Cmdlet(VerbsCommon.Get, "TfsWorkItemType")]
    [OutputType(typeof(WorkItemType))]
    public class GetTfsWorkItemTypeCmdlet : TfsProjectCmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false, HelpMessage = "Work item type.")]
        [Alias("t")]
        public object Type { get; set; }

        protected override void ProcessRecordInEH()
        {
            if (Type == null)
            {
                Type = "*";
            }

            Project project = EnsureProject();

            if (Type is WorkItemType)
            {
                WorkItemType rawType = Type as WorkItemType;
                if (rawType.Project.Guid == project.Guid)
                {
                    WriteObject(Type as WorkItemType);
                }
                else
                {
                    throw new PSArgumentException(string.Format("Project {0} doesn't have type {1}.", project.Name, rawType.Name));
                }
            }
            else if (Type is string)
            {
                string workItemTypeName = Type as string;
                if (string.IsNullOrWhiteSpace(workItemTypeName))
                {
                    workItemTypeName = "*";
                }

                foreach (var workItemType in project.GetWorkItemTypes(workItemTypeName))
                {
                    WriteObject(workItemType);
                }
            }
            else
            {
                throw new PSArgumentException("The type of WorkItemType must be WorkItemType or string.");
            }
        }
    }
}
