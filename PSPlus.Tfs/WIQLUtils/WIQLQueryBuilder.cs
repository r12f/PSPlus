using System.Collections.Generic;
using System.Text;

namespace PSPlus.Tfs.WIQLUtils
{
    public class WIQLQueryBuilder
    {
        public WIQLQueryBuilder()
        {
        }

        public List<int> Ids { get; set; }
        public List<string> WorkItemTypes { get; set; }
        public List<string> States { get; set; }
        public List<string> AssignedTo { get; set; }
        public string Title { get; set; }
        public string AreaPath { get; set; }
        public string UnderAreaPath { get; set; }
        public string IterationPath { get; set; }
        public string UnderIterationPath { get; set; }
        public string ExtraFilters { get; set; }

        public string Build()
        {
            StringBuilder wiqlConditionsBuilder = new StringBuilder();
            if (Ids != null && Ids.Count > 0)
            {
                wiqlConditionsBuilder.AppendFormat("{0} In ({1})", WIQLSystemFieldNames.Id, string.Join(",", Ids));
            }

            if (WorkItemTypes != null && WorkItemTypes.Count > 0)
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                BuildInStringListCondition(wiqlConditionsBuilder, WIQLSystemFieldNames.WorkItemType, WorkItemTypes);
            }

            if (States != null && States.Count > 0)
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                BuildInStringListCondition(wiqlConditionsBuilder, WIQLSystemFieldNames.State, States);
            }

            if (AssignedTo != null && AssignedTo.Count > 0)
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                BuildInStringListCondition(wiqlConditionsBuilder, WIQLSystemFieldNames.State, AssignedTo);
            }

            if (!string.IsNullOrEmpty(Title))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.AppendFormat("{0} CONTAINS '{1}'", WIQLSystemFieldNames.Title, Title);
            }

            if (!string.IsNullOrWhiteSpace(AreaPath))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.AppendFormat("{0} = '{1}'", WIQLSystemFieldNames.AreaPath, AreaPath);
            }

            if (!string.IsNullOrWhiteSpace(UnderAreaPath))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.AppendFormat("{0} UNDER '{1}'", WIQLSystemFieldNames.AreaPath, UnderAreaPath);
            }

            if (!string.IsNullOrWhiteSpace(IterationPath))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.AppendFormat("{0} = '{1}'", WIQLSystemFieldNames.IterationPath, IterationPath);
            }

            if (!string.IsNullOrWhiteSpace(UnderIterationPath))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.AppendFormat("{0} UNDER '{1}'", WIQLSystemFieldNames.IterationPath, UnderIterationPath);
            }

            if (!string.IsNullOrWhiteSpace(ExtraFilters))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.Append(ExtraFilters);
            }

            StringBuilder wiqlQueryBuilder = new StringBuilder();
            wiqlQueryBuilder.Append("SELECT * FROM workitems");

            if (wiqlConditionsBuilder.Length > 0)
            {
                wiqlQueryBuilder.Append(" WHERE ");
                wiqlQueryBuilder.Append(wiqlConditionsBuilder.ToString());
            }

            return wiqlQueryBuilder.ToString();
        }

        private static void AppendAndConnectorIfNeeded(StringBuilder s)
        {
            if (s.Length > 0)
            {
                s.Append(" AND ");
            }
        }

        private static void BuildInStringListCondition(StringBuilder s, string fieldName, IEnumerable<string> fieldValues)
        {
            s.AppendFormat("{0} IN (", fieldName);

            foreach (var fieldValue in fieldValues)
            {
                s.AppendFormat("'{0}'", fieldValue);
            }

            s.Append(")");
        }
    }
}
